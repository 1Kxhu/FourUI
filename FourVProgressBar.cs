using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

public class FourVProgressBar : Control
{
    private int value = 0;
    private int minimum = 0;
    private int maximum = 100;

    Color progressColor = Color.FromArgb(33, 133, 255);
    Color _bgColor = Color.FromArgb(21, 21, 21);

    [Browsable(true)]
    [Category("FourUI")]
    [Description("The progress color.")]
    public Color ProgressColor
    {
        get { return progressColor; }
        set { progressColor = value; Invalidate(); }
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
        this.Size = new Size(200, 30);
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

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using (GraphicsPath trackPathBehindThumb = new GraphicsPath())
        using (GraphicsPath trackPathAfterThumb = new GraphicsPath())
        {
            int trackHeight = 0;
            if (Height > 5)
            {
                trackHeight = Height - 4;
            }
            else
            {
                trackHeight = Height;
            }
            float thumbPosition = (float)((value - minimum) / (double)(maximum - minimum)) * (Width - 20);
            RectangleF trackRectBehindThumb = new RectangleF(2, (Height - trackHeight) / 2, thumbPosition, trackHeight);
            RectangleF trackRectAfterThumb = new RectangleF(thumbPosition, (Height - trackHeight) / 2, Width - thumbPosition, trackHeight);
            float radius = trackHeight / 2;

            Color trackColorBehindThumb = progressColor;

            int offset = 2;

            if (Value == 1)
            {
                offset = 7;
            }

            if (Value == 2)
            {
                offset = 3;
            }

            //blue thingy
            trackPathBehindThumb.AddArc(trackRectBehindThumb.Left, trackRectBehindThumb.Top, radius * 2, radius * 2, 180, 90);
            trackPathBehindThumb.AddArc(trackRectBehindThumb.Right + offset + (trackHeight / 2) - radius * 2, trackRectBehindThumb.Top, radius * 2, radius * 2, 270, 90);
            trackPathBehindThumb.AddArc(trackRectBehindThumb.Right + offset + (trackHeight / 2) - radius * 2, trackRectBehindThumb.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            trackPathBehindThumb.AddArc(trackRectBehindThumb.Left, trackRectBehindThumb.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);

            //gray thingy
            trackPathAfterThumb.AddArc(trackRectAfterThumb.Left, trackRectAfterThumb.Top, radius * 2, radius * 2, 180, 90);
            trackPathAfterThumb.AddArc(trackRectAfterThumb.Right - trackHeight, trackRectAfterThumb.Top, radius * 2, radius * 2, 270, 90);
            trackPathAfterThumb.AddArc(trackRectAfterThumb.Right - trackHeight, trackRectAfterThumb.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            trackPathAfterThumb.AddArc(trackRectAfterThumb.Left, trackRectAfterThumb.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            trackPathAfterThumb.CloseFigure();

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
        }
    }






    public void Wait(int time)
    {
        Thread thread = new Thread(delegate ()
        {
            System.Threading.Thread.Sleep(time);
        });
        thread.Start();
        while (thread.IsAlive)
            Application.DoEvents();
    }

    protected virtual void OnValueChanged(EventArgs e)
    {
        ValueChanged?.Invoke(this, e);
    }
}
