using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cazzateeeee
{
    public static class FileManager
    {
        private static string percorso = Environment.CurrentDirectory + "/mosse.txt";

        // Scrive una riga nel file, aggiungendola in coda
        public static void Write(string testo)
        {
            MessageBox.Show(percorso);

            using (StreamWriter sw = new StreamWriter(percorso, true))
            {
                sw.WriteLine(testo);
            }
        }

        // SalvaFile.Scrivi($"{giocatoreCorrente} ({NumTris}{NumRiga},{NumColonna})");
    }
}