namespace cazzateeeee.AI
{
    /// <summary>
    /// Bot che fa mosse completamente casuali
    /// Usato per allenare l'AlberoPesato
    /// </summary>
    internal class RandomBot : IBot
    {
        private Random random;

        public RandomBot()
        {
            random = new Random();
        }

        public (int numTris, int row, int col)? CalcolaMossa(string boardState, int trisObbligatoria, char turno)
        {
            List<(int numTris, int row, int col)> mosseValide = TrovaMosseValide(boardState, trisObbligatoria);

            if (mosseValide.Count == 0)
                return null; // Nessuna mossa disponibile

            // Sceglie una mossa casuale tra quelle valide
            int indice = random.Next(mosseValide.Count);
            return mosseValide[indice];
        }

        public void NotificaRisultatoPartita(bool? haVinto)
        {
            // Il bot casuale non impara, quindi ignora il risultato
        }

        public void ResetPartita()
        {
            // Nessuno stato da resettare
        }

        /// <summary>
        /// Trova tutte le mosse valide dato lo stato corrente
        /// </summary>
        private List<(int numTris, int row, int col)> TrovaMosseValide(string boardState, int trisObbligatoria)
        {
            List<(int numTris, int row, int col)> mosse = new List<(int numTris, int row, int col)>();

            if (trisObbligatoria == -1)
            {
                // Mossa libera - controlla tutti i tris
                for (int numTris = 0; numTris < 9; numTris++)
                {
                    AggiungiMosseTris(mosse, boardState, numTris);
                }
            }
            else
            {
                // Deve giocare in un tris specifico
                AggiungiMosseTris(mosse, boardState, trisObbligatoria);
            }

            return mosse;
        }

        /// <summary>
        /// Aggiunge tutte le mosse valide per un tris specifico
        /// </summary>
        private void AggiungiMosseTris(List<(int numTris, int row, int col)> mosse, string boardState, int numTris)
        {
            // Ogni tris occupa 9 caratteri nella stringa boardState
            int offset = numTris * 9;

            for (int i = 0; i < 9; i++)
            {
                if (boardState[offset + i] == '-') // Cella vuota
                {
                    int row = i / 3;
                    int col = i % 3;
                    mosse.Add((numTris, row, col));
                }
            }
        }
    }
}