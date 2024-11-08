using Microsoft.VisualStudio.Shell.Interop;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using IServiceProvider = System.IServiceProvider;

namespace Test1;

internal sealed class TabGroupJump
{
    public const int CommandIdJumpLeft = 0x0200;
    public const int CommandIdJumpRight = 0x0201;
    public const int CommandIdJumpUp = 0x0202;
    public const int CommandIdJumpDown = 0x0203;
    public const int CommandIdJumpPrevious = 0x0204;
    public const int CommandIdJumpNext = 0x0205;

    /// <summary>
    /// Command menu group (command set GUID).
    /// </summary>
    public static readonly Guid CommandSet = new Guid("3324b810-3638-4d86-9a10-635e784a75e7");

    /// <summary>
    /// VS Package that provides this command, not null.
    /// </summary>
    private readonly Package package;

    private readonly TabGroupMoverUpDown _tabGroupMoverUpDown;
    private readonly TabGroupMoverLeftRight _tabGroupMoverLeftRight;
    private readonly TabGroupMoverNextPrevious _tabGroupMoverNextPrevious;

    /// <summary>
    /// Initializes a new instance of the <see cref="TabGroupJump"/> class.
    /// Adds our command handlers for menu (commands must exist in the command table file)
    /// </summary>
    /// <param name="package">Owner package, not null.</param>
    private TabGroupJump(Package package)
    {
        if (package == null)
        {
            throw new ArgumentNullException("package");
        }

        this.package = package;

        _tabGroupMoverLeftRight = new TabGroupMoverLeftRight(ServiceProvider);
        _tabGroupMoverUpDown = new TabGroupMoverUpDown(ServiceProvider);
        _tabGroupMoverNextPrevious = new TabGroupMoverNextPrevious(ServiceProvider);

        if (ServiceProvider.GetService(typeof(IMenuCommandService)) is OleMenuCommandService commandService)
        {
            // Add the jump commands to the handler
            commandService.AddCommand(
              new MenuCommand(_tabGroupMoverLeftRight.MenuItemCallback,
                              new CommandID(CommandSet, CommandIdJumpLeft)));
            commandService.AddCommand(
              new MenuCommand(_tabGroupMoverLeftRight.MenuItemCallback,
                              new CommandID(CommandSet, CommandIdJumpRight)));

            commandService.AddCommand(
              new MenuCommand(_tabGroupMoverUpDown.MenuItemCallback,
                              new CommandID(CommandSet, CommandIdJumpUp)));
            commandService.AddCommand(
              new MenuCommand(_tabGroupMoverUpDown.MenuItemCallback,
                              new CommandID(CommandSet, CommandIdJumpDown)));

            commandService.AddCommand(
              new MenuCommand(_tabGroupMoverNextPrevious.MenuItemCallback,
                              new CommandID(CommandSet, CommandIdJumpPrevious)));
            commandService.AddCommand(
              new MenuCommand(_tabGroupMoverNextPrevious.MenuItemCallback,
                              new CommandID(CommandSet, CommandIdJumpNext)));
        }
    }

    /// <summary>
    /// Gets the instance of the package.
    /// </summary>
    public static TabGroupJump Instance { get; private set; }

    /// <summary>
    /// Gets the service provider from the owner package.
    /// </summary>
    private IServiceProvider ServiceProvider => package;

    /// <summary>
    /// Initializes the singleton instance of the command.
    /// </summary>
    /// <param name="package">Owner package, not null.</param>
    public static void Initialize(Package package)
    {
        Instance = new TabGroupJump(package);
    }

    /// <summary>
    ///  The implementation for moving left/right or up/down or next/previous is basically the same.
    ///  What's different in each case is which command determines if we're moving forward or backward
    ///  and how the tabs should be ordered when determining what is forward or backward. So those two
    ///  functions are abstract methods and the rest of the implementation is in
    ///  <see cref="MenuItemCallback"/>.
    /// </summary>
    private abstract class TabGroupMover
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IVsUIShell _uiShell;

        protected TabGroupMover(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _uiShell = (IVsUIShell)serviceProvider.GetService(typeof(SVsUIShell));
        }

        /// <summary>
        ///  How the document-windows should ordered.  They should be ordered such that when
        ///  <see cref="ShouldMoveForward"/> returns true the Nth + 1 element is the logical group to jump
        ///  to.  For example, if you sort the groups vertically (smallest first), then ShouldMoveFoward()
        ///  should return true when moving down.
        /// </summary>
        /// <seealso cref="ShouldMoveForward"/>
        protected abstract IEnumerable<ActivePane> FilterAndSort(IEnumerable<ActivePane> panes,
                                                                 ActivePane activePane);

