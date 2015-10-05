using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Connect4.Interfaces;

namespace Connect4
{
    public class BoardSettings : IBoardSettings
    {
        public int Columns
        {
            get;
            set;
        }

        public int Rows
        {
            get;
            set;
        }

        public int WinningCount
        {
            get;
            set;
        }
    }
}
