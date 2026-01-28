using cazzateeeee.Classes;
using cazzateeeee.Helpers;

namespace cazzateeeee
{
    public partial class GameForm : Form
    {
        private GameManager gm;
        private Form FormInizale;

        public GameForm(int mod, Form SelectionForm, ThemeManager tm)
        {
            // cose da designer 
            InitializeComponent();

            // inizialize la UI, quindi bottoni label e altre cose
            InitializeUI();

            // "Creo il gioco"
            gm = new GameManager();

            // in base alla modalitá sceltá dal giocatore avvio il gioco senza bot (0) con 1 bot (1) con 2 bot (2)
            switch (mod)
            {
                case 0:
                    gm.StartGamePVP();
                    break;
                case 1:
                    gm.StartGamePVE();
                    break;
                case 2:
                    gm.StartGameEVE();
                    break;
            }

            this.FormInizale = SelectionForm;
        }

        internal void InitializeUI()
        {
            int BUTTON_SIZE = 30;

            //int OFFSET_TRIS_TO_TRIS = 120, 
            //    DISTANZA_DAL_MARGINE = 30,
            //    DISTANZA_TETTO_STRIS = 40;
            //    Location = new Point(DISTANZA_DAL_MARGINE + (OFFSET_TRIS_TO_TRIS * (num_tris % 3)) + (40 * num_col), 80 + (OFFSET_TRIS_TO_TRIS * y_tris) + (40 * num_row)),


            for (int num_tris = 0, y_tris = 0; num_tris < 9; num_tris++)
            {
                for (int num_row = 0; num_row < 3; num_row++)
                {
                    for (int num_col = 0; num_col < 3; num_col++)
                    {
                        if (num_tris > 5) y_tris = 2; else if (num_tris > 2) y_tris = 1; else y_tris = 0;

                        // crea bottone
                        Button btn = new Button
                        {
                            Text = "",
                            Location = new Point(30 + (120 * (num_tris % 3)) + (40 * num_col), 80 + (120 * y_tris) + (40 * num_row)),
                            Size = new Size(BUTTON_SIZE, BUTTON_SIZE),
                            Tag = $"Tris{num_tris}Row{num_row}Col{num_col}"
                        };

                        // aggiungi ai sender e dai il metodo mossa
                        btn.Click += Mossa;
                        Controls.Add(btn);
                    }
                }
            }
        }

        private void Mossa(object? sender, EventArgs e)
        {
            if (sender is Button btn && sender != null)
            {
                string Tag = btn.Tag.ToString(); 
                int i = 0;
                int[] NumeriTag = new int[3];
                char won;

                foreach (char c in Tag)
                {
                    if (char.IsDigit(c))
                    { 
                        NumeriTag[i] = (int.Parse(c.ToString()));
                        i++;
                    }
                }

                if (gm.MakeMove(NumeriTag[0], NumeriTag[1], NumeriTag[2])) //8 0 0 
                {
                    // mostro all' utente la mossa
                    btn.Text = gm.GetTurno().ToString();

                    // controllo se qualcuno ha vinto
                    won = gm.CheckWin();

                    // scrivo su file la mossa
                    FileManager.Write($"{gm.GetTurno()} {NumeriTag[0]}{NumeriTag[1]}{NumeriTag[2]}");
                    
                    // se qualcuno vince lo mostro
                    if (won != '-')
                    {
                        MessageBox.Show($"{won} ah vinto");
                    }

                    // cambio il turno per il programma 
                    gm.CambiaTurno();
                }
            }
            else
            {
                MessageBox.Show("Ci e' stato un problema socio, riavvia, se persiste flamma mellon");
            }
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormInizale.Close();
        }
    }
}
