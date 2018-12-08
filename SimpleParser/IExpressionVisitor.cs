namespace SimpleParser
{
    public interface IExpressionVisitor
    {
        void Visit(ExpressionNode expressionNode);
        void Visit(IdentifierNode identifierNode);
    }
}