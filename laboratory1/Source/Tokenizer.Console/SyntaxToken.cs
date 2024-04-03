namespace Tokenizer.Console
{
    public struct SyntaxToken
    {
        public SyntaxTokenType TokenType;

        public int Index { get; set; }

        public int Position { get; set; }

        public override string ToString()
        {
            return $"Type: {this.TokenType}, Index: {Index}, Position: {Position}";
        }
    }
}
