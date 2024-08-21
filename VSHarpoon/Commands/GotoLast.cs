namespace Test1
{
    [Command(PackageIds.GotoLast)]
    public sealed class GotoLast : BaseCommand<GotoLast>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            for (int i = 9; i > -1; i--)
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
