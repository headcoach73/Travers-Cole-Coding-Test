using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest.Commands
{
    static class CommandManager
    {
        private static List<CommandBase> m_commands = new List<CommandBase>();

        public static List<CommandBase> Commands { get => m_commands; set => m_commands = value; }

        private static ConcurrentQueue<string> m_inputQueue = new ConcurrentQueue<string>();

        /// <summary>
        /// Adds command profile commands and starts reading command inputs.
        /// Use HandleCommandQueue() to processes commands.
        /// </summary>
        /// <param name="commandProfile"></param>
        public static void Initialize(CommandProfile commandProfile)
        {
            m_commands.AddRange(commandProfile.LoadCommands());
            ReadInputsAsync();
        }

        private static async void ReadInputsAsync()
        {
            Console.WriteLine("Type 'help' for a list of commands.");
            Console.Write("-> ");
            string input = "";

            while (true)
            {
                await Task.Run(() => input = Console.ReadLine());
                m_inputQueue.Enqueue(input);

                while (!m_inputQueue.IsEmpty)
                {
                    //Wait for queue to empty
                }

                Console.Write("-> ");
            }
        }

        /// <summary>
        /// Handles all queued inputs
        /// </summary>
        public static void HandleCommandQueue()
        {
            string input;

            if (m_inputQueue.IsEmpty) return; //If nothing to processes then dequeue

            while (m_inputQueue.TryPeek(out input))
            {
                //Proccess before dequeueing 
                HandleCommandInput(input);
                m_inputQueue.TryDequeue(out input);
            }
            
        }

        /// <summary>
        /// Shows all available commands.
        /// </summary>
        public static void ShowCommands()
        {
            foreach (var command in Commands)
            {
                Console.WriteLine($"(): {command.CommandFormat} - {command.CommandDescription}");
            }
        }

        private static void HandleCommandInput(string input)
        {
            string[] commandProperties = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (commandProperties.Length == 0) return;

            string commandId = commandProperties[0].ToLower();

            List<string> commandArgs = new List<string>(commandProperties);
            if (commandArgs.Count > 0) 
            {
                commandArgs.RemoveAt(0); //Remove command id
                CommandBase command = Commands.Find(x => x.CommandID == commandId);
                command?.HandleCommand(commandArgs);
            } 
        }
    }
}
