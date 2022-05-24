using Lexing;

namespace InputHandling;

public class InputHandler
{
    private string filename;
    public StartInterpreter startInterpreter;

    public InputHandler(string[] args)
    {
        // if no arguments with filename provided run interactive mode
        // else start file mode (unless there is invalid number of arguments)
        if (args.Length == 0)
            startInterpreter = InteractiveMode;
        else if (args.Length == 1)
        {
            if (!File.Exists(args[0]))
                throw new FileNotFoundException("File not found", args[0]);
            else
            {
                filename = args[0];
                startInterpreter = FileMode;
            }
        }
        else
            throw new ArgumentException("Too many arguments. Provide only one file.");
    }

    private void InteractiveMode()
    {
        var lexer = new Lexer();
        var line = Console.ReadLine();
        // var line = "12a + b";

        try
        {
            lexer.Scan(line);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        foreach (var token in lexer.Tokens)
        {
            Console.WriteLine(token.lexeme);
        }
    }

    private void FileMode()
    {

    }
}

public delegate void StartInterpreter();
