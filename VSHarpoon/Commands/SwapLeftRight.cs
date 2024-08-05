using System.Collections.Generic;

namespace Test1
{
    [Command(PackageIds.SwapLeft)]
    public sealed class SwapLeft : BaseCommand<SwapLeft>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();
            if (HarpoonPackage.fileNameIndexMap.ContainsKey(activeDoc.FilePath))
            {
                int index = HarpoonPackage.fileNameIndexMap[activeDoc.FilePath];
                if (index >= 1)
                {
                    string left = HarpoonPackage.fileNamesArr[index-1];
                    HarpoonPackage.fileNamesArr[index - 1] = activeDoc.FilePath;
                    Helper.UpdateLabel(index-1, activeDoc.FilePath);
                    HarpoonPackage.fileNameIndexMap[activeDoc.FilePath] = index-1;
                    HarpoonPackage.fileNamesArr[index] = left;
                    Helper.UpdateLabel(index, left);

                    if (left != null)
                    {
                        HarpoonPackage.fileNameIndexMap[left] = index;
                    }
                }
            }
        }
    }

    [Command(PackageIds.SwapRight)]
    public sealed class SwapRight : BaseCommand<SwapRight>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();
            if (HarpoonPackage.fileNameIndexMap.ContainsKey(activeDoc.FilePath))
            {
                int index = HarpoonPackage.fileNameIndexMap[activeDoc.FilePath];
                if (index <= 8)
                {
                    string right = HarpoonPackage.fileNamesArr[index + 1];
                    HarpoonPackage.fileNamesArr[index + 1] = activeDoc.FilePath;
                    Helper.UpdateLabel(index + 1, activeDoc.FilePath);
                    HarpoonPackage.fileNameIndexMap[activeDoc.FilePath] = index + 1;
                    HarpoonPackage.fileNamesArr[index] = right;
                    Helper.UpdateLabel(index, right);

                    if (right != null)
                    {
                        HarpoonPackage.fileNameIndexMap[right] = index;
                    }
                }
            }
        }
    }
}
