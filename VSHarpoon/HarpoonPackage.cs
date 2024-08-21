global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using System;
global using Task = System.Threading.Tasks.Task;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Test1
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.HarpoonString)]
    [ProvideToolWindow(typeof(HarpoonWindow.Pane))]
    public sealed class HarpoonPackage : ToolkitPackage
    {
        public static string[] fileNamesArr = new string[10];
        public static Dictionary<string, int> fileNameIndexMap = new();
        public static string sessionPath = $"{Helper.TryGetSolutionDirectoryInfo()}\\.harpoon_session";
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            LoadSession();
            await this.RegisterCommandsAsync();
            this.RegisterToolWindows();
        }

        private void LoadSession()
        {
            if (File.Exists(sessionPath))
            {
                var sessionData = File.ReadAllText(sessionPath);
                var obj = JsonConvert.DeserializeObject<HarpoonSession>(sessionData);

                if (obj != null)
                {
                    HarpoonPackage.fileNamesArr = obj.fileNamesArr;
                    HarpoonPackage.fileNameIndexMap =  obj.fileNameIndexMap;
                }
            }
        }
    }
}