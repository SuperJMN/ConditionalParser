namespace SimpleParser
{
    public interface IExpression
    {
        void Accept(IExpressionVisitor visitor);
    }
}