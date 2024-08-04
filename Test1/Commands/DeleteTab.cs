using System.Collections.Generic;

namespace Harpoon
{
    [Command(PackageIds.DeleteTab)]
    public sealed class DeleteTab : BaseCommand<DeleteTab>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();
            if (HarpoonPackage.tabNamesIndexMap.ContainsKey(activeDoc.FilePath))
            {
                int index = HarpoonPackage.tabNamesIndexMap[activeDoc.FilePath];
                HarpoonPackage.indexTabNamesMap.Remove(index);
                HarpoonPackage.tabNamesIndexMap.Remove(activeDoc.FilePath);

                //index, index + 1, index + 2, appendIndex
                for (int i = index + 1; index < HarpoonPackage.appendIndex; index++)
                {
                    
                }
                HarpoonPackage.appendIndex--;
            }
        }
    }
}
