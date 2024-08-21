namespace Test1
{
    [Command(PackageIds.GotoFirst)]
    public sealed class GotoFirst : BaseCommand<GotoFirst>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            for (int i = 0; i < HarpoonPackage.fileNamesArr.Length; i++)
            {
                if (!string.IsNullOrEmpty(HarpoonPackage.fileNamesArr[i]))
                {
                    await VS.Documents.OpenAsync(HarpoonPackage.fileNamesArr[i]);
                    Helper.UpdateLabel(i, HarpoonPackage.fileNamesArr[i]);
                    break;
                }
            }
        }
    }
}
