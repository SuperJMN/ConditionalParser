using System.Collections.Generic;

namespace ConditionalParser
{
    public class Evaluator
    {
        public bool Evaluate(string expr, IDictionary<string, bool> values)
        {
            var visitor = new EvaluatorVisitor(values);
            var parser = new Parser(Tokenizer.Create());
            var ast = parser.Parse(expr);
            ast.Accept(visitor);
            return visitor.Result;
        }
    }
}