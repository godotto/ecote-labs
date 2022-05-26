using Lexing;
using Parsing;
using Evalutaion;

namespace InputHandling;

public class InputHandler
{
    private string filePath;
    public StartInterpreter startInterpreter;
    private Lexer lexer = new Lexer();
    private Parser parser = new Parser();
    private Evaluator evaluator = new Evaluator();

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
                filePath = args[0];
                startInterpreter = FileMode;
            }
        }
        else
            throw new ArgumentException("Too many arguments. Provide only one file.");
    }

    private void InteractiveMode()
    {
        var line = "";

        while (true)
        {
            Console.Write(">>> "); // prompt
            line = Console.ReadLine() + '\n';

            if (line.ToLower() == "exit\n") // terminate interpreter on "exit" command
                break;

            try
            {
                lexer.Scan(line);
                List<IExpression> expressions = parser.Parse(lexer.Tokens);
                evaluator.Evaluate(expressions);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    private void FileMode()
    {
        var lines = System.IO.File.ReadAllLines(filePath);

        try
        {
            lexer.Scan(String.Join('\n', lines));
            List<IExpression> expressions = parser.Parse(lexer.Tokens);
            evaluator.Evaluate(expressions);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

public delegate void StartInterpreter();
