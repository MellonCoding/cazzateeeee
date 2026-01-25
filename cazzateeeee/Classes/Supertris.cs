namespace cazzateeeee.Classes
{
    internal class Supertris
    {
        private Tris[,] arrayOfMiniBoard;
        private char winner;

        public Supertris() 
        {
            arrayOfMiniBoard = new Tris[3, 3];
            winner = '-';

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    arrayOfMiniBoard[row, col] = new Tris();
                }
            }
        }

        // -------------------------------- HELPERS -------------------------------- //



        // -------------------------------- END HELPERS ---------------------------- // 
        
        // Metodo per fare la mossa
        // Argomenti player: Che player sta cercando di fare la mossa - tris/row/col posizione della mossa nel super tris
        public bool MakeMove(char player, int trisX, int trisY, int row, int col)
        {
            // se nessuno ha vinto per ora 
            if (winner == '-')
            {
                // Debug: Questa parte serve a capire secondo il programma chi ha vinto su un tris
                char won = arrayOfMiniBoard[trisX, trisY].CheckWin();
                // if (won != '-') MessageBox.Show($"{won}");

                // ritorno se la mossa nel tris (miniBoard(row, col)) nella posizione [TrisX, TrisX] e' valida o meno
                return arrayOfMiniBoard[trisX, trisY].MakeMove(player, row, col);
            }
            else
            {
                // se c'e' gia' un vincitore non lascio fare altre mosse
                return false;
            }
        }

        public char CheckWin()
        {
            /*
            il controllo e' sempre uguale cambiano solo le coordinate, la struttura e' questa -> faro' l'esempio con le righe

            winner = board[x,y].WonBy() == board[x+1,y].WonBy() && board[x+1,y].WonBy() == board[x+2,y].WonBy() ? board[x,y] : winner;
            il vincitore diventare (winner =) la prima casella che ho controllato (board[row, 0]) se 
            la prima casella e' uguale alla seconda (board[row, 0].wonBy() == board[row, 1].wonBy()) e
            la seconda casella e' uguale alle terza (board[row, 1].wonBy() == board[row, 2].wonBy())
            se questo non succede, allora winner rimane invariato (: winner)
            */

            // controllo vittoria righe
            for (int row = 0; row < 3; row++)
            {
                // se qualcuno e' ha fatto tris salto i prossimi controlli
                if (winner != '-') continue;
                winner = arrayOfMiniBoard[row, 0].wonBy() == arrayOfMiniBoard[row, 1].wonBy() && arrayOfMiniBoard[row, 1].wonBy() == arrayOfMiniBoard[row, 2].wonBy() ? arrayOfMiniBoard[row, 0].wonBy() : winner;
            }

            // controllo vittoria colonne
            for (int col = 0; col < 3; col++)
            {
                // se qualcuno e' ha fatto tris salto i prossimi controlli
                if (winner != '-') continue;
                winner = arrayOfMiniBoard[0, col].wonBy() == arrayOfMiniBoard[1, col].wonBy() && arrayOfMiniBoard[1, col].wonBy() == arrayOfMiniBoard[2, col].wonBy() ? arrayOfMiniBoard[0, col].wonBy() : winner;
            }

            // controllo vittoria diagonale verso -> \
            int x = 0, y = 0;

            winner = arrayOfMiniBoard[x, y].wonBy() == arrayOfMiniBoard[x + 1, y + 1].wonBy() && arrayOfMiniBoard[x + 1, y + 1].wonBy() == arrayOfMiniBoard[x + 2, y + 2].wonBy() ? arrayOfMiniBoard[x, y].wonBy() : winner;

            y += 2;

            // controllo vittoria diagonale verso -> /
            winner = arrayOfMiniBoard[x, y].wonBy() == arrayOfMiniBoard[x + 1, y - 1].wonBy() && arrayOfMiniBoard[x + 1, y - 1].wonBy() == arrayOfMiniBoard[x + 2, y - 2].wonBy() ? arrayOfMiniBoard[x, y].wonBy() : winner;
            
            // Debug
            Console.WriteLine("Supertris winner: " + winner);
            
            // ritorno chi ha vinto
            return winner;
        }
    }
}
