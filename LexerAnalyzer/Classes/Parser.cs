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
          //  for (int i = 0; i < listWords.Count; i++)
          //       Console.WriteLine("{0} - {1}", listWords[i], listCounts[i]);

            var dic = listWords.Zip(listCounts, (k, v) => new { k, v })
              .ToDictionary(x => x.k, x => x.v);
            /*
            foreach(var a in dic)
            {
                Console.WriteLine(a);
            }
            */
            return dic;
        }

        public void ShowWords(string readPath)
        {
            listWords = TextStream.ReadWords(readPath);
            foreach(var word in listWords)
                Console.WriteLine(word);
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
                    Console.WriteLine("{0}..........{1}: ", word.Key, word.Value);
                }
            }
        }

        public string Replace(string text, string newWord, int matchingWordLength)
        {
            return Regex.Replace(text, @"\b\w{" + matchingWordLength + @"}\b", newWord);
        }

    }
}
