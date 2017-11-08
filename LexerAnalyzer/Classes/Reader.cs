using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LexerAnalyzer.Classes
{
    public static class Reader
    {
        public static List<string> ReadWords(string readPath)
        {
            string textFromFile = File.ReadAllText(readPath, Encoding.Default);
            char[] separators = { ' ', ',', '.', '-', '(', ')', '!', '?', ':', ';' };
            List<string> listWords = textFromFile.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            return listWords;
        }

        public static List<string> ReadSentences(string readPath)
        {
            string textFromFile = File.ReadAllText(readPath, Encoding.Default);
            char[] separators = { '.', '!', '?' };
            List<string> listSentence = textFromFile.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            return listSentence;
        }

        public static string Read(string readPath)
        {
            string textFromFile = File.ReadAllText(readPath, Encoding.Default);
            return textFromFile;
        }
    }
}
