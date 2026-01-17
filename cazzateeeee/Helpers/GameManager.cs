using cazzateeeee.Classes;

namespace cazzateeeee.Helpers
{
    internal class GameManager
    {
        private Supertris board;    // il campo da gioco
        private int nextTris;       // prossimo tris
        private char turno;         // il turno del player corrente
        private bool pve;           // se sono in modalita' con i bot
        public event Action<char>? MoveCompleted; // char = chi ha mosso (X/O)

        public GameManager() 
        { 
            // creo la board del supertris
            board = new Supertris();
            // creo il file mossa.txt
            FileManager.Start();
            // 
            turno = 'X';
            nextTris = '-';
        }

        // -------------------------------- HELPERS -------------------------------- //

        public char GetTurno() { return turno; }
        public void CambiaTurno() { if (turno == 'X') turno = 'O'; else turno = 'X'; }

        // -------------------------------- END HELPERS ---------------------------- // 


        public void StartGamePVP()
        {
            pve = false;
            turno = 'X';
            nextTris = '-';
        }

        public void StartGamePVE()
        {
            pve = true;
            turno = 'X';
            nextTris = '-';
        }

        public void StartGameEVE()
        {
            pve = true;
            turno = 'X';
            nextTris = '-';
        }

        public bool MakeMove(int tris, int row, int col)
        {
            var vtris = row + col + row * 2;

            bool mossaFatta = false;

            if (nextTris == '-')
            {
                mossaFatta = board.MakeMove(turno, tris, row, col);


                //if (board.MakeMove(turno, tris, row, col))
                //{
                //    nextTris = vtris;
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
            }
            else
            {
                if (nextTris == tris)
                    mossaFatta = board.MakeMove(turno, tris, row, col);
                else
                    mossaFatta = false;

                //if (nextTris == tris)
                //{
                //    if (board.MakeMove(turno, tris, row, col))
                //    {
                //        nextTris = vtris;
                //        return true;
                //    }
                //    else
                //    {
                //        return false;
                //    }
                //}
            }

            if (!mossaFatta) return false;

            nextTris = vtris;

            // notifica: la mossa valida è stata fatta
            MoveCompleted?.Invoke(turno);

            return true;
        }

        public char CheckWin()
        {
            char won = board.CheckWin();
            Console.WriteLine("gm winner " + won);
            return '-';
        }
    }
}
