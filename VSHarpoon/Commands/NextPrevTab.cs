﻿using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace Test1
{
    [Command(PackageIds.NextTab)]
    public sealed class NextTab : BaseCommand<NextTab>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();
            int index = -1;
            if (HarpoonPackage.fileNameIndexMap.ContainsKey(activeDoc.FilePath))
            {
                index = HarpoonPackage.fileNameIndexMap[activeDoc.FilePath] + 1;
            }

            if (index >= 0 && index < HarpoonPackage.fileNamesArr.Length)
            {
                for (int i = index; i < HarpoonPackage.fileNamesArr.Length; i++)
                {
                    if (HarpoonPackage.fileNamesArr[i] != null)
                    {
                        await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[i]);
                        Helper.UpdateLabel(i, HarpoonPackage.fileNamesArr[i]);
                        break;
                    }
                }
            }

            if (index == -1) // if user is on untracked tab, still the command should bring user to the first tracked index
            {
                for (int i = 0; i < HarpoonPackage.fileNamesArr.Length; i++)
                {
                    if (HarpoonPackage.fileNamesArr[i] != null)
                    {
                        await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[i]);
                        Helper.UpdateLabel(i, HarpoonPackage.fileNamesArr[i]);
                        break;
                    }
                }
            }
        }
    }
    [Command(PackageIds.PrevTab)]
    public sealed class PrevTab : BaseCommand<PrevTab>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();
            int index = -1;
            if (HarpoonPackage.fileNameIndexMap.ContainsKey(activeDoc.FilePath))
            {
                index = HarpoonPackage.fileNameIndexMap[activeDoc.FilePath] - 1;
            }

            if (index >= 0)
            {
                for (int i = index; i >= 0; i--)
                {
                    if (HarpoonPackage.fileNamesArr[i] != null)
                    {
                        await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[i]);
                        Helper.UpdateLabel(i, HarpoonPackage.fileNamesArr[i]);
                        break;
                    }
                }
            }

            if(index == -1) // if user is on untracked tab, still the command should bring user to the first tracked index
            {
                for (int i = 0; i < HarpoonPackage.fileNamesArr.Length; i++)
                {
                    if(HarpoonPackage.fileNamesArr [i] != null)
                    {
                        await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[i]);
                        Helper.UpdateLabel(i, HarpoonPackage.fileNamesArr[i]);
                        break;
                    }
                }
            }

        }
    }
}
