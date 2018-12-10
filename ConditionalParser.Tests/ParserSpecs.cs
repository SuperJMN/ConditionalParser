using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace ConditionalParser.Tests
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

    public class EvaluatorSpecs
    {
        [Fact]
        public void Test1()
        {
            Test("A", new[] { ("A", false) }, false);
        }

        [Fact]
        public void Test2()
        {
            Test("A", new[] { ("A", true) }, true);
        }

        [Fact]
        public void Test3()
        {
            Test("A AND B", new[] { ("A", true), ("B", true) }, true);
        }

        [Fact]
        public void Test4()
        {
            Test("A AND B", new[] { ("A", true), ("B", false) }, false);
        }

        [Fact]
        public void Test5()
        {
            Test("A AND B AND NOT C", new[] { ("A", true), ("B", true), ("C", false) }, true);
        }

        [Fact]
        public void Test6()
        {
            Test("A AND B AND NOT C", new[] { ("A", true), ("B", true), ("C", true) }, false);
        }

        [Fact]
        public void Test7()
        {
            Test("A OR B AND NOT C", new[] { ("A", false), ("B", false), ("C", true) }, false);
        }


        [Fact]
        public void Test8()
        {
            Test("A OR B AND NOT C", new[] { ("A", true), ("B", false), ("C", false) }, true);
        }

        [Fact]
        public void Test9()
        {
            Test("NOT A", new[] { ("A", false) }, true);
        }

        private void Test(string expr, IEnumerable<(string, bool)> values, bool expected)
        {
            var visitor = new EvaluatorVisitor(values.ToDictionary(x => x.Item1, y => y.Item2));
            var parser = new Parser(Tokenizer.Create());
            var ast = parser.Parse(expr);
            ast.Accept(visitor);

            visitor.Result.Should().Be(expected);
        }
    }
}
