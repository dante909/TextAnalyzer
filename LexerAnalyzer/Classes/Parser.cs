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

        public Dictionary<string,int> Corcondance(string readPath)
        {
            List<int> listCounts = new List<int>();
            listWords = TextStream.ReadWords(readPath);
            string[] arrayWords = listWords.ToArray();
            listWords.Clear();

            foreach (string word in arrayWords)
                if (!listWords.Contains(word))
                {
                    listWords.Add(word);
                    listCounts.Add(1);
                }
                else listCounts[listWords.IndexOf(word)]++;

            var dic = listWords.Zip(listCounts, (k, v) => new { k, v })
              .ToDictionary(x => x.k, x => x.v);

            return dic;
        }

        public void ShowText(string readPath)
        {
            Console.WriteLine(TextStream.Read(readPath));
        }

        public void ToGroup(Dictionary<string, int> dic)
        {
            var wordQuery = from word in dic
                            group word by word.Key.ToLowerInvariant().ElementAt(0) into wordGroup
                            orderby wordGroup.Key
                            select wordGroup;

            foreach (var groupOfWords in wordQuery)
            {
                Console.Write("\n{0}\n",char.ToUpper(groupOfWords.Key));
                foreach (var word in groupOfWords)
                {
                    Console.WriteLine("{0}...............{1}", word.Key, word.Value);
                }
            }
        }

        public void FindSentences(string readPath)
        {
            listWords = TextStream.ReadWords(readPath);
            listSentences = TextStream.ReadSentences(readPath);

            var distinctListWods = listWords.Distinct();
            listWords = distinctListWods.ToList();

            for (int i = 0; i < listWords.Count; i++)
            {
                Console.Write("{0}: ", listWords[i]);
                for(int j = 0; j < listSentences.Count; j++)
                {
                    if (listSentences[j].Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' },
                        StringSplitOptions.RemoveEmptyEntries).Contains(listWords[i]))
                    {
                        Console.Write("{0} ", j+1);
                    }             
                }
                Console.WriteLine();
            }
        }

        public string Replace(string text, string newWord, int matchingWordLength)
        {
            return Regex.Replace(text, @"\b\w{" + matchingWordLength + @"}\b", newWord);
        }

    }
}
