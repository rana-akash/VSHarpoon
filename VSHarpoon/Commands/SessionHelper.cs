﻿using Newtonsoft.Json;
using System.IO;

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
}