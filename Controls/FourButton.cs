using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourUI
{
    public class FourButton : Button
    {
        private int cornerRadius = 5;
        private int borderWidth = 1;

        private Color _UnfocusedborderColor = Color.FromArgb(45, 45, 45);
        private Color _FocusedborderColor = Color.FromArgb(45, 45, 45);


        private Color _hoverColor = Color.FromArgb(14,14,14);
        private Color _fillColor = Color.FromArgb(10,10,10);
        private Color _pressColor = Color.FromArgb(6,6,6);

        private Color currentBorderColor = Color.Empty;
        private Color currentColor;

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The hover color.")]
        public Color HoverColor
        {
            get { return _hoverColor; }
            set
            {
                _hoverColor = value;
                Refresh();
            }
        }

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The press color.")]
        public Color PressColor
        {
            get { return _pressColor; }
            set
            {
                _pressColor = value;
                Refresh();
            }
        }

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The default state color.")]
        public Color FillColor
        {
            get { return _fillColor; }
            set
            {
                _fillColor = value;
                Refresh(); currentColor = value;
            }
        }

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The rounding value.")]
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
                Refresh();
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("The color of the border.")]
        public Color UnfocusedBorderColor
        {
            get { return _UnfocusedborderColor; }
            set
            {

                _UnfocusedborderColor = value;
                currentBorderColor = Color.Empty;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("The color of the border.")]
        public Color FocusedBorderColor
        {
            get { return _FocusedborderColor; }
            set
            {

                _FocusedborderColor = value;
                currentBorderColor = Color.Empty;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("The color of the border.")]
        public int BorderWidth
        {
            get { return borderWidth; }
            set
            {

                borderWidth = value;
                Invalidate();
            }
        }

        public FourButton()
        {
            currentColor = FillColor;

            if (HoverColor.IsEmpty || !HoverColor.IsEmpty)
                HoverColor = _hoverColor;

            ForeColor = Color.White;
            Font = new Font("Microsoft Yahei UI", 10, FontStyle.Regular);
            Size = new Size(130, 40);

            ForeColor = Color.FromArgb(100, 100, 100);

            MouseEnter += MouseEnterEvent;
            MouseLeave += MouseLeaveEvent;
            MouseClick += MouseClickEvent;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private CancellationTokenSource cancellationTokenSource;
        private async void MouseClickEvent(object sender, EventArgs e)
        {
            if (_buttonmode == ButtonModeEnum.Default)
            {
                cancellationTokenSource?.Cancel();
                cancellationTokenSource = new CancellationTokenSource();

                try
                {
                    _fillColor = _pressColor;
                    int steps = 15;
                    int delay = 1;
                    int deltaR = (int)Math.Round((currentColor.R - _fillColor.R) / (double)steps);
                    int deltaG = (int)Math.Round((currentColor.G - _fillColor.G) / (double)steps);
                    int deltaB = (int)Math.Round((currentColor.B - _fillColor.B) / (double)steps);
                    int deltaA = (int)Math.Round((currentColor.A - _fillColor.A) / (double)steps);

                    _fillColor = _pressColor;
                    Invalidate();
                    Application.DoEvents();

                    Point mousePosition = PointToClient(MousePosition);
                    while (MouseButtons != MouseButtons.None && ClientRectangle.Contains(mousePosition))
                    {
                        mousePosition = PointToClient(MousePosition);
                        int newR = _fillColor.R + deltaR;
                        int newG = _fillColor.G + deltaG;
                        int newB = _fillColor.B + deltaB;
                        int newA = _fillColor.A + deltaA;

                        _fillColor = Color.FromArgb(newA, newR, newG, newB);
                        Invalidate();
                        Application.DoEvents();

                        await Task.Delay(TimeSpan.FromMilliseconds(delay), cancellationTokenSource.Token);
                        if (MouseButtons != MouseButtons.None && ClientRectangle.Contains(mousePosition))
                        {
                            if (steps > 1)
                            {
                                deltaR = (int)Math.Round((_hoverColor.R - _fillColor.R) / (double)(steps - 1));
                                deltaG = (int)Math.Round((_hoverColor.G - _fillColor.G) / (double)(steps - 1));
                                deltaB = (int)Math.Round((_hoverColor.B - _fillColor.B) / (double)(steps - 1));
                                deltaA = (int)Math.Round((_hoverColor.A - _fillColor.A) / (double)(steps - 1));
                            }
                        }
                    }

                    deltaR = (int)Math.Round((currentColor.R - _fillColor.R) / (double)steps);
                    deltaG = (int)Math.Round((currentColor.G - _fillColor.G) / (double)steps);
                    deltaB = (int)Math.Round((currentColor.B - _fillColor.B) / (double)steps);
                    deltaA = (int)Math.Round((currentColor.A - _fillColor.A) / (double)steps);

                    for (int i = 1; i <= steps; i++)
                    {
                        if (!hovering)
                        {
                            _fillColor = currentColor;
                            break;
                        }

                        if (ClientRectangle.Contains(mousePosition))
                        {
                            int newR = _fillColor.R + deltaR;
                            int newG = _fillColor.G + deltaG;
                            int newB = _fillColor.B + deltaB;
                            int newA = _fillColor.A + deltaA;

                            _fillColor = Color.FromArgb(newA, newR, newG, newB);
                            Invalidate();

                            try
                            {
                                if (cancellationTokenSource != null && cancellationTokenSource.Token != null && !cancellationTokenSource.IsCancellationRequested)
                                    await Task.Delay(delay, cancellationTokenSource.Token);
                            }
                            catch
                            {
                            }

                            if (i != steps)
                            {
                                deltaR = (int)Math.Round((_hoverColor.R - _fillColor.R) / (double)(steps - i));
                                deltaG = (int)Math.Round((_hoverColor.G - _fillColor.G) / (double)(steps - i));
                                deltaB = (int)Math.Round((_hoverColor.B - _fillColor.B) / (double)(steps - i));
                                deltaA = (int)Math.Round((_hoverColor.A - _fillColor.A) / (double)(steps - i));
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (!hovering)
                    {
                        _fillColor = currentColor;
                    }

                    Invalidate();
                }
                catch
                {
                }
            }
            else
            {
                Checked = !Checked;
            }
        }


        bool hovering = false;

        private void MouseLeaveEvent(object sender, EventArgs e)
        {
            _fillColor = currentColor;
            currentBorderColor = _UnfocusedborderColor;
            Invalidate();
            hovering = false;
        }

        private void MouseEnterEvent(object sender, EventArgs e)
        {
            _fillColor = _hoverColor;
            currentBorderColor = _FocusedborderColor;
            Invalidate();
            hovering = true;
        }


        private ImageAlignmentOption imageAlignment = ImageAlignmentOption.Left;

        [Browsable(true)]
        [Category("FourUI")]
        public ImageAlignmentOption ImageAlignment
        {
            get { return imageAlignment; }
            set
            {
                if (imageAlignment != value)
                {
                    imageAlignment = value;
                    Invalidate();
                }
            }
        }



        public enum ImageAlignmentOption
        {
            Left,
            Center,
            Right
        }


        private Size imageSize = new Size(20, 20);
        private bool stretchImage = false;

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The size of the image displayed on the button.")]
        public Size ImageSize
        {
            get { return imageSize; }
            set
            {
                imageSize = value;
                Refresh();
            }
        }

        [Browsable(true)]
        [Category("FourUI")]
        [Description("Specifies whether the image should be stretched to fit the specified size.")]
        public bool StretchImage
        {
            get { return stretchImage; }
            set
            {
                stretchImage = value;
                Refresh();
            }
        }

        private Image buttonImage;

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The image displayed on the button.")]
        public Image ButtonImage
        {
            get { return buttonImage; }
            set
            {
                buttonImage = value;
                Refresh();
            }
        }

        private Point imgoffset = new Point(0, 0);

        [Browsable(true)]
        [Category("FourUI")]
        public Point ImageOffset
        {
            get
            {
                return imgoffset;
            }
            set
            {
                imgoffset = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            int imageX;
            if (imageAlignment == ImageAlignmentOption.Left)
            {
                imageX = 5;
            }
            else if (imageAlignment == ImageAlignmentOption.Center)
            {
                imageX = (Width - imageSize.Width) / 2;
            }
            else
            {
                imageX = Width - imageSize.Width - 5;
            }

            Size displaySize = imageSize;
            if (StretchImage)
            {
                displaySize = new Size(Width - 10, Height - 10);
            }

            if (currentBorderColor == null || currentBorderColor == Color.Empty)
            {
                currentBorderColor = _UnfocusedborderColor;
            }

            imageX += ImageOffset.X;


            using (SolidBrush brush = new SolidBrush(Parent.BackColor))
            {
                pevent.Graphics.FillRectangle(brush, ClientRectangle);
            }

            if (_buttonmode == ButtonModeEnum.CheckButton && Checked)
            {
                Rectangle rect = new Rectangle(ClientRectangle.Left, ClientRectangle.Top,
                                   ClientRectangle.Width - 1, ClientRectangle.Height - 1);

                GraphicsPath path = new GraphicsPath();
                int arcSize = 2 * cornerRadius;

                path.AddArc(rect.Left, rect.Top, arcSize, arcSize, 180, 90);
                path.AddArc(rect.Right - arcSize, rect.Top, arcSize, arcSize, 270, 90);
                path.AddArc(rect.Right - arcSize, rect.Bottom - arcSize, arcSize, arcSize, 0, 90);
                path.AddArc(rect.Left, rect.Bottom - arcSize, arcSize, arcSize, 90, 90);

                path.CloseFigure();

                using (Pen borderPen = new Pen(this.BackColor, borderWidth + 74))
                {
                    pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    pevent.Graphics.DrawPath(borderPen, path);
                }

                using (SolidBrush brush = new SolidBrush(CheckedFillColor))
                {
                    pevent.Graphics.FillPath(brush, path);
                }

                using (Pen pen = new Pen(FocusedBorderColor, BorderWidth))
                {
                    pevent.Graphics.DrawPath(pen, path);
                }

                TextFormatFlags flag = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;

                if (checkedbuttonImage != null)
                {
                    pevent.Graphics.DrawImage(checkedbuttonImage, new Rectangle(new Point(imageX, ImageOffset.Y + (Height - displaySize.Height) / 2), displaySize));
                }


                TextRenderer.DrawText(pevent.Graphics, Text, Font, rect, CheckedForeColor,

    flag);
            }
            else
            {

                Rectangle rect = new Rectangle(ClientRectangle.Left, ClientRectangle.Top,
                                   ClientRectangle.Width - 1, ClientRectangle.Height - 1);

                GraphicsPath path = new GraphicsPath();
                int arcSize = 2 * cornerRadius;

                path.AddArc(rect.Left, rect.Top, arcSize, arcSize, 180, 90);
                path.AddArc(rect.Right - arcSize, rect.Top, arcSize, arcSize, 270, 90);
                path.AddArc(rect.Right - arcSize, rect.Bottom - arcSize, arcSize, arcSize, 0, 90);
                path.AddArc(rect.Left, rect.Bottom - arcSize, arcSize, arcSize, 90, 90);

                path.CloseFigure();

                using (Pen borderPen = new Pen(this.BackColor, borderWidth + 74))
                {
                    pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    pevent.Graphics.DrawPath(borderPen, path);
                }


                using (SolidBrush brush = new SolidBrush(_fillColor))
                {
                    pevent.Graphics.FillPath(brush, path);
                }

                using (Pen pen = new Pen(currentBorderColor, BorderWidth))
                {
                    pevent.Graphics.DrawPath(pen, path);
                }

                TextFormatFlags flag = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;

                if (ButtonImage != null)
                {
                    pevent.Graphics.DrawImage(ButtonImage, new Rectangle(new Point(imageX, ImageOffset.Y + (Height - displaySize.Height) / 2), displaySize));
                }

                int xoffset = TextOffset.X;
                int yoffset = TextOffset.Y;

                Rectangle textrect = rect;
                textrect.Offset(xoffset, yoffset);

                TextRenderer.DrawText(pevent.Graphics, Text, Font, textrect, ForeColor,

    flag);
            }
        }

        [Browsable(true)]
        [Category("FourUI")]
        public Point TextOffset { get; set; } = new Point(0, 0);

        private ButtonModeEnum _buttonmode { get; set; } = ButtonModeEnum.Default;

        [Browsable(true)]
        [Category("FourUI")]
        public ButtonModeEnum ButtonMode
        {
            get => _buttonmode;
            set
            {

                _buttonmode = value;
                Invalidate();
            }
        }

        public enum ButtonModeEnum
        {
            Default,
            CheckButton
        }

        private bool _checked = false;

        [Category("FourUI")]
        public bool Checked
        {
            get => _checked;
            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    Invalidate();
                }
            }
        }

        private Color _checkedForeColor = Color.White;

        [Category("FourUI")]
        public Color CheckedForeColor
        {
            get => _checkedForeColor;
            set
            {
                if (_checkedForeColor != value)
                {
                    _checkedForeColor = value;
                    Invalidate();
                }
            }
        }

        private Color _checkedFillColor = Color.FromArgb(28, 131, 255);

        [Category("FourUI")]
        public Color CheckedFillColor
        {
            get => _checkedFillColor;
            set
            {
                if (_checkedFillColor != value)
                {
                    _checkedFillColor = value;
                    Invalidate();
                }
            }
        }

        private Image checkedbuttonImage;

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The image displayed on the button when checked.")]
        public Image CheckedButtonImage
        {
            get { return checkedbuttonImage; }
            set
            {
                checkedbuttonImage = value;
                Refresh();
            }
        }

    }
}