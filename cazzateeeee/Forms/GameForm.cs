using cazzateeeee.Classes;
using cazzateeeee.Helpers;
using cazzateeeee.AI;

namespace cazzateeeee
{
    public partial class GameForm : Form
    {
        private GameManager gm;
        private Form FormIniziale;
        private Button[,] buttons;  // Array per accesso rapido ai bottoni
        private Label lblTurno;
        private Label lblInfo;
        private int bot;            // se bot é utilizzato 1 = AlberoPesato 2 = Algoritmico
        private AlberoPesato botAllenato;
        private ColorManager colorManager = new ColorManager();
            
        public GameForm(int mod, Form SelectionForm, int modBot)
        {
            InitializeComponent();

            BackColor = colorManager.coloreSfondo;
            Size = new Size(550, 620);
            StartPosition = FormStartPosition.CenterScreen;

            buttons = new Button[9, 9];  // 9 mini-tris, ognuno con 9 celle

            InitializeUI();

            gm = new GameManager();
            bot = modBot;

            switch (mod)
            {
                case 0:
                    gm.StartGamePVP();
                    break;
                case 1:
                    gm.StartGamePVE();
                    if (bot == 1)
                    {
                        botAllenato = new AlberoPesato();
                        botAllenato.CaricaPesi("supertris_bot.weights");
                    }
                    else
                    { 
                        // qui ci va il bot algoritmico
                    }
                    break;
                case 2:
                    gm.StartGameEVE();
                    if (bot == 1)
                    {
                        botAllenato = new AlberoPesato();
                        botAllenato.CaricaPesi("supertris_bot.weights");
                    }
                    else
                    {
                        // qui ci va il bot algoritmico
                    }
                    break;
            }

            this.FormIniziale = SelectionForm;
            AggiornaVisualizzazione();
        }

