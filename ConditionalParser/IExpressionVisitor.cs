namespace ConditionalParser
{
    public interface IExpressionVisitor
    {
        void Visit(ExpressionNode expressionNode);
        void Visit(IdentifierNode identifierNode);
    }
}