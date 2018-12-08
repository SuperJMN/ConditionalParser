namespace ConditionalParser
{
    public abstract class Expression : IExpression
    {
        public abstract void Accept(IExpressionVisitor visitor);
    }
}