using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    public static class Helper
    {
        public static void OverWriteAtIndex(string filePath, int index)
        {
            if (HarpoonPackage.fileNamesArr[index] == null)
            {
                if (HarpoonPackage.fileNameIndexMap.ContainsKey(filePath))
                {
                    HarpoonPackage.fileNamesArr[HarpoonPackage.fileNameIndexMap[filePath]] = null; //prev reference
                }
                HarpoonPackage.fileNamesArr[index] = filePath;
                HarpoonPackage.fileNameIndexMap[filePath] = index;
            }
            else if (HarpoonPackage.fileNamesArr[index] == filePath)
            {
                return;
            }
            else // some other file path exists
            {
                //old
                string oldFileName = HarpoonPackage.fileNamesArr[index];
                HarpoonPackage.fileNamesArr[index] = null;
                HarpoonPackage.fileNameIndexMap.Remove(oldFileName);

                //new
                HarpoonPackage.fileNamesArr[index] = filePath;
                HarpoonPackage.fileNameIndexMap[filePath] = index;
            }
        }


    }
}
