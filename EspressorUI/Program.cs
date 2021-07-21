using System;
using Espressorlibrary;


namespace EspressorUI
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Espressor espressor = new Espressor();
            string commandFromLine;
            string endMessage = "FINISH";
            string message = "Write "+ endMessage +" to END programm. Available commands: ";
            string requestCommand = "Enter your command: ";
            State currentState = State.Initial_state;

            while (true)
            {
                Console.WriteLine("----------------------------------------\n");
                Console.WriteLine("----------------------------------------\n");
                Console.WriteLine("----------------------------------------\n");
                Console.WriteLine(espressor);
                Console.WriteLine("----------------------------------------\n");
                Console.WriteLine(message);
                espressor.PrintAvailableAction(currentState);
                Console.WriteLine("\n");
                Console.Write(requestCommand);
                commandFromLine = Console.ReadLine();
                if (commandFromLine == endMessage) return;
                if (espressor.IsTheCommandPossible(currentState, commandFromLine))
                {
                    espressor.ExecuteCommand(commandFromLine, ref currentState);
                }
                currentState = espressor.GetNextState(currentState, commandFromLine);
            }
            
        }
    }
}
