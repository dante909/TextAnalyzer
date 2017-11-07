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

            Parser parser = new Parser();
            parser.ToGroup(parser.Corcondance(readPath));
            Console.WriteLine();

            int lengthWord = 3;
            Console.WriteLine($"Length of word that should be replaced is: {lengthWord}");
            Console.WriteLine(parser.Replace(TextStream.Read(readPath), "ЗАМЕНА", lengthWord));
        }
    }
}
