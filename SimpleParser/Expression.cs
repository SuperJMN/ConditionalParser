namespace SimpleParser
{
    public abstract class Expression : IExpression
    {
        public abstract void Accept(IExpressionVisitor visitor);
    }
}