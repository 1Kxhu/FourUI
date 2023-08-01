using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FourUI
{
    public partial class FourRound : Component
    {
        private Form targetForm; private int cornerRadius = 10;
        public FourRound()
        {

        }

        public FourRound(IContainer container)
        {
            container.Add(this);

        }

        [Browsable(true)]
        [DefaultValue(5)]
        public int CornerRadius
        {
            get { return cornerRadius; }
            set
            {
                cornerRadius = value;
                if (targetForm != null)
                {
                    SetRoundedCorners(targetForm, cornerRadius);
                    targetForm.Refresh();
                }
            }
        }

        [Browsable(false)]
        public Color DontChangeThis
        {
            get { return fillColor; }
            set
            {

                if (targetForm != null)
                {

                    SetRoundedCorners(targetForm, cornerRadius);

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
                }
            }
        }

        private void TargetForm_Load(object sender, EventArgs e)
        {
            fillColor = targetForm.BackColor;
            SetRoundedCorners(targetForm, cornerRadius);
        }

        Random r = new Random();

        private void TargetForm_BackColorChanged(object sender, EventArgs e)
        {

            if (r.Next(1, 100) == 1)
                SetRoundedCorners(targetForm, cornerRadius);
        }

        private Color fillColor = Color.White;

        private void SetRoundedCorners(Control control, int radius)
        {
            fillColor = targetForm.BackColor;
            GraphicsPath path = new GraphicsPath();

            targetForm.ResizeEnd += TargetForm_BackColorChanged;
            targetForm.Resize += TargetForm_BackColorChanged;
            targetForm.SizeChanged += TargetForm_BackColorChanged;

            targetForm.ResizeEnd -= TargetForm_BackColorChanged;

            int diameter = radius;
            int left = control.ClientRectangle.Left;
            int top = control.ClientRectangle.Top;
            int right = control.ClientRectangle.Right;
            int bottom = control.ClientRectangle.Bottom;

            Rectangle rectTL = new Rectangle(left, top, diameter, diameter);
            Rectangle rectTR = new Rectangle(right - diameter - 1, top, diameter, diameter);
            Rectangle rectBR = new Rectangle(right - diameter - 1, bottom - diameter - 1, diameter, diameter);
            Rectangle rectBL = new Rectangle(left, bottom - diameter - 1, diameter, diameter);

            path.AddArc(rectTL, 180, 90);
            path.AddLine(left + radius, top, right - radius, top);
            path.AddArc(rectTR, 270, 90);
            path.AddLine(right, top + radius, right, bottom - radius);
            path.AddArc(rectBR, 0, 90);
            path.AddLine(right - radius, bottom, left + radius, bottom);
            path.AddArc(rectBL, 90, 90);
            path.AddLine(left, bottom - radius, left, top + radius);

            path.CloseFigure();

            control.Region = new Region(path);

            using (SolidBrush brush = new SolidBrush(fillColor))
            {
                control.CreateGraphics().FillPath(brush, path);
            }
        }
    }
}
