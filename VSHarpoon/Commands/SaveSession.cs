using System.Runtime.ExceptionServices;

namespace Test1;

[Command(PackageIds.SaveCurrentSession)]
public sealed class SaveCurrentSession : BaseCommand<SaveCurrentSession>
{
    protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
    {
        SessionHelper.SaveCurrentSession();
    }
}

[Command(PackageIds.SaveAllSessions)]
public sealed class SaveAllSessions : BaseCommand<SaveAllSessions>
{
    protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
    {
        SessionHelper.SaveAllSessions();
    }
}
