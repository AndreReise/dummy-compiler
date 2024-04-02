namespace CocoSample.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var fs = File.Open("./Text/test.fakec", FileMode.Open, FileAccess.Read);

            var scanner = new Scanner(fs);
            var parser = new Parser(scanner);

            parser.Parse();

            System.Console.WriteLine("{0} errors detected", parser.errors.count);
        }
    }
}
