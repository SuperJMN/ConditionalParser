using System;
using System.Text;

namespace ConditionalParser
{
    public class FormattingVisitor : IExpressionVisitor
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

        public string GetString()
        {
            return stringBuilder.ToString();
        }

        public void Visit(ExpressionNode expressionNode)
        {
            switch (expressionNode.Operands.Length)
            {
                case 1:
                    VisitUnary(expressionNode);
                    break;
                case 2:
                    VisitBinary(expressionNode);
                    break;
                default:
                    throw new InvalidOperationException();

            }
        }

        private void VisitBinary(ExpressionNode expressionNode)
        {
            var left = expressionNode.Operands[0];
            var right = expressionNode.Operands[1];

            VisitAndEncloseIfNecessary(left, expressionNode.Operator);
            stringBuilder.AppendFormat(" {0} ", expressionNode.Operator.Symbol);
            VisitAndEncloseIfNecessary(right, expressionNode.Operator);
        }

        private void VisitAndEncloseIfNecessary(IExpression child, Operator parentOperator)
        {
            if (child is ExpressionNode on && on.Operator.Precedence < parentOperator.Precedence)
            {
                stringBuilder.Append("(");
                child.Accept(this);
                stringBuilder.Append(")");
            }
            else
            {
                child.Accept(this);
            }
        }

        private void VisitUnary(ExpressionNode expressionNode)
        {
            var operand = expressionNode.Operands[0];
            stringBuilder.Append(expressionNode.Operator.Symbol);
            VisitAndEncloseIfNecessary(operand, expressionNode.Operator);
        }

        public void Visit(IdentifierNode identifierNode)
        {
            stringBuilder.Append(identifierNode.Identifier);
        }
    }
}