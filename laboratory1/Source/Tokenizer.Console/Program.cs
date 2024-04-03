using System.Data.SqlTypes;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace Tokenizer.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var fs = File.Open("./Text/program3.txt", FileMode.Open, FileAccess.Read);

            var lexer = new LexicalAnalyzer(fs);

            var tokens = lexer.Process();

            foreach (var token in tokens)
            {
                System.Console.WriteLine(token);
            }

            var analyzer = new SyntaxAnalyzer(tokens);

            var node = analyzer.ReadStatement();
          
        }
    }
}
