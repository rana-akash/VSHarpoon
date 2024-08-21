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

                int i = index - 1;
                for (; i >= 0; i--)
                {
                    if (HarpoonPackage.fileNamesArr[i] != null)
                    {
                        await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[i]);
                        Helper.UpdateLabel(i, HarpoonPackage.fileNamesArr[i]);
                        break;
                    }
                }
                if (i == -1) // if left tab not found, make the closest right tab active
                {
                    i = index + 1;
                    for (; i <= HarpoonPackage.fileNamesArr.Length; i++)
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
    }
}
