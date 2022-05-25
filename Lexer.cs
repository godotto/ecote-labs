using System.Text;

namespace Lexing;

public class Lexer
{
    private char[] whitespaceCharacters; // collect all possible whitespace characters (except for new line character)
    private int currentLine = 1;    // track current line in order to provide location of tokens
    private int currentCharacter;
    private string loadedSource;
    public List<Token> Tokens { get; }

    public Lexer()
    {
        Tokens = new List<Token>();
        whitespaceCharacters = new char[] { ' ', '\t' };
    }

    public void Scan(string sourceCode)
    {
        loadedSource = sourceCode;

        for (currentCharacter = 1; currentCharacter <= loadedSource.Length; currentCharacter++)
        {
            var character = CurrentCharacter();

            switch (character)
            {
                // simple cases - single character tokens
                case '+':
                    Tokens.Add(new Token(TokenType.Plus, character.ToString(), currentLine));
                    break;
                case '-':
                    Tokens.Add(new Token(TokenType.Minus, character.ToString(), currentLine));
                    break;
                case '*':
                    Tokens.Add(new Token(TokenType.Asterisk, character.ToString(), currentLine));
                    break;
                case '/':
                    Tokens.Add(new Token(TokenType.Slash, character.ToString(), currentLine));
                    break;
                case '(':
                    Tokens.Add(new Token(TokenType.LeftBracket, character.ToString(), currentLine));
                    break;
                case ')':
                    Tokens.Add(new Token(TokenType.RightBracket, character.ToString(), currentLine));
                    break;
                case '=':
                    Tokens.Add(new Token(TokenType.Equal, character.ToString(), currentLine));
                    break;
                case '\n':
                    Tokens.Add(new Token(TokenType.NewLine, character.ToString(), currentLine));
                    currentLine++;
                    break;
                default:
                    if (char.IsLetter(character))
                    {
                        ScanIdentifier();   // identifiers of variables
                        break;
                    }
                    else if (char.IsDigit(character))
                    {
                        ScanTimeLiteral();  // numerical literals with time units
                        break;
                    }
                    else if (IsWhitespace(character))
                        break;

                    throw new UnexpectedCharacter(character, currentLine);
            }
        }
    }

    private void ScanIdentifier()
    {
        var consumedCharacters = new StringBuilder();

        for (; currentCharacter <= loadedSource.Length; currentCharacter++)
        {
            consumedCharacters.Append(CurrentCharacter());

            if (!char.IsLetterOrDigit(NextCharacter())) // unlike in the beginning of the token allow for digits inside
                break;
        }

        Tokens.Add(new Token(TokenType.Identifier, consumedCharacters.ToString(), currentLine));
    }

    private void ScanTimeLiteral()
    {
        var consumedCharacters = new StringBuilder();
        bool isDecimalPointEncountered = false; // keep track of decimal point (there can be only 0 or 1 of them)

        for (; currentCharacter <= loadedSource.Length; currentCharacter++)
        {
            if (CurrentCharacter() == '.')
            {
                if (isDecimalPointEncountered)
                    throw new UnexpectedCharacter(CurrentCharacter(), currentLine, $"Unexpected additional decimal point at line {currentLine}");

                isDecimalPointEncountered = true;
            }

            consumedCharacters.Append(CurrentCharacter());

            if (!char.IsDigit(NextCharacter()) && (NextCharacter() != '.'))
                break;
        }

        // manage the time unit at the end of literal
        if (NextCharacter() == 's' || NextCharacter() == 'h')
        {
            currentCharacter++;
            consumedCharacters.Append(CurrentCharacter());
        }
        else if (loadedSource.Substring(currentCharacter, 3) == "min")
        {
            consumedCharacters.Append(loadedSource.Substring(currentCharacter, 3));
            currentCharacter += 3;
        }
        else
            throw new UnexpectedCharacter(NextCharacter(), currentLine, $"Unexpected wrong time suffix character at line {currentLine}");

        Tokens.Add(new Token(TokenType.Time, consumedCharacters.ToString(), currentLine));
    }

    // --------------- auxiliary methods -------------------
    private bool IsAtEnd()
    {
        return loadedSource.Length <= currentCharacter;
    }

    private char CurrentCharacter()
    {
        return loadedSource[currentCharacter - 1];
    }

    private char NextCharacter()
    {
        if (!IsAtEnd())
            return loadedSource[currentCharacter];
        else
            return '\0';
    }

    private bool IsWhitespace(char character)
    {
        return Array.Exists(whitespaceCharacters, element => element == character);
    }
}
