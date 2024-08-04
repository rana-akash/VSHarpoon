using System.Collections.Generic;

namespace Harpoon
{
    [Command(PackageIds.AppendTab)]
    public sealed class AppendTab : BaseCommand<AppendTab>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();
            if (!HarpoonPackage.tabNamesIndexMap.ContainsKey(activeDoc.FilePath))
            {
                if (HarpoonPackage.appendIndex == 10) // only 9 tabs supported
                {
                    return;
                }
                HarpoonPackage.indexTabNamesMap.Add(HarpoonPackage.appendIndex, activeDoc.FilePath);
                HarpoonPackage.tabNamesIndexMap.Add(activeDoc.FilePath, HarpoonPackage.appendIndex);
                HarpoonPackage.appendIndex++;
            }

            //await VS.Documents.OpenAsync(HarpoonPackage.indexTabNamesMap[0]);
        }
    }
}
