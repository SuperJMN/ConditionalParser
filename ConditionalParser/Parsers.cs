using Superpower;
using Superpower.Parsers;

namespace ConditionalParser
{
    public static class Parsers
    {
        private static readonly TokenListParser<SimpleToken, Expression> Identifier = Token.EqualTo(SimpleToken.Identifier).Select(token => (Expression)new IdentifierNode(token.ToStringValue()));

        private static readonly TokenListParser<SimpleToken, Operator> Add = Token.EqualTo(SimpleToken.And).Value(Operators.And);
        private static readonly TokenListParser<SimpleToken, Operator> Or = Token.EqualTo(SimpleToken.Or).Value(Operators.Or);
        private static readonly TokenListParser<SimpleToken, Operator> Not = Token.EqualTo(SimpleToken.Not).Value(Operators.Not);

        private static readonly TokenListParser<SimpleToken, Operator> Operator = Add.Or(Or).Or(Not);

        private static readonly TokenListParser<SimpleToken, Expression> Item = Identifier;

        private static readonly TokenListParser<SimpleToken, Expression> Unary = 
            from unary in Not
            from p in Factor
            select (Expression)new ExpressionNode(unary, p);

        private static readonly TokenListParser<SimpleToken, Expression> Factor =
            Parse.Ref(() => Expression.BetweenParenthesis())
                .Or(Item);

        private static readonly TokenListParser<SimpleToken, Expression> Operand =
            Unary.Or(Factor);
        
        public static readonly TokenListParser<SimpleToken, Expression> Expression = Parse.Chain(Operator, Operand, MakeBinary);

        private static Expression MakeBinary(Operator op, Expression left, Expression right)
        {
            return new ExpressionNode(op, left, right);
        }
    }
}