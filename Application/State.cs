using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect4
{
    internal class State
    {
        public int[,] Values;
        public int[] RowCount;
        public int Turn;
        public int StateValue;
        public int MoveCount;
        public Dictionary<int, State> Successors;
        public static int Rows = 6;

        /// <summary>
        /// New state base on old state
        /// </summary>
        /// <param name="original"></param>
        public State(State original)
        {
            Values = (int[,])original.Values.Clone();
            RowCount = (int[])original.RowCount.Clone();
            Turn = original.Turn;
            MoveCount = original.MoveCount;
        }

        /// <summary>
        /// New State
        /// </summary>
        public State()
        {
            Values = new int[7, 6];
            RowCount = new int[7];
            MoveCount = 0;
        }

        #region Game Methods

        /// <summary>
        /// Creates a new state after the move
        /// </summary>
        /// <param name="iMove">A number between 1-7 representing the chosen column.</param>
        /// <returns></returns>
        public State Move(int iMove)
        {
            State state = new State(this);
            int m = iMove - 1;

            if (state.RowCount[m] == Rows) return null;  //column full.

            state.RowCount[m]++;
            state.Values[m, (Rows - state.RowCount[m])] = state.Turn; //updates the board.
            state.Turn = (state.Turn == 1) ? 2 : 1; //switch player.
            state.MoveCount++;
            return state;
        }

        /// <summary>
        /// Iterate through all possibilities
        /// </summary>
        /// <param name="values"></param>
        /// <returns>returns winning number, otherwise 0.</returns>
        private int CheckLine(params int[] values)
        {
            int last = 0;
            int lastCount = 0;

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == last)
                {
                    lastCount++;
                }
                else
                {
                    last = values[i];
                    lastCount = 1;
                }

                if ((lastCount == 4) && (last > 0))
                    break;
            }
            if ((lastCount == 4) && (last > 0))
                return last;
            return 0;
        }   
        

        /// <summary>
        /// Check for winning sequence
        /// </summary>
        /// <returns>Returns winning nubmer else 0</returns>
        public int CheckWin()
        {
            int result = 0;

            //Check Horizontal
            for(int i = 0; i < Rows; i++)
            {
                result = CheckLine(Values[0, i], Values[1, i], Values[2, i], Values[3, i], Values[4, i], Values[5, i], Values[6, i]);
                if (result != 0) return result;
            }

            //Check Vertical
            for (int i = 0; i < Rows; i++)
            {
                result = CheckLine(Values[i, 0], Values[i, 1], Values[i, 2], Values[i, 3], Values[i, 4], Values[i, 5]);
                if (result != 0) return result;
            }
            
            //Check Diagonal
            result = CheckLine(Values[0, 2], Values[1, 3], Values[2, 4], Values[3, 5]);
            if (result != 0) return result;
            result = CheckLine(Values[0, 1], Values[1, 2], Values[2, 3], Values[3, 4], Values[4, 5]);
            if (result != 0) return result;
            result = CheckLine(Values[0, 0], Values[1, 1], Values[2, 2], Values[3, 3], Values[4, 4], Values[5, 5]);
            if (result != 0) return result;
            result = CheckLine(Values[1, 0], Values[2, 1], Values[3, 2], Values[4, 3], Values[5, 4], Values[6, 5]);
            if (result != 0) return result;
            result = CheckLine(Values[2, 0], Values[3, 1], Values[4, 2], Values[5, 3], Values[6, 4]);
            if (result != 0) return result;
            result = CheckLine(Values[3, 0], Values[4, 1], Values[5, 2], Values[6, 3]);
            if (result != 0) return result;

            //Check Diagonal 2
            result = CheckLine(Values[3, 0], Values[2, 1], Values[1, 2], Values[0, 3]);
            if (result != 0) return result;
            result = CheckLine(Values[4, 0], Values[3, 1], Values[2, 2], Values[1, 3], Values[0, 4]);
            if (result != 0) return result;
            result = CheckLine(Values[5, 0], Values[4, 1], Values[3, 2], Values[2, 3], Values[1, 4], Values[0, 5]);
            if (result != 0) return result;
            result = CheckLine(Values[6, 0], Values[5, 1], Values[4, 2], Values[3, 3], Values[2, 4], Values[1, 5]);
            if (result != 0) return result;
            result = CheckLine(Values[6, 1], Values[5, 2], Values[4, 3], Values[3, 4], Values[2, 5]);
            if (result != 0) return result;
            result = CheckLine(Values[6, 2], Values[5, 3], Values[4, 4], Values[3, 5]);
            if (result != 0) return result;

            //Check full board
            bool full = false;
            for(int i = 0; i < this.RowCount.Length; i++)
            {
                if (RowCount[i] < Rows)
                {
                    full = false;
                    break; //row has space, board not full so breka out of loop.
                }
            }
            if(full) return -1;
            return 0;
        }
        
        #endregion

        #region Helper Methods

        /// <summary>
        /// Build string for display on console
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\t1\t2\t3\t4\t5\t6\t7\n\n");
            
            for(int j = 0; j <Rows; j++)
            {
                for(int i = 0; i < 7; i++)
                    sb.Append("\t" + ((Values[i, j] == 0) ? "*" : ((Values[i, j] == 1) ? "1" : "2")));

                sb.Append("\n\n");
            }
            
            sb.Append(string.Format("\n\nMoves: {0} \nNext turn: Player {1}\n", MoveCount.ToString(), Turn.ToString()));
            
            return sb.ToString();
        }

        #endregion
    }
}
