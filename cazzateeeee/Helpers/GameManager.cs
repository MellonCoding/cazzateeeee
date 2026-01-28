using cazzateeeee.Classes;

namespace cazzateeeee.Helpers
{
    internal class GameManager
    {
        private Supertris board;    // il campo da gioco
        private int TrisDoveFareMossa;       // prossimo tris
        private char turno;         // il turno del player corrente
        private bool pve;           // se sono in modalita' con i bot
        private bool mossaFatta;

        public GameManager() 
        { 
            // creo la board del supertris
            board = new Supertris();
            // creo il file mossa.txt
            FileManager.Start();
            // 
            turno = 'X';
            TrisDoveFareMossa = -1;
            mossaFatta = false;
        }

        // -------------------------------- HELPERS -------------------------------- //

        public char GetTurno() { return turno; }
        public void CambiaTurno() { if (turno == 'X') turno = 'O'; else turno = 'X'; }

        // -------------------------------- END HELPERS ---------------------------- // 


        public void StartGamePVP()
        {
            pve = false;
            turno = 'X';
            TrisDoveFareMossa = -1;
        }

        public void StartGamePVE()
        {
            pve = true;
            turno = 'X';
            TrisDoveFareMossa = -1;
        }

        public void StartGameEVE()
        {
            pve = true;
            turno = 'X';
            TrisDoveFareMossa = -1;
        }

        public bool MakeMove(int TrisInputMossa, int row, int col)
        {
            // per fare indexing dell'array 2d devo trasformare il numero in 2 numeri che rappresentino
            // il tris sotto forma di X e Y

            // calcolo che colonna del tris (miniBoard)
            int TrisProssimo = TrisInputMossa;
            int COL_COUNT = TrisInputMossa % 3;
            int ROW_COUNT = 0;
            // e anche la riga del tris (miniBoard)
            while (TrisProssimo > 2)
            {
                TrisProssimo -= 3;
                ROW_COUNT++;
            }

            TrisProssimo = row + col + row * 2;

            mossaFatta = false;

            if (TrisDoveFareMossa == -1)
            {
                TrisDoveFareMossa = TrisProssimo;
                mossaFatta = board.MakeMove(turno, ROW_COUNT, COL_COUNT, row, col);
            }
            else
            {
                if (TrisDoveFareMossa == TrisInputMossa)
                {
                    TrisDoveFareMossa = TrisProssimo;
                    mossaFatta = board.MakeMove(turno, ROW_COUNT, COL_COUNT, row, col);

                }
                else
                {
                    mossaFatta = false;
                }
            }

            // riutilizzo le variabili per tempo, 
            TrisInputMossa = TrisProssimo;
            COL_COUNT = TrisProssimo % 3;
            ROW_COUNT = 0;
            // e anche la riga del tris (miniBoard)
            while (TrisInputMossa > 2)
            {
                TrisInputMossa -= 3;
                ROW_COUNT++;
            }

            if (board.CheckWinMiniBoard(ROW_COUNT, COL_COUNT)) TrisDoveFareMossa = -1;

            if (!mossaFatta) return false;

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
