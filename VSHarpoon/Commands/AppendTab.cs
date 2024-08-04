﻿using System.Collections.Generic;

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
                        break;
                    }
                }
            }
        }
    }
}
