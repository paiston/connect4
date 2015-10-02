using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connect4.Players;

namespace Connect4
{
    /// <summary>
    /// Main Connect4 game
    /// </summary>
    public class Connect4
    {
        internal State CurrentState; //Current game state.
        public bool Winner; //Winner found.
        internal IPlayer Player1;
        internal IPlayer Player2;

        public Connect4()
        {
            //Initialise
            Winner = false;
            CurrentState = new State();
            CurrentState.Turn = 1;
        }

        public bool Turn(int move)
        {
            State state = CurrentState.Move(move);
            
            if (state == null)
                return false;

            CurrentState = state;

            return true;
        }

        public void PlayGame()
        {
            int w = 0;
            while(!Winner)
            {
                if (CurrentState.Turn == 1)
                    Turn(Player1.PlayerTurn(CurrentState));
                else
                    Turn(Player2.PlayerTurn(CurrentState));

                w = CurrentState.CheckWin(); //0=No Winner; 1=Player1 wins; 2=Player2 wins; -1 = Tie
                Winner = (w !=0);

                Console.Clear();
                Console.WriteLine(CurrentState);
            }
            
            Console.Clear();
            Console.WriteLine(" # # # # # # # # # # # #    CONNECT 4     # # # # # # # # # # # # \n");
            Console.Write(CurrentState);
            
            if(w==-1)
                Console.WriteLine(" ITS A TIE!!!");
            else
                Console.WriteLine(string.Format("PLAYER {0} IS THE WINNER!", (w == 1) ? "1" : "2"));
        }
    }
}
