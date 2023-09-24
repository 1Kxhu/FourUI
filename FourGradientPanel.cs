using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FourUI
{
    public class FourGradientPanel : Panel
    {
        private Color gradientTopColor = Color.FromArgb(33, 133, 255);
        private Color gradientBottomColor = Color.Transparent; 
        private SelectionOption selection = SelectionOption.Vertical;

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The first gradient color.")]
        public Color GradientTopColor
        {
            get => gradientTopColor;
            set
            {
                gradientTopColor = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The second gradient color.")]
        public Color GradientBottomColor
        {
            get => gradientBottomColor;
            set
            {
                gradientBottomColor = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The way in which the gradient goes")]
        public SelectionOption UserChoice
        {
            get => selection;
            set
            {
                selection = value;
                Invalidate();
            }
        }

        public enum SelectionOption
        {
            Horizontal,
            Vertical
        }


        public FourGradientPanel()
        {
            DoubleBuffered = true; SetStyle(ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            

            if (gradientBottomColor == Color.Transparent)
            {
                using (var parentBrush = new SolidBrush(Parent.BackColor))
                {
                    e.Graphics.FillRectangle(parentBrush, ClientRectangle);
                }
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            LinearGradientMode mode;
            if (selection == SelectionOption.Horizontal)
            {
                mode = LinearGradientMode.Horizontal;
            }
            else
            {
                mode = LinearGradientMode.Vertical;
            }

            if (gradientTopColor.A < 255 || gradientBottomColor.A < 255)
            {
                using (var blendBrush = new LinearGradientBrush(ClientRectangle, gradientTopColor, gradientBottomColor, mode))
                {
                    ColorBlend blend = new ColorBlend();
                    blend.Positions = new[] { 0f, 1f };
                    blend.Colors = new[] { gradientTopColor, gradientBottomColor };
                    blendBrush.InterpolationColors = blend;

                    e.Graphics.FillRectangle(blendBrush, ClientRectangle);
                }
            }
            else
            {
                using (var brush = new LinearGradientBrush(ClientRectangle, gradientTopColor, gradientBottomColor, mode))
                {
                    e.Graphics.FillRectangle(brush, ClientRectangle);
                }
            }
        }

    }
}
