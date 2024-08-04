using System.Collections.Generic;

namespace Test1
{
    [Command(PackageIds.DeleteTab)]
    public sealed class DeleteTab : BaseCommand<DeleteTab>
    {   
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();
            if (HarpoonPackage.fileNameIndexMap.ContainsKey(activeDoc.FilePath))
            {
                HarpoonPackage.fileNamesArr[HarpoonPackage.fileNameIndexMap[activeDoc.FilePath]] = null;
                HarpoonPackage.fileNameIndexMap.Remove(activeDoc.FilePath);
            }
        }
    }
}
