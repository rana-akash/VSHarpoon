using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    public static class Helper
    {
        public static Label Label0 { get; set; }
        public static Label Label1 { get; set; }
        public static Label Label2 { get; set; }
        public static Label Label3 { get; set; }
        public static Label Label4 { get; set; }
        public static Label Label5 { get; set; }
        public static Label Label6 { get; set; }
        public static Label Label7 { get; set; }
        public static Label Label8 { get; set; }
        public static Label Label9 { get; set; }

        public static void SetLabelInitValue()
        {
            Label0.Content = "0  :  ";
            Label1.Content = "1  :  ";
            Label2.Content = "2  :  ";
            Label3.Content = "3  :  ";
            Label4.Content = "4  :  ";
            Label5.Content = "5  :  ";
            Label6.Content = "6  :  ";
            Label7.Content = "7  :  ";
            Label8.Content = "8  :  ";
            Label9.Content = "9  :  ";
        }

        public static void ClearAllLabels()
        {
            Label0.Content = string.Empty;
            Label1.Content = string.Empty;
            Label2.Content = string.Empty;
            Label3.Content = string.Empty;
            Label4.Content = string.Empty;
            Label5.Content = string.Empty;
            Label6.Content = string.Empty;
            Label7.Content = string.Empty;
            Label8.Content = string.Empty;
            Label9.Content = string.Empty;
        }

        public static void OverWriteAtIndex(string filePath, int index)
        {
            if (HarpoonPackage.fileNamesArr[index] == null)
            {
                if (HarpoonPackage.fileNameIndexMap.ContainsKey(filePath))
                {
                    HarpoonPackage.fileNamesArr[HarpoonPackage.fileNameIndexMap[filePath]] = null; //prev reference
                    UpdateLabel(HarpoonPackage.fileNameIndexMap[filePath], null);
                }
                HarpoonPackage.fileNamesArr[index] = filePath;
                UpdateLabel(index, filePath);
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
                UpdateLabel(index, null);
                HarpoonPackage.fileNameIndexMap.Remove(oldFileName);

                //new
                HarpoonPackage.fileNamesArr[index] = filePath;
                UpdateLabel(index, filePath);
                HarpoonPackage.fileNameIndexMap[filePath] = index;
            }
        }

        public static void UpdateLabel(int index, string filePath)
        {
            string[] arr;
            string fileName = string.Empty;
            if (filePath != null)
            {
                arr = filePath.Split('\\');
                fileName =  $"{arr[arr.Length - 1]}";
            }
            fileName = index + "  :  " + fileName;
            switch (index)
            {
                case 0:
                    Label0.Content = fileName;
                    break;
                case 1: Label1.Content = fileName;
                    break;
                case 2:
                    Label2.Content = fileName;
                    break;
                case 3: Label3.Content = fileName;
                    break;
                case 4:
                    Label4.Content = fileName;
                    break;
                case 5:
                    Label5.Content = fileName;
                    break;
                case 6:
                    Label6.Content = fileName;
                    break;
                case 7:
                    Label7.Content = fileName;
                    break;
                case 8:
                    Label8.Content = fileName;
                    break;
                case 9:
                    Label9.Content = fileName;
                    break;
                default: break;
            }

        }
    }
}
