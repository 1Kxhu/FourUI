using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FourUI
{
    public partial class FourLabel : Control
    {
        public FourLabel()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
        }

        public enum TextAlignment
        {
            Left,
            Center,
            Right
        }

        private string[] lines = new string[0];

        [Category("FourUI")]
        public new string Text
        {
            get { return string.Join(Environment.NewLine, lines); }
            set
            {
                TextLines = value.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            }
        }

        [Category("FourUI")]
        public string[] TextLines
        {
            get { return lines; }
            set
            {
                if (value != null)
                    lines = value;
                else
                    lines = new string[0];

                Invalidate();
            }
        }

        private TextAlignment textAlignmentEnum = TextAlignment.Left;

        public TextAlignment TextAlign
        { get
            {
                return textAlignmentEnum;
            } 
            set 
            { 
                textAlignmentEnum = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Rectangle crect = ClientRectangle;
            crect.Inflate(5,5);
            crect.Offset(-2,-2);

            if (BackColor != Color.Transparent)
            {
                e.Graphics.FillRectangle(new SolidBrush(BackColor),crect);
            }

            StringFormat stringFormat = new StringFormat();
            if (TextAlign == TextAlignment.Center)
                stringFormat.Alignment = StringAlignment.Center;
            else if (TextAlign == TextAlignment.Right)
                stringFormat.Alignment = StringAlignment.Far;

            stringFormat.LineAlignment = StringAlignment.Near;

            using (var brush = new SolidBrush(ForeColor))
            {
                RectangleF rect = new RectangleF(0, 0, Width, Height);

                string[] lines = Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                for (int i = 0; i < lines.Length; i++)
                {
                    float y = i * e.Graphics.MeasureString(lines[i], Font).Height;
                    rect.Y = y;
                    e.Graphics.DrawString(lines[i], Font, brush, rect, stringFormat);
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                if (Parent != null)
                {
                    using (var bmp = new Bitmap(Parent.Width * 2, Parent.Height * 2))
                    {
                        Parent.Controls.Cast<Control>().Where(c => Parent.Controls.GetChildIndex(c) > Parent.Controls.GetChildIndex(this))
                            .ToList()
                            .ForEach(c => c.DrawToBitmap(bmp, new Rectangle(c.Bounds.X - 1, c.Bounds.Y - 1, c.Width + 1, c.Height + 1)));

                        e.Graphics.DrawImage(bmp, -Left, -Top);
                    }
                }
                else
                {
                    base.OnPaintBackground(e);
                }
            }
            catch
            {
                base.OnPaintBackground(e);
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }
    }
}
