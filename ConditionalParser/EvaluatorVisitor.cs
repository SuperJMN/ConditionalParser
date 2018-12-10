using System;
using System.Collections.Generic;

namespace ConditionalParser
{
    public class EvaluatorVisitor : IExpressionVisitor
    {
        private bool current;

        public EvaluatorVisitor(IDictionary<string, bool> values)
        {
            Values = values;
        }

        public void Visit(ExpressionNode expressionNode)
        {
            var list = new List<bool>();
            foreach (var op in expressionNode.Operands)
            {
                op.Accept(this);
                list.Add(current);
            }

            switch (expressionNode.Operator.Kind)
            {
                case OperatorKind.And:
                    current = list[0] && list[1];
                    break;
                case OperatorKind.Or:
                    current = list[0] || list[1];
                    break;
                case OperatorKind.Not:
                    current = !list[0];
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }            
        }

        public void Visit(IdentifierNode identifierNode)
        {
            current = Values[identifierNode.Identifier];
        }        

        public IDictionary<string, bool> Values { get;  }
        public bool Result => current;
    }  
}