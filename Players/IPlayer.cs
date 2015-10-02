using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect4.Players
{
    /// <summary>
    /// Player Interface
    /// </summary>
    internal interface IPlayer
    {
        int PlayerTurn(State state);
    }
}
