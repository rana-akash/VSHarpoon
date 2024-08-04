using System.Collections.Generic;

namespace Test1
{
    [Command(PackageIds.NextTab)]
    public sealed class NextTab : BaseCommand<NextTab>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

        }
    }
    [Command(PackageIds.PrevTab)]
    public sealed class PrevTab : BaseCommand<PrevTab>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

        }
    }
}
