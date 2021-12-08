using CodingTest.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;


namespace CodingTest.Commands
{
    /// <summary>
    /// Handles commands that give a shapeID follow by another input.
    /// Supported Input types: int, float, string, Vector2, Color.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class ShapeCommand<T> : CommandBase
    {
        private Action<string, T> m_shapeCommand;

        public ShapeCommand(string id, string description, string format, Action<string, T> command) : base(id, description, format)
        {
            m_shapeCommand = command;
        }

        public override void HandleCommand(List<string> args)
        {
            if (args.Count <= 1)
            {
                Console.WriteLine($"Not enough arguements given. Usage: {CommandID} <shapeID> <value>");
                return;
            }

            if (typeof(T) == typeof(int))
            {
                int value;
                bool parseSuccess = int.TryParse(args[1], out value);
                if (parseSuccess)
                {
                    (m_shapeCommand as Action<string,int>).Invoke(args[0], value);
                }
                else
                {
                    Console.WriteLine($"Incorrect type given, Usage: {CommandID} <shapeID> <int>");
                    return;
                }
            }
            else if (typeof(T) == typeof(float))
            {
                float value;
                bool parseSuccess = float.TryParse(args[1], out value);
                if (parseSuccess)
                {
                    (m_shapeCommand as Action<string, float>).Invoke(args[0], value);
                }
                else
                {
                    Console.WriteLine($"Incorrect type given, Usage: {CommandID} <shapeID> <float>");
                    return;
                }
            }
            else if (typeof(T) == typeof(string))
            {
                (m_shapeCommand as Action<string, string>).Invoke(args[0], args[1]);
            }
            else if (typeof(T) == typeof(Vector2))
            {
                if (args.Count <= 2)
                {
                    Console.WriteLine($"No arguements given. Usage: {CommandID} <shapeID> <xvalue> <yvalue>");
                    return;
                }

                float xvalue;
                bool parseSuccess = float.TryParse(args[1], out xvalue);
                if (!parseSuccess)
                {
                    Console.WriteLine($"Failed to parse xvalue arguement");
                    return;
                }

                float yvalue;
                parseSuccess = float.TryParse(args[2], out yvalue);
                if (!parseSuccess)
                {
                    Console.WriteLine($"Failed to parse yvalue arguement");
                    return;
                }

                (m_shapeCommand as Action<string, Vector2>).Invoke(args[0], new Vector2(xvalue, yvalue));
            }
            else if (typeof(T) == typeof(Color))
            {
                if (args.Count <= 3)
                {
                    Console.WriteLine($"No arguements given. Usage: {CommandID} <shapeID> <r> <g> <b>");
                    return;
                }

                int r;
                bool parseSuccess = int.TryParse(args[1], out r);
                if (!parseSuccess)
                {
                    Console.WriteLine($"Failed to parse r arguement");
                    return;
                }

                int g;
                parseSuccess = int.TryParse(args[2], out g);
                if (!parseSuccess)
                {
                    Console.WriteLine($"Failed to parse g arguement");
                    return;
                }

                int b;
                parseSuccess = int.TryParse(args[3], out b);
                if (!parseSuccess)
                {
                    Console.WriteLine($"Failed to parse b arguement");
                    return;
                }

                (m_shapeCommand as Action<string, Color>).Invoke(args[0], Color.FromArgb(r,g,b));
            }
        }
    }
}
