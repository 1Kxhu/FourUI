using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FourUI
{
    public partial class FourPanel : Control
    {
        private int cornerRadius = 5;
        private Color panelColor = Color.FromArgb(10, 10, 10);
        private Color borderColor = Color.FromArgb(45, 45, 45);
        private int borderWidth = 2;


        [Browsable(true)]
        [Category("FourUI")]
        [Description("The radius of the rounded corners.")]
        public int CornerRadius
        {
            get { return cornerRadius; }
            set
            {
                if (value == 0)
                {
                    value = 1;
                }
                cornerRadius = Math.Max(0, value);
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The color of the panel.")]
        public Color PanelColor
        {
            get { return panelColor; }
            set
            {

                panelColor = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The color of the border.")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {

                borderColor = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The thickness of the border.")]
        public int BorderWidth
        {
            get { return borderWidth; }
            set
            {

                borderWidth = value;
                Invalidate();
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20; return cp;
            }
        }

        public FourPanel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        [Browsable(true)]
        [Category("FourUI")]
        [Description("Controls the opacity of the panel. (paints over with opaque backcolor)")]
        public int Opacity
        {
            get => _opacity;
            set
            {
                _opacity = Math.Max(0, Math.Min(100, value));
            }
        }

        private int _opacity = 0;


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rawrect = e.ClipRectangle;
            rawrect.Inflate(-1, -1);

            GraphicsPath roundrect = RoundedRectangle(rawrect, cornerRadius);

            Pen borderpen = new Pen(borderColor);
            SolidBrush fillbrush = new SolidBrush(panelColor);

            g.FillPath(fillbrush, roundrect);
            g.DrawPath(borderpen, roundrect);

            Color semiTransparentColor = Color.FromArgb((int)(Opacity * 2.55), this.BackColor);

            using (SolidBrush semiTransparentBrush = new SolidBrush(semiTransparentColor))
            {
                e.Graphics.FillRectangle(semiTransparentBrush, this.ClientRectangle);
            }
        }

        private GraphicsPath RoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90); path.AddArc(rect.Right - 2 * radius, rect.Y, radius * 2, radius * 2, 270, 90); path.AddArc(rect.Right - 2 * radius, rect.Bottom - 2 * radius, radius * 2, radius * 2, 0, 90); path.AddArc(rect.X, rect.Bottom - 2 * radius, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();

            return path;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}
