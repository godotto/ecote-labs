namespace Lexing;

public enum TokenType
{
    // single characters
    Plus, Minus, Equal, Asterisk, Slash, LeftBracket, RightBracket,
    // literals and variables
    Time, Identifier,
    // special whitespace characters
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
