using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_02
{
    public partial class Form1 : Form
    {
        private Keyboard keyboard;
        private TextBox textBox;

        public Form1()
        {
            InitializeComponent();

            Text = "Экранная клавиатура";
            ClientSize = new Size(550, 300);

            textBox = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                Dock = DockStyle.Top,
                Height = 90,
                Font = new Font("Arial", 14)
            };

            keyboard = new Keyboard
            {
                Dock = DockStyle.Fill
            };

            keyboard.TextUpdated += Keyboard_TextUpdated;

            Controls.Add(keyboard);
            Controls.Add(textBox);
        }

        private void Keyboard_TextUpdated(object sender, EventArgs e)
        {
            textBox.Text = keyboard.TypedText;

            textBox.SelectionStart = textBox.Text.Length;
            textBox.ScrollToCaret();
        }
    }
}
