using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FourUI
{
    // A component for enabling draggable behavior for a Windows Form
    public partial class FourDrag : Component
    {
        private Form targetControl; // The form to be dragged
        private bool isDragging = false; // Flag to indicate if dragging is in progress
        private Point mouseOffset; // Offset between the mouse cursor and the form's position
        private float smoothness = 7f; // Default smoothness value for smooth dragging

        private Timer smoothMoveTimer; // Timer for smooth movement

        public FourDrag()
        {
            InitializeTimer();
        }

        public FourDrag(IContainer container)
        {
            container.Add(this);
            InitializeTimer();
        }

        // Initializes the smoothMoveTimer
        private void InitializeTimer()
        {
            smoothMoveTimer = new Timer();
            smoothMoveTimer.Interval = 1; // Adjust this value if needed
            smoothMoveTimer.Tick += SmoothMoveTimer_Tick;
        }

        public Form TargetControl
        {
            get { return targetControl; }
            set
            {
                // Unsubscribe from event handlers of the previous form
                if (targetControl != null)
                {
                    targetControl.MouseDown -= TargetControl_MouseDown;
                    targetControl.MouseMove -= TargetControl_MouseMove;
                    targetControl.MouseUp -= TargetControl_MouseUp;
                }

                targetControl = value;

                // Subscribe to event handlers of the new form
                if (targetControl != null)
                {
                    targetControl.MouseDown += TargetControl_MouseDown;
                    targetControl.MouseMove += TargetControl_MouseMove;
                    targetControl.MouseUp += TargetControl_MouseUp;
                }
            }
        }

        public float Smoothness
        {
            get { return smoothness; }
            set { smoothness = value; }
        }

        private void TargetControl_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            mouseOffset = new Point(e.X, e.Y);
        }

        private void TargetControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point mousePos = targetControl.PointToScreen(e.Location);
                mousePos.Offset(-mouseOffset.X, -mouseOffset.Y);

                float targetX = mousePos.X;
                float targetY = mousePos.Y;

                float dx = targetX - targetControl.Location.X;
                float dy = targetY - targetControl.Location.Y;

                smoothMoveTimer.Start();
                smoothMoveTimer.Tag = new PointF(dx / smoothness, dy / smoothness);
            }
        }

        private void TargetControl_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            smoothMoveTimer.Stop();
        }

        private void SmoothMoveTimer_Tick(object sender, EventArgs e)
        {
            PointF increment = (PointF)smoothMoveTimer.Tag;

            float newX = targetControl.Location.X + increment.X;
            float newY = targetControl.Location.Y + increment.Y;

            targetControl.Location = new Point((int)newX, (int)newY);

            if (Math.Abs(increment.X) < 1 && Math.Abs(increment.Y) < 1)
            {
                smoothMoveTimer.Stop();
            }
        }
    }
}
