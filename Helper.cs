using System.Drawing;
using System.Drawing.Drawing2D;

namespace FourUI
{
    public static class Helper
    {
        public static GraphicsPath RoundedRectangle(Rectangle rectangle, int borderRadius)
        {
            GraphicsPath path = new GraphicsPath();

            int diameter = borderRadius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(rectangle.Location, size);

            path.AddArc(arc, 180, 90); arc.X = rectangle.Right - diameter - 1;
            path.AddArc(arc, 270, 90); arc.Y = rectangle.Bottom - diameter - 1;
            path.AddArc(arc, 0, 90); arc.X = rectangle.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();

            return path;
        }

        public static GraphicsPath RoundedRectangleXY(float rectX, float rectY, float rectWidth, float rectHeight, int cornerRadius)
        {
            GraphicsPath path = new GraphicsPath();

            int diameter = cornerRadius * 2;
            Size size = new Size(diameter, diameter);
            RectangleF arc = new RectangleF(rectX, rectY, diameter, diameter);

            path.AddArc(arc, 180, 90);
            arc.X = rectX + rectWidth - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = rectY + rectHeight - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = rectX;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}
