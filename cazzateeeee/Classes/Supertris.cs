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

        // -------------------------------- HELPERS -------------------------------- //



        // -------------------------------- END HELPERS ---------------------------- // 
        public bool MakeMove(char player, char tris, char row, char col)
        {
            int COSODELLEOPERAZIONI = Convert.ToInt32(tris) - 48; // trasformo da char a int, c'é un modo migliore, si, lo conosco, probabilemente
            int colCount = COSODELLEOPERAZIONI % 3;
            int rawCount = 0;

            while (COSODELLEOPERAZIONI > 2)
            {
                COSODELLEOPERAZIONI -= 3;
                rawCount++;
            }

            char won = board[rawCount, colCount].CheckWin();
             if (won != '-') MessageBox.Show($"{won}");

            return board[rawCount, colCount].MakeMove(player, row, col);
        }

        public char CheckWin()
        {
            char won = '-';

            for (int row = 0; row < 3; row++)
            {
                if (won != '-') continue;
                won = board[row, 0].wonBy() == board[row, 1].wonBy() && board[row, 1].wonBy() == board[row, 2].wonBy() ? board[row, 0].wonBy() : won;
            }

            for (int col = 0; col < 3; col++)
            {
                if (won != '-') continue;
                won = board[0, col].wonBy() == board[1, col].wonBy() && board[1, col].wonBy() == board[2, col].wonBy() ? board[0, col].wonBy() : won;
            }

            int x = 0, y = 0;

            won = board[x, y].wonBy() == board[x + 1, y + 1].wonBy() && board[x + 1, y + 1].wonBy() == board[x + 2, y + 2].wonBy() ? board[x, y].wonBy() : won;

            y += 2;

            won = board[x, y].wonBy() == board[x + 1, y - 1].wonBy() && board[x + 1, y - 1].wonBy() == board[x + 2, y - 2].wonBy() ? board[x, y].wonBy() : won;

            return won;
        }
    }
}
