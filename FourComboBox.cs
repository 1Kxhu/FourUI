using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class FourComboBox : ComboBox
{
    private Color _selectedItemForeColor = Color.Black;
    private Color _idleItemForeColor = Color.Black;
    private Color _dropdownBackground = Color.White;
    private Color _selecteddropdownBackground = Color.White;

    public Color SelectedItemForeColor
    {
        get { return _selectedItemForeColor; }
        set { _selectedItemForeColor = value; }
    }

    public Color IdleItemForeColor
    {
        get { return _idleItemForeColor; }
        set { _idleItemForeColor = value; }
    }

    public Color DropdownBackground
    {
        get { return _dropdownBackground; }
        set { _dropdownBackground = value; }
    }

    public Color SelectedDropdownBackground
    {
        get { return _selecteddropdownBackground; }
        set { _selecteddropdownBackground = value; }
    }

    public FourComboBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.UserPaint, true);
        DoubleBuffered = true;
        DrawMode = DrawMode.OwnerDrawFixed;
        FlatStyle = FlatStyle.Flat;

        MouseEnter += MetroComboBox_MouseEnter;
        MouseLeave += MetroComboBox_MouseLeave;
        SelectedIndexChanged += MetroComboBox_SelectedIndexChanged;
        Application.Idle += yes;



        Invalidate();
    }

    private bool isMouseOver = false;

    private void yes(object sender, EventArgs e)
    {
        if (isMouseOver)
        {
            expanderrotation = 90;
        }
        else
        {
            expanderrotation = 0;
        }

        Invalidate();
    }

    private void MetroComboBox_MouseEnter(object sender, EventArgs e)
    {
        isMouseOver = true;
        Invalidate();
    }

    private void MetroComboBox_MouseLeave(object sender, EventArgs e)
    {
        isMouseOver = false;
        Invalidate();
    }

    private void MetroComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        Parent.Focus();
        isMouseOver = false;
        expanderrotation = 0;
        Invalidate();

    }





    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        if (e.Index >= 0)
        {
            string itemText = Items[e.Index].ToString();
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;



            Color textColor = isSelected ? SelectedItemForeColor : IdleItemForeColor;
            Color backgroundColor = isSelected ? SelectedDropdownBackground : DropdownBackground;

            using (Brush textBrush = new SolidBrush(textColor))
            using (Brush backgroundBrush = new SolidBrush(backgroundColor))
            {
                Rectangle rect = e.Bounds;
                GraphicsPath path = RoundedRectangle(rect, 5);
                e.Graphics.FillRectangle(new SolidBrush(DropdownBackground), e.Bounds);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                PointF textLocation = new PointF(rect.X + (rect.Width - e.Graphics.MeasureString(itemText, e.Font).Width) / 2, e.Index * (ItemHeight) + (ItemHeight / 5));

                e.Graphics.FillPath(backgroundBrush, path);

                e.Graphics.DrawString(itemText, e.Font, textBrush, textLocation);

                path.Dispose();
            }
        }
    }




    int expanderrotation = 0;

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Rectangle ra = new Rectangle(0, 0, Width, Height);
        e.Graphics.FillRectangle(new SolidBrush(BackColor), ra);

        Rectangle rect = ClientRectangle;

        using (GraphicsPath path = RoundedRectangle(rect, 5))
        using (SolidBrush backBrush = new SolidBrush(DropdownBackground))
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillPath(backBrush, path);



            int width = 8; int height = 6; int x = Width - 20;
            int y = (Height - height) / 2;

            Point[] trianglePoints = new Point[]
            {
    new Point(x, y),
    new Point(x + width, y),
    new Point(x + (width / 2), y + height)
            };


            Matrix rotationMatrix = new Matrix();
            rotationMatrix.RotateAt(expanderrotation, new PointF(x + (width / 2), y + (height / 2)));

            rotationMatrix.TransformPoints(trianglePoints);

            e.Graphics.FillPolygon(new SolidBrush(IdleItemForeColor), trianglePoints);

            string selectedItemText = (SelectedItem != null) ? SelectedItem.ToString() : "";
            using (SolidBrush textBrush = new SolidBrush(isMouseOver ? IdleItemForeColor : SelectedItemForeColor))
            {
                SizeF textSize = e.Graphics.MeasureString(selectedItemText, Font);
                float textX = (Width - textSize.Width) / 2;
                float textY = (Height - textSize.Height) / 2;
                PointF textPosition = new PointF(textX, textY);

                e.Graphics.DrawString(selectedItemText, Font, textBrush, textPosition);
            }
        }
    }



    public static GraphicsPath CreateRoundedTriangle(PointF point1, PointF point2, PointF point3, float cornerRadius)
    {
        GraphicsPath path = new GraphicsPath();

        PointF[] trianglePoints = { point1, point2, point3 };
        path.AddPolygon(trianglePoints);

        PointF centroid = new PointF((point1.X + point2.X + point3.X) / 3f, (point1.Y + point2.Y + point3.Y) / 3f);
        float distanceToVertex = (float)(cornerRadius / Math.Sqrt(3));

        PointF controlPoint1 = new PointF(centroid.X + distanceToVertex, centroid.Y - cornerRadius);
        PointF controlPoint2 = new PointF(centroid.X - distanceToVertex, centroid.Y - cornerRadius);
        PointF controlPoint3 = new PointF(centroid.X, centroid.Y + (2 * cornerRadius));

        path.AddArc(new RectangleF(controlPoint1.X - cornerRadius, controlPoint1.Y - cornerRadius, 2 * cornerRadius, 2 * cornerRadius), 240, 60);
        path.AddArc(new RectangleF(controlPoint2.X - cornerRadius, controlPoint2.Y - cornerRadius, 2 * cornerRadius, 2 * cornerRadius), 300, 60);
        path.AddArc(new RectangleF(controlPoint3.X - cornerRadius, controlPoint3.Y - cornerRadius, 2 * cornerRadius, 2 * cornerRadius), 0, 60);

        return path;
    }
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x20; return cp;
        }
    }


    private GraphicsPath RoundedRectangle(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        int diameter = radius * 2;
        int arcWidth = diameter;
        int arcHeight = diameter;

        path.StartFigure();
        path.AddArc(rect.Left, rect.Top, arcWidth, arcHeight, 180, 90);
        path.AddArc(rect.Right - 1 - arcWidth, rect.Top, arcWidth, arcHeight, 270, 90);
        path.AddArc(rect.Right - 1 - arcWidth, rect.Bottom - arcHeight, arcWidth, arcHeight - 1, 0, 90);
        path.AddArc(rect.Left, rect.Bottom - arcHeight, arcWidth, arcHeight - 1, 90, 90);
        path.CloseFigure();

        return path;
    }
}
