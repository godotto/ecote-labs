using System.IO;

namespace input_handler
{
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

        }

        private void FileMode()
        {

        }
    }

    public delegate void StartInterpreter();
}