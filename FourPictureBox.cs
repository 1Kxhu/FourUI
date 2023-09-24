using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

public class FourPictureBox : Control
{
    private Image _image;
    private int _cornerRadius = 5;

    public FourPictureBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public Image Image
    {
        get { return _image; }
        set
        {
            _image = value;
            Invalidate();
        }
    }

    public int CornerRadius
    {
        get { return _cornerRadius; }
        set
        {
            _cornerRadius = value;
            Invalidate();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (_image != null)
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

                    matrix.Scale(scaleX, scaleY);

                    brush.Transform = matrix;
                    brush.WrapMode = WrapMode.Clamp;

                    e.Graphics.FillPath(brush, path);
                }
            }
        }
    }




}
