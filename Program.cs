using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QfxConverter
{
    public class Program
    {
        static void Main(string[] args)
        {
            Runner runner = new Runner(args[0]);
        }
    }

    public class Runner
    {
        private string _fileName;

        private List<string> _fileContents;
        public Runner(string fileName)
        {
            _fileName = fileName;
            _fileContents = File.ReadAllLines(_fileName).ToList();
            ConvertToQbo();
        }

        private void ConvertToQbo()
        {
            int userIdLineIndex = -1, bIdLineIndex = -1;
            Parallel.ForEach(_fileContents, line =>
            {
                if(line.Contains("USERID"))
                    userIdLineIndex = _fileContents.IndexOf(line);
                if (line.Contains("BID"))
                    bIdLineIndex = _fileContents.IndexOf(line);
            });
            if(userIdLineIndex >= 0)
                _fileContents.RemoveAt(userIdLineIndex);
            if (bIdLineIndex >= 0)
                _fileContents[bIdLineIndex] = _fileContents[bIdLineIndex].Substring(0, 10) + "2430";

            File.WriteAllLines(_fileName.Replace(Path.GetExtension(_fileName), ".qbo"), _fileContents);
        }
    }
}