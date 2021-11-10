using System.Collections.Generic;
using static System.IO.File;

namespace Arc
{
    /// <summary>
    /// Arcfile, made by terminal.cs. (Version 1.0)
    /// </summary>
    public class ArcFile
    {
        private readonly string Path = "";
        private readonly Dictionary<string, string> Arcs = new Dictionary<string, string>();
        private readonly double ArcVersion = 1.0;

        public ArcFile(string ArcPath)
        {
            Path = ArcPath;
            foreach (string str in ReadAllLines(ArcPath))
            {
                Arcs.Add(str.Split('=')[0], str.Split('=')[1]);
            }
        }

        public virtual string Read(string Key) => Arcs[Key];
        public virtual void Create(string Key, string Value) => Arcs.Add(Key, Value);
        public virtual void Modify(string Key, string Value) => Arcs[Key] = Value;
        public virtual bool Exists(string Key) => Arcs.ContainsKey(Key);
        public virtual void Remove(string Key) => Arcs.Remove(Key);
        public virtual void Save()
        {
            string Final = "ArcVersion=" + ArcVersion;
            foreach (var Pair in Arcs)
            {
                Final = Final + "\n" + Pair.Key + "=" + Pair.Value;
            }
            WriteAllText(Path, Final);
        }
    }
}