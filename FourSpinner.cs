using System;
using System.Drawing;
using System.Windows.Forms;

public class FourSpinner : Control
{
    private float rotationAngle = 0;
    private DateTime lastRenderTime = DateTime.Now;
    private int sweepAngle = 270;
    private int borderSize = 1;

    public FourSpinner()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
        Application.Idle += Application_Idle;

        ForeColor = Color.FromArgb(33, 133, 255);
    }

    public int SweepAngle
    {
        get => sweepAngle;

        set
        {
            if (value > 0 && value <= 360)
                sweepAngle = value;
            else
                throw new ArgumentException("SweepAngle must be between 1 and 360 degrees.");
        }
    }

    public int BorderSize
    {
        get => borderSize;

        set
        {
            if (value > 0)
                borderSize = value;
            else
                throw new ArgumentException("BorderSize must be greater than 0.");
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        int centerX = Width / 2;
        int centerY = Height / 2;

        e.Graphics.TranslateTransform(centerX-1, centerY-1);
        e.Graphics.RotateTransform(rotationAngle);
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        int spinnerSize = Math.Min(centerX, centerY);
        int spinnerThickness = (spinnerSize / 10) * borderSize;

        using (var pen = new Pen(ForeColor, spinnerThickness))
        {
            float startAngle = 0;
            e.Graphics.DrawArc(pen, -spinnerSize / 2, -spinnerSize / 2, spinnerSize, spinnerSize, startAngle, sweepAngle);
        }
    }

    private const float RotationSpeed = 180.0f;

    private void Application_Idle(object sender, EventArgs e)
    {
        DateTime currentTime = DateTime.Now;
        double elapsedMilliseconds = (currentTime - lastRenderTime).TotalMilliseconds;
        float angleChange = (float)(RotationSpeed * elapsedMilliseconds / 900.0);
        rotationAngle = (rotationAngle + angleChange) % 360;
        lastRenderTime = currentTime;
        Invalidate();
    }
}
