using Newtonsoft.Json;
using System.IO;
using System.Runtime.ExceptionServices;

namespace Test1;

[Command(PackageIds.SaveCurrentSession)]
public sealed class SaveCurrentSession : BaseCommand<SaveCurrentSession>
{
    protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
    {
        try
        {
            HarpoonPackage.sessions.KeyValuePairs[HarpoonPackage.activeSessionName] = new HarpoonSession() { fileNamesArr = HarpoonPackage.fileNamesArr, fileNameIndexMap = HarpoonPackage.fileNameIndexMap };

            if (!File.Exists(HarpoonPackage.sessionPath))
            {
                File.WriteAllText(HarpoonPackage.sessionPath, JsonConvert.SerializeObject(HarpoonPackage.sessions));
            }

            string sessionData = File.ReadAllText(HarpoonPackage.sessionPath);
            var harpoonSessions = JsonConvert.DeserializeObject<HarpoonSessions>(sessionData);

            if (harpoonSessions != null && harpoonSessions.KeyValuePairs != null)
            {
                harpoonSessions.KeyValuePairs[HarpoonPackage.activeSessionName] =
                    new HarpoonSession() { fileNameIndexMap = HarpoonPackage.fileNameIndexMap, fileNamesArr = HarpoonPackage.fileNamesArr };
                File.WriteAllText(HarpoonPackage.sessionPath, JsonConvert.SerializeObject(
                    harpoonSessions
                ));
                Helper.SetActivityLog($"Saved session {HarpoonPackage.activeSessionName}");
            }
            else
            {
                Helper.SetActivityLog($"no sessions found in file {HarpoonPackage.sessionPath}");
            }

        }
        catch (Exception ex)
        {
            Helper.SetActivityLog(ex.Message);
        }
    }
}

[Command(PackageIds.SaveAllSessions)]
public sealed class SaveAllSessions : BaseCommand<SaveAllSessions>
{
    protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
    {
        try
        {
            HarpoonPackage.sessions.KeyValuePairs[HarpoonPackage.activeSessionName] = new HarpoonSession() { fileNamesArr = HarpoonPackage.fileNamesArr, fileNameIndexMap = HarpoonPackage.fileNameIndexMap };

            File.WriteAllText(HarpoonPackage.sessionPath, JsonConvert.SerializeObject(HarpoonPackage.sessions));
            Helper.SetActivityLog($"Saved all sessions at {Path.GetFullPath( HarpoonPackage.sessionPath)}");

        }
        catch (Exception ex)
        {
            Helper.SetActivityLog(ex.Message);
        }
    }
}

