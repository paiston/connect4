using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connect4.Interfaces;
using Connect4.Interfaces.Players;

namespace Connect4
{
    /// <summary>
    /// Main Connect4 game
    /// </summary>
    public class Connect4
    {
        internal State GameState; //Current game state.
        public bool Winner; //Winner found.
        public IPlayer Player1;
        public IPlayer Player2;
        protected readonly IMessage _Message;
                
        /// <summary>
        /// Initialises a new game with specifed grid size.
        /// </summary>
        /// <param name="gridColumns">No. of columns in grid</param>
        /// <param name="gridRows">No. of rows in grid</param>
        public Connect4(IBoardSettings boardSettings, IMessage message)
        {
            //Initialise
            _Message = message;
            Winner = false;
            GameState = new State(boardSettings.Columns,boardSettings.Rows);
            GameState.Turn = 1;
        }
        
        public bool Turn(int move)
        {
            try
            {
                State state = GameState.Move(move);

                if (state == null)
                    return false;

                GameState = state;
            }
            catch(Exception ex)
            {
                _Message.Write(string.Format("Invalid move - {0}", ex.Message));
            }
            return true;
        }

        /// <summary>
        /// Checks the state of the game to see if there is a winning sequence.
        /// </summary>
        /// <returns>//0=No Winner; 1=Player1 wins; 2=Player2 wins; -1 = Tie</returns>
        public int CheckGameStateForWin()
        {
            return GameState.CheckWin(); //0=No Winner; 1=Player1 wins; 2=Player2 wins; -1 = Tie
        }

        public void PlayGame()
        {
            int w = 0;
            while(!Winner)
            {
                if (GameState.Turn == 1)
                    Turn(Player1.PlayerTurn(GameState));
                else
                    Turn(Player2.PlayerTurn(GameState));

                w = CheckGameStateForWin();
                Winner = (w !=0);

                Console.Clear();
                Console.WriteLine(GameState);
            }
            
            Console.Clear();
            Console.WriteLine(" # # # # # # # # # # # #    CONNECT 4     # # # # # # # # # # # # \n");
            Console.Write(GameState);
            
            if(w==-1)
                Console.WriteLine(" ITS A TIE!!!");
            else
                Console.WriteLine(string.Format("PLAYER {0} IS THE WINNER!", (w == 1) ? "1" : "2"));
        }
    }
}
