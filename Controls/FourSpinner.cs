using System;
using System.Drawing;
using System.Windows.Forms;

public class FourSpinner : Control
{
    private float rotationAngle = 0;
    private DateTime lastRenderTime = DateTime.Now;
    private int sweepAngle = 270;
    private int _thickness = 1;
    private int rotationSpeed = 1;

    public enum SpinnerTypes
    {
        DefaultSpinner,
        PacMan
    }

    public SpinnerTypes SpinnerType { get; set; } = SpinnerTypes.DefaultSpinner;


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

    public int RotationSpeed
    {
        get => rotationSpeed;

        set
        {
            if (value > 0 && value <= 360)
                rotationSpeed = value;
            else
                throw new ArgumentException("SweepAngle must be between 1 and 360 degrees.");
        }
    }

    public int Thickness
    {
        get => _thickness;

        set
        {
            if (value > 0)
                _thickness = value;
            else
                throw new ArgumentException("Thickness must be greater than 0.");
        }
    }

    int pulsingSize;
    bool animatingphase = false;

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        int centerX = Width / 2;
        int centerY = Height / 2;

        e.Graphics.TranslateTransform(centerX, centerY);
        e.Graphics.RotateTransform(rotationAngle);
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        int spinnerSize = Math.Min(centerX, centerY);
        int spinnerThickness = (spinnerSize / 10) * _thickness;

        if (SpinnerType == SpinnerTypes.DefaultSpinner)
        {


            using (var pen = new Pen(ForeColor, spinnerThickness))
            {
                float startAngle = 0;
                e.Graphics.DrawArc(pen, -spinnerSize / 2, -spinnerSize / 2, spinnerSize, spinnerSize, startAngle, sweepAngle);
            }
        }
        else if (SpinnerType == SpinnerTypes.PacMan)
        {

            int newspinnerSize = pulsingSize;

            if (pulsingSize < 1)
            {
                pulsingSize = 1;
                animatingphase = false;
            }
            else
            {
                spinnerSize = newspinnerSize;
            }

            using (var pen = new Pen(ForeColor, newspinnerSize))
            {
                float startAngle = 0;
                e.Graphics.DrawArc(pen, -spinnerSize / 2, -spinnerSize / 2, spinnerSize, spinnerSize, startAngle, sweepAngle);
            }

        }
    }

    private void Application_Idle(object sender, EventArgs e)
    {
        DateTime currentTime = DateTime.Now;
        double elapsedMilliseconds = (currentTime - lastRenderTime).TotalMilliseconds;
        float angleChange = (float)((RotationSpeed * 90f) * elapsedMilliseconds / 900.0);
        rotationAngle = (rotationAngle + angleChange) % 360;
        lastRenderTime = currentTime;

        int centerX = Width / 2;
        int centerY = Height / 2;
        pulsingSize = Math.Min(centerX, centerY); ;
        if (animatingphase)
        {
            pulsingSize += 1;
        }
        else
        {
            pulsingSize -= 1;
        }

        if (pulsingSize < 1)
        {
            pulsingSize = Math.Min(centerX, centerY);
        }

        if (pulsingSize > Math.Min(centerX, centerY) + 5)
        {
            animatingphase = false;
        }
        else if (pulsingSize < Math.Min(centerX, centerY) - 5)
        {
            animatingphase = true;
        }


        Invalidate();
    }
}
