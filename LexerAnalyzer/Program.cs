using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexerAnalyzer.Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            string readPath = "D:\\Projects_epam\\LexerAnalyzer\\LexerAnalyzer\\Input.txt";
            string writePath = "D:\\Projects_epam\\LexerAnalyzer\\LexerAnalyzer\\Output.txt";


            Parser parser = new Parser();
            parser.Corcondance(readPath);
          //  parser.FindSentences(readPath);
            parser.ShowText(readPath);
            int lengthWord = 3;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(writePath, true))
            {
                file.WriteLine();
                file.WriteLine($"Length of words that should be replaced is: {lengthWord}");
                file.WriteLine(parser.Replace(Reader.Read(readPath), "ЗАМЕНА", lengthWord));
            }
        }
    }
}
