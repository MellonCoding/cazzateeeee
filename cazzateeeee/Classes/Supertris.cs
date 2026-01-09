using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cazzateeeee.Classes
{
    internal class Supertris
    {
        private Tris[,] board;

        public Supertris() 
        {
            board = new Tris[3, 3];

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = new Tris();
                }
            }
        }

        public char MakeMove(char player, char tris, char row, char col)
        {
            int COSODELLEOPERAZIONI = Convert.ToInt32(tris) - 48; // trasformo da char a int, c'é un modo migliore, si, lo conosco, probabilemente
            int colCount = COSODELLEOPERAZIONI % 3;
            int rawCount = 0;

            while (COSODELLEOPERAZIONI > 0)
            {
                COSODELLEOPERAZIONI -= 3;
                rawCount++;
            }

            return board[rawCount, colCount].MakeMove(player, row, col);
        }
    }
}
