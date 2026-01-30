
/// FILE MANAGER:
/// - Start: Se il file al percorso "persorso" non esiste lo crea se esiste lo svuta, 
///          questa funzione deve avere SOLO 1 reference o c'é un problema.
/// - Write: Prende una stringa e la scrive su file, per ora non ci sono controlli, sarebbe meglio fare qualche controllo in effetti.

namespace cazzateeeee
{
    public static class FileManager
    {
        private static string percorso = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "mosse.txt");

        public static bool Start()
        { 
            try
            {
                // Crea o svuota il file
                File.WriteAllText(percorso, string.Empty);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        // SalvaFile.Scrivi($"{giocatoreCorrente} ({NumTris}{NumRiga},{NumColonna})");
        public static void Write(string testo)
        {
            // MessageBox.Show(percorso);
            using (StreamWriter sw = new StreamWriter(percorso, true))
            {
                sw.WriteLine(testo);
            }
        }
    }
}