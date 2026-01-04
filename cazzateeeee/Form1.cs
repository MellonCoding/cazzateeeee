using cazzateeeee.Classes;

namespace cazzateeeee
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeUI();
            InitilizeGame();
        }
        private static readonly char[] trimChars = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        internal void InitializeUI()
        {
            int BUTTON_SIZE = 30;

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
                            Location = new Point(10 + (120 * (num_tris % 3)) + (40 * num_col), 80 + (120 * y_tris) + (40 * num_row)),
                            Size = new Size(BUTTON_SIZE, BUTTON_SIZE),
                            Tag = $"Tris{num_tris}Row{num_row}Col{num_col}"
                        };

                        // aggiungi ai sender
                        btn.Click += Mossa; 
                        Controls.Add(btn);
                    }
                }   
            }
        }

        private void Mossa(object? sender, EventArgs e)
        {
            // in questa funzione devo prendere il tag che ha questa forma {Tris0Raw1Col2} e devo spezzarlo in 3 string 
            // cosí {Tris0} {Raw1} {Col2}
            string StringaTag = (sender as Button).Tag.ToString();
            string[] StringeTag = StringaTag.Split('0', '1', '2');

            foreach (string str in StringeTag)
            {
                MessageBox.Show(str);
            }
        }

        internal void InitilizeGame()
        {
            Supertris st = new Supertris();
        }
    }
}
