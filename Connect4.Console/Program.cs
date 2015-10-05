using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connect4.Players;

namespace Connect4.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            StartGame();
        }

        private static void StartGame()
        {
            Connect4 c4 = new Connect4(new BoardSettings() { Columns = 7, Rows = 6 }, new Message());
            c4.Player1 = new Human();
            c4.Player2 = new Computer(2);

            //Uncomment code below for Computer vs computer game.
            c4.Player1 = new Computer(1);
            (c4.Player1 as Computer).CutOffLevel = 7;
            (c4.Player2 as Computer).Weights = new int[] { 1, 2, 4, 8, 1, 2, 8, 16 };

            c4.PlayGame();

            System.Console.Write("Would you like another game? (Y or N)");
            char c = System.Console.ReadKey().KeyChar;
            if (c.ToString().ToUpper() == "Y")
                StartGame();
            else
                Environment.Exit(0);
        }
    }
}
