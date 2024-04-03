using System.Data.SqlTypes;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace Tokenizer.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var fs = File.Open("./Text/program2.txt", FileMode.Open, FileAccess.Read);

            var analyzer = new LexicalAnalyzer(fs);

            var tokens = analyzer.Process();

            foreach (var token in tokens)
            {
                System.Console.WriteLine(token);
            }
        }
    }
}
