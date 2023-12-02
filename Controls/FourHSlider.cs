using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

public class FourHSlider : Control
{
    private int value = 0;
    private int minimum = 0;
    private int maximum = 100;
    private bool isDragging = false;

    Color thumbColor = Color.FromArgb(33, 133, 255);
    Color trackColorAfterThumb = Color.FromArgb(21, 21, 21);

    [Browsable(true)]
    [Category("FourUI")]
    [Description("The thumb color.")]
    public Color ThumbColor
    {
        get { return thumbColor; }
        set { thumbColor = value; Invalidate(); }
    }

    [Browsable(true)]
    [Category("FourUI")]
    [Description("The color of the track.")]
    public Color TrackColor
    {
        get { return trackColorAfterThumb; }
        set { trackColorAfterThumb = value; Invalidate(); }
    }


    public event EventHandler ValueChanged;

    public FourHSlider()
    {
        this.Size = new Size(200, 30);
        this.DoubleBuffered = true;

        this.MouseDown += _MouseDown;
        this.MouseUp += _MouseUp;
        this.MouseMove += _MouseMove;
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

        using (GraphicsPath trackPathBehindThumb = new GraphicsPath())
        using (GraphicsPath trackPathAfterThumb = new GraphicsPath())
        {
            int trackHeight = 8;
            float thumbPosition = (float)((value - minimum) / (double)(maximum - minimum)) * (Width - 20);
            RectangleF trackRectBehindThumb = new RectangleF(2, (Height - trackHeight) / 2, thumbPosition, trackHeight);
            RectangleF trackRectAfterThumb = new RectangleF(thumbPosition + 20, (Height - trackHeight) / 2, Width - thumbPosition - 20, trackHeight);
            float radius = trackHeight / 2;

            Color trackColorBehindThumb = thumbColor;

            trackPathBehindThumb.AddArc(trackRectBehindThumb.Left, trackRectBehindThumb.Top, radius * 2, radius * 2, 180, 90);
            trackPathBehindThumb.AddArc(trackRectBehindThumb.Right + 6 - radius * 2, trackRectBehindThumb.Top, radius * 2, radius * 2, 270, 90);
            trackPathBehindThumb.AddArc(trackRectBehindThumb.Right + 6 - radius * 2, trackRectBehindThumb.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            trackPathBehindThumb.AddArc(trackRectBehindThumb.Left, trackRectBehindThumb.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);


            trackPathAfterThumb.AddArc(trackRectAfterThumb.Left - 6, trackRectAfterThumb.Top, radius * 2, radius * 2, 180, 90);
            trackPathAfterThumb.AddArc(trackRectAfterThumb.Right - 1 - radius * 2, trackRectAfterThumb.Top, radius * 2, radius * 2, 270, 90);
            trackPathAfterThumb.AddArc(trackRectAfterThumb.Right - 1 - radius * 2, trackRectAfterThumb.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            trackPathAfterThumb.AddArc(trackRectAfterThumb.Left - 6, trackRectAfterThumb.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            trackPathAfterThumb.CloseFigure();

            using (GraphicsPath thumbPath = new GraphicsPath())
            {
                int thumbSize = 20;
                RectangleF thumbRect = new RectangleF(thumbPosition, (Height - thumbSize) / 2, thumbSize, thumbSize);
                radius = thumbSize / 2;
                thumbPath.AddEllipse(thumbRect.Left, thumbRect.Top, thumbSize, thumbSize);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;


                using (SolidBrush trackBrushBehindThumb = new SolidBrush(trackColorBehindThumb))
                using (SolidBrush trackBrushAfterThumb = new SolidBrush(trackColorAfterThumb))
                using (SolidBrush thumbBrush = new SolidBrush(thumbColor))
                {
                    e.Graphics.FillPath(trackBrushBehindThumb, trackPathBehindThumb);
                    e.Graphics.FillPath(trackBrushAfterThumb, trackPathAfterThumb);
                    e.Graphics.FillPath(thumbBrush, thumbPath);
                }
            }
        }
    }

    private void _MouseDown(object sender, MouseEventArgs e)
    {
        int thumbSize = 20;
        RectangleF thumbRect = new RectangleF((float)((value - minimum) / (double)(maximum - minimum)) * (Width - thumbSize), (Height - thumbSize) / 2, thumbSize, thumbSize);
        if (thumbRect.Contains(e.Location))
        {
            isDragging = true;
        }
        else
        {
            int newValue = (int)(((double)(e.X - thumbSize / 2 - (0 / 20)) / (Width - 20) * (maximum - minimum)) + minimum);
            Value = ((Value * 2) + newValue) / 3;
            isDragging = true;
        }
    }

    private void _MouseUp(object sender, MouseEventArgs e)
    {
        isDragging = false;
    }

    private void _MouseMove(object sender, MouseEventArgs e)
    {
        int thumbSize = 20;
        if (isDragging)
        {
            int newValue = (int)(((double)(e.X - thumbSize / 2 - (0 / 20)) / (Width - 20) * (maximum - minimum)) + minimum);

            Value = ((Value * 2) + newValue) / 3;
        }
    }

    protected virtual void OnValueChanged(EventArgs e)
    {
        ValueChanged?.Invoke(this, e);
    }
}
