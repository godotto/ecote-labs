using Lexing;

namespace Parsing;

public class Parser
{
    private List<Token> tokens;
    private int currentToken;

    public List<IExpression> Parse(List<Token> tokens)
    {
        this.tokens = tokens;
        currentToken = 0;
        var statements = new List<IExpression>();

        while (!IsAtEnd())
        {
            statements.Add(Statement());
            currentToken++;
        }

        return statements;
    }

    private IExpression Statement()
    {
        var expression = Expression();

        if (CheckTokenType(NextToken(), TokenType.NewLine))
            currentToken++;
        return expression;
    }

    private IExpression Expression()
    {
        return Term();
    }

    // private IExpression Assignment()
    // {

    // }

    private IExpression Term()
    {
        var expression = Factor();

        if (CheckTokenType(NextToken(), TokenType.Plus) || CheckTokenType(NextToken(), TokenType.Minus))
        {
            currentToken++;
            var binaryOperator = CurrentToken();

            currentToken++;
            var rightExpression = Factor();

            expression = new Binary(expression, binaryOperator, rightExpression);
        }

        return expression;
    }

    private IExpression Factor()
    {
        var expression = Primary();

        if (CheckTokenType(NextToken(), TokenType.Asterisk) || CheckTokenType(NextToken(), TokenType.Slash))
        {
            currentToken++;
            var binaryOperator = CurrentToken();

            currentToken++;
            var rightExpression = Primary();

            expression = new Binary(expression, binaryOperator, rightExpression);
        }

        return expression;
    }

    private IExpression Primary()
    {
        if (CheckTokenType(CurrentToken(), TokenType.Time))
            return new Literal(CurrentToken());
        else if (CheckTokenType(CurrentToken(), TokenType.LeftBracket))
        {
            currentToken++;
            var expression = Expression();

            currentToken++;
            if (CheckTokenType(CurrentToken(), TokenType.RightBracket))
                return new Grouping(expression);
            else
                throw new SyntaxError(CurrentToken().location, "Expected closing bracket.");
        }
        else
            throw new SyntaxError(CurrentToken().location);
    }

    // -------------- auxiliary methods -------------------
    private bool IsAtEnd()
    {
        return CurrentToken().type == TokenType.Eof;
    }

    private Token CurrentToken()
    {
        return tokens[currentToken];
    }

    private Token NextToken()
    {
        if (!IsAtEnd())
            return tokens[currentToken + 1];
        else
            return new Token(TokenType.Eof, "", 0);
    }

    private bool CheckTokenType(Token token, TokenType type)
    {
        if (IsAtEnd())
            return false;

        return token.type == type;
    }
}
