using System.Collections.Generic;
using System.Configuration;

namespace Test1
{
    [Command(PackageIds.ScoochIndexes)]
    public sealed class ScoochIndexes : BaseCommand<ScoochIndexes>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {

            string[] newArr = new string[10];
            int j = 0;
            Dictionary<string, int> newFileNameIndexMap = new();

            for (int i = 0; i < HarpoonPackage.fileNamesArr.Length; i++)
            {
                if (HarpoonPackage.fileNamesArr[i] != null)
                {
                    newArr[j] = HarpoonPackage.fileNamesArr[i];
                    newFileNameIndexMap[HarpoonPackage.fileNamesArr[i]] = j;
                    j++;
                }
            }

            HarpoonPackage.fileNamesArr = newArr;
            HarpoonPackage.fileNameIndexMap = newFileNameIndexMap;

            Helper.ReloadLabels();
        }
    }
}
