using System.Collections.Generic;
using System.IO;

namespace Arc
{
    /// <summary>
    /// Arcfile, made by terminal.cs. (Version 1.1)
    /// https://github.com/terminal-cs/Arc
    /// </summary>
    public class ArcFile<T> : Dictionary<string, T>
    {
        public readonly string Path = "";

        public ArcFile(string PathToFile)
        {
            Path = PathToFile;
            if (!File.Exists(PathToFile)) { return; }
            foreach (string Line in File.ReadAllText(Path).Split('\n'))
            {
                if(Line?.Length == 0) { continue; }
                string[] ThisLine = Line.Split(':');
                Add(ThisLine[0], (T)(object)ThisLine[1]);
            }
        }
        public void Save()
        {
            string Final = "";
            foreach (KeyValuePair<string, T> Pair in this)
            {
                Final += Pair.Key + ':' + Pair.Value + "\n";
            }
            File.WriteAllText(Path, Final);
        }
    }
}