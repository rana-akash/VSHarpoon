using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Test1;

public static class SessionHelper
{
    public static void SaveCurrentSession()
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

    public static void SaveAllSessions()
    {
        try
        {
            HarpoonPackage.sessions.KeyValuePairs[HarpoonPackage.activeSessionName] = new HarpoonSession() { fileNamesArr = HarpoonPackage.fileNamesArr, fileNameIndexMap = HarpoonPackage.fileNameIndexMap };

            File.WriteAllText(HarpoonPackage.sessionPath, JsonConvert.SerializeObject(HarpoonPackage.sessions));
            Helper.SetActivityLog($"Saved all sessions at {Path.GetFullPath(HarpoonPackage.sessionPath)}");

        }
        catch (Exception ex)
        {
            Helper.SetActivityLog(ex.Message);
        }
    }

    public static void ChangeToSession(string sessionName)
    {
        if (string.IsNullOrEmpty(sessionName))
        {
            throw new ArgumentNullException(nameof(sessionName));
        }

        if (sessionName == HarpoonPackage.activeSessionName)
        {
            return;
        }

        HarpoonPackage.activeSessionName = sessionName;
        HarpoonPackage.fileNamesArr = HarpoonPackage.sessions.KeyValuePairs[sessionName].fileNamesArr;
        HarpoonPackage.fileNameIndexMap = HarpoonPackage.sessions.KeyValuePairs[sessionName].fileNameIndexMap;

        Helper.ReloadLabels();
        Helper.SetDropDownValues();
    }

    public static void RemoveSession(string sessionName)
    {
        if (string.IsNullOrEmpty(sessionName))
        {
            return;
        }

        if(sessionName == "default")
        {
            return;
        }

        HarpoonPackage.sessions.KeyValuePairs.Remove(sessionName);
       
        HarpoonPackage.activeSessionName = HarpoonPackage.sessions.KeyValuePairs.First().Key;
        HarpoonPackage.fileNamesArr = HarpoonPackage.sessions.KeyValuePairs[HarpoonPackage.activeSessionName].fileNamesArr;
        HarpoonPackage.fileNameIndexMap = HarpoonPackage.sessions.KeyValuePairs[HarpoonPackage.activeSessionName].fileNameIndexMap;
        Helper.ReloadLabels();
        Helper.SetDropDownValues();
    }

    public static void AddSession(string newSessionName)
    {
        if (string.IsNullOrEmpty(newSessionName))
        {
            throw new ArgumentException(nameof(newSessionName));
        }

        if (HarpoonPackage.sessions.KeyValuePairs.ContainsKey(Helper.NewSessionName.Text))
        {
            return;
        }

        HarpoonPackage.sessions.KeyValuePairs.Add(newSessionName, new HarpoonSession() { fileNameIndexMap = new(), fileNamesArr = new string[10] });
        
        HarpoonPackage.activeSessionName = Helper.NewSessionName.Text;
        HarpoonPackage.fileNamesArr = HarpoonPackage.sessions.KeyValuePairs[HarpoonPackage.activeSessionName].fileNamesArr;
        HarpoonPackage.fileNameIndexMap = HarpoonPackage.sessions.KeyValuePairs[HarpoonPackage.activeSessionName].fileNameIndexMap;
        SessionHelper.SaveCurrentSession();
        Helper.ReloadLabels();
        Helper.SetDropDownValues();
    }
}