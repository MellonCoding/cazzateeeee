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
        private char winner;

        public Tris()
        {
            miniBoard = new char[3, 3];
            winner = '-';

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    miniBoard[row, col] = '-';
                }
            }
        }

        // -------------------------------- HELPERS -------------------------------- //

        public char wonBy() { return winner; }

        // -------------------------------- END HELPERS ---------------------------- // 

        public bool MakeMove(char player, int row, int col)
        {
            if (winner != '-') return false;

            // CHECK QUI; qui CheckWin fa i conrolli e ritorna il char del vincente, ti va bene? becuse possimao anche fare che se la mossa e' valida
            // la fa e se vede che qualcuno ha vinto allora ritrono player, vedi se ti torna utile per qualcosa
            
            if (miniBoard[row, col] == '-') miniBoard[row, col] = player;
            else return false;

            return true;
        }

        public char CheckWin()
        { 
            // DA RIVEDERE
            char won = '-';

            for (int row = 0; row < 3; row++)
            {
                if (won != '-') continue;
                won = miniBoard[row, 0] == miniBoard[row, 1] && miniBoard[row, 1] == miniBoard[row, 2] ? miniBoard[row, 0] : won;
            }

            for (int col = 0; col < 3; col++)
            {
                if (won != '-') continue;
                won = miniBoard[0, col] == miniBoard[1, col] && miniBoard[1, col] == miniBoard[2, col] ? miniBoard[0, col] : won;
            }

            int x = 0, y = 0;

            won = miniBoard[x, y] == miniBoard[x+1, y+1] && miniBoard[x+1, y+1] == miniBoard[x+2, y+2] ? miniBoard[x, y] : won;

            y += 2;

            won = miniBoard[x, y] == miniBoard[x + 1, y - 1] && miniBoard[x + 1, y - 1] == miniBoard[x + 2, y - 2] ? miniBoard[x, y] : won;

            winner = won;
            Console.WriteLine("Tris winner: " + won);
            return won;
        }
    }
}
