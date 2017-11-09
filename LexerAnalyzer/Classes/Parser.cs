using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LexerAnalyzer.Classes
{
    public class Parser
    {
        
        List<string> listWords = new List<string>();
        List<string> listSentences = new List<string>();
        List<int> listKeyWords = new List<int>();
        List<int> listKeysSentences = new List<int>();

        public void Corcondance(string readPath)
        {
            listSentences = Reader.ReadSentences(readPath);
            int i = 1;
            foreach (var ls in listSentences)
            {
                listKeysSentences.Add(i);
                i++;
            }

            var dicSentences = listKeysSentences.Zip(listSentences, (k, v) => new { k, v })
              .ToDictionary(x => x.k, x => x.v);

            listWords = Reader.ReadWords(readPath);
            string[] arrayWords = listWords.ToArray();
            listWords.Clear();

            foreach (string word in arrayWords)
                if (!listWords.Contains(word))
                {
                    listWords.Add(word);
                    listKeyWords.Add(1);
                }
                else listKeyWords[listWords.IndexOf(word)]++;

            var dicWords = listWords.Zip(listKeyWords, (k, v) => new { k, v })
              .ToDictionary(x => x.k, x => x.v);

            var wordQuery = from word in dicWords
                            group word by word.Key.ToLowerInvariant().ElementAt(0) into wordGroup
                            orderby wordGroup.Key
                            select wordGroup;

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\Projects_epam\\LexerAnalyzer\\LexerAnalyzer\\Output.txt"))
            {
                foreach (var groupOfWords in wordQuery)
                {
                    file.WriteLine();
                    file.Write("{0}", char.ToUpper(groupOfWords.Key));
                    file.WriteLine();
                    foreach (var word in groupOfWords)
                    {
                        file.Write("{0}...............{1}: ", word.Key, word.Value);
                        foreach (var sentence in dicSentences)
                        {
                            if (sentence.Value.Split(new char[] { '(', ')','"','«', '»', '—', '\n', '\r',
                                '.', '?', '!', ' ', ';', ':', ',' },
                                StringSplitOptions.RemoveEmptyEntries).Contains(word.Key))
                                file.Write("{0} ", sentence.Key);
                       }
                        file.WriteLine();
                    }
                }
            }
        }

        public void ShowText(string readPath)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\Projects_epam\\LexerAnalyzer\\LexerAnalyzer\\Output.txt", true))
            {
                file.WriteLine();
                file.WriteLine(Reader.Read(readPath));
                file.WriteLine();
            }           
        }

        public string Replace(string text, string newWord, int matchingWordLength)
        {
            return Regex.Replace(text, @"\b\w{" + matchingWordLength + @"}\b", newWord);
        }

    }
}