        internal void InitializeUI()
        {
            const int BUTTON_SIZE = 50;
            const int BUTTON_MARGIN = 2;
            const int TRIS_SPACING = 10;
            const int START_X = 30;
            const int START_Y = 80;

            // Label per info mossa
            lblInfo = new Label
            {
                Location = new Point(START_X + 100, 25),
                Size = new Size(400, 20),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(150, 150, 150),
                BackColor = Color.Transparent,
                Text = "Mossa libera - Gioca dove vuoi!"
            };
            Controls.Add(lblInfo);

            // Label per il turno
            lblTurno = new Label
            {
                Location = new Point(START_X, 20),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = colorManager.coloreTesto,
                BackColor = Color.Transparent,
                Text = "Turno: X"
            };
            Controls.Add(lblTurno);

            // Creazione griglia di bottoni
            for (int numTris = 0; numTris < 9; numTris++)
            {
                int trisRow = numTris / 3;
                int trisCol = numTris % 3;

                // Crea un panel per ogni mini-tris per raggruppare visivamente
                Panel trisPanel = new Panel
                {
                    Location = new Point(
                        START_X + trisCol * (BUTTON_SIZE * 3 + BUTTON_MARGIN * 2 + TRIS_SPACING),
                        START_Y + trisRow * (BUTTON_SIZE * 3 + BUTTON_MARGIN * 2 + TRIS_SPACING)
                    ),
                    Size = new Size(BUTTON_SIZE * 3 + BUTTON_MARGIN * 2, BUTTON_SIZE * 3 + BUTTON_MARGIN * 2),
                    BackColor = colorManager.coloreTrisNormale,
                    Tag = $"Panel{numTris}"
                };
                Controls.Add(trisPanel);

                // Crea i 9 bottoni del mini-tris
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        Button btn = new Button
                        {
                            Text = "",
                            Location = new Point(
                                col * (BUTTON_SIZE + BUTTON_MARGIN),
                                row * (BUTTON_SIZE + BUTTON_MARGIN)
                            ),
                            Size = new Size(BUTTON_SIZE, BUTTON_SIZE),
                            Tag = $"Tris{numTris}Row{row}Col{col}",
                            FlatStyle = FlatStyle.Flat,
                            BackColor = Color.FromArgb(55, 55, 58),
                            ForeColor = colorManager.coloreTesto,
                            Font = new Font("Segoe UI", 14, FontStyle.Bold),
                            Cursor = Cursors.Hand
                        };

                        btn.FlatAppearance.BorderSize = 1;
                        btn.FlatAppearance.BorderColor = colorManager.coloreBordo;

                        // Eventi hover
                        btn.MouseEnter += (s, e) =>
                        {
                            if (btn.Text == "")
                                btn.BackColor = colorManager.coloreHover;
                        };

                        btn.MouseLeave += (s, e) =>
                        {
                            if (btn.Text == "")
                                btn.BackColor = Color.FromArgb(55, 55, 58);
                        };

                        btn.Click += Mossa;
                        trisPanel.Controls.Add(btn);

                        // Salva riferimento al bottone
                        int buttonIndex = row * 3 + col;
                        buttons[numTris, buttonIndex] = btn;
                    }
                }
            }
        }

        private void AggiornaVisualizzazione()
        {
            // Aggiorna label turno
            lblTurno.Text = $"Turno: {gm.GetTurno()}";
            lblTurno.ForeColor = gm.GetTurno() == 'X' ? colorManager.coloreX : colorManager.coloreO;

            int prossimoTris = gm.GetProssimaTrisObbligatoria();

            if (prossimoTris == -1)
            {
                lblInfo.Text = "Mossa libera!";
                lblInfo.ForeColor = Color.FromArgb(100, 200, 100);
            }
            else
            {
                lblInfo.Text = $"Devi giocare nel tris #{prossimoTris + 1}";
                lblInfo.ForeColor = Color.FromArgb(255, 200, 100);
            }

            // Evidenzia i tris disponibili
            for (int i = 0; i < 9; i++)
            {
                Panel? panel = Controls.Find($"Panel{i}", false).FirstOrDefault() as Panel;
                if (panel != null)
                {
                    if (prossimoTris == -1)
                    {
                        // Mossa libera - tutti i tris disponibili
                        panel.BackColor = colorManager.coloreTrisNormale;
                    }
                    else if (i == prossimoTris)
                    {
                        // Questo è il tris dove si deve giocare
                        panel.BackColor = colorManager.coloreTrisAttivo;
                    }
                    else
                    {
                        // Tris non disponibile
                        panel.BackColor = colorManager.coloreTrisCompletato;
                    }
                }
            }
        }

        private void Mossa(object? sender, EventArgs e)
        {
            if (sender is not Button btn)
            {
                MessageBox.Show("Errore interno del gioco. Riavvia l'applicazione.");
                return;
            }

            string tag = btn.Tag?.ToString() ?? "";
            List<int> numeriTag = new List<int>();

            foreach (char c in tag)
            {
                if (char.IsDigit(c))
                {
                    numeriTag.Add(int.Parse(c.ToString()));
                }
            }

            if (numeriTag.Count != 3) return;

            int numTris = numeriTag[0];
            int row = numeriTag[1];
            int col = numeriTag[2];

            if (gm.MakeMove(numTris, row, col))
            {
                // Mossa valida - aggiorna la UI
                char turnoAttuale = gm.GetTurno();
                btn.Text = turnoAttuale.ToString();
                btn.ForeColor = turnoAttuale == 'X' ? colorManager.coloreX : colorManager.coloreO;
                btn.Enabled = false;
                btn.BackColor = Color.FromArgb(40, 40, 43);

                // Scrivi su file
                FileManager.Write($"{turnoAttuale} {numTris}{row}{col}");

                // Controlla vittoria
                char vincitore = gm.CheckWin();
                if (vincitore != '-')
                {
                    string nomeVincitore = vincitore == 'X' ? "X" : "O";
                    Color coloreVincitore = vincitore == 'X' ? colorManager.coloreX : colorManager.coloreO;

                    lblInfo.Text = $"🎉 {nomeVincitore} ha vinto! 🎉";
                    lblInfo.ForeColor = coloreVincitore;
                    lblInfo.Font = new Font("Segoe UI", 12, FontStyle.Bold);

                    MessageBox.Show(
                        $"Complimenti! {nomeVincitore} ha vinto la partita!",
                        "Fine Partita",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Disabilita tutti i bottoni
                    foreach (Control ctrl in Controls)
                    {
                        if (ctrl is Panel panel)
                        {
                            foreach (Control btnCtrl in panel.Controls)
                            {
                                if (btnCtrl is Button b)
                                    b.Enabled = false;
                            }
                        }
                    }
                    return;
                }

                // Cambia turno
                gm.CambiaTurno();

                // Aggiorna visualizzazione
                AggiornaVisualizzazione();
            }
            else
            {
                // Mossa non valida - feedback visivo
                lblInfo.Text = "❌ Mossa non valida! Controlla dove puoi giocare.";
                lblInfo.ForeColor = Color.FromArgb(255, 100, 100);

                // Animazione shake del bottone (opzionale)
                Task.Run(async () =>
                {
                    for (int i = 0; i < 3; i++)
                    {
                        await Task.Delay(50);
                        btn.Invoke(() => btn.BackColor = Color.FromArgb(150, 50, 50));
                        await Task.Delay(50);
                        btn.Invoke(() => btn.BackColor = Color.FromArgb(55, 55, 58));
                    }
                });
            }
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormIniziale.Close();
        }
    }
}