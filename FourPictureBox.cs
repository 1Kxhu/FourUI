using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class FourPictureBox : Control
{
    private Image _image;
    private int _cornerRadius = 5;
    private float _rotationAngle = 0;
    private Matrix _translationMatrix = new Matrix();
    private bool _displayImage = true;
    public FourPictureBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
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

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (_image != null && _displayImage)
        {
            int diameter = _cornerRadius * 2;
            int offset = 1;

            using (var path = new GraphicsPath())
            {
                path.AddArc(offset, offset - offset, diameter, diameter, 180, 90);
                path.AddArc(Width - diameter - offset, offset - offset, diameter, diameter, 270, 90);
                path.AddArc(Width - diameter - offset, Height - diameter - offset, diameter, diameter, 0, 90);
                path.AddArc(offset, Height - diameter - offset, diameter, diameter, 90, 90);
                path.CloseFigure();

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (var brush = new TextureBrush(_image))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.InterpolationMode = InterpolationMode.Low;

                    var scaleX = (float)Width / _image.Width;
                    var scaleY = (float)Height / _image.Height;

                    var matrix = new Matrix();

                    matrix.Multiply(_translationMatrix);

                    matrix.RotateAt(_rotationAngle, new PointF(Width / 2, Height / 2));

                    matrix.Scale(scaleX, scaleY);

                    brush.Transform = matrix;
                    brush.WrapMode = WrapMode.Clamp;

                    e.Graphics.FillPath(brush, path);
                }
            }
        }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        _displayImage = false;
        Invalidate();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);

        _displayImage = true;
        Invalidate();
    }
}
