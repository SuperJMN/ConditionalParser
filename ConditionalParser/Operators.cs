namespace ConditionalParser
{
    public class Operators
    {
        public static readonly Operator And = new Operator(OperatorKind.And, "&&", 1);
        public static readonly Operator Or = new Operator(OperatorKind.Or, "||", 1);
        public static readonly Operator Not = new Operator(OperatorKind.Not, "!", 2);
    }
}