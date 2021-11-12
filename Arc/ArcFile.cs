using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Arc.Cryptography;

namespace Arc
{
    /// <summary>
    /// Arcfile, made by terminal.cs. (Version 1.1)
    /// https://github.com/terminal-cs/Arc
    /// </summary>
    public class ArcFile
    {
        private readonly string Key, Path;
        private string Final = "";
        private readonly Dictionary<string, string> Arcs = new Dictionary<string, string>();

        public ArcFile(string ArcPath, string CryptKey)
        {
            Path = ArcPath;
            Key = CryptKey;
            if (File.Exists(Path))
            {
                foreach (string Line in Decrypt(File.ReadAllText(Path), CryptKey).Split('\n'))
                {
                    if (!Line.Contains('=')) { continue; }

                    string[] LineData = Line.Split('=');
                    Arcs.Add(LineData[0], LineData[1]);
                }
            }
            else
            { File.WriteAllText(Path, ""); }
        }

        public string Read(string Key) => Arcs[Key];
        public void Create(string Key, string Value) => Arcs.Add(Key, Value);
        public void Modify(string Key, string Value) => Arcs[Key] = Value;
        public bool Exists(string Key) => Arcs.ContainsKey(Key);
        public void Remove(string Key) => Arcs.Remove(Key);
        public void Save()
        {
            Final = "";
            foreach (KeyValuePair<string, string> Pair in Arcs)
            {
                Final += Pair.Key + '=' + Pair.Value + '\n';
            }
            File.WriteAllText(Path, Encrypt(Final, Key));
        }
    }
}