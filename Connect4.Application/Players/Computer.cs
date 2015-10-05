using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connect4.Interfaces.Players;

namespace Connect4.Players
{
    public class Computer : IPlayer
    {
        static int Rows = 6;
        static int Columns = 6;
        private int number;
        private int firstLevel = 0;
        public int CutOffLevel = 6;
        public int[] Weights = new int[] { 1, 2, 4, 8, 1, 2, 8, 16 };

        public Computer(int playerNumber)
        {
            number = playerNumber;
        }

        int IPlayer.PlayerTurn(State state)
        {
            //find best action to perform...
            return MoveSearch(state);
        }

        private int MoveSearch(State state)
        {
            state.Successors = Successors(state);
            firstLevel = state.MoveCount;
            int j = MaxValue(state, int.MinValue, int.MaxValue);
            foreach(int a in state.Successors.Keys)
            {
                if (state.Successors[a].StateValue == j)
                    return a;
            }
            return -1;
        }

        /// <summary>
        /// Maximum player move for computer.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private int MaxValue(State state, int min, int max)
        {
            if (CutOffTest(state))
                return state.StateValue;

            state.StateValue = int.MinValue;
            Dictionary<int, State> succ = (state.Successors != null) ? state.Successors : Successors(state);

            foreach(int a in succ.Keys)
            {
                state.StateValue = Math.Max(state.StateValue, MinValue(succ[a], min, max));
                if (state.StateValue >= max) 
                    return state.StateValue;
                min = Math.Max(min, state.StateValue);
            }
            return state.StateValue;
        }

        /// <summary>
        /// Minimal player move for computer
        /// </summary>
        /// <param name="state"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private int MinValue(State state, int min, int max)
        {
            if (CutOffTest(state)) return Eval(state.Values);
            state.StateValue = int.MaxValue;
            Dictionary<int, State> succ = (state.Successors != null) ? state.Successors : Successors(state);
            foreach(int a in succ.Keys)
            {
                state.StateValue = Math.Min(state.StateValue, MaxValue(succ[a], min, max));
                if (state.StateValue <= min)
                    return state.StateValue;
                max = Math.Min(max, state.StateValue);
            }
            return state.StateValue;
        }
        
        /// <summary>
        /// Check for win or look at moves ahead.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private bool CutOffTest(State state)
        {
            state.StateValue = Eval(state.Values); //check for win...

            if (Math.Abs(state.StateValue) > 5000) 
                return true;

            if ((state.MoveCount - firstLevel) > CutOffLevel)
                return true;

            return false;
        }


        /// <summary>
        /// List of possible successor states.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private Dictionary<int, State> Successors(State state)
        {
            Dictionary<int, State> succ = new Dictionary<int, State>(7);
            for(int i = 1; i <= 7; i++)
            {
                State s = state.Move(i);
                if (s != null) succ.Add(i, s);  // if move is valid add it to succ.
            }
            return succ;
        }

        private int Eval(int[,] state)
        {
            int score = 0;

            //Horizontal
            for (int i = 0; i < Rows; i++)
                score += CheckLine(state[0, i], state[1, i], state[2, i], state[3, i], state[4, i], state[5, i], state[6, i]);

            //Vertical
            for(int i = 0; i < Columns; i++)
                score += CheckLine(state[i, 0], state[i, 1], state[i, 2], state[i, 3], state[i, 4], state[i, 5]);

            //Diagonal
            score += CheckLine(state[0, 2], state[1, 3], state[2, 4], state[3, 5]);
            score += CheckLine(state[0, 1], state[1, 2], state[2, 3], state[3, 4], state[4, 5]);
            score += CheckLine(state[0, 0], state[1, 1], state[2, 2], state[3, 3], state[4, 4], state[5, 5]);
            score += CheckLine(state[1, 0], state[2, 1], state[3, 2], state[4, 3], state[5, 4], state[6, 5]);
            score += CheckLine(state[2, 0], state[3, 1], state[4, 2], state[5, 3], state[6, 4]);
            score += CheckLine(state[3, 0], state[4, 1], state[5, 2], state[6, 3]);

            //Diagonal 2
            score += CheckLine(state[3, 0], state[2, 1], state[1, 2], state[0, 3]);
            score += CheckLine(state[4, 0], state[3, 1], state[2, 2], state[1, 3], state[0, 4]);
            score += CheckLine(state[5, 0], state[4, 1], state[3, 2], state[2, 3], state[1, 4], state[0, 5]);
            score += CheckLine(state[6, 0], state[5, 1], state[4, 2], state[3, 3], state[2, 4], state[1, 5]);
            score += CheckLine(state[6, 1], state[5, 2], state[4, 3], state[3, 4], state[2, 5]);
            score += CheckLine(state[6, 2], state[5, 3], state[4, 4], state[3, 5]);
            return score; ;
        }

        /// <summary>
        /// Iterate through all possibilities.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private int CheckLine(params int[] values)
        {
            int score = 0;
            for(int i = 0; i < (values.Length - 3); i++)
            {
                int c = 0;
                int p = 0;
                for(int j = 0; j < 4; j++)
                {
                    if (values[i + j] == number)
                        c++;
                    else if (values[i + j] != 0)
                        p++;
                }

                if((c>0) && (p > 0))
                {
                    //Computer Opp
                    if (c == 4)
                        return Weights[3]; //WIN.
                    
                    score -= ((c / 3) * Weights[2]) + ((c / 2) * Weights[1]) + Weights[0];
                }
                else if((c==0) && (p>0))
                {
                    //Player Opp
                    if (p == 4)
                        return -1 * Weights[7]; //WIN

                    score -= ((p / 3) * Weights[6]) + ((p / 2) * Weights[5]) + Weights[4];
                }
            }
            return score;
        }
    }
}
