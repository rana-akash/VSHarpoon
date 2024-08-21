using Newtonsoft.Json;
using System.IO;

namespace Test1
{
    [Command(PackageIds.SaveSession)]
    public sealed class SaveSession : BaseCommand<SaveSession>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            File.WriteAllText(HarpoonPackage.sessionPath, JsonConvert.SerializeObject(new HarpoonSession() { fileNameIndexMap = HarpoonPackage.fileNameIndexMap, fileNamesArr = HarpoonPackage.fileNamesArr }));
        }
    }
}
