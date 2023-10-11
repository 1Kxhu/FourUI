using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows;
using System.Windows.Forms;

namespace FourUI
{
    public partial class FourBorder : Component
    {
        private Form targetForm;
        private int borderWidth = 1; private Color borderColor = Color.Red; private int borderRadius = 5;

        public FourBorder()
        {
            InitializeComponent();
        }

        public FourBorder(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Disposed += FourBorder_Disposed;
        }

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The radius of the rounding.")]
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value;
                if (targetForm != null)
                {
                    targetForm.Invalidate();
                }
            }
        }

        [Browsable(true)]
        [Description("Select the target form to apply the border.")]
        [Category("Behavior")]
        public Form TargetForm
        {
            get { return targetForm; }
            set
            {
                if (value != null && value != targetForm)
                {
                    if (targetForm != null)
                    {
                        targetForm.Paint -= TargetForm_Paint;
                        targetForm.Resize -= TargetForm_Resize;
                        targetForm.Invalidate();
                    }

                    targetForm = value;
                    targetForm.Paint += TargetForm_Paint;
                    targetForm.Resize += TargetForm_Resize;
                }
            }
        }

        private GraphicsPath RoundedRectangle(int x, int y, int width, int height, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.StartFigure();
            path.AddArc(x, y, diameter, diameter, 180, 90);
            path.AddArc(x + width - diameter, y, diameter, diameter, 270, 90);
            path.AddArc(x + width - diameter, y + height - diameter, diameter, diameter, 0, 90);
            path.AddArc(x, y + height - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();

            return path;
        }

        private void TargetForm_Paint(object sender, PaintEventArgs e)
        {

            if (targetForm != null)
            {
                if (BorderRadius > 0)
                {
                    using (Pen borderPen = new Pen(borderColor, borderWidth))
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        e.Graphics.DrawPath(borderPen, RoundedRectangle(0, 0, targetForm.Width - 1, targetForm.Height - 1, BorderRadius * 2));
                    }
                }
                else
                {
                    using (Pen borderPen = new Pen(borderColor, borderWidth))
                    {
                        Rectangle rektangel = new Rectangle(0, 0, targetForm.Width - 1, targetForm.Height - 1);
                        e.Graphics.DrawRectangle(borderPen, rektangel);
                    }
                }
            }
        }

        private void TargetForm_Resize(object sender, EventArgs e)
        {
            if (targetForm != null)
            {
                targetForm.Invalidate();
            }
        }

        private void FourBorder_Disposed(object sender, EventArgs e)
        {
            if (targetForm != null)
            {
                targetForm.Paint -= TargetForm_Paint;
                targetForm.Resize -= TargetForm_Resize;
                targetForm.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Set the border width.")]
        [Category("Appearance")]
        public int BorderWidth
        {
            get { return borderWidth; }
            set
            {
                borderWidth = value;
                if (targetForm != null)
                {
                    targetForm.Invalidate();
                }
            }
        }

        [Browsable(true)]
        [Description("Set the border color.")]
        [Category("Appearance")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                if (targetForm != null)
                {
                    targetForm.Invalidate();
                }
            }
        }
    }
}
