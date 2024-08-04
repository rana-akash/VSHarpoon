using System.Collections.Generic;

namespace Test1
{
    [Command(PackageIds.SwitchToTab0)]
    public sealed class SwitchToTab0 : BaseCommand<SwitchToTab0>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();
            
        }
    }
    [Command(PackageIds.SwitchToTab1)]
    public sealed class SwitchToTab1 : BaseCommand<SwitchToTab1>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

        }
    }
    [Command(PackageIds.SwitchToTab2)]
    public sealed class SwitchToTab2 : BaseCommand<SwitchToTab2>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

        }
    }
    [Command(PackageIds.SwitchToTab3)]
    public sealed class SwitchToTab3 : BaseCommand<SwitchToTab3>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

        }
    }
    [Command(PackageIds.SwitchToTab4)]
    public sealed class SwitchToTab4 : BaseCommand<SwitchToTab4>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

        }
    }
    [Command(PackageIds.SwitchToTab5)]
    public sealed class SwitchToTab5 : BaseCommand<SwitchToTab5>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

        }
    }
    [Command(PackageIds.SwitchToTab6)]
    public sealed class SwitchToTab6 : BaseCommand<SwitchToTab6>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

        }
    }
    [Command(PackageIds.SwitchToTab7)]
    public sealed class SwitchToTab7 : BaseCommand<SwitchToTab7>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

        }
    }
    [Command(PackageIds.SwitchToTab8)]
    public sealed class SwitchToTab8 : BaseCommand<SwitchToTab8>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

        }
    }
    [Command(PackageIds.SwitchToTab9)]
    public sealed class SwitchToTab9 : BaseCommand<SwitchToTab9>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var activeDoc = await VS.Documents.GetActiveDocumentViewAsync();

        }
    }
}
