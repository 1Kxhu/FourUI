using System;
using System.Drawing;
using System.Windows.Forms;

namespace FourUI
{
    public partial class FourLabel : Control
    {
        public FourLabel()
        {
            DoubleBuffered = true;
        }

        public enum textalignment
        {
            Left,
            Center,
            Right

        }

        private textalignment textalignmentenum = textalignment.Left;
        public textalignment TextAlignment { get { return textalignmentenum; } set { textalignmentenum = value; Invalidate(); } }

        protected override void OnPaint(PaintEventArgs e)
        {
            var a = e.Graphics;

            a.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (TextAlignment == textalignment.Left)
            {
                a.DrawString(Text, Font, new SolidBrush(ForeColor), 0, 0);
            }
            else if (TextAlignment == textalignment.Center)
            {
                a.DrawString(Text, Font, new SolidBrush(ForeColor), (Width - a.MeasureString(Text, Font).Width) / 2, 0);
            }
            else if (TextAlignment == textalignment.Right)
            {
                a.DrawString(Text, Font, new SolidBrush(ForeColor), (Width - a.MeasureString(Text, Font).Width), 0);
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }
    }
}