        /// <summary>
        ///  True if the given command represents moving forward in the collection returned by
        ///  <see cref="FilterAndSort"/>.
        /// </summary>
        /// <seealso cref="FilterAndSort"/>
        protected abstract bool ShouldMoveForward(int commandId);

        /// <summary>
        ///  The callback that should be passed into the <see cref="MenuCommand"/> constructor.
        /// </summary>
        /// <param name="sender"> Source of the event. </param>
        /// <param name="e"> Event information. </param>
        public void MenuItemCallback(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            DTE2 dte = (DTE2)_serviceProvider.GetService(typeof(DTE));
            int commandId = ((MenuCommand)sender).CommandID.ID;

            var activePanes = GetActivePanes(dte).ToList();

            var currentlyActiveWindow = dte.ActiveWindow;

            var activePane =
              LookupPaneByWindow(currentlyActiveWindow, activePanes)
              ?? LookupPaneByHierarchy(currentlyActiveWindow, activePanes);

            if (activePane == null)
            {
                return;
            }

            var filteredPanes = FilterAndSort(activePanes, activePane).ToList();

            if (filteredPanes.Count == 0)
                return;

            var isMovingForward = ShouldMoveForward(commandId);
            var indexOfCurrentTabGroup = filteredPanes.IndexOf(activePane);

            // get the tab to activate
            var offset = isMovingForward ? 1 : -1;
            int nextIndex = Clamp(filteredPanes.Count, indexOfCurrentTabGroup + offset);

            // and activate it
            filteredPanes[nextIndex].Window.Activate();
        }

        /// <summary> Looks up a pane by comparing the window of the pane to a given value. </summary>
        private static ActivePane LookupPaneByWindow(Window windowToSearchFor, List<ActivePane> activePanes)
        {
            ActivePane activePane = null;
            foreach (var pane in activePanes)
            {
                if (pane.Window == windowToSearchFor)
                {
                    activePane = pane;
                    break;
                }
            }
            return activePane;
        }

        /// <summary>
        ///  Searches for a pane by seeing if any of the window's parents are a pane that we know about.
        /// </summary>
        private static ActivePane LookupPaneByHierarchy(Window childWindow, List<ActivePane> activePanes)
        {
            // welp, we're not directly in a document.  BUT, what if the window is inside of a
            // document?  In that case, try to go up until we find a document pane that we know about. 
            //        
            // This happens for Project Property panes
            var currentHwnd = childWindow.HWnd;

            // max out at 20 just in case we keep going up and never find anything. 
            for (int i = 0; i < 20 && currentHwnd != IntPtr.Zero; i++)
            {
                currentHwnd = GetParent(currentHwnd);
                var activePane = LookupPaneByHwnd(currentHwnd);
                if (activePane != null)
                {
                    return activePane;
                }
            }

            return null;

            ActivePane LookupPaneByHwnd(IntPtr searchHwnd)
            {
                foreach (var pane in activePanes)
                {
                    if (pane.Window.HWnd == searchHwnd)
                    {
                        return pane;
                    }
                }

                return null;
            }
        }

        /// <summary> Get all of the Windows that have an associated frame. </summary>
        private IEnumerable<ActivePane> GetActivePanes(DTE2 dte)
        {
            var framesAndAssociatedWindows = GetActiveWindowToFramesLookup(dte.ActiveWindow);

            foreach (var window in GetActiveWindows(dte))
            {
                if (framesAndAssociatedWindows.TryGetValue(window, out var frame))
                {
                    yield return new ActivePane(window, frame);
                }
            }
        }

        /// <summary> Gets all of the windows that are currently positioned with a valid Top or Left. </summary>
        private IEnumerable<Window> GetActiveWindows(DTE2 dte)
        {
            // documents that are not the focused document in a group will have Top == 0 && Left == 0
            return from window in dte.Windows.Cast<Window>()
                   where window.Kind == "Document"
                   select window;
        }

        

        /// <summary> Get all known <see cref="IVsWindowFrame"/>, lazily, that are active/on-screen. </summary>
        private Dictionary<Window, IVsWindowFrame> GetActiveWindowToFramesLookup(Window activeWindow)
        {
            var actives = from frame in GetFrames()
                          let window = VsShellUtilities.GetWindowObject(frame)
                          where window != null
                          //where window == activeWindow
                          select new { window, frame };

            //List<dynamic> actives = new List<dynamic>();

            //foreach(var frameWindow in activesFrameWindows)
            //{
            //    var condition1 = (frameWindow.frame as WindowFrame).IsOnScreenAsync;
            //    bool result = AsyncContext.Run<bool>(condition1);

            //    if (result || frameWindow.window == activeWindow)
            //    {
            //        actives.Add(frameWindow);
            //    }
            //}

            var windowToFrameLookup = new Dictionary<Window, IVsWindowFrame>();
            foreach (var active in actives)
            {
                windowToFrameLookup[active.window] = active.frame;
            }

            return windowToFrameLookup;
        }

