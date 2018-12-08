using Superpower;
using Superpower.Parsers;
using Superpower.Tokenizers;

namespace ConditionalParser
{
    public class Tokenizer
    {
        public static Tokenizer<SimpleToken> Create()
        {
            var tokenizer = new TokenizerBuilder<SimpleToken>()
                .Ignore(Span.WhiteSpace)

                .Match(Character.EqualTo('('), SimpleToken.LeftParenthesis)
                .Match(Character.EqualTo(')'), SimpleToken.RightParenthesis)
                .Match(Span.EqualTo("AND"), SimpleToken.And)
                .Match(Span.EqualTo("OR"), SimpleToken.Or)
                .Match(Span.EqualTo("NOT"), SimpleToken.Not)

                .Match(Span.Regex(@"\w*"), SimpleToken.Identifier, true)
                .Build();

            return tokenizer;
        }
    }
}
