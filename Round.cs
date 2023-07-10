using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FourUI
{
    // A component for creating rounded corners on a Windows Form
    public partial class FourRound : Component
    {
        private Form targetForm; // The form to apply rounded corners to
        private int cornerRadius = 10; // The radius of the rounded corners
        private Color backgroundColor; // The background color of the form

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
                }
            }
        }

        [Browsable(true)]
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
                // Unsubscribe from event handlers of the previous form
                if (targetForm != null)
                {
                    targetForm.Load -= TargetForm_Load;
                    targetForm.BackColorChanged -= TargetForm_BackColorChanged;
                }

                targetForm = value;

                // Subscribe to event handlers of the new form
                if (targetForm != null)
                {
                    targetForm.Load += TargetForm_Load;
                    targetForm.BackColorChanged += TargetForm_BackColorChanged;
                }
            }
        }

        // Event handler for when the target form is loaded
        private void TargetForm_Load(object sender, EventArgs e)
        {
            SetRoundedCorners(targetForm, cornerRadius);
        }

        Random r = new Random();

        // Event handler for when the target form's background color changes
        private void TargetForm_BackColorChanged(object sender, EventArgs e)
        {

            // Randomly update rounded corners for some variation
            if (r.Next(1, 100) == 1)
                SetRoundedCorners(targetForm, cornerRadius);
        }

        private Color fillColor = Color.White;

        // Method to set the rounded corners for a control
        private void SetRoundedCorners(Control control, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            // Subscribe to events related to form resizing
            targetForm.ResizeEnd += TargetForm_BackColorChanged;
            targetForm.Resize += TargetForm_BackColorChanged;
            targetForm.SizeChanged += TargetForm_BackColorChanged;

            // Unsubscribe from events after setting the rounded corners
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

            // Add arcs and lines to create the rounded corners path
            path.AddArc(rectTL, 180, 90);
            path.AddLine(left + radius, top, right - radius, top);
            path.AddArc(rectTR, 270, 90);
            path.AddLine(right, top + radius, right, bottom - radius);
            path.AddArc(rectBR, 0, 90);
            path.AddLine(right - radius, bottom, left + radius, bottom);
            path.AddArc(rectBL, 90, 90);
            path.AddLine(left, bottom - radius, left, top + radius);

            path.CloseFigure();

            // Set the control's region to the rounded corners path
            control.Region = new Region(path);

            // Fill the rounded corners with the background color
            using (SolidBrush brush = new SolidBrush(fillColor))
            {
                control.CreateGraphics().FillPath(brush, path);
            }
        }
    }
}
