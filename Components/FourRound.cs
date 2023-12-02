using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FourUI
{
    public partial class FourRound : Component
    {
        private Form targetForm;
        private int cornerRadius = 4;

        public FourRound()
        {

        }

        public FourRound(IContainer container)
        {
            container.Add(this);
        }

        [Browsable(true)]
        [DefaultValue(4)]
        public int CornerRadius
        {
            get { return cornerRadius; }
            set
            {
                if (value == 0)
                {
                    value = 1;
                }
                cornerRadius = value;
                if (targetForm != null)
                {
                    SetRoundedCorners(targetForm, cornerRadius);
                    targetForm.Invalidate();
                }
            }
        }

        public Form TargetForm
        {
            get { return targetForm; }
            set
            {
                if (targetForm != null)
                {
                    targetForm.Load -= TargetForm_Load;
                    targetForm.BackColorChanged -= TargetForm_BackColorChanged;
                }

                targetForm = value;

                if (targetForm != null)
                {
                    targetForm.Load += TargetForm_Load;
                    targetForm.BackColorChanged += TargetForm_BackColorChanged;
                    targetForm.ResizeEnd += ResizeEnd;
                    targetForm.ImeMode = ImeMode.Off;
                    targetForm.Invalidate();
                }
            }
        }

        private void ResizeEnd(object sender, EventArgs e)
        {
            SetRoundedCorners(targetForm, cornerRadius);
            targetForm.Invalidate();
        }


        private void TargetForm_Load(object sender, EventArgs e)
        {
            fillColor = targetForm.BackColor;
            SetRoundedCorners(targetForm, cornerRadius);
            targetForm.Invalidate();
        }

        private void TargetForm_BackColorChanged(object sender, EventArgs e)
        {
            SetRoundedCorners(targetForm, cornerRadius);
            targetForm.Invalidate();
        }

        private Color fillColor = Color.White;

        private void SetRoundedCorners(Control control, int radius)
        {

            int diameter = radius * 2;
            int borderWidth = 6;

            Rectangle clientRect = control.ClientRectangle;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(new Rectangle(clientRect.Left, clientRect.Top, diameter, diameter), 180, 90);
                path.AddLine(clientRect.Left + radius, clientRect.Top, clientRect.Right - radius, clientRect.Top);
                path.AddArc(new Rectangle(clientRect.Right - diameter - 1, clientRect.Top, diameter, diameter), 270, 90);
                path.AddLine(clientRect.Right, clientRect.Top + radius, clientRect.Right, clientRect.Bottom - radius);
                path.AddArc(new Rectangle(clientRect.Right - diameter - 1, clientRect.Bottom - diameter - 1, diameter, diameter), 0, 90);
                path.AddLine(clientRect.Right - radius, clientRect.Bottom, clientRect.Left + radius, clientRect.Bottom);
                path.AddArc(new Rectangle(clientRect.Left, clientRect.Bottom - diameter - 1, diameter, diameter), 90, 90);
                path.AddLine(clientRect.Left, clientRect.Bottom - radius, clientRect.Left, clientRect.Top + radius);

                path.CloseFigure();


                control.Region = new Region(path);

                using (Graphics graphics = control.CreateGraphics())
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    using (SolidBrush solidBrush = new SolidBrush(control.BackColor))
                    {
                        graphics.FillPath(solidBrush, path);
                    }

                    path.Widen(new Pen(Color.Transparent, borderWidth));
                    using (SolidBrush borderBrush = new SolidBrush(Color.FromArgb(80, control.BackColor)))
                    {
                        graphics.FillPath(borderBrush, path);
                    }
                }
            }
        }
    }
}
