using cazzateeeee.Classes;

namespace cazzateeeee.Helpers
{
    internal class GameManager
    {
        private Supertris board;
        private int prossimaTrisObbligatoria;  // -1 = mossa libera, altrimenti indica il tris dove giocare
        private char turnoCorrente;
        private bool pve;
        private bool mossaValida;

        public GameManager()
        {
            board = new Supertris();
            FileManager.Start();
            turnoCorrente = 'X';
            prossimaTrisObbligatoria = -1;  // Prima mossa libera
            mossaValida = false;
        }

        // -------------------------------- HELPERS -------------------------------- //

        public char GetTurno() => turnoCorrente;

        public void CambiaTurno()
        {
            turnoCorrente = turnoCorrente == 'X' ? 'O' : 'X';
        }

        public int GetProssimaTrisObbligatoria() => prossimaTrisObbligatoria;

        // -------------------------------- END HELPERS ---------------------------- // 

        public void StartGamePVP()
        {
            pve = false;
            turnoCorrente = 'X';
            prossimaTrisObbligatoria = -1;
        }

        public void StartGamePVE()
        {
            pve = true;
            turnoCorrente = 'X';
            prossimaTrisObbligatoria = -1;
        }

        public void StartGameEVE()
        {
            pve = true;
            turnoCorrente = 'X';
            prossimaTrisObbligatoria = -1;
        }

        public bool MakeMove(int numTris, int row, int col)
        {
            // Calcolo le coordinate del mini-tris nella griglia 3x3
            int trisRow = numTris / 3;
            int trisCol = numTris % 3;

            // Calcolo il prossimo tris dove dovrà giocare l'avversario
            int prossimoNumTris = row * 3 + col;

            mossaValida = false;

            // Controllo se la mossa è valida in base alla tris obbligatoria
            if (prossimaTrisObbligatoria == -1)
            {
                // Mossa libera - può giocare ovunque
                mossaValida = board.MakeMove(turnoCorrente, trisRow, trisCol, row, col);
            }
            else
            {
                // Deve giocare nella tris obbligatoria
                if (prossimaTrisObbligatoria == numTris)
                {
                    mossaValida = board.MakeMove(turnoCorrente, trisRow, trisCol, row, col);
                }
                else
                {
                    // Mossa non valida - tris sbagliata
                    return false;
                }
            }

            if (!mossaValida) return false;

            // Calcolo coordinate del prossimo mini-tris
            int prossimoTrisRow = prossimoNumTris / 3;
            int prossimoTrisCol = prossimoNumTris % 3;

            // FIX PRINCIPALE: Se il prossimo tris è già completato, la mossa diventa libera
            if (board.IsTrisCompleted(prossimoTrisRow, prossimoTrisCol))
            {
                prossimaTrisObbligatoria = -1;  // Mossa libera!
            }
            else
            {
                prossimaTrisObbligatoria = prossimoNumTris;
            }

            return true;
        }

        public char CheckWin()
        {
            return board.CheckWin();
        }
    }
}