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
                int index = HarpoonPackage.fileNameIndexMap[activeDoc.FilePath];

                HarpoonPackage.fileNamesArr[index] = null;
                Helper.UpdateLabel(index, null);
                HarpoonPackage.fileNameIndexMap.Remove(activeDoc.FilePath);

                for (int i = index - 1 ; i >= 0; i--)
                {
                    if (HarpoonPackage.fileNamesArr[i] != null)
                    {
                        await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[i]);
                        break;
                    }
                }
            }
        }
    }
}
