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
        List<int> listCounts = new List<int>();

        public void Corcondance(string readPath)
        {
            listSentences = Reader.ReadSentences(readPath);
            List<int> listKeysSentences = new List<int>();
            int i = 1;
            foreach (var ls in listSentences)
            {
                listKeysSentences.Add(i);
                i++;
            }

            var dicSentences = listSentences.Zip(listKeysSentences, (k, v) => new { k, v })
              .ToDictionary(x => x.k, x => x.v);


            listWords = Reader.ReadWords(readPath);
            string[] arrayWords = listWords.ToArray();
            listWords.Clear();

            foreach (string word in arrayWords)
                if (!listWords.Contains(word))
                {
                    listWords.Add(word);
                    listCounts.Add(1);
                }
                else listCounts[listWords.IndexOf(word)]++;

            var dicWords = listWords.Zip(listCounts, (k, v) => new { k, v })
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
                            if (sentence.Key.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' },
                            StringSplitOptions.RemoveEmptyEntries).Contains(word.Key))
                                file.Write("{0} ", sentence.Value);
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

        public void FindSentences(string readPath)
        {
            listWords = Reader.ReadWords(readPath);
            listSentences = Reader.ReadSentences(readPath);

            var distinctListWods = listWords.Distinct();
            listWords = distinctListWods.ToList();

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\Projects_epam\\LexerAnalyzer\\LexerAnalyzer\\Output.txt", true))
            {
                file.WriteLine();
                for (int i = 0; i < listWords.Count; i++)
                {
                    file.Write("{0}: ", listWords[i]);
                    for (int j = 0; j < listSentences.Count; j++)
                    {
                        if (listSentences[j].Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' },
                            StringSplitOptions.RemoveEmptyEntries).Contains(listWords[i]))
                        {
                            file.Write("{0} ", j + 1);
                        }
                    }
                    file.WriteLine();
                }
            }
        }

        public string Replace(string text, string newWord, int matchingWordLength)
        {
            return Regex.Replace(text, @"\b\w{" + matchingWordLength + @"}\b", newWord);
        }

    }
}
