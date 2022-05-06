using input_handler;

try
{
    var inputHandler = new InputHandler(args);
    inputHandler.startInterpreter();
}
catch (FileNotFoundException e)
{
    Console.WriteLine($"{e.FileName}: {e.Message}");
}
catch (ArgumentException e)
{
    Console.WriteLine(e.Message);
}
