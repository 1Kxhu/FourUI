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
        private int cornerRadius = 10;

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
                    targetForm.Invalidate(); // Trigger repaint with new settings
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
                    targetForm.Invalidate(); // Trigger repaint with current settings
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
                    targetForm.Paint -= OnPaint; // Unhook the old Paint event
                }

                targetForm = value;

                if (targetForm != null)
                {
                    targetForm.Load += TargetForm_Load;
                    targetForm.BackColorChanged += TargetForm_BackColorChanged;
                    targetForm.Paint += OnPaint; // Hook the new Paint event
                }
            }
        }

        private void TargetForm_Load(object sender, EventArgs e)
        {
            fillColor = targetForm.BackColor;
            SetRoundedCorners(targetForm, cornerRadius);
            targetForm.Invalidate(); // Trigger repaint on load
        }

        Random r = new Random();

        private void TargetForm_BackColorChanged(object sender, EventArgs e)
        {
            if (r.Next(1, 100) == 1)
            {
                SetRoundedCorners(targetForm, cornerRadius);
                targetForm.Invalidate(); // Trigger repaint when back color changes
            }
        }

        private Color fillColor = Color.White;

        private void SetRoundedCorners(Control control, int radius)
        {
            fillColor = targetForm.BackColor;
            GraphicsPath path = new GraphicsPath();

            int diameter = radius * 2;
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

            using (Graphics graphics = control.CreateGraphics())
            {
                // Enable anti-aliasing
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Fill the path with the main BackColor
                using (SolidBrush solidBrush = new SolidBrush(targetForm.BackColor))
                {
                    graphics.FillPath(solidBrush, path);
                }

                // Draw the semi-transparent border around the edges
                int borderWidth = 6; // The width of the semi-transparent border
                int adjustedRadius = radius + borderWidth / 2;

                Rectangle borderRect = new Rectangle(left - borderWidth / 2, top - borderWidth / 2, right - left + borderWidth, bottom - top + borderWidth);

                using (GraphicsPath borderPath = new GraphicsPath())
                {
                    borderPath.AddArc(rectTL, 180, 90);
                    borderPath.AddLine(left + adjustedRadius, top, right - adjustedRadius, top);
                    borderPath.AddArc(rectTR, 270, 90);
                    borderPath.AddLine(right, top + adjustedRadius, right, bottom - adjustedRadius);
                    borderPath.AddArc(rectBR, 0, 90);
                    borderPath.AddLine(right - adjustedRadius, bottom, left + adjustedRadius - 1, bottom);
                    borderPath.AddArc(rectBL, 90, 90);
                    borderPath.AddLine(left, bottom - adjustedRadius, left + 1, top + adjustedRadius + 2);

                    borderPath.CloseFigure();

                    using (SolidBrush borderBrush = new SolidBrush(Color.FromArgb(80, targetForm.BackColor)))
                    {
                        graphics.FillPath(borderBrush, borderPath);
                    }
                }
            }
        }

        protected void OnPaint(object sender, PaintEventArgs e)
        {
            if (targetForm != null)
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = cornerRadius;
                    int diameter = radius * 2;
                    int left = targetForm.ClientRectangle.Left;
                    int top = targetForm.ClientRectangle.Top;
                    int right = targetForm.ClientRectangle.Right;
                    int bottom = targetForm.ClientRectangle.Bottom;

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

                    using (SolidBrush brush = new SolidBrush(targetForm.BackColor))
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        e.Graphics.FillPath(brush, path);
                    }

                    // Draw the semi-transparent border around the edges in the OnPaint method as well
                    int borderWidth = 6; // The width of the semi-transparent border
                    int adjustedRadius = radius + borderWidth / 2;

                    Rectangle borderRect = new Rectangle(left - borderWidth / 2, top - borderWidth / 2, right - left + borderWidth, bottom - top + borderWidth);

                    using (GraphicsPath borderPath = new GraphicsPath())
                    {
                        borderPath.AddArc(rectTL, 180, 90);
                        borderPath.AddLine(left + adjustedRadius, top, right - adjustedRadius, top);
                        borderPath.AddArc(rectTR, 270, 90);
                        borderPath.AddLine(right, top + adjustedRadius, right, bottom - adjustedRadius);
                        borderPath.AddArc(rectBR, 0, 90);
                        borderPath.AddLine(right - adjustedRadius, bottom, left + adjustedRadius, bottom);
                        borderPath.AddArc(rectBL, 90, 90);
                        borderPath.AddLine(left, bottom - adjustedRadius, left, top + adjustedRadius);

                        borderPath.CloseFigure();

                        using (SolidBrush borderBrush = new SolidBrush(Color.FromArgb(80, targetForm.BackColor)))
                        {
                            e.Graphics.FillPath(borderBrush, borderPath);
                        }
                    }
                }
            }
        }
    }
}
