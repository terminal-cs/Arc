using System.Collections.Generic;
using System.Linq;
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
                foreach (string line in ReadAllLines(Path))
                {
                    if(!line.Contains('='))
                        continue;
                    var LineData = str.Split('=');
                    Arcs.Add(LineData[0], LineData[1]);
                }
            }
            else { WriteAllText(Path, ""); }
        }

        public string this[string key]
        {
            get {
                return Read(key);
            }
            set {
                if(Exists(key))
                {
                    Modify(key, value);
                }
                else
                {
                    Create(key, value);
                }
            }
        }

        public string Read(string Key) => Arcs[Key];
        public void Create(string Key, string Value) => Arcs.Add(Key, Value);
        public void Modify(string Key, string Value) => Arcs[Key] = Value;
        public bool Exists(string Key) => Arcs.ContainsKey(Key);
        public void Remove(string Key) => Arcs.Remove(Key);
        public void Save()
        {
            WriteAllText(Path, this.ToString());
        }

        public override string ToString()
        {
            return string.Join(
                Arcs.Select(i => {
                return $@"{i.Key}={i.Value}";
            }), 
            System.Environment.NewLine);
        }

    }
}