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

namespace Parsing
{
    public class SyntaxError : Exception
    {
        public int Line { get; }

        public SyntaxError()
            : base()
        { }

        public SyntaxError(int line)
            : base($"Syntax error at line {line}.")
        {
            Line = line;
        }

        public SyntaxError(int line, string message)
            : base($"Syntax error at line {line}. {message}")
        {
            Line = line;
        }

        public SyntaxError(int line, Exception innerException)
            : base($"Syntax error at line {line}.", innerException)
        {
            Line = line;
        }

        public SyntaxError(int line, string message, Exception innerException)
            : base($"Syntax error at line {line}. {message}", innerException)
        {
            Line = line;
        }
    }
}

namespace Evalutaion
{
    public class UndefinedVariable : Exception
    {
        public int Line { get; }
        public string Identifier { get; }

        public UndefinedVariable()
            : base()
        { }

        public UndefinedVariable(int line, string identifier)
            : base($"Runtime error at {line}. Undefined variable {identifier}.")
        {
            Line = line;
            Identifier = identifier;
        }

        public UndefinedVariable(int line, string identifier, string message)
            : base($"Runtime error at line {line}. {message}")
        {
            Line = line;
            Identifier = identifier;
        }

        public UndefinedVariable(int line, string identifier, Exception innerException)
            : base($"Runtime error at {line}. Undefined variable {identifier}.", innerException)
        {
            Line = line;
            Identifier = identifier;
        }

        public UndefinedVariable(int line, string identifier, string message, Exception innerException)
            : base($"Runtime error at line {line}. {message}", innerException)
        {
            Line = line;
            Identifier = identifier;
        }
    }
}
