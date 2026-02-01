using cazzateeeee.Helpers;

namespace cazzateeeee.Forms
{
    /// <summary>
    /// Dialog per scegliere se essere Giocatore 1 o 2 in modalità EvE
    /// </summary>
    public partial class EvESetupDialog : Form
    {
        public bool SonoGiocatore1 { get; private set; }
        private ColorManager colorManager = new ColorManager();

        public EvESetupDialog()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Setup Modalità Bot vs Bot";
            this.Size = new Size(500, 300);
            this.BackColor = colorManager.coloreSfondo;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Titolo
            Label lblTitolo = new Label
            {
                Text = "🤖 Modalità Bot vs Bot (EvE)",
                Location = new Point(20, 20),
                Size = new Size(460, 40),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = colorManager.coloreTesto,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(lblTitolo);

            // Descrizione
            Label lblDescrizione = new Label
            {
                Text = "Scegli quale bot sei in questa partita.\n" +
                       "Il Giocatore 1 (X) fa la prima mossa.\n" +
                       "Il Giocatore 2 (O) aspetta la mossa dell'avversario.\n\n" +
                       "⚠️ Assicurati che l'altra istanza del gioco sia già aperta!",
                Location = new Point(20, 70),
                Size = new Size(460, 100),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(200, 200, 200),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.TopCenter
            };
            Controls.Add(lblDescrizione);

            // Pulsante Giocatore 1
            Button btnPlayer1 = new Button
            {
                Text = "🎯 Giocatore 1 (X)\nFaccio la prima mossa",
                Location = new Point(50, 180),
                Size = new Size(180, 60),
                BackColor = colorManager.coloreX,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnPlayer1.FlatAppearance.BorderSize = 0;
            btnPlayer1.Click += (s, e) =>
            {
                SonoGiocatore1 = true;
                DialogResult = DialogResult.OK;
                Close();
            };
            Controls.Add(btnPlayer1);

            // Pulsante Giocatore 2
            Button btnPlayer2 = new Button
            {
                Text = "⏳ Giocatore 2 (O)\nAspetto la prima mossa",
                Location = new Point(270, 180),
                Size = new Size(180, 60),
                BackColor = colorManager.coloreO,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnPlayer2.FlatAppearance.BorderSize = 0;
            btnPlayer2.Click += (s, e) =>
            {
                SonoGiocatore1 = false;
                DialogResult = DialogResult.OK;
                Close();
            };
            Controls.Add(btnPlayer2);
        }
    }
}