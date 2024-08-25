global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using System;
global using Task = System.Threading.Tasks.Task;
using Microsoft.VisualStudio.Shell.Interop;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public static string sessionPath = $"{Helper.TryGetSolutionDirectoryInfo().FullName}\\.harpoon_sessions";
        public static string activeSessionName = "session1";
        public static HarpoonSessions sessions = new();
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            LoadSessions();
            await this.RegisterCommandsAsync();
            this.RegisterToolWindows();
        }

        private void LoadSessions()
        {
            try
            {
                if (File.Exists(sessionPath))
                {
                    string sessionData = File.ReadAllText(sessionPath);
                    var harpoonSessions = JsonConvert.DeserializeObject<HarpoonSessions>(sessionData);

                    if (harpoonSessions != null && harpoonSessions.KeyValuePairs != null && harpoonSessions.KeyValuePairs.Count > 0)
                    {
                        HarpoonPackage.sessions = harpoonSessions;
                        HarpoonPackage.activeSessionName = harpoonSessions.KeyValuePairs.First().Key;
                        HarpoonPackage.fileNamesArr = harpoonSessions.KeyValuePairs[HarpoonPackage.activeSessionName].fileNamesArr;
                        HarpoonPackage.fileNameIndexMap = harpoonSessions.KeyValuePairs[HarpoonPackage.activeSessionName].fileNameIndexMap;

                        //Helper.SetActivityLog($"Active session is set to {HarpoonPackage.activeSessionName}");
                    }
                    else
                    {
                        //Helper.SetActivityLog($"No Harpoon sessions file found");
                    }
                }
                else
                {
                    //Helper.SetActivityLog($"No Harpoon sessions file found");
                }
            }
            catch (Exception ex)
            {
                //Helper.SetActivityLog(ex.Message);

            }
        }
    }
}