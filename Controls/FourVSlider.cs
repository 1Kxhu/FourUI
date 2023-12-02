using FourUI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class FourVSlider : Control
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

    public FourVSlider()
    {
        this.Size = new Size(30, 200);
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
            int trackWidth = 8;
            float thumbPosition = (float)((value - minimum) / (double)(maximum - minimum)) * (Height - 20);
            Rectangle trackRectBehindThumb = new Rectangle((Width - trackWidth) / 2, 4, trackWidth, (int)thumbPosition);
            Rectangle trackRectAfterThumb = new Rectangle((Width - trackWidth) / 2, (int)(thumbPosition + 10), trackWidth, Height - (int)thumbPosition - 16);
            int radius = trackWidth / 2;

            Color trackColorBehindThumb = thumbColor;

            trackPathBehindThumb.AddPath(Helper.RoundedRectangle(trackRectBehindThumb, radius), true);
            trackPathAfterThumb.AddPath(Helper.RoundedRectangle(trackRectAfterThumb, radius), true);

            using (GraphicsPath thumbPath = new GraphicsPath())
            {
                int thumbSize = 20;
                Rectangle thumbRect = new Rectangle((Width - thumbSize) / 2, (int)thumbPosition, thumbSize, thumbSize);
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
        RectangleF thumbRect = new RectangleF((Width - thumbSize) / 2, (float)((value - minimum) / (double)(maximum - minimum)) * (Height - thumbSize), thumbSize, thumbSize); // Adjust for vertical layout
        if (thumbRect.Contains(e.Location))
        {
            isDragging = true;
        }
        else
        {
            int newValue = (int)(((double)(e.Y - thumbSize / 2) / (Height - 20) * (maximum - minimum)) + minimum); // Adjust for vertical layout
            Value = ((Value * 2) + newValue) / 3;
            isDragging = true;
        }
    }

    private void _MouseMove(object sender, MouseEventArgs e)
    {
        int thumbSize = 20;
        if (isDragging)
        {
            int newValue = (int)(((double)(e.Y - thumbSize / 2) / (Height - 20) * (maximum - minimum)) + minimum); // Adjust for vertical layout
            Value = ((Value * 2) + newValue) / 3;
        }
    }


    private void _MouseUp(object sender, MouseEventArgs e)
    {
        isDragging = false;
    }

    protected virtual void OnValueChanged(EventArgs e)
    {
        ValueChanged?.Invoke(this, e);
    }
}
