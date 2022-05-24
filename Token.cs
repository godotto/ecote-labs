namespace Lexing;

public enum TokenType
{
    Plus, Minus, Equal, Asterisk, Slash, LeftBracket, RightBracket,
    Time, Identifier,
    NewLine, Eof
}

public class Token
{
    public TokenType type;
    public string lexeme;
    public int location;

    public Token(TokenType type, string lexeme, int location)
    {
        this.type = type;
        this.lexeme = lexeme;
        this.location = location;
    }
}
