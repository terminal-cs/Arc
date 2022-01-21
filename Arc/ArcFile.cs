using System.Collections.Generic;
using System.IO;

namespace Arc
{
    /// <summary>
    /// Arcfile, made by terminal.cs. (Version 1.1)
    /// https://github.com/terminal-cs/Arc
    /// </summary>
    public class ArcFile<T>
    {
        private readonly string Path = "";
        private readonly Dictionary<string, T> Arcs = new Dictionary<string, T>();

        public ArcFile(string PathToFile)
        {
            Path = PathToFile;
            foreach (string Line in File.ReadAllText(Path).Split('\n'))
            {
                string[] LineData = Line.Split('=');
                Arcs.Add(LineData[0], (T)System.Convert.ChangeType(LineData[1], typeof(T)));
            }
        }

        public T Read(string Key) => Arcs[Key];
        public void Create(string Key, T Value) => Arcs.Add(Key, Value);
        public void Modify(string Key, T Value) => Arcs[Key] = Value;
        public bool Exists(string Key) => Arcs.ContainsKey(Key);
        public void Remove(string Key) => Arcs.Remove(Key);
        public void Save()
        {
            string Final = "";
            foreach (KeyValuePair<string, T> Pair in Arcs)
            {
                Final += Pair.Key + '=' + Pair.Value + '\n';
            }
            File.WriteAllText(Path, Final);
        }
    }
}