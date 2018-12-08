using Superpower;
using Superpower.Parsers;

namespace SimpleParser
{
    public static class ParserMixin
    {
        public static TokenListParser<SimpleToken, T> BetweenParenthesis<T>(this TokenListParser<SimpleToken, T> self)
        {
            return self.Between(Token.EqualTo(SimpleToken.LeftParenthesis), Token.EqualTo(SimpleToken.RightParenthesis));
        }      
    }
}