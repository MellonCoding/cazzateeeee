using cazzateeeee.Classes;

namespace cazzateeeee.Helpers
{
    internal class GameManager
    {
        private Supertris board;    // il campo da gioco
        private int nextTris;       // prossimo tris
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

        public bool MakeMove(int tris, int row, int col)
        {
            var vtris = row + col + row * 2;

            if (nextTris == '-')
            {
                if (board.MakeMove(turno, tris, row, col))
                {
                    nextTris = vtris;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (nextTris == tris)
                {
                    if (board.MakeMove(turno, tris, row, col))
                    {
                        nextTris = vtris;
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
            Console.WriteLine("gm winner " + won);
            return '-';
        }
    }
}
