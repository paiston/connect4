using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connect4.Players;

namespace Connect4
{
    class Program
    {
        static void Main(string[] args)
        {
            StartGame();            
        }

        private static void StartGame()
        {
            Connect4 c4 = new Connect4();
            c4.Player1 = new Human();
            c4.Player2 = new Computer(2);

            //Uncomment code below for Computer vs computer games.
            //c4.Player1 = new Computer(1);
            //(c4.Player1 as Computer).CutOffLevel = 7;
            //(c4.Player2 as Computer).Weights = new int[] { 1, 5, 100, 10000, 1, 5, 200, 15000 };

            c4.PlayGame();

            Console.Write("Would you like another game? (Y or N)");
            char c = Console.ReadKey().KeyChar;
            if (c.ToString().ToUpper() == "Y")
                StartGame();
            else
                Environment.Exit(0);
        }   
    }
}
