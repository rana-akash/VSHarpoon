using System.Collections.Generic;
using System.Configuration;

namespace Test1
{
    [Command(PackageIds.ScoochIndexes)]
    public sealed class ScoochIndexes : BaseCommand<ScoochIndexes>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            //var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();
            //int index = -1;

            //if (HarpoonPackage.fileNameIndexMap.ContainsKey(activeDoc.FilePath))
            //{
            //    index = HarpoonPackage.fileNameIndexMap[activeDoc.FilePath];
            //}

            List<string> newArr = new();
            Dictionary<string, int> newFileNameIndexMap = new();

            for (int i = 0; i < HarpoonPackage.fileNamesArr.Length; i++)
            {
                if (HarpoonPackage.fileNamesArr[i] != null)
                {
                    newArr.Add(HarpoonPackage.fileNamesArr[i]);
                    newFileNameIndexMap[HarpoonPackage.fileNamesArr[i]] = newArr.Count-1;
                }
            }

            HarpoonPackage.fileNamesArr = newArr.ToArray();
            HarpoonPackage.fileNameIndexMap = newFileNameIndexMap;

            Helper.ReloadLabels();
        }
    }
}
