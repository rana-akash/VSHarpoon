global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using System;
global using Task = System.Threading.Tasks.Task;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace Harpoon
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.HarpoonString)]
    public sealed class HarpoonPackage : ToolkitPackage
    {
        public static Dictionary<int, string> indexTabNamesMap = new();
        public static Dictionary<string, int> tabNamesIndexMap = new();
        public static int appendIndex = 1;
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.RegisterCommandsAsync();
        }
    }
}