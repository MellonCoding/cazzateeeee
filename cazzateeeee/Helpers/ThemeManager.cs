using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cazzateeeee.Helpers
{
    public sealed class Theme
    {
        public Color Back { get; init; }
        public Color Fore { get; init; }
        public Color Surface { get; init; }          // pannelli, groupbox, ecc.
        public Color Border { get; init; }
        public Color Accent { get; init; }
        public Color AccentText { get; init; }
        public Color MutedText { get; init; }

        public Font FontRegular { get; init; }
        public Font FontBold { get; init; }

        public static Theme Dark() => new Theme
        {
            Back = Color.FromArgb(18, 18, 20),
            Fore = Color.Gainsboro,
            Surface = Color.FromArgb(26, 26, 30),
            Border = Color.FromArgb(55, 55, 65),
            Accent = Color.FromArgb(80, 140, 255),
            AccentText = Color.White,
            MutedText = Color.FromArgb(160, 160, 170),
            FontRegular = SystemFonts.MessageBoxFont,
            FontBold = new Font(SystemFonts.MessageBoxFont, FontStyle.Bold),
        };

        public static Theme Light() => new Theme
        {
            Back = Color.White,
            Fore = Color.FromArgb(20, 20, 22),
            Surface = Color.FromArgb(245, 245, 248),
            Border = Color.FromArgb(210, 210, 220),
            Accent = Color.FromArgb(40, 110, 255),
            AccentText = Color.White,
            MutedText = Color.FromArgb(100, 100, 110),
            FontRegular = SystemFonts.MessageBoxFont,
            FontBold = new Font(SystemFonts.MessageBoxFont, FontStyle.Bold),
        };
    }

    public sealed class ThemeManager
    {
        public Theme Current { get; private set; }

        public ThemeManager(Theme theme) => Current = theme;

        public void SetTheme(Theme theme)
        {
            Current = theme;
        }

        /// <summary>
        /// Applica il tema a un controllo root (tipicamente il Form).
        /// </summary>
        public void Apply(Control root)
        {
            if (root == null) return;

            // Per ridurre flicker mentre applichi tutto
            root.SuspendLayout();
            try
            {
                ApplyToControlRecursive(root);
            }
            finally
            {
                root.ResumeLayout(true);
                root.Invalidate(true);
            }
        }

        private void ApplyToControlRecursive(Control c)
        {
            ApplySingle(c);

            foreach (Control child in c.Controls)
                ApplyToControlRecursive(child);
        }

        private void ApplySingle(Control c)
        {
            // Font di base
            c.Font = Current.FontRegular;

            switch (c)
            {
                case Form f:
                    f.BackColor = Current.Back;
                    f.ForeColor = Current.Fore;
                    f.Font = Current.FontRegular;
                    break;

                case Panel p:
                    p.BackColor = Current.Surface;
                    p.ForeColor = Current.Fore;
                    break;

                case Label l:
                    l.BackColor = Color.Transparent;
                    l.ForeColor = Current.Fore;
                    break;

                case Button b:
                    StyleButton(b);
                    break;

                case TextBox tb:
                    StyleTextBox(tb);
                    break;

                case RichTextBox rtb:
                    rtb.BackColor = Current.Surface;
                    rtb.ForeColor = Current.Fore;
                    rtb.BorderStyle = BorderStyle.FixedSingle;
                    break;

            }
        }

        private void StyleButton(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 1;
            b.FlatAppearance.BorderColor = Current.Border;

            b.BackColor = Current.Surface;
            b.ForeColor = Current.Fore;

            // “Primary” semplice: se Tag contiene "primary"
            if (b.Tag is string s && s.Equals("primary", StringComparison.OrdinalIgnoreCase))
            {
                b.BackColor = Current.Accent;
                b.ForeColor = Current.AccentText;
                b.FlatAppearance.BorderColor = Current.Accent;
            }

            // Hover
            b.MouseEnter -= Button_MouseEnter;
            b.MouseLeave -= Button_MouseLeave;
            b.MouseEnter += Button_MouseEnter;
            b.MouseLeave += Button_MouseLeave;
        }

        private void Button_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is not Button b) return;

            // Se primary, schiarisci leggermente
            bool primary = b.Tag is string s && s.Equals("primary", StringComparison.OrdinalIgnoreCase);

            b.BackColor = primary
                ? ControlPaint.Light(Current.Accent, 0.15f)
                : ControlPaint.Light(Current.Surface, 0.10f);
        }

        private void Button_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is not Button b) return;

            bool primary = b.Tag is string s && s.Equals("primary", StringComparison.OrdinalIgnoreCase);

            b.BackColor = primary ? Current.Accent : Current.Surface;
            b.ForeColor = primary ? Current.AccentText : Current.Fore;
            b.FlatAppearance.BorderColor = primary ? Current.Accent : Current.Border;
        }

        private void StyleTextBox(TextBox tb)
        {
            tb.BackColor = Current.Surface;
            tb.ForeColor = Current.Fore;
            tb.BorderStyle = BorderStyle.FixedSingle;

            // Placeholder “manuale” (se vuoi) si fa con eventi, qui non lo forzo
        }
    }
}