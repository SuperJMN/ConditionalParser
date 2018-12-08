namespace SimpleParser
{
    public class ExpressionNode : Expression
    {
        public Operator Operator { get; }
        public Expression[] Operands { get; }
        
        public ExpressionNode(Operator op, params Expression[] operands)
        {
            Operator = op;
            Operands = operands;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}