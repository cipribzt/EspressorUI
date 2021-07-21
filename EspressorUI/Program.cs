using System;
using Espressorlibrary;

namespace EspressorUI
{
    
    class Program
    {
        const string END_MESSAGE = "FINISH";
        const string MESSAGE = "Write " + END_MESSAGE + " to END programm. Available commands: ";
        const string REQUEST_COMMAND = "Enter your command: ";
        static void Main(string[] args)
        {
            Espressor espressor = new Espressor();
            string commandFromLine;            
            State currentState = State.Initial_state;

            while (true)
            {
                Console.WriteLine("----------------------------------------\n");
                Console.WriteLine("----------------------------------------\n");
                Console.WriteLine("----------------------------------------\n");
                Console.WriteLine(espressor);
                Console.WriteLine("----------------------------------------\n");
                Console.WriteLine(MESSAGE);
                espressor.PrintAvailableAction(currentState);
                Console.WriteLine("\n");
                Console.Write(REQUEST_COMMAND);
                commandFromLine = Console.ReadLine();
                if (commandFromLine == END_MESSAGE) return;
                if (espressor.IsTheCommandPossible(currentState, commandFromLine))
                {
                    espressor.ExecuteCommand(commandFromLine, ref currentState);
                }
                currentState = espressor.GetNextState(currentState, commandFromLine);
            }
            
        }
    }
}
