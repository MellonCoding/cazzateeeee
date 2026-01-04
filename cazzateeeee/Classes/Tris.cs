using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cazzateeeee.Classes
{
    internal class Tris
    {
        private char[,] miniBoard;

        public Tris()
        {
            miniBoard = new char[3, 3];

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    miniBoard[row, col] = '-';
                }
            }
        }
    }
}
