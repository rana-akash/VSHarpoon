using System.Collections.Generic;

namespace Test1
{
    [Command(PackageIds.HarpoonToolWindow)]
    public sealed class HarpoonWindowCommand : BaseCommand<HarpoonWindowCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await HarpoonWindow.ShowAsync();
        }
    }
}
