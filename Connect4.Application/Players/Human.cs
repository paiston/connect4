using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connect4.Interfaces.Players;

namespace Connect4.Players
{
    public class Human: IPlayer
    {
        public Human()
        {
        }

        int IPlayer.PlayerTurn(State state)
        {
            Console.Clear();
            Console.WriteLine(" # # # #   CONNECT 4   # # # # \n");
            Console.WriteLine(state);
            Console.Write("Whats your move (1-7)?");
            char c = Console.ReadKey().KeyChar;
            int m = int.Parse(c.ToString()); // TODO: If time allows look at validation of input.
            return m;
        }
    }
}
