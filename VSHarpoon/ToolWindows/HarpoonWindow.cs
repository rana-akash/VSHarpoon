using Microsoft.VisualStudio.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Test1
{
    public class HarpoonWindow : BaseToolWindow<HarpoonWindow>
    {
        public override string GetTitle(int toolWindowId) => "HarpoonWindow";

        public override Type PaneType => typeof(Pane);

        public override Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        {
            return Task.FromResult<FrameworkElement>(new HarpoonWindowControl());
        }

        [Guid("d8c4a981-d973-477c-9df7-0f293bf9b807")]
        internal class Pane : ToolkitToolWindowPane
        {
            public Pane()
            {
                BitmapImageMoniker = KnownMonikers.ToolWindow;
            }
        }
    }
}  
