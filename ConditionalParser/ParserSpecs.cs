using FluentAssertions;
using SimpleParser;
using Xunit;

namespace ConditionalParser
{
    public class ParserSpecs
    {
        [Fact]
        public void BinarySimpleExpression()
        {
            AssertParse("A AND B", "A && B");
        }

        [Fact]
        public void Complex()
        {
            AssertParse("A AND B OR C", "A && B || C");
        }

        [Fact]
        public void SimpleWithNegation()
        {
            AssertParse("A AND NOT B", "A && !B");
        }

        [Fact]
        public void Not()
        {
            AssertParse("NOT A", "!A");
        }

        [Fact]
        public void Parentheses()
        {
            AssertParse("NOT (A OR B)", "!(A || B)");
        }

        private static void AssertParse(string input, string expected)
        {
            var ast = new Parser(Tokenizer.Create()).Parse(input);
            var formatter = new FormattingVisitor();
            ast.Accept(formatter);
            formatter.GetString().Should().Be(expected);
        }
    }  
}
