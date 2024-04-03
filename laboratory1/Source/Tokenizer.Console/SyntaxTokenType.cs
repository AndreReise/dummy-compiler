using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer.Console
{
    public enum SyntaxTokenType
    {
        Identifier,
        Number,
        String,
        If,
        Else,
        Id,
        PO, // par open
        PC, // par close,
        Semicolon,
        CO, // curly open
        CC, // curly close,
        BO,
        BC,
        Equal,
        Assignment,
        Increment,
        Decrement,
        Multiplication,
        Division,
        AssigmentAdd,
        AssigmentSubtract,
        AssigmentMultiplication,
        AssigmentDivision,
        LogicalAnd,
        LogicalOr,
        BitwiseAnd,
        BitwiseOr,
        Addition,
        Subtraction,
        More,
        MoreOrEqual,
        Less,
        LessOrEqual,
    }
}
