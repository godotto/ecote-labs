using Lexing;

namespace Parsing;

public interface IExpression
{
}

public class Binary : IExpression
{
    private IExpression left;
    private Token binaryOperator;
    private IExpression right;

    public Binary(IExpression left, Token binaryOperator, IExpression right)
    {
        this.left = left;
        this.binaryOperator = binaryOperator;
        this.right = right;
    }
}

public class Assignment : IExpression
{
    private Token identifier;
    private IExpression value;

    public Assignment(Token identifier, IExpression value)
    {
        this.identifier = identifier;
        this.value = value;
    }
}

public class Grouping : IExpression
{
    private IExpression expression;

    public Grouping(IExpression expression)
    {
        this.expression = expression;
    }
}

public class Literal : IExpression
{
    private Token value;

    public Literal(Token value)
    {
        this.value = value;
    }
}