        /// <summary> Get all known <see cref="IVsWindowFrame"/>, lazily. </summary>
        private IEnumerable<IVsWindowFrame> GetFrames()
        {
            var array = new IVsWindowFrame[1];
            _uiShell.GetDocumentWindowEnum(out var frames);

            while (true)
            {
                var errorCode = frames.Next(1, array, out _);
                if (errorCode != VSConstants.S_OK)
                    break;

                yield return array[0];
            }
        }

        /// <summary> Clamp the given value to be between 0 and <paramref name="count"/>. </summary>
        private static int Clamp(int count, int number)
          => (number < 0 ? number + count : number) % count;
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

    [DllImport("user32.dll", ExactSpelling = true)]
    public static extern IntPtr GetParent(IntPtr hWnd);

    /// <summary> Information about an active window. </summary>
    private class ActivePane
    {
        public ActivePane(Window window, IVsWindowFrame associatedFrame)
        {
            Window = window;
            AssociatedFrame = associatedFrame;
            Bounds = MeasureBounds();
        }

        public Window Window { get; }

        public IVsWindowFrame AssociatedFrame { get; }

        public RECT Bounds { get; }

        /// <summary> Measure the bounds of the given window </summary>
        private RECT MeasureBounds()
        {
            var window = Window;
            var textView = VsShellUtilities.GetTextView(AssociatedFrame);

            if (textView != null && GetWindowRect(textView.GetWindowHandle(), out var rect))
            {
                return rect;
            }

            if ((long)Window.HWnd != 0 && GetWindowRect(Window.HWnd, out var rect2))
            {
                return rect2;
            }

            // fallback where Top is wrong for windows that are vertically split. 
            return new RECT
            {
                left = window.Left,
                right = window.Left + window.Width,
                top = window.Top,
                bottom = window.Top + window.Height
            };
        }
    }

    /// <summary> Command implementation for moving up/down. </summary>
    private class TabGroupMoverUpDown : TabGroupMover
    {
        public TabGroupMoverUpDown(IServiceProvider serviceProvider)
          : base(serviceProvider)
        {
        }

        /// <inheritdoc />
        protected override IEnumerable<ActivePane> FilterAndSort(IEnumerable<ActivePane> panes,
                                                                 ActivePane activePane)
        {
            return from pane in panes
                       // we always need the active window in the list
                   where pane == activePane
                         // only return those that are aligned vertically
                         || pane.Bounds.left == activePane.Bounds.left
                   orderby pane.Bounds.top
                   select pane;
        }

        /// <inheritdoc />
        protected override bool ShouldMoveForward(int commandId)
          => commandId == CommandIdJumpDown;
    }

    /// <summary> Command implementation for moving left/right. </summary>
    private class TabGroupMoverLeftRight : TabGroupMover
    {
        public TabGroupMoverLeftRight(IServiceProvider serviceProvider)
          : base(serviceProvider)
        {
        }

        /// <inheritdoc />
        protected override IEnumerable<ActivePane> FilterAndSort(IEnumerable<ActivePane> panes,
                                                                 ActivePane activePane)
        {
            return from pane in panes
                       // we always need the active window in the list
                   where pane == activePane
                         // only return those that aren't aligned vertically
                         || pane.Bounds.left != activePane.Bounds.left
                   orderby pane.Bounds.left, pane.Bounds.top
                   select pane;
        }

        /// <inheritdoc />
        protected override bool ShouldMoveForward(int commandId)
          => commandId == CommandIdJumpRight;
    }

    /// <summary> Command implementation for moving next/previous. </summary>
    private class TabGroupMoverNextPrevious : TabGroupMover
    {
        public TabGroupMoverNextPrevious(IServiceProvider serviceProvider)
          : base(serviceProvider)
        {
        }

        /// <inheritdoc />
        protected override IEnumerable<ActivePane> FilterAndSort(IEnumerable<ActivePane> panes,
                                                                 ActivePane activePane)
        {
            return from pane in panes
                   orderby pane.Bounds.left, pane.Bounds.top
                   select pane;
        }

        /// <inheritdoc />
        protected override bool ShouldMoveForward(int commandId)
          => commandId == CommandIdJumpNext;
    }

    
}
