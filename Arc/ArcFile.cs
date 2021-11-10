using System.Collections.Generic;
using static System.IO.File;

namespace Arc
{
    /// <summary> Arcfile, made by terminal.cs. (Version 1.0) </summary>
    public class ArcFile
    {
        private readonly string Path = "";
        private readonly Dictionary<string, string> Arcs = new Dictionary<string, string>();
        public ArcFile(string ArcPath)
        {
            Path = ArcPath;
            if (Exists(Path))
            {
                foreach (string str in ReadAllLines(Path))
                {
                    Arcs.Add(str.Split('=')[0], str.Split('=')[1]);
                }
            }
            else { WriteAllText(Path, ""); }
        }

        public string Read(string Key) => Arcs[Key];
        public void Create(string Key, string Value) => Arcs.Add(Key, Value);
        public void Modify(string Key, string Value) => Arcs[Key] = Value;
        public bool Exists(string Key) => Arcs.ContainsKey(Key);
        public void Remove(string Key) => Arcs.Remove(Key);
        public void Save()
        {
            string Final = "";
            foreach (KeyValuePair<string, string> Pair in Arcs)
            {
                Final = Final + Pair.Key + "=" + Pair.Value + "\n";
            }
            WriteAllText(Path, Final);
        }
    }
}