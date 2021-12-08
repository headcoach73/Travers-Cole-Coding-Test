using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CodingTest.Commands
{
    abstract class CommandBase
    {
        public string CommandID { get => m_commandID; }
        public string CommandDescription { get => m_commandDescription; }
        public string CommandFormat { get => m_commandFormat; }

        private string m_commandID;
        private string m_commandDescription;
        private string m_commandFormat;

        public CommandBase(string id, string description, string format)
        {
            m_commandID = id;
            m_commandDescription = description;
            m_commandFormat = format;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">
        /// List of arguements given after the command ID
        /// </param>
        public abstract void HandleCommand(List<string> args);
    }

    /// <summary>
    /// Handles commands that require not input
    /// </summary>
    class Command : CommandBase
    {
        private Action m_command;

        public Command(string id, string description, string format, Action command) : base(id, description, format)
        {
            m_command = command;
        }

        public override void HandleCommand(List<string> args)
        {
            m_command.Invoke();
        }
    }

    /// <summary>
    /// Handles commands that take a single input.
    /// Supported types: int, float, string, Vector2
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Command<T> : CommandBase
    {
        private Action<T> m_command;

        public Command(string id, string description, string format) : base(id, description, format)
        {
        }

        public Command(string id, string description, string format, Action<T> command) : base(id, description, format)
        {
            m_command = command;
        }

        public override void HandleCommand(List<string> args)
        {
            if (args.Count <= 0)
            {
                Console.WriteLine($"No arguements given. Usage: {CommandID} <value>");
                return;
            }

            if (typeof(T) == typeof(int))
            {
                int value;
                bool parseSuccess = int.TryParse(args[0], out value);
                if (parseSuccess)
                {
                    (m_command as Action<int>).Invoke(value);
                }
                else
                {
                    Console.WriteLine($"Incorrect type given, Usage: {CommandID} <int>");
                    return;
                }
            }
            else if (typeof(T) == typeof(float))
            {
                float value;
                bool parseSuccess = float.TryParse(args[0], out value);
                if (parseSuccess)
                {
                    (m_command as Action<float>).Invoke(value);
                }
                else
                {
                    Console.WriteLine($"Incorrect type given, Usage: {CommandID} <float>");
                    return;
                }
            }
            else if (typeof(T) == typeof(string))
            {
                (m_command as Action<string>).Invoke(args[0]);
            }
            else if (typeof(T) == typeof(Vector2))
            {
                if (args.Count <= 1)
                {
                    Console.WriteLine($"No arguements given. Usage: {CommandID} <xvalue> <yvalue>");
                    return;
                }

                float xvalue;
                bool parseSuccess = float.TryParse(args[0], out xvalue);
                if (!parseSuccess)
                {
                    Console.WriteLine($"Failed to parse xvalue arguement");
                    return;
                }

                float yvalue;
                parseSuccess = float.TryParse(args[1], out yvalue);
                if (!parseSuccess)
                {
                    Console.WriteLine($"Failed to parse yvalue arguement");
                    return;
                }

                (m_command as Action<Vector2>).Invoke(new Vector2(xvalue, yvalue));
            }
        }
    }

}
