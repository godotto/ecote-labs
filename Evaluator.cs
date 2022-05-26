using Lexing;
using Parsing;

namespace Evalutaion;

public class Evaluator
{
    private const string second = "s";
    private const string minute = "min";
    private const string hour = "h";
    private Environment environment = new Environment();

    public void Evaluate(List<IExpression> expressions)
    {
        foreach (var expression in expressions)
        {
            Console.WriteLine(Evaluate(expression).ToString());
        }
    }

    private Time Evaluate(IExpression expression)
    {
        if (expression is Binary)
            return EvaluateBinary(expression as Binary);
        else if (expression is Grouping)
            return EvaluateGrouping(expression as Grouping);
        else if (expression is Literal)
            return EvaluateLiteral(expression as Literal);
        else if (expression is Variable)
            return EvaluateVariable(expression as Variable);
        else if (expression is Assignment)
            return EvaluateAssignment(expression as Assignment);

        throw new Exception("error");
    }

    private Time EvaluateAssignment(Assignment expression)
    {
        var value = Evaluate(expression.Value);
        environment.AssignVariable(expression.Identifier.lexeme, value);
        return value;
    }

    private Time EvaluateBinary(Binary expression)
    {
        var left = Evaluate(expression.Left);
        var right = Evaluate(expression.Right);

        switch (expression.BinaryOperator.type)
        {
            case TokenType.Plus:
                return EvaluateArithmetic(left, right, Add);
            case TokenType.Minus:
                return EvaluateArithmetic(left, right, Substract);
            case TokenType.Asterisk:
                return EvaluateArithmetic(left, right, Multiply);
            case TokenType.Slash:
                return EvaluateArithmetic(left, right, Divide);
        }

        throw new Exception("error");
    }

    private Time EvaluateGrouping(Grouping expression)
    {
        return Evaluate(expression.Expression);
    }

    private Time EvaluateLiteral(Literal expression)
    {
        return new Time(expression.Value.lexeme);
    }

    private Time EvaluateVariable(Variable variable)
    {
        return environment.GetValue(variable.Identifier);
    }

    private float Add(float left, float right) { return left + right; }
    private float Substract(float left, float right) { return left - right; }
    private float Multiply(float left, float right) { return left * right; }
    private float Divide(float left, float right) { return left / right; }

    private Time EvaluateArithmetic(Time left, Time right, ArithmeticOperation arthOp)
    {
        if (left.Unit == second)
        {
            if (right.Unit == second)
                return new Time(arthOp(left.Value, right.Value), second);
            else if (right.Unit == minute)
                return new Time(arthOp(left.Value, right.Value * 60), second);
            else
                return new Time(arthOp(left.Value, right.Value * 3600), second);
        }
        else if (left.Unit == minute)
        {
            if (right.Unit == minute)
                return new Time(arthOp(left.Value, right.Value), minute);
            else if (right.Unit == second)
                return new Time(arthOp(left.Value * 60, right.Value), second);
            else
                return new Time(arthOp(left.Value, right.Value * 60), minute);
        }
        else
        {
            if (right.Unit == hour)
                return new Time(arthOp(left.Value, right.Value), hour);
            else if (right.Unit == second)
                return new Time(arthOp(left.Value * 3600, right.Value), second);
            else
                return new Time(arthOp(left.Value * 60, right.Value), minute);
        }
    }
}

public delegate float ArithmeticOperation(float left, float right);
