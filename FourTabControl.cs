using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class FourTabControl : TabControl
{
    public int cornerRadius { get; set; } = 5;
    public Color backColor = Color.White;
    public Color UnselectedTabBackgroundColor { get; set; } = Color.FromArgb(21, 21, 21);
    public Color SelectedTabBackgroundColor { get; set; } = Color.FromArgb(42, 42, 42);
    public Color HoveredTabBackgroundColor { get; set; } = Color.FromArgb(31, 31, 31);
    public Color TabBorderColor { get; set; } = Color.FromArgb(42, 42, 42);
    public Color TabTextColor { get; set; } = Color.Silver;
    public int widthExtend { get; set; } = 3;
    public int widthInflate { get; set; } = 2;
    public bool ShowInline { get; set; } = false;

    private int hoveredTabIndex = -1;

    public Color BackgroundColor
    {
        get { return backColor; }
        set { backColor = value; Invalidate(); }

    }


    public FourTabControl()
    {
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.DoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        Appearance = TabAppearance.FlatButtons;
        ItemSize = new Size(200, 40);

    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        using (var brush = new SolidBrush(BackgroundColor))
        {
            e.Graphics.FillRectangle(brush, ClientRectangle);
        }

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        for (int i = 0; i < TabCount; i++)
        {
            var tabRect = GetTabRect(i);
            tabRect.Inflate(widthInflate, 0);
            tabRect.Offset(widthExtend, 0);

            var path = RoundedRectangle(tabRect, cornerRadius);
            Brush tabBrush;

            if (i == SelectedIndex)
            {
                tabBrush = new SolidBrush(SelectedTabBackgroundColor);
            }
            else if (i == hoveredTabIndex)
            {
                tabBrush = new SolidBrush(HoveredTabBackgroundColor);
            }
            else
            {
                tabBrush = new SolidBrush(UnselectedTabBackgroundColor);
            }

            Pen tabBorderPen = new Pen(TabBorderColor);

            e.Graphics.FillPath(tabBrush, path);
            e.Graphics.DrawPath(tabBorderPen, path);
            e.Graphics.DrawString(TabPages[i].Text, Font, new SolidBrush(TabTextColor), tabRect, new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            });

            /* if (i == SelectedIndex)     <----- this is still in works, a close button. i just wanna push this bugfix for the drag right now.
            {

                Point loc = new Point(tabRect.Right-13, 1+tabRect.Height/3);
                Rectangle close = new Rectangle(loc, new Size(1+GetTabRect(i).Height/4, 1+GetTabRect(i).Height / 4));
                e.Graphics.FillPath(new SolidBrush(Color.Red), RoundedRectangle(close, 2));

            }  */



        }
    }


    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        for (int i = 0; i < TabCount; i++)
        {
            if (GetTabRect(i).Contains(e.Location))
            {
                if (hoveredTabIndex != i)
                {
                    hoveredTabIndex = i;
                    Invalidate();
                }
                break;
            }
        }


    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        if (hoveredTabIndex != -1)
        {
            hoveredTabIndex = -1;
            Invalidate();
        }
    }

    private GraphicsPath RoundedRectangle(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();

        path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90); path.AddArc(rect.Right - 2 * radius, rect.Y, radius * 2, radius * 2, 270, 90); path.AddArc(rect.Right - 2 * radius, rect.Bottom - 2 * radius, radius * 2, radius * 2, 0, 90); path.AddArc(rect.X, rect.Bottom - 2 * radius, radius * 2, radius * 2, 90, 90);
        path.CloseFigure();

        return path;
    }
}
