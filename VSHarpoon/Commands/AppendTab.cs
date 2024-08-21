﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Markup;

namespace Test1
{
    [Command(PackageIds.AppendTab)]
    public sealed class AppendTab : BaseCommand<AppendTab>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();
            if (HarpoonPackage.fileNameIndexMap.Count < 10 && !HarpoonPackage.fileNameIndexMap.ContainsKey(activeDoc.FilePath))
            {
                for (int i = 0; i < HarpoonPackage.fileNamesArr.Length; i++)
                {
                    if (HarpoonPackage.fileNamesArr[i] == null) // assign to first empty block
                    {
                        HarpoonPackage.fileNamesArr[i] = activeDoc.FilePath;
                        HarpoonPackage.fileNameIndexMap.Add(activeDoc.FilePath, i);
                        Helper.UpdateLabel(i, activeDoc.FilePath);
                        break;
                    }
                }
            }
        }
    }

    [Command(PackageIds.SaveSession)]
    public sealed class SaveSession : BaseCommand<SaveSession>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            File.WriteAllText(HarpoonPackage.sessionPath, JsonConvert.SerializeObject(new HarpoonSession() { fileNameIndexMap = HarpoonPackage.fileNameIndexMap, fileNamesArr = HarpoonPackage.fileNamesArr }));
        }
    }

    public class HarpoonSession
    {
        public string[] fileNamesArr { get; set; }
        public Dictionary<string, int> fileNameIndexMap { get; set; }
    }
}
