using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Principal;
using System.Windows.Forms;

public class FourStarRating : Control
{
    private float rating = 0.0f; private Color starColor = Color.FromArgb(255, 174, 35);

    public event EventHandler RatingChanged;

    public float Rating
    {
        get { return rating; }
        set
        {
            if (value < 0.0f)
                rating = 0.0f;
            else if (value > 10.0f)
                rating = 10.0f;
            else
                rating = value;

            RatingChanged?.Invoke(this, EventArgs.Empty);
            Invalidate();
        }
    }

    public Color StarColor
    {
        get { return starColor; }
        set
        {
            starColor = value;
            Invalidate();
        }
    }

    public FourStarRating()
    {
        Size = new Size(152, 28);
        DoubleBuffered = true;
        MinimumSize = new Size(21, 4);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);





        Graphics g = e.Graphics;
        g.InterpolationMode = InterpolationMode.NearestNeighbor; g.SmoothingMode = SmoothingMode.AntiAlias;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        int starCount = 5;
        int starWidth = Height - 2;
        int spacing = 5;

        for (int i = 0; i < starCount; i++)
        {
            int starLeft = i * (starWidth + spacing);
            Rectangle starRect = new Rectangle(starLeft, 0, starWidth, this.Height);
            starRect.Offset(starWidth / 2, 0);
            GraphicsPath starPath = GetRounded5PointStarPath(starLeft + starWidth / 2, this.Height / 2, starWidth / 2, starWidth / 3.8f, 5);

            if ((i + 1) * 2 <= Rating)
            {
                g.FillPath(new SolidBrush(starColor), starPath);
            }
            else if (i * 2 + 1 == Rating)
            {
                g.FillPath(new SolidBrush(starColor), starPath);
                g.FillRectangle(new SolidBrush(BackColor), starRect);
            }

            g.DrawPath(new Pen(starColor), starPath);
        }
    }





    public static GraphicsPath GetRounded5PointStarPath(float centerX, float centerY, float outerRadius, float innerRadius, int numPoints)
    {
        if (numPoints % 2 == 0 || numPoints < 5)
        {
            throw new ArgumentException("Number of points must be an odd number and greater than or equal to 5.");
        }

        var path = new GraphicsPath();
        float angleIncrement = 360f / numPoints;
        float currentAngle = -90f;
        PointF[] points = new PointF[numPoints * 2];

        for (int i = 0; i < numPoints * 2; i += 2)
        {
            points[i] = PointOnCircle(centerX, centerY, outerRadius, currentAngle);
            points[i + 1] = PointOnCircle(centerX, centerY, innerRadius, currentAngle + angleIncrement / 2);
            currentAngle += angleIncrement;
        }

        path.AddPolygon(points);

        return path;
    }

    private static PointF PointOnCircle(float centerX, float centerY, float radius, float angleInDegrees)
    {
        float angleInRadians = (float)(angleInDegrees * Math.PI / 180.0);
        float x = centerX + radius * (float)Math.Cos(angleInRadians);
        float y = centerY + radius * (float)Math.Sin(angleInRadians);
        return new PointF(x, y);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        if (e.Button == MouseButtons.Left)
        {
            float starWidth = Height - 2;
            int spacing = 5;
            int starCount = 5;

            int mouseX = e.X + 5;

            if (mouseX < 0)
            {
                Rating = 0;
            }
            else if (mouseX > starCount * (starWidth + spacing))
            {
                Rating = 10;
            }
            else
            {
                int starClicked = (mouseX - spacing) / ((int)starWidth + spacing);
                float remainder = (mouseX - spacing) % (starWidth + spacing);

                if (remainder > starWidth / 2)
                {
                    Rating = (starClicked + 1) * 2;
                }
                else
                {
                    Rating = starClicked * 2 + 1;
                }
            }
        }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);

        float starWidth = Height - 2;
        int spacing = 5;
        int starCount = 5;

        int mouseX = e.X + 5;

        if (mouseX < 0)
        {
            Rating = 0;
        }
        else if (mouseX > starCount * (starWidth + spacing))
        {
            Rating = 10;
        }
        else
        {
            int starClicked = (mouseX - spacing) / ((int)starWidth + spacing);
            float remainder = (mouseX - spacing) % (starWidth + spacing);

            if (remainder > starWidth / 2)
            {
                Rating = (starClicked + 1) * 2;
            }
            else
            {
                Rating = starClicked * 2 + 1;
            }
        }
    }




}
