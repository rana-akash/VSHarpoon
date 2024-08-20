namespace Test1
{
    [Command(PackageIds.ClearAll)]
    public sealed class ClearAll : BaseCommand<ClearAll>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            Array.Clear(HarpoonPackage.fileNamesArr, 0, HarpoonPackage.fileNamesArr.Length);
            HarpoonPackage.fileNameIndexMap.Clear();
            Helper.ClearAllLabels();
        }
    }
}
