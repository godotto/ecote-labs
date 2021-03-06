using Lexing;

namespace Evalutaion;

public class Environment
{
    private Dictionary<String, Time> values = new Dictionary<string, Time>();

    public void AssignVariable(string identifier, Time value)
    {
        if (!values.ContainsKey(identifier))
        {
            values.Add(identifier, value);
        }
        else
            values[identifier] = value;

        Console.Write(identifier + " = ");
    }

    public Time GetValue(Token identifier)
    {
        if (values.ContainsKey(identifier.lexeme))
            return values[identifier.lexeme];
        throw new UndefinedVariable(identifier.location, identifier.lexeme);
    }
}