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
            string message = "Available commands: ";
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
                commandFromLine = Console.ReadLine();
                if (espressor.IsTheCommandPossible(currentState, commandFromLine))
                {
                    espressor.ExecuteCommand(commandFromLine, ref currentState);
                }
                currentState = espressor.GetNextState(currentState, commandFromLine);
            }
            
        }
    }
}
