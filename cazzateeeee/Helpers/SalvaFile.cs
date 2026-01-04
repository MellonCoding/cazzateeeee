using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cazzateeeee
{
    public static class SalvaFile
    {
        private static string percorso = "mosse.txt";

        // Scrive una riga nel file, aggiungendola in coda
        public static void Scrivi(string testo)
        {
            using (StreamWriter sw = new StreamWriter(percorso, true))
            {
                sw.WriteLine(testo);
            }
        }

        // SalvaFile.Scrivi($"{giocatoreCorrente} ({rigaGlobale},{colGlobale})");
    }
}