using System.Collections.Generic;

namespace Test1
{
    [Command(PackageIds.OverwriteTab0)]
    public sealed class OverwriteTab0 : BaseCommand<OverwriteTab0>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

            Helper.OverWriteAtIndex(activeDoc.FilePath, 0);
        }
    }
    [Command(PackageIds.OverwriteTab1)]
    public sealed class OverwriteTab1 : BaseCommand<OverwriteTab1>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

            Helper.OverWriteAtIndex(activeDoc.FilePath, 1);
        }
    }
    [Command(PackageIds.OverwriteTab2)]
    public sealed class OverwriteTab2 : BaseCommand<OverwriteTab2>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

            Helper.OverWriteAtIndex(activeDoc.FilePath, 2);
        }
    }
    [Command(PackageIds.OverwriteTab3)]
    public sealed class OverwriteTab3 : BaseCommand<OverwriteTab3>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

            Helper.OverWriteAtIndex(activeDoc.FilePath, 3);
        }
    }
    [Command(PackageIds.OverwriteTab4)]
    public sealed class OverwriteTab4 : BaseCommand<OverwriteTab4>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

            Helper.OverWriteAtIndex(activeDoc.FilePath, 4);
        }
    }
    [Command(PackageIds.OverwriteTab5)]
    public sealed class OverwriteTab5 : BaseCommand<OverwriteTab5>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

            Helper.OverWriteAtIndex(activeDoc.FilePath, 5);
        }
    }
    [Command(PackageIds.OverwriteTab6)]
    public sealed class OverwriteTab6 : BaseCommand<OverwriteTab6>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

            Helper.OverWriteAtIndex(activeDoc.FilePath, 6);
        }
    }
    [Command(PackageIds.OverwriteTab7)]
    public sealed class OverwriteTab7 : BaseCommand<OverwriteTab7>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

            Helper.OverWriteAtIndex(activeDoc.FilePath, 7);
        }
    }
    [Command(PackageIds.OverwriteTab8)]
    public sealed class OverwriteTab8 : BaseCommand<OverwriteTab8>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

            Helper.OverWriteAtIndex(activeDoc.FilePath, 8);
        }
    }
    [Command(PackageIds.OverwriteTab9)]
    public sealed class OverwriteTab9 : BaseCommand<OverwriteTab9>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

            Helper.OverWriteAtIndex(activeDoc.FilePath, 9);
        }
    }
}
