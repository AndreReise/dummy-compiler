using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer.Console
{
    public class SyntaxNode
    {

    }

    public class LocalVariableSyntaxNode : SyntaxNode
    {
        public string Type { get; set; }

        public string Name { get; set; }
    }

    public class DeclarationSyntaxNode : SyntaxNode
    {
        public object Variable { get; set; }

        public object Value { get; set; }
    }

    public class ConditionalSyntaxNode : SyntaxNode
    {
        public object ConditionBlock { get; set; }

        public object ThenBlock { get; set; }

        public object ElseBlock { get; set; }
    }

    public class UnaryOperatorSyntaxNode : SyntaxNode
    {
        public int Type { get; set; }

        public object Operand { get; set; }
    }

    public class BinaryOperatorSyntaxNode : SyntaxNode
    {
        public int Type { get; set; }
        public object Left { get; set; }

        public object Right { get; set; }
    }

    public class VariableSyntaxNode : SyntaxNode
    {
        public string VariableName { get; set; }
    }

    public class ConstantSyntaxNode : SyntaxNode
    {
        public string Value { get; set; }
    }

    public class CompoundSyntaxNode : SyntaxNode
    {
        public SyntaxNode[] InnerNodes { get; set; }
    }

    public class BooleanSyntaxNode : SyntaxNode
    {
        public SyntaxNode Condition { get; set; }
    }
}

