using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Newtonsoft.Json.Bson;

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
        public static Label Activity { get; set; }
        public static TextBox NewSessionName { get; set; }
        public static ComboBox DropDownSessionList { get; set; }

        public static void SetInitValues()
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
            Activity.Content = "Logs  :  ";
            SetDropDownValues();
        }

        private static void SetDropDownText()
        {
            DropDownSessionList.Text = HarpoonPackage.activeSessionName;
        }
        public static void SetDropDownValues()
        {
            DropDownSessionList.Items.Clear();
            foreach (var item in HarpoonPackage.sessions.KeyValuePairs)
            {
                DropDownSessionList.Items.Add(item.Key);
            }
            SetDropDownText();
        }

        public static void SetActivityLog(string msg)
        {
            Activity.Content = string.Empty;

            if (!string.IsNullOrEmpty(msg))
            {
                Activity.Content = msg;
            }
        }

        public static void ReloadLabels()
        {
            ClearAllLabels();
            for (int i = 0; i < HarpoonPackage.fileNamesArr.Length; i++)
            {
                if (HarpoonPackage.fileNamesArr[i] != null)
                {
                    UpdateLabel(i, HarpoonPackage.fileNamesArr[i]);
                }
            }
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

            clearBoldOffLabels();
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
                int newFilePathOldIndex = -1;
                if (HarpoonPackage.fileNameIndexMap.ContainsKey(filePath))
                {
                    newFilePathOldIndex = HarpoonPackage.fileNameIndexMap[filePath];
                }

                //old
                string oldFileName = HarpoonPackage.fileNamesArr[index];
                HarpoonPackage.fileNamesArr[index] = null;
                UpdateLabel(index, null);
                HarpoonPackage.fileNameIndexMap.Remove(oldFileName);
                // remove new's old ref
                if (newFilePathOldIndex != -1)
                {
                    HarpoonPackage.fileNamesArr[newFilePathOldIndex] = null;
                    UpdateLabel(newFilePathOldIndex, null);
                }

                //new
                HarpoonPackage.fileNamesArr[index] = filePath;
                UpdateLabel(index, filePath);
                HarpoonPackage.fileNameIndexMap[filePath] = index;

            }
        }

        public static DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null)
        {
            var directory = new DirectoryInfo(currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }

        public static void clearBoldOffLabels()
        {
            Label0.FontWeight = FontWeights.Normal;
            Label1.FontWeight = FontWeights.Normal;
            Label2.FontWeight = FontWeights.Normal;
            Label3.FontWeight = FontWeights.Normal;
            Label4.FontWeight = FontWeights.Normal;
            Label5.FontWeight = FontWeights.Normal;
            Label6.FontWeight = FontWeights.Normal;
            Label7.FontWeight = FontWeights.Normal;
            Label8.FontWeight = FontWeights.Normal;
            Label9.FontWeight = FontWeights.Normal;
        }

        public static void UpdateLabel(int index, string filePath)
        {
            string[] arr;
            string fileName = string.Empty;
            if (filePath != null)
            {
                arr = filePath.Split('\\');
                fileName = $"{arr[arr.Length - 1]}";
            }
            fileName = index + "  :  " + fileName;
            clearBoldOffLabels();
            switch (index)
            {
                case 0:
                    Label0.Content = fileName;
                    if (fileName != null)
                        Label0.FontWeight = FontWeights.Bold;
                    break;
                case 1:
                    Label1.Content = fileName;
                    if (fileName != null)
                        Label1.FontWeight = FontWeights.Bold;
                    break;
                case 2:
                    Label2.Content = fileName;
                    if (fileName != null)
                        Label2.FontWeight = FontWeights.Bold;
                    break;
                case 3:
                    Label3.Content = fileName;
                    if (fileName != null)
                        Label3.FontWeight = FontWeights.Bold;
                    break;
                case 4:
                    Label4.Content = fileName;
                    if (fileName != null)
                        Label4.FontWeight = FontWeights.Bold;
                    break;
                case 5:
                    Label5.Content = fileName;
                    if (fileName != null)
                        Label5.FontWeight = FontWeights.Bold;
                    break;
                case 6:
                    Label6.Content = fileName;
                    if (fileName != null)
                        Label6.FontWeight = FontWeights.Bold;
                    break;
                case 7:
                    Label7.Content = fileName;
                    if (fileName != null)
                        Label7.FontWeight = FontWeights.Bold;
                    break;
                case 8:
                    Label8.Content = fileName;
                    if (fileName != null)
                        Label8.FontWeight = FontWeights.Bold;
                    break;
                case 9:
                    Label9.Content = fileName;
                    if (fileName != null)
                        Label9.FontWeight = FontWeights.Bold;
                    break;
                default: break;
            }

        }
    }
}
