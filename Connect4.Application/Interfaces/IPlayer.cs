using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect4.Interfaces.Players
{
    /// <summary>
    /// Player Interface
    /// </summary>
    public interface IPlayer
    {
        int PlayerTurn(State state);
    }
}
