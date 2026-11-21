using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnDoIt_Click(object sender, EventArgs e)
        {
            int key;

            if (!int.TryParse(txtKey.Text, out key))
            {
                MessageBox.Show("Некорректный ключ");
                return;
            }

            if (cmbAction.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите действие!");
                return;
            }

            bool encrypt = cmbAction.SelectedItem.ToString() == "Encrypt";

            txtText.Text = Caesar_encryption(txtText.Text, key, encrypt);

        }

        private string Caesar_encryption(string text, int key, bool encrypt)
        {
            string res = "";

            key %= 26;
            
            if (!encrypt)
            {
                key = -key;
            }

            foreach (char sym in text)
            {
                if (sym >= 'A' && sym <= 'Z')
                {
                    int ind = sym - 'A';
                    int new_ind = (ind + key + 26) % 26;
                    res += (char)('A' + new_ind);
                }
                else if (sym >= 'a' && sym <= 'z')
                {
                    int ind = sym - 'a';
                    int new_ind = (ind + key + 26) % 26;
                    res += (char)('a' + new_ind);
                }
                else
                {
                    res += sym;
                }
            }

            return res;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtText.Text = File.ReadAllText(dialog.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            dialog.DefaultExt = "txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dialog.FileName, txtText.Text);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = dialog.Color;
            }
        }

        private void buttonStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (btnDoIt.FlatStyle == FlatStyle.Standard)
            {
                btnDoIt.FlatStyle = FlatStyle.Flat;
            }
            else
            {
                btnDoIt.FlatStyle = FlatStyle.Standard;
            }
        }
    }
}
