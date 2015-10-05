using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Connect4.Players;

namespace Connect4.Tests
{
    [TestFixture]
    public class GameTests
    {
        Connect4 c4 = null;

        [SetUp]
        public void Setup()
        {
            c4 = new Connect4(new BoardSettings() { Columns = 7, Rows = 6 }, new Message());
            c4.Player1 = new Human();
            c4.Player2 = new Computer(2);
        }

        [Test]
        public void PartFillGridNoWin()
        {
            // 1 2 3 4 5 6 7
            // -------------
            // - - - - - - -
            // - - - - - - -
            // - - - - - - -
            // 1 2 1 2 1 2 1
            // 2 1 2 1 2 1 2
            // 1 2 1 2 1 2 1

            for (int i = 0; i <= 7; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    c4.Turn(i);
                }
            }
                        
            Assert.AreEqual(c4.CheckGameStateForWin(),0);
        }

        [Test]
        public void HorizontalWin()
        {
            // 1 2 3 4 5 6 7
            // -------------
            // - - - - - - -
            // - - - - - - -
            // - - - - - - -
            // - - - - - - -
            // - - - - - - -
            // 1 1 1 1 2 2 2
            c4.Turn(1); //Player1 Column 1
            c4.Turn(7); //Player2 Column 7
            c4.Turn(2); //Player1 Column 2
            c4.Turn(6); //Player2 Column 6
            c4.Turn(3); //Player1 Column 3
            c4.Turn(5); //Player2 Column 5
            c4.Turn(4); //Player1 Column 4

            Assert.Greater(c4.CheckGameStateForWin(), 0);
        }

        [Test]
        public void VerticalWin()
        {
            // 1 2 3 4 5 6 7
            // -------------
            // - - - - - - -
            // - - - - - - -
            // 1 - - - - - -
            // 1 - - - - - -
            // 1 - - - - - -
            // 1 2 2 2 - - -
            c4.Turn(1); //Player1 Column 1
            c4.Turn(2); //Player2 Column 2
            c4.Turn(1); //Player1 Column 1
            c4.Turn(3); //Player2 Column 3
            c4.Turn(1); //Player1 Column 1
            c4.Turn(4); //Player2 Column 4
            c4.Turn(1); //Player1 Column 4

            Assert.Greater(c4.CheckGameStateForWin(), 0);
        }

        [Test]
        public void DiagonalWin1()
        {
            // | 1 2 3 4 5 6 7
            // |--------------
            //6| - - - - - - -
            //5| - - - - - - -
            //4| 1 - - - - - -
            //3| 2 1 - - - - -
            //2| 2 2 1 - - - -
            //1| 1 2 1 1 2 - -                        
            c4.Turn(1); //Player1 Column 1
            c4.Turn(2); //Player2 Column 2
            c4.Turn(3); //Player1 Column 3
            c4.Turn(1); //Player2 Column 1
            c4.Turn(4); //Player1 Column 4
            c4.Turn(2); //Player2 Column 2
            c4.Turn(3); //Player1 Column 3
            c4.Turn(5); //Player2 Column 5
            c4.Turn(2); //Player1 Column 2
            c4.Turn(1); //Player2 Column 1
            c4.Turn(1); //Player1 Column 1

            Assert.Greater(c4.CheckGameStateForWin(), 0);
        }

        [Test]
        public void DiagonalWin2()
        {
            // | 1 2 3 4 5 6 7
            // |--------------
            //6| - - - - - - -
            //5| - - - - - - -
            //4| - - - - - - 1
            //3| - - - - - 1 2
            //2| - - - - 1 2 2
            //1| - - 2 1 1 2 1 
            c4.Turn(7); //Player1 Column 7
            c4.Turn(6); //Player2 Column 6
            c4.Turn(5); //Player1 Column 5
            c4.Turn(7); //Player2 Column 7
            c4.Turn(4); //Player1 Column 4
            c4.Turn(6); //Player2 Column 6
            c4.Turn(5); //Player1 Column 5
            c4.Turn(3); //Player2 Column 3
            c4.Turn(6); //Player1 Column 6
            c4.Turn(7); //Player2 Column 7
            c4.Turn(7); //Player1 Column 7

            Assert.Greater(c4.CheckGameStateForWin(), 0);
        }

        [Test]
        public void Tie()
        {
            // 1 2 3 4 5 6 7
            // -------------
            // 1 2 1 2 1 2 1
            // 2 1 2 1 2 1 2
            // 1 2 1 2 1 2 1
            // 1 2 1 2 1 2 1
            // 2 1 2 1 2 1 2
            // 1 2 1 2 1 2 1

            for (int i = 0; i <= 6; i++)
            {
                for (int j = 0; j <= 5; j++)
                {
                    c4.Turn(i);
                }
            }

            Assert.AreEqual(c4.CheckGameStateForWin(), -1);
        
        }



    }
}
