using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FourUI
{
    public class FourGradientPanel : Panel
    {
        private Color gradientTopColor = Color.Blue; // Default top color
        private Color gradientBottomColor = Color.Transparent; // Default bottom color

        public Color GradientTopColor
        {
            get => gradientTopColor;
            set
            {
                gradientTopColor = value;
                Invalidate(); // Redraw the panel when the top color changes
            }
        }

        public Color GradientBottomColor
        {
            get => gradientBottomColor;
            set
            {
                gradientBottomColor = value;
                Invalidate(); // Redraw the panel when the bottom color changes
            }
        }

        public FourGradientPanel()
        {
            DoubleBuffered = true; // Enable double-buffering for smoother rendering
            SetStyle(ControlStyles.ResizeRedraw, true); // Redraw when resizing
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Check if the bottom color is transparent
            if (gradientBottomColor == Color.Transparent)
            {
                // If it is, draw the background of the parent control (e.g., the form) first
                using (var parentBrush = new SolidBrush(Parent.BackColor))
                {
                    e.Graphics.FillRectangle(parentBrush, ClientRectangle);
                }
            }

            // Use a blend if either top or bottom color is transparent
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
                // Use a regular LinearGradientBrush if both colors are opaque
                using (var brush = new LinearGradientBrush(ClientRectangle, gradientTopColor, gradientBottomColor, LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, ClientRectangle);
                }
            }
        }
    }
}
