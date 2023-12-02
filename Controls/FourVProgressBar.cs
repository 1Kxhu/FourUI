using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class FourVProgressBar : Control
{
    private int value = 0;
    private int minimum = 0;
    private int maximum = 100;

    private Color borderColor = Color.FromArgb(45, 45, 45);
    private int borderWidth = 2;

    Color progressColor = Color.FromArgb(33, 133, 255);
    Color _bgColor = Color.FromArgb(10, 10, 10);

    [Browsable(true)]
    [Category("FourUI")]
    [Description("The progress color.")]
    public Color ProgressColor
    {
        get { return progressColor; }
        set { progressColor = value; Invalidate(); }
    }

    [Browsable(true)]
    [Category("Appearance")]
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

    [Browsable(true)]
    [Category("FourUI")]
    [Description("The background color.")]
    public Color BackgroundColor
    {
        get { return _bgColor; }
        set { _bgColor = value; Invalidate(); }
    }


    public event EventHandler ValueChanged;

    public FourVProgressBar()
    {
        this.Size = new System.Drawing.Size(200, 30);
        this.DoubleBuffered = true;
    }

    public int Value
    {
        get { return value; }
        set
        {
            if (value < minimum)
                this.value = minimum;
            else if (value > maximum)
                this.value = maximum;
            else
            {
                this.value = value;
                OnValueChanged(EventArgs.Empty);
                this.Invalidate();
            }
        }
    }

    public int Minimum
    {
        get { return minimum; }
        set
        {
            minimum = value;
            if (value > maximum)
                maximum = value;
            if (this.value < minimum)
                this.value = minimum;
            this.Invalidate();
        }
    }

    public int Maximum
    {
        get { return maximum; }
        set
        {
            maximum = value;
            if (value < minimum)
                minimum = value;
            if (this.value > maximum)
                this.value = maximum;
            this.Invalidate();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (Parent != null)
        {
            MaximumSize = new Size(Parent.Width, 62);
            MinimumSize = new Size(Height, 0);
        }

        Rectangle crect = ClientRectangle;
        crect.Inflate(5, 5);
        crect.Offset(-2, -2);

        if (BackColor != Color.Transparent)
        {
            e.Graphics.FillRectangle(new SolidBrush(BackColor), crect);
        }

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        int trackHeight;
        if (Height > 5)
        {
            trackHeight = Height - 4;
        }
        else
        {
            trackHeight = Height;
        }
        float thumbPosition = (float)((value - minimum) / (double)(maximum - minimum)) * (Width - 20);
        Rectangle trackRectBehindThumb = new Rectangle(2, (Height - trackHeight) / 2, (int)thumbPosition, trackHeight);
        Rectangle trackRectAfterThumb = new Rectangle((int)thumbPosition, (Height - trackHeight) / 2, Width - (int)thumbPosition, trackHeight);
        float radius = trackHeight / 2;

        Color trackColorBehindThumb = progressColor;

        int offset = 27;

        float multiplier = offset / (float)Width;

        float behindThumbOffset = trackRectBehindThumb.Width;
        behindThumbOffset *= multiplier;

        if (thumbPosition > Width / 2)
        {
            //i dont know why, but this just works
            behindThumbOffset -= (behindThumbOffset / (float)4.5);
        }

        trackRectBehindThumb.Inflate((int)-behindThumbOffset, 0);
        trackRectBehindThumb.Offset((int)-behindThumbOffset, 0);
        trackRectBehindThumb.Inflate(offset, 0);
        trackRectBehindThumb.Offset(offset, 0);

        int roundedradius = Convert.ToInt32(Math.Round(radius, 0));

        GraphicsPath trackPathAfterThumb = RoundedRectangle(trackRectAfterThumb, roundedradius);

        GraphicsPath trackPathBehindThumb = RoundedRectangle(trackRectBehindThumb, roundedradius);

        Rectangle wholeRect = e.ClipRectangle;
        wholeRect.Inflate(-1, -1);

        var outlinepath = RoundedRectangle(wholeRect, roundedradius);

        using (GraphicsPath thumbPath = new GraphicsPath())
        {



            using (SolidBrush trackBrushBehindThumb = new SolidBrush(trackColorBehindThumb))
            using (SolidBrush trackBrushAfterThumb = new SolidBrush(_bgColor))
            using (SolidBrush thumbBrush = new SolidBrush(progressColor))
            {

                e.Graphics.FillPath(trackBrushAfterThumb, trackPathAfterThumb);
                if (value != 0)
                {


                    e.Graphics.FillPath(trackBrushBehindThumb, trackPathBehindThumb);
                }


            }
        }

        e.Graphics.DrawPath(new Pen(BorderColor), outlinepath);
    }

    private GraphicsPath RoundedRectangle(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();

        path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90); path.AddArc(rect.Right - 2 * radius, rect.Y, radius * 2, radius * 2, 270, 90); path.AddArc(rect.Right - 2 * radius, rect.Bottom - 2 * radius, radius * 2, radius * 2, 0, 90); path.AddArc(rect.X, rect.Bottom - 2 * radius, radius * 2, radius * 2, 90, 90);
        path.CloseFigure();

        return path;
    }

    protected virtual void OnValueChanged(EventArgs e)
    {
        ValueChanged?.Invoke(this, e);
    }
}
