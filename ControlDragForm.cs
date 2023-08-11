﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FourUI
{
    public partial class FourControlDrag : Component
    {
        private Control targetControl; private bool isDragging = false; private Point mouseOffset; private float smoothness = 7f;
        private Timer smoothMoveTimer;
        public FourControlDrag()
        {
            InitializeTimer();
        }

        public FourControlDrag(IContainer container)
        {
            container.Add(this);
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            smoothMoveTimer = new Timer();
            smoothMoveTimer.Interval = 1; smoothMoveTimer.Tick += SmoothMoveTimer_Tick;
        }

        public Control TargetControl
        {
            get { return targetControl; }
            set
            {
                if (targetControl != null)
                {
                    targetControl.MouseDown -= TargetControl_MouseDown;
                    targetControl.MouseMove -= TargetControl_MouseMove;
                    targetControl.MouseUp -= TargetControl_MouseUp;
                }

                targetControl = value;

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
                Point mousePos = targetControl.Parent.PointToScreen(e.Location);
                mousePos.Offset(-mouseOffset.X, -mouseOffset.Y);

                float targetX = mousePos.X;
                float targetY = mousePos.Y;

                float dx = targetX - targetControl.Parent.Location.X;
                float dy = targetY - targetControl.Parent.Location.Y;

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

            float newX = targetControl.Parent.Location.X + increment.X;
            float newY = targetControl.Parent.Location.Y + increment.Y;

            targetControl.Parent.Location = new Point((int)newX, (int)newY);

            if (Math.Abs(increment.X) < 1 && Math.Abs(increment.Y) < 1)
            {
                smoothMoveTimer.Stop();
            }
        }
    }
}