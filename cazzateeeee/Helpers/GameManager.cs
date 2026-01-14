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
        private Supertris board;    //  il campo da gioco
        private char turno;         // il turno del player corrente
        
        //private char[,,] virtualBoard;  // board virtuale che si popolerá leggendo dal file

        public GameManager() 
        { 
            board = new Supertris();
            //virtualBoard = new char[9, 3, 3];
            turno = 'X'; //  statico inizia X bot o persona che sia
        }

        // ---------------- HELPERS --------------------------- vediamo se sta sintassi funziona, se non funzia leviamo
        public char GetTurno() { return turno; }
        public void CambiaTurno() { if (turno == 'X') turno = 'O'; else turno = 'X'; }

        // ---------------- END HELPERS ---------------------------


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
            // DA DELEGARE FileManager.Write($"{turno} {tris}{row}{col}");
            return board.MakeMove(turno, tris, row, col);
        }

        public char CheckWin()
        {


            return '-';
        }
    }
}
