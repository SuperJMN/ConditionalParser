using Superpower;

namespace ConditionalParser
{
    public class Parser
    {
        private readonly Tokenizer<SimpleToken> tokenizer;

        public Parser(Tokenizer<SimpleToken> tokenizer)
        {
            this.tokenizer = tokenizer;
        }

        public Expression Parse(string input)
        {
            var tokenList = tokenizer.Tokenize(input);
           return Parsers.Expression.Parse(tokenList);
        }
    }
}