namespace ConditionalParser
{
    public interface IExpression
    {
        void Accept(IExpressionVisitor visitor);
    }
}