using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FourUI
{
    public class FourGradientPanel : Panel
    {
        private Color gradientTopColor = Color.Blue; private Color gradientBottomColor = Color.Transparent;
        public Color GradientTopColor
        {
            get => gradientTopColor;
            set
            {
                gradientTopColor = value;
                Invalidate();
            }
        }

        public Color GradientBottomColor
        {
            get => gradientBottomColor;
            set
            {
                gradientBottomColor = value;
                Invalidate();
            }
        }

        public FourGradientPanel()
        {
            DoubleBuffered = true; SetStyle(ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (gradientBottomColor == Color.Transparent)
            {
                using (var parentBrush = new SolidBrush(Parent.BackColor))
                {
                    e.Graphics.FillRectangle(parentBrush, ClientRectangle);
                }
            }

            if (gradientTopColor.A < 255 || gradientBottomColor.A < 255)
            {
                using (var blendBrush = new LinearGradientBrush(ClientRectangle, gradientTopColor, gradientBottomColor, LinearGradientMode.Vertical))
                {
                    ColorBlend blend = new ColorBlend();
                    blend.Positions = new[] { (float)0, (float)1 };
                    blend.Colors = new[] { gradientTopColor, gradientBottomColor };
                    blendBrush.InterpolationColors = blend;

                    e.Graphics.FillRectangle(blendBrush, ClientRectangle);
                }
            }
            else
            {
                using (var brush = new LinearGradientBrush(ClientRectangle, gradientTopColor, gradientBottomColor, LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, ClientRectangle);
                }
            }
        }
    }
}
