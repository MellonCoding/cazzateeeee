using System.Text;

namespace cazzateeeee.AI
{
    /// <summary>
    /// Bot che usa un albero pesato per apprendere le mosse migliori
    /// Apprende tramite reinforcement learning giocando partite
    /// </summary>
    internal class AlberoPesato : IBot
    {
        // Tabella che mappa stato -> pesi delle mosse
        // Chiave: stato della board (es. "---X-----O---...") + trisObbligatoria
        // Valore: dizionario di mosse con i loro pesi
        private Dictionary<string, Dictionary<string, float>> tabellaStati;

        // Percorso seguito durante la partita corrente
        private List<(string stato, string mossa)> percorsoPartita;

        // Random per esplorare mosse nuove (epsilon-greedy opzionale)
        private Random random;

        // Parametri di apprendimento
        private float incrementoVittoria = 0.1f;
        private float decrementoSconfitta = 0.1f;
        private float pesoIniziale = 0.5f; // Peso neutro per mosse mai viste

        public AlberoPesato()
        {
            tabellaStati = new Dictionary<string, Dictionary<string, float>>();
            percorsoPartita = new List<(string, string)>();
            random = new Random();
        }

        public (int numTris, int row, int col)? CalcolaMossa(string boardState, int trisObbligatoria, char turno)
        {
            // Crea la chiave dello stato corrente
            string chiaveStato = CreaChiaveStato(boardState, trisObbligatoria);

            // Trova tutte le mosse valide
            List<(int numTris, int row, int col)> mosseValide = TrovaMosseValide(boardState, trisObbligatoria);

            if (mosseValide.Count == 0)
                return null;

            // Assicurati che lo stato esista nella tabella
            if (!tabellaStati.ContainsKey(chiaveStato))
            {
                tabellaStati[chiaveStato] = new Dictionary<string, float>();
            }

            // Trova la mossa con il peso più alto
            string mossaMigliore = null;
            float pesoMigliore = float.MinValue;

            foreach (var mossa in mosseValide)
            {
                string chiaveMossa = CreaChiaveMossa(mossa.numTris, mossa.row, mossa.col);

                // Se la mossa non è mai stata vista, inizializza con peso neutro
                if (!tabellaStati[chiaveStato].ContainsKey(chiaveMossa))
                {
                    tabellaStati[chiaveStato][chiaveMossa] = pesoIniziale;
                }

                float peso = tabellaStati[chiaveStato][chiaveMossa];

                if (peso > pesoMigliore)
                {
                    pesoMigliore = peso;
                    mossaMigliore = chiaveMossa;
                }
            }

            // Salva lo stato e la mossa nel percorso
            percorsoPartita.Add((chiaveStato, mossaMigliore));

            // Decodifica la mossa dalla chiave
            return DecodificaMossa(mossaMigliore);
        }

        public void NotificaRisultatoPartita(bool? haVinto)
        {
            if (haVinto == null)
            {
                // Pareggio - nessun cambio ai pesi
                percorsoPartita.Clear();
                return;
            }

            float delta = haVinto.Value ? incrementoVittoria : -decrementoSconfitta;

            // Aggiorna i pesi di tutte le mosse nel percorso
            foreach (var (stato, mossa) in percorsoPartita)
            {
                if (tabellaStati.ContainsKey(stato) && tabellaStati[stato].ContainsKey(mossa))
                {
                    tabellaStati[stato][mossa] += delta;

                    // Mantieni i pesi in un range ragionevole (0.0 - 1.0)
                    tabellaStati[stato][mossa] = Math.Max(0.0f, Math.Min(1.0f, tabellaStati[stato][mossa]));
                }
            }

            // Pulisci il percorso per la prossima partita
            percorsoPartita.Clear();
        }

        public void ResetPartita()
        {
            percorsoPartita.Clear();
        }

        /// <summary>
        /// Salva i pesi appresi su file
        /// </summary>
        public void SalvaPesi(string percorsoFile)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(percorsoFile))
                {
                    foreach (var statoEntry in tabellaStati)
                    {
                        string stato = statoEntry.Key;

                        foreach (var mossaEntry in statoEntry.Value)
                        {
                            string mossa = mossaEntry.Key;
                            float peso = mossaEntry.Value;

                            // Formato: stato|mossa|peso
                            writer.WriteLine($"{stato}|{mossa}|{peso}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel salvare i pesi: {ex.Message}");
            }
        }

        /// <summary>
        /// Carica i pesi da file
        /// </summary>
        public void CaricaPesi(string percorsoFile)
        {
            try
            {
                if (!File.Exists(percorsoFile))
                    return;

                tabellaStati.Clear();

                using (StreamReader reader = new StreamReader(percorsoFile))
                {
                    string linea;
                    while ((linea = reader.ReadLine()) != null)
                    {
                        string[] parti = linea.Split('|');
                        if (parti.Length != 3)
                            continue;

                        string stato = parti[0];
                        string mossa = parti[1];
                        float peso = float.Parse(parti[2]);

                        if (!tabellaStati.ContainsKey(stato))
                        {
                            tabellaStati[stato] = new Dictionary<string, float>();
                        }

                        tabellaStati[stato][mossa] = peso;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel caricare i pesi: {ex.Message}");
            }
        }

        /// <summary>
        /// Ottieni statistiche sull'apprendimento
        /// </summary>
        public (int statiAppresi, int mosseApprese) OttieniStatistiche()
        {
            int statiAppresi = tabellaStati.Count;
            int mosseApprese = tabellaStati.Values.Sum(dict => dict.Count);
            return (statiAppresi, mosseApprese);
        }

        // ==================== METODI HELPER ==================== //

        private string CreaChiaveStato(string boardState, int trisObbligatoria)
        {
            return $"{boardState}:{trisObbligatoria}";
        }

        private string CreaChiaveMossa(int numTris, int row, int col)
        {
            return $"T{numTris}R{row}C{col}";
        }

        private (int numTris, int row, int col) DecodificaMossa(string chiaveMossa)
        {
            // Formato: "T4R1C2" -> numTris=4, row=1, col=2
            int numTris = int.Parse(chiaveMossa.Substring(1, 1));
            int row = int.Parse(chiaveMossa.Substring(3, 1));
            int col = int.Parse(chiaveMossa.Substring(5, 1));
            return (numTris, row, col);
        }

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

        private void AggiungiMosseTris(List<(int numTris, int row, int col)> mosse, string boardState, int numTris)
        {
            int offset = numTris * 9;

            for (int i = 0; i < 9; i++)
            {
                if (boardState[offset + i] == '-')
                {
                    int row = i / 3;
                    int col = i % 3;
                    mosse.Add((numTris, row, col));
                }
            }
        }
    }
}