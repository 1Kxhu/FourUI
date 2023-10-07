using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FourUI
{
    public class FourSwitch : Control
    {
        private Color thumbColorUnchecked = Color.Crimson;
        private Color thumbColorChecked = Color.FromArgb(33, 133, 255);
        private Color trackColor = Color.FromArgb(21, 21, 21);

        private bool ischecked = false;
        private int thumbX;
        private int startX = 4;
        private int endX;
        private int thumbWidth;

        private designchoice dchoice = FourSwitch.designchoice.Inward;

        public enum designchoice
        {
            Inward,
            Outward
        }

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The unchecked switch thumb color.")]
        public Color UncheckedColor
        {
            get { return thumbColorUnchecked; }
            set
            {
                thumbColorUnchecked = value;
                if (Checked)
                {
                    thumbColor = thumbColorChecked;
                }
                else
                {
                    thumbColor = thumbColorUnchecked;
                }
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The checked switch thumb color.")]
        public Color CheckedColor
        {
            get { return thumbColorChecked; }
            set
            {
                thumbColorChecked = value;
                if (Checked)
                {
                    thumbColor = thumbColorChecked;
                }
                else
                {
                    thumbColor = thumbColorUnchecked;
                }
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The style in which the control displays.")]
        public designchoice DesignChoice
        {
            get { return dchoice; }
            set
            {
                dchoice = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The checked switch thumb color.")]
        public Color TrackColor
        {
            get { return trackColor; }
            set
            {
                trackColor = value;
                Invalidate();
            }
        }



        int animatedThumbX;



        public FourSwitch()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);



            endX = Width - thumbWidth - 5;

            ischecked = Checked;

            AnimateThumb();

            MouseClick += (sender, e) =>
            {
                ischecked = !ischecked;
                AnimateThumb();
            };

            InitializeTimer();
        }



        private void InitializeTimer()
        {

            smoothMoveTimer = new Timer();
            smoothMoveTimer.Interval = 4;
            smoothMoveTimer.Tick += SmoothMoveTimer_Tick;
        }

        private Timer smoothMoveTimer;


        private void SmoothMoveTimer_Tick(object sender, EventArgs e)
        {

        }

        [Browsable(true)]
        [Category("FourUI")]
        [Description("It is what you think it is.")]
        public bool Checked
        {
            get { return ischecked; }
            set
            {
                ischecked = value;
                Invalidate();
            }
        }

        Color thumbColor = Color.White;


        protected override void OnPaint(PaintEventArgs e)
        {



            int cornerRadius = (Height / 2) - 1;
            int rectX = 0;
            int rectY = 0;
            int rectWidth = Width - 1;
            int rectHeight = Height - 1;


            thumbWidth = Height - 9;


            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (dchoice == designchoice.Inward)
            {
                endX = Width - (Height - 9) - 3;
                using (GraphicsPath path = RoundedRectangle(rectX, rectY, rectWidth, rectHeight, cornerRadius))
                {

                    using (Brush bgBrush = new SolidBrush(trackColor))
                    {
                        e.Graphics.FillPath(bgBrush, path);
                    }

                }
            }
            else if (dchoice == designchoice.Outward)
            {
                endX = Width - (Height - 6);
                rectWidth = rectWidth - 5;
                rectHeight = rectHeight - 2;
                cornerRadius = (rectHeight / 6);
                using (GraphicsPath path = RoundedRectangle(-1 + rectX + rectWidth / 10, (rectY + rectHeight / 3), rectWidth - rectWidth / 10, 3 + rectHeight / 3, cornerRadius))
                {

                    using (Brush bgBrush = new SolidBrush(trackColor))
                    {
                        e.Graphics.FillPath(bgBrush, path);
                    }

                }
            }

            animatedThumbX = thumbX;




            if (Checked)
            {
                thumbColor = Color.FromArgb(((thumbColor.R * 2) + thumbColorChecked.R) / 3, ((thumbColor.G * 2) + thumbColorChecked.G) / 3, ((thumbColor.B * 2) + thumbColorChecked.B) / 3);
            }
            else
            {
                thumbColor = Color.FromArgb(((thumbColor.R * 2) + thumbColorUnchecked.R) / 3, ((thumbColor.G * 2) + thumbColorUnchecked.G) / 3, ((thumbColor.B * 2) + thumbColorUnchecked.B) / 3);
            }


            if (DesignMode)
            {
                if (Checked)
                {
                    thumbColor = thumbColorChecked;
                    thumbX = endX;
                    animatedThumbX = endX - 1;
                }
                else
                {
                    thumbColor = thumbColorUnchecked;
                    thumbX = startX;
                    animatedThumbX = startX;
                }
            }

            using (Brush thumbBrush = new SolidBrush(thumbColor))
            {
                e.Graphics.FillEllipse(thumbBrush, animatedThumbX, 4, thumbWidth, thumbWidth);
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


        private void AnimateThumb()
        {
            Timer animationTimer = new Timer();

            animationTimer.Interval = 4;




            animationTimer.Tick += (sender, e) =>
                {

                    if (Checked || thumbX == endX)
                    {
                        thumbX = ((thumbX * 2) + endX) / 3;

                        if (thumbX > (endX - 1))
                        {
                            animationTimer.Stop();

                        }

                    }
                    else
                    {

                        thumbX = ((thumbX * 2) + startX) / 3;

                        if (thumbX < (startX + 1))
                        {
                            animationTimer.Stop();

                        }
                    }
                    Invalidate();
                };

            animationTimer.Start();
        }
    }
}