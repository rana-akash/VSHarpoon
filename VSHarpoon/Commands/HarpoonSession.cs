using System.Collections.Generic;

namespace Test1
{
    public class HarpoonSessions
    {
        public Dictionary<string, HarpoonSession> KeyValuePairs { get; set; } = new();
    }

    public class HarpoonSession
    {
        public string[] fileNamesArr { get; set; } = new string[10];
        public Dictionary<string, int> fileNameIndexMap { get; set; } = new();
    }
}
