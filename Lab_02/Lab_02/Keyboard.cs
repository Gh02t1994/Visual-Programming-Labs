using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_02
{
    public class Keyboard : Panel
    {
        private FlowLayoutPanel keyboardLayout;

        private bool english = false;

        public string TypedText
        {
            get;
            private set;
        } = "";

        public event EventHandler TextUpdated;

        public Keyboard()
        {
            BackColor = Color.White;

            keyboardLayout = new FlowLayoutPanel();
            keyboardLayout.Dock = DockStyle.Fill;
            keyboardLayout.FlowDirection = FlowDirection.TopDown;

            Controls.Add(keyboardLayout);

            CreateKeyboard();
        }

        private void CreateKeyboard()
        {
            while (keyboardLayout.Controls.Count > 0)
            {
                Control control = keyboardLayout.Controls[0];

                keyboardLayout.Controls.RemoveAt(0);
                control.Dispose();
            }

            if (english == false)
            {
                AddRow("ЙЦУКЕНГШЩЗ");
                AddRow("ФЫВАПРОЛДЖЭ");
                AddRow("ЯЧСМИТЬБЮЁ");
            }
            else
            {
                AddRow("QWERTYUIOP");
                AddRow("ASDFGHJKL");
                AddRow("ZXCVBNM");
            }

            AddSpecialKeys();
        }

        private void AddRow(string letters)
        {
            FlowLayoutPanel row = new FlowLayoutPanel();

            row.Width = 550;
            row.Height = 50;
            row.Margin = new Padding(0);

            for (int i = 0; i < letters.Length; i++)
            {
                KeyboardKey key = CreateKey(letters[i].ToString(), 45);

                row.Controls.Add(key);
            }

            keyboardLayout.Controls.Add(row);
        }

        private void AddSpecialKeys()
        {
            FlowLayoutPanel row = new FlowLayoutPanel();

            row.Width = 550;
            row.Height = 50;
            row.WrapContents = false;
            row.Margin = new Padding(0);

            row.Controls.Add(CreateKey("RU/EN", 100));
            row.Controls.Add(CreateKey("SPACE", 325));
            row.Controls.Add(CreateKey("<=", 100));

            keyboardLayout.Controls.Add(row);
        }

        private KeyboardKey CreateKey(string text, int width)
        {
            KeyboardKey key = new KeyboardKey();

            key.Text = text;
            key.Size = new Size(width, 45);
            key.Margin = new Padding(2);

            key.Click += Key_Click;

            return key;
        }

        private void Key_Click(object sender, EventArgs e)
        {
            KeyboardKey key = (KeyboardKey)sender;

            if (key.Text == "RU/EN")
            {
                english = !english;
                CreateKeyboard();
            }
            else if (key.Text == "SPACE")
            {
                TypedText += " ";
                OnTextUpdated();
            }
            else if (key.Text == "<=")
            {
                if (TypedText.Length > 0)
                {
                    TypedText = TypedText.Substring(0, TypedText.Length - 1);

                    OnTextUpdated();
                }
            }
            else
            {
                TypedText += key.Text;
                OnTextUpdated();
            }
        }

        private void OnTextUpdated()
        {
            if (TextUpdated != null)
            {
                TextUpdated(this, EventArgs.Empty);
            }
        }
    }
}
