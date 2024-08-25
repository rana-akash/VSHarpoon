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
            Helper.Activity = loglbl;
            Helper.NewSessionName = NewSessionName;
            Helper.DropDownSessionList = DropDownSessionList;
            Helper.SetInitValues();
            Helper.ReloadLabels();
            VS.Events.ProjectItemsEvents.AfterRenameProjectItems += HandleRenameOfDocuments;
            VS.Events.ProjectItemsEvents.AfterRemoveProjectItems += HandleRemoveOfDocuments;
            VS.Events.WindowEvents.ActiveFrameChanged += HandleActiveFrameChanged;
        }

        private void HandleActiveFrameChanged(ActiveFrameChangeEventArgs args)
        {
            bool matched = false;
            for (int i = 0; i < HarpoonPackage.fileNamesArr.Length; i++)
            {
                if (HarpoonPackage.fileNamesArr[i] != null && HarpoonPackage.fileNamesArr[i].Contains(args.NewFrame.Caption))
                {
                    Helper.UpdateLabel(i, HarpoonPackage.fileNamesArr[i]);
                    matched = true;
                    break;
                }
            }
            if (!matched)
            {
                Helper.clearBoldOffLabels();
            }
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
            if (HarpoonPackage.fileNamesArr[0] != null)
            {
                await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[0]);
                Helper.UpdateLabel(0, HarpoonPackage.fileNamesArr[0]);

            }
        }
        private async void lblHeadline1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (HarpoonPackage.fileNamesArr[1] != null)
            {
                await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[1]); Helper.UpdateLabel(1, HarpoonPackage.fileNamesArr[1]);
            }
        }

        private async void lblHeadline2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (HarpoonPackage.fileNamesArr[2] != null)
            {
                await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[2]); Helper.UpdateLabel(2, HarpoonPackage.fileNamesArr[2]);

            }
        }
        private async void lblHeadline3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (HarpoonPackage.fileNamesArr[3] != null)
            {
                await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[3]); Helper.UpdateLabel(3, HarpoonPackage.fileNamesArr[3]);

            }
        }
        private async void lblHeadline4_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (HarpoonPackage.fileNamesArr[4] != null)
            {
                await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[4]); Helper.UpdateLabel(4, HarpoonPackage.fileNamesArr[4]);

            }
        }
        private async void lblHeadline5_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (HarpoonPackage.fileNamesArr[5] != null)
            {
                await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[5]); Helper.UpdateLabel(5, HarpoonPackage.fileNamesArr[5]);

            }
        }
        private async void lblHeadline6_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (HarpoonPackage.fileNamesArr[6] != null)
            {
                await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[6]); Helper.UpdateLabel(6, HarpoonPackage.fileNamesArr[6]);

            }
        }
        private async void lblHeadline7_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (HarpoonPackage.fileNamesArr[7] != null)
            {
                await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[7]); Helper.UpdateLabel(7, HarpoonPackage.fileNamesArr[7]);

            }
        }
        private async void lblHeadline8_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (HarpoonPackage.fileNamesArr[8] != null)
            {
                await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[8]); Helper.UpdateLabel(8, HarpoonPackage.fileNamesArr[8]);

            }
        }
        private async void lblHeadline9_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (HarpoonPackage.fileNamesArr[9] != null)
            {
                await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[9]); Helper.UpdateLabel(9, HarpoonPackage.fileNamesArr[9]);

            }
        }

        private void SaveCurrentSessionButton_Click(object sender, RoutedEventArgs e)
        {
            SessionHelper.SaveCurrentSession();
        }

        private void SaveAllSessionsButton_Click(object sender, RoutedEventArgs e)
        {
            SessionHelper.SaveAllSessions();
        }

        private void AddSessionButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Helper.NewSessionName.Text))
            {
                return;
            }

            HarpoonPackage.activeSessionName = Helper.NewSessionName.Text;
            SessionHelper.SaveCurrentSession();
            Helper.SetDropDownValues();
        }
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void DropDownSessionList_Selected(object sender, EventArgs e)
        //{
        //    string selectedValue = "";
        //    SessionHelper.ChangeToSession(selectedValue);
        //}

        private void DropDownSessionList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedValue = ((ComboBox)e.Source).SelectedValue;
            if (selectedValue == null) {
                return;
            }
            SessionHelper.ChangeToSession(selectedValue.ToString());
        }
    }
}
