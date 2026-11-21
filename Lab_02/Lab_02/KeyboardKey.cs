using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_02
{
    public class KeyboardKey : Control
    {
        public KeyboardKey()
        {
            Size = new Size(45, 45);
            BackColor = Color.LightGray;
            ForeColor = Color.Black;
            Font = new Font("Arial", 12, FontStyle.Regular);
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);

            e.Graphics.DrawRectangle(Pens.Gray, 0, 0, Width - 1, Height - 1);

            TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            Font = new Font("Arial", 15, FontStyle.Bold);
            BackColor = Color.LightBlue;

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            Font = new Font("Arial", 12, FontStyle.Regular);
            BackColor = Color.LightGray;

            Invalidate();
        }
    }
}
