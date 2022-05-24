using System.Text;

namespace Lexing;

public class Lexer
{
    private char[] whitespaceCharacters;
    private int currentLine = 1;
    private int currentCharacter = 0;
    public List<Token> Tokens { get; }

    public Lexer()
    {
        Tokens = new List<Token>();
        whitespaceCharacters = new char[] { ' ', '\t' };
    }

    public void Scan(string sourceCode)
    {
        var consumedCharacters = new StringBuilder();

        foreach (var character in sourceCode)
        {
            currentCharacter++;

            if (!Array.Exists(whitespaceCharacters, element => element == character))
            {
                consumedCharacters.Append(character);

                if (currentCharacter != sourceCode.Length)
                    continue;
            }

            ScanToken(consumedCharacters);
            consumedCharacters.Clear();
        }
    }

    private void ScanToken(StringBuilder consumedCharacters)
    {
        try
        {
            if (consumedCharacters.Length == 1)
                ScanSingleCharacterToken(consumedCharacters.ToString());
        }
        catch (UnexpectedCharacter e)
        {
            Console.WriteLine($"Lexer error for \"{e.Character}\" lexemme.");
            throw;
        }
    }

    private void ScanSingleCharacterToken(string character)
    {
        switch (character)
        {
            case "+":
                Tokens.Add(new Token(TokenType.Plus, character, currentLine));
                break;
            case "-":
                Tokens.Add(new Token(TokenType.Minus, character, currentLine));
                break;
            case "*":
                Tokens.Add(new Token(TokenType.Asterisk, character, currentLine));
                break;
            case "/":
                Tokens.Add(new Token(TokenType.Slash, character, currentLine));
                break;
            case "(":
                Tokens.Add(new Token(TokenType.LeftBracket, character, currentLine));
                break;
            case ")":
                Tokens.Add(new Token(TokenType.RightBracket, character, currentLine));
                break;
            case "=":
                Tokens.Add(new Token(TokenType.Equal, character, currentLine));
                break;
            case "\n":
                Tokens.Add(new Token(TokenType.NewLine, character, currentLine));
                currentLine++;
                break;
            default:
                throw new UnexpectedCharacter(character, currentLine);
        }
    }
}
