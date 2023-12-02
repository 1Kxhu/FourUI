using FourUI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class FourPictureBox : Control
{
    private Image _image;
    private int _cornerRadius = 5;
    private float _rotationAngle = 0;
    private Matrix _translationMatrix = new Matrix();
    private Color borderColor = Color.White;
    private int borderWidth = 1;

    public FourPictureBox()
    {
        SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        DoubleBuffered = true;
        BackColor = Color.Transparent;
    }

    [Category("FourUI")]
    [Description("The image itself that will be displayed.")]
    public Image Image
    {
        get { return _image; }
        set
        {
            _image = value;
            Invalidate();
        }
    }

    [Category("FourUI")]
    [Description("The color of the border.")]
    public Color BorderColor
    {
        get { return borderColor; }
        set
        {
            borderColor = value;
            Invalidate();
        }
    }

    [Category("FourUI")]
    [Description("The rounding radius.")]
    public int BorderWidth
    {
        get { return borderWidth; }
        set
        {
            borderWidth = value;
            Invalidate();
        }
    }

    [Category("FourUI")]
    [Description("The rounding radius.")]
    public int CornerRadius
    {
        get { return _cornerRadius; }
        set
        {
            _cornerRadius = value;
            Invalidate();
        }
    }

    [Category("FourUI")]
    [Description("The rotation angle, very specific purpose.")]
    public float RotationAngle
    {
        get { return _rotationAngle; }
        set
        {
            _rotationAngle = value;
            Invalidate();
        }
    }

    [Browsable(false)]
    public Matrix TranslationMatrix
    {
        get { return _translationMatrix; }
        set
        {
            _translationMatrix = value;
            Invalidate();
        }
    }

    private GraphicsPath RoundedRectangle(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();

        path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90); path.AddArc(rect.Right - 2 * radius, rect.Y, radius * 2, radius * 2, 270, 90); path.AddArc(rect.Right - 2 * radius, rect.Bottom - 2 * radius, radius * 2, radius * 2, 0, 90); path.AddArc(rect.X, rect.Bottom - 2 * radius, radius * 2, radius * 2, 90, 90);
        path.CloseFigure();

        return path;
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        try
        {
            if (Parent != null)
            {
                using (var bmp = new Bitmap(Parent.Width * 2, Parent.Height * 2))
                {
                    Parent.Controls.Cast<Control>().Where(c => Parent.Controls.GetChildIndex(c) > Parent.Controls.GetChildIndex(this))
                        .ToList()
                        .ForEach(c => c.DrawToBitmap(bmp, new Rectangle(c.Bounds.X - 1, c.Bounds.Y - 1, c.Width + 1, c.Height + 1)));

                    e.Graphics.DrawImage(bmp, -Left, -Top);
                }

                using (var bmp = new Bitmap(Parent.Width * 2, Parent.Height * 2))
                {
                    Parent.Controls.Cast<FourGradientPanel>().Where(c => Parent.Controls.GetChildIndex(c) > Parent.Controls.GetChildIndex(this))
                        .ToList()
                        .ForEach(c => c.DrawToBitmap(bmp, new Rectangle(c.Bounds.X - 1, c.Bounds.Y - 1, c.Width + 1, c.Height + 1)));

                    e.Graphics.DrawImage(bmp, -Left, -Top);
                }
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }
        catch
        {
            base.OnPaintBackground(e);
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        Rectangle crect = ClientRectangle;
        crect.Inflate(5, 5);
        crect.Offset(-2, -2);

        if (BackColor != Color.Transparent)
        {
            e.Graphics.FillRectangle(new SolidBrush(BackColor), crect);
        }

        if (_image != null)
        {
            int diameter = _cornerRadius * 2;

            Rectangle rawrect = e.ClipRectangle;
            rawrect.Inflate(-1, -1);

            using (var path = RoundedRectangle(rawrect, CornerRadius))
            {


                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (var pen = new Pen(borderColor, BorderWidth))
                using (var brush = new TextureBrush(_image))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.InterpolationMode = InterpolationMode.Low;

                    var scaleX = (float)Width / _image.Width;
                    var scaleY = (float)Height / _image.Height;

                    Matrix matrix = new Matrix();

                    matrix.Multiply(_translationMatrix);

                    matrix.RotateAt(_rotationAngle, new PointF(Width / 2, Height / 2));
                    matrix.Scale(scaleX, scaleY);


                    brush.Transform = matrix;
                    brush.WrapMode = WrapMode.Clamp;

                    e.Graphics.FillPath(brush, path);
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }
    }
}
