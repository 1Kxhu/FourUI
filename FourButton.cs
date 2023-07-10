using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourUI
{

    //checking if push works
    public class FourButton : Button
    {
        private int rippleRadius = 0;

        private int cornerRadius = 7; // default corner radius value
        private int borderWidth = 2; // border width
        private Color backColor2 = Color.FromArgb(32, 32, 32); // default BackColor2 value
        private Color originalOriginalColor;
        private Color originalColor;
        private Color BackColor2;
        private Color pressCol;

        public FourButton()
        {
            //
            //those are the default values, you can change them in the properties when you add the dll to your project.
            //
            TransitionColor1 = Color.FromArgb(32, 32, 32);
            TransitionColor2 = Color.FromArgb(41, 41, 41);
            pressCol = Color.FromArgb(244, 244, 244); // Assign pressCol here
            PressColor = Color.FromArgb(244, 244, 244);

            // Assign a default value to originalOriginalColor
            originalOriginalColor = TransitionColor1;
            BackColor2 = backColor2;

            if (BackColor2.IsEmpty || !BackColor2.IsEmpty)
                BackColor2 = backColor2;

            ForeColor = Color.White;
            Font = new Font("Microsoft Yahei UI", 10, FontStyle.Regular);
            Text = "Text";
            Size = new Size(130, 40);

            MouseEnter += CustomButton_MouseEnter;
            MouseLeave += CustomButton_MouseLeave;
            MouseClick += Ye;
            SetStyle(ControlStyles.ResizeRedraw, true);
            // Subscribe to the BackColorChanged event of the targetForm
        }

        private Color a = Color.White;

        private void TargetForm_BackColorChanged(object sender, EventArgs e)
        {
            // Update the backgroundColor variable with the new BackColor
            a = Parent.BackColor;
        }

        private CancellationTokenSource cancellationTokenSource; // Used to cancel the animation


        private async void Ye(object sender, EventArgs e)
        {
            // Cancel any ongoing animation
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                originalColor = pressCol; // Set the original color to the press color
                int steps = 15; // Number of steps for the color transition
                int delay = 1; // Delay in milliseconds between color transitions

                int deltaR = (int)Math.Round((originalOriginalColor.R - originalColor.R) / (double)steps);
                int deltaG = (int)Math.Round((originalOriginalColor.G - originalColor.G) / (double)steps);
                int deltaB = (int)Math.Round((originalOriginalColor.B - originalColor.B) / (double)steps);


                // Instant transition to the press color
                originalColor = pressCol;
                Invalidate();
                System.Windows.Forms.Application.DoEvents();

                Point mousePosition = PointToClient(MousePosition);
                while (MouseButtons != MouseButtons.None && ClientRectangle.Contains(mousePosition))
                {
                    mousePosition = PointToClient(MousePosition);
                    int newR = originalColor.R + deltaR;
                    int newG = originalColor.G + deltaG;
                    int newB = originalColor.B + deltaB;

                    originalColor = Color.FromArgb(newR, newG, newB); // Update the original color
                    Invalidate(); // Redraw the control
                    System.Windows.Forms.Application.DoEvents(); // Allow the UI to update

                    await Task.Delay(TimeSpan.FromMilliseconds(delay), cancellationTokenSource.Token); // Delay the animation

                    if (MouseButtons != MouseButtons.None && ClientRectangle.Contains(mousePosition)) // Check if the mouse button is still down
                    {
                        if (steps > 1)
                        {
                            deltaR = (int)Math.Round((backColor2.R - originalColor.R) / (double)(steps - 1));
                            deltaG = (int)Math.Round((backColor2.G - originalColor.G) / (double)(steps - 1));
                            deltaB = (int)Math.Round((backColor2.B - originalColor.B) / (double)(steps - 1));
                        }
                    }
                }

                // Smooth transition to the original color
                deltaR = (int)Math.Round((originalOriginalColor.R - originalColor.R) / (double)steps);
                deltaG = (int)Math.Round((originalOriginalColor.G - originalColor.G) / (double)steps);
                deltaB = (int)Math.Round((originalOriginalColor.B - originalColor.B) / (double)steps);

                for (int i = 1; i <= steps; i++)
                {
                    if (ClientRectangle.Contains(mousePosition))
                    {
                        int newR = originalColor.R + deltaR;
                        int newG = originalColor.G + deltaG;
                        int newB = originalColor.B + deltaB;

                        originalColor = Color.FromArgb(newR, newG, newB); // Update the original color
                        Invalidate(); // Redraw the control

                        try
                        {
                            if (cancellationTokenSource != null && cancellationTokenSource.Token != null && !cancellationTokenSource.IsCancellationRequested)
                                await Task.Delay(delay, cancellationTokenSource.Token);
                        }
                        catch
                        {
                            // Handle any exceptions
                        }

                        if (i != steps) // Skip division on the last step
                        {
                            deltaR = (int)Math.Round((backColor2.R - originalColor.R) / (double)(steps - i));
                            deltaG = (int)Math.Round((backColor2.G - originalColor.G) / (double)(steps - i));
                            deltaB = (int)Math.Round((backColor2.B - originalColor.B) / (double)(steps - i));
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                Invalidate(); // Redraw the control with the original color
            }
            catch
            {
                // Handle any exceptions
            }
        }


        private void CustomButton_MouseLeave(object sender, EventArgs e)
        {
            // Cancel any ongoing animation
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = null;

            int steps = 15; // number of steps for the color transition
            int delay = 1; // delay in milliseconds between color transitions

            int deltaR = (originalOriginalColor.R - originalColor.R) / steps;
            int deltaG = (originalOriginalColor.G - originalColor.G) / steps;
            int deltaB = (originalOriginalColor.B - originalColor.B) / steps;

            for (int i = 1; i <= steps; i++)
            {
                int newR = originalColor.R + deltaR;
                int newG = originalColor.G + deltaG;
                int newB = originalColor.B + deltaB;

                originalColor = Color.FromArgb(newR, newG, newB); // update the originalColor
                Invalidate(); // redraw the control
                System.Windows.Forms.Application.DoEvents(); // allow the UI to update

                Thread.Sleep(delay);

                if (i != steps) // skip division on the last step
                {
                    deltaR = (originalOriginalColor.R - originalColor.R) / (steps - i);
                    deltaG = (originalOriginalColor.G - originalColor.G) / (steps - i);
                    deltaB = (originalOriginalColor.B - originalColor.B) / (steps - i);
                }
            }

            originalColor = originalOriginalColor; // reset originalColor to originalOriginalColor
            Invalidate(); // redraw the control with the original color
        }

        private void CustomButton_MouseEnter(object sender, EventArgs e)
        {
            // Cancel any ongoing animation
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = null;

            Color targetColor = backColor2; // get the target color from BackColor2 property
            int steps = 15; // number of steps for the color transition
            int delay = 1; // delay in milliseconds between color transitions

            int deltaR = (targetColor.R - originalColor.R) / steps;
            int deltaG = (targetColor.G - originalColor.G) / steps;
            int deltaB = (targetColor.B - originalColor.B) / steps;

            Point mousePosition = PointToClient(MousePosition);

            for (int i = 1; i <= steps && ClientRectangle.Contains(mousePosition); i++)
            {
                mousePosition = PointToClient(MousePosition);
                int newR = originalColor.R + deltaR;
                int newG = originalColor.G + deltaG;
                int newB = originalColor.B + deltaB;

                if (newR < 256 && newB < 256 && newG < 256)
                    originalColor = Color.FromArgb(newR, newG, newB); // update the originalColor
                if (newR < 256 && newB < 256 && newG < 256)
                    BackColor = originalColor; // update the BackColor
                System.Windows.Forms.Application.DoEvents(); // allow the UI to update

                Thread.Sleep(delay);
            }

            Invalidate(); // redraw the control with the final color
        }

        public int CornerRadius
        {
            get { return cornerRadius; }
            set
            {
                if (value >= 0)
                {
                    cornerRadius = value;
                    Refresh(); // redraw the button with the new corner radius
                }
            }
        }

        public Color TransitionColor1
        {
            get { return originalColor; }
            set
            {
                originalColor = value;
                Refresh(); // redraw the button with the new originalColor
                originalOriginalColor = value;
            }
        }

        public Color TransitionColor2
        {
            get { return backColor2; }
            set
            {
                backColor2 = value;
                Refresh(); // redraw the button with the new BackColor2
            }
        }

        public Color PressColor
        {
            get { return pressCol; }
            set
            {
                pressCol = value;
                Refresh(); // redraw the button with the new originalColor
            }
        }

        public Color RippleColor { get; set; } = Color.White;

        public int RippleRadius
        {
            get { return rippleRadius; }
            set
            {
                rippleRadius = value;
                Invalidate();
            }
        }


        protected override void OnPaint(PaintEventArgs pevent)
        {
            Parent.BackColorChanged += TargetForm_BackColorChanged;
            base.OnPaint(pevent);

            GraphicsPath path = new GraphicsPath();
            int arcSize = 2 * cornerRadius;

            // Define the main rounded rectangle path using Bézier curves
            path.AddArc(borderWidth - 2, borderWidth, arcSize, arcSize, 180, 90);
            path.AddArc(Width - arcSize - borderWidth, borderWidth, arcSize, arcSize, 270, 90);
            path.AddArc(Width - arcSize - borderWidth, Height - arcSize - borderWidth, arcSize, arcSize, 0, 90);
            path.AddArc(borderWidth - 2, Height - arcSize - borderWidth, arcSize, arcSize, 90, 90);

            path.CloseFigure();

            // create the region for the button using the main rounded rectangle path
            Region = new Region(path);

            // draw the button using the main rounded rectangle path with anti-aliasing
            using (SolidBrush brush = new SolidBrush(originalColor))
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                pevent.Graphics.FillPath(brush, path);
            }




            // draw the border outline
            using (Pen borderPen = new Pen(Color.Transparent, borderWidth))
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                pevent.Graphics.DrawPath(borderPen, path);
            }





            // draw the button text
            TextRenderer.DrawText(pevent.Graphics, Text, Font, ClientRectangle, ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Do nothing to prevent the base class from painting the background
        }


    }
}