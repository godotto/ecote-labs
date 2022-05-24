namespace Lexing
{
    public class UnexpectedCharacter : Exception
    {
        public char Character { get; }
        public int Line { get; }

        public UnexpectedCharacter()
            : base()
        { }

        public UnexpectedCharacter(char character, int line)
            : base($"Unexpected character '{character}' at line {line}.")
        {
            Character = character;
            Line = line;
        }

        public UnexpectedCharacter(char character, int line, string message)
            : base(message)
        {
            Character = character;
            Line = line;
        }

        public UnexpectedCharacter(char character, int line, Exception innerException)
            : base($"Unexpected character '{character}' at line {line}.", innerException)
        {
            Character = character;
            Line = line;
        }

        public UnexpectedCharacter(char character, int line, string message, Exception innerException)
            : base(message, innerException)
        {
            Character = character;
            Line = line;
        }
    }
}