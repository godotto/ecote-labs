using Lexing;

namespace Parsing;

public interface IExpression
{
}

public class Binary : IExpression
{
    public IExpression Left { get; }
    public Token BinaryOperator { get; }
    public IExpression Right { get; }

    public Binary(IExpression left, Token binaryOperator, IExpression right)
    {
        this.Left = left;
        this.BinaryOperator = binaryOperator;
        this.Right = right;
    }
}

public class Assignment : IExpression
{
    public Token Identifier { get; }
    public IExpression Value { get; }

    public Assignment(Token identifier, IExpression value)
    {
        this.Identifier = identifier;
        this.Value = value;
    }
}

public class Grouping : IExpression
{
    public IExpression Expression { get; }

    public Grouping(IExpression expression)
    {
        this.Expression = expression;
    }
}

public class Literal : IExpression
{
    public Token Value { get; }

    public Literal(Token value)
    {
        this.Value = value;
    }
}
