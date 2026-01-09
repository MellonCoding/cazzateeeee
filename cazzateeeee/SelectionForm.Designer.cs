namespace cazzateeeee
{
    partial class SelectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnPlayerPlayer = new Button();
            label1 = new Label();
            btnPlayerBot = new Button();
            btnBotBot = new Button();
            SuspendLayout();
            // 
            // btnPlayerPlayer
            // 
            btnPlayerPlayer.Font = new Font("Cascadia Code SemiBold", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnPlayerPlayer.Location = new Point(50, 150);
            btnPlayerPlayer.Name = "btnPlayerPlayer";
            btnPlayerPlayer.Size = new Size(300, 50);
            btnPlayerPlayer.TabIndex = 0;
            btnPlayerPlayer.Text = "Player vs Player";
            btnPlayerPlayer.UseVisualStyleBackColor = true;
            btnPlayerPlayer.Click += btnPlayerPlayer_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cascadia Code SemiBold", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(50, 50);
            label1.Name = "label1";
            label1.Size = new Size(279, 63);
            label1.TabIndex = 1;
            label1.Text = "Supertris";
            // 
            // btnPlayerBot
            // 
            btnPlayerBot.Font = new Font("Cascadia Code SemiBold", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnPlayerBot.Location = new Point(50, 225);
            btnPlayerBot.Name = "btnPlayerBot";
            btnPlayerBot.Size = new Size(300, 50);
            btnPlayerBot.TabIndex = 2;
            btnPlayerBot.Text = "Player vs Bot";
            btnPlayerBot.UseVisualStyleBackColor = true;
            btnPlayerBot.Click += btnPlayerBot_Click;
            // 
            // btnBotBot
            // 
            btnBotBot.Font = new Font("Cascadia Code SemiBold", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnBotBot.Location = new Point(50, 300);
            btnBotBot.Name = "btnBotBot";
            btnBotBot.Size = new Size(300, 50);
            btnBotBot.TabIndex = 3;
            btnBotBot.Text = "Bot vs Bot";
            btnBotBot.UseVisualStyleBackColor = true;
            btnBotBot.Click += btnBotBot_Click;
            // 
            // SelectionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 500);
            Controls.Add(btnBotBot);
            Controls.Add(btnPlayerBot);
            Controls.Add(label1);
            Controls.Add(btnPlayerPlayer);
            Name = "SelectionForm";
            Text = "SelectionForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnPlayerPlayer;
        private Label label1;
        private Button btnPlayerBot;
        private Button btnBotBot;
    }
}