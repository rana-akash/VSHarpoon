using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Test1
{
    public partial class HarpoonWindowControl : UserControl
    {
        public HarpoonWindowControl()
        {
            InitializeComponent();
            Helper.Label0 = lblHeadline0;
            Helper.Label1 = lblHeadline1;
            Helper.Label2 = lblHeadline2;
            Helper.Label3 = lblHeadline3;
            Helper.Label4 = lblHeadline4;
            Helper.Label5 = lblHeadline5;
            Helper.Label6 = lblHeadline6;
            Helper.Label7 = lblHeadline7;
            Helper.Label8 = lblHeadline8;
            Helper.Label9 = lblHeadline9;
            Helper.SetLabelInitValue();
            VS.Events.ProjectItemsEvents.AfterRenameProjectItems += HandleRenameOfDocuments;
            VS.Events.ProjectItemsEvents.AfterRemoveProjectItems += HandleRemoveOfDocuments;
        }
        public void HandleRemoveOfDocuments(AfterRemoveProjectItemEventArgs args)
        {
            foreach (ProjectItemRemoveDetails document in args.ProjectItemRemoves)
            {
                if (!HarpoonPackage.fileNameIndexMap.ContainsKey(document.RemovedItemName))
                {
                    continue;
                }
                int index = HarpoonPackage.fileNameIndexMap[document.RemovedItemName];
                HarpoonPackage.fileNamesArr[index] = null;
                HarpoonPackage.fileNameIndexMap.Remove(document.RemovedItemName);
                Helper.UpdateLabel(index, null);
            }
        }

        public void HandleRenameOfDocuments(AfterRenameProjectItemEventArgs args)
        {
            foreach (var document in args.ProjectItemRenames)
            {
                string oldFilePath = document.OldName;
                string newFilePath = document.SolutionItem.FullPath;
                if (!HarpoonPackage.fileNameIndexMap.ContainsKey(oldFilePath))
                {
                    continue;
                }

                int index = HarpoonPackage.fileNameIndexMap[oldFilePath];
                HarpoonPackage.fileNamesArr[index] = newFilePath;
                Helper.UpdateLabel(index, newFilePath);
                HarpoonPackage.fileNameIndexMap.Remove(oldFilePath);
                HarpoonPackage.fileNameIndexMap.Add(newFilePath, index);
            }
        }

        private async void lblHeadline0_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[0]);

        }
        private async void lblHeadline1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[1]);
        }

        private async void lblHeadline2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[2]);

        }
        private async void lblHeadline3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[3]);

        }
        private async void lblHeadline4_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[4]);

        }
        private async void lblHeadline5_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[5]);

        }
        private async void lblHeadline6_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[6]);

        }
        private async void lblHeadline7_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[7]);

        }
        private async void lblHeadline8_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[8]);

        }
        private async void lblHeadline9_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[9]);

        }

    }
}
