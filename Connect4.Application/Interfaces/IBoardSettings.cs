using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect4.Interfaces
{
    public interface IBoardSettings
    {
        int Columns { get; set; }
        int Rows { get; set; }
        int WinningCount { get; set; }
    }
}
