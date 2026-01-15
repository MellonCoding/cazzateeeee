using cazzateeeee.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace cazzateeeee.Helpers
{
    internal class GameManager
    {
        private Supertris board;    // il campo da gioco
        private char nextTris;      // prossimo tris
        private char turno;         // il turno del player corrente
        
        public GameManager() 
        { 
            board = new Supertris();

            FileManager.Start();

            turno = 'X';
            nextTris = '-';
        }

        // -------------------------------- HELPERS -------------------------------- //

        public char GetTurno() { return turno; }
        public void CambiaTurno() { if (turno == 'X') turno = 'O'; else turno = 'X'; }

        // -------------------------------- END HELPERS ---------------------------- // 


        public void StartGamePVP()
        { 
            // mod se mod player v player

             // Turno player X
                // makeMove chiamata dal bottone
                // MakeMove prende la posizione del bottone e scrive su file
            

        }

        public void StartGamePVE()
        {
            // mod se mod player v player

            // Turno player X
            // makeMove chiamata dal bottone
            // MakeMove prende la posizione del bottone e scrive su file


        }

        public void StartGameEVE()
        {
            // mod se mod player v player

            // Turno player X
            // makeMove chiamata dal bottone
            // MakeMove prende la posizione del bottone e scrive su file


        }

        public bool MakeMove(char tris, char row, char col)
        {

            int vRow = Convert.ToInt16(row), vCol = Convert.ToInt16(col);
            char vTris = Convert.ToChar((vRow + vCol + (vRow * 2) + 48) / 4);

            if (nextTris == '-')
            {
                if (board.MakeMove(turno, tris, row, col))
                {
                    nextTris = vTris;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (nextTris == vTris)
                {
                    if (board.MakeMove(turno, tris, row, col))
                    {
                        nextTris = vTris;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public char CheckWin()
        {
            char won = board.CheckWin();

            if (won != '-') 
            {
                MessageBox.Show($"{won} ha vinto, yeyy!!"); 
                return won; 
            }

            return '-';
        }
    }
}
