using FourUI;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class FourListBox : ListBox
{
    private int borderRadius = 4;

    public FourListBox()
    {
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        DoubleBuffered = true;
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.UserPaint, true);
        DrawMode = DrawMode.OwnerDrawFixed;
        BorderStyle = BorderStyle.None;
        ItemHeight = 30;
        ForeColor = Color.FromArgb(84,84,84);
        SelectionMode = SelectionMode.One;
        Invalidate();
    }

    //selected

    private Color selectedItemColor = Color.FromArgb(14,14,14);

    public Color SelectedItemColor
    {
        get { return selectedItemColor; }
        set
        {
            selectedItemColor = value;
            this.Invalidate();
        }
    }

    private Color selectedItemBorderColor = Color.FromArgb(32,32,32);

    public Color SelectedItemBorderColor
    {
        get { return selectedItemBorderColor; }
        set
        {
            selectedItemBorderColor = value;
            this.Invalidate();
        }
    }

    //unselected

    private Color unselectedItemColor = Color.FromArgb(10, 10, 10);

    public Color UnselectedItemColor
    {
        get { return unselectedItemColor; }
        set
        {
            unselectedItemColor = value;
            this.Invalidate();
        }
    }

    private Color unselectedItemBorderColor = Color.FromArgb(18,18,18);

    public Color UnselectedItemBorderColor
    {
        get { return unselectedItemBorderColor; }
        set
        {
            unselectedItemBorderColor = value;
            this.Invalidate();
        }
    }

    //others

    private Color backgroundColor = Color.FromArgb(10, 10, 10);

    public Color BackgroundColor
    {
        get { return backgroundColor; }
        set
        {
            backgroundColor = value;
            this.Invalidate();
        }
    }

    private Color backgroundBorderColor = Color.FromArgb(10,10,10);

    public Color BackgroundBorderColor
    {
        get { return backgroundBorderColor; }
        set
        {
            backgroundBorderColor = value;
            this.Invalidate();
        }
    }

    //forecolor

    private Color selectedForeColor = Color.FromArgb(124,124,124);

    public Color SelectedForeColor
    {
        get { return selectedForeColor; }
        set
        {
            selectedForeColor = value;
            this.Invalidate();
        }
    }

    private bool _hidebg = true;

    public bool HideBackground
    {
        get
        {
            return _hidebg;
        }
        set
        {
            _hidebg = value;
            SetStyle(ControlStyles.Opaque, _hidebg);
            Invalidate();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        SelectionMode = SelectionMode.One;
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        Rectangle cr = ClientRectangle;
        Rectangle backgroundRect = cr;
        backgroundRect.Inflate(5,5);
        backgroundRect.Offset(-1,-1);
        cr.Inflate(-1, -1);

        g.FillRectangle(new SolidBrush(backgroundColor), backgroundRect);

        GraphicsPath path2 = Helper.RoundedRectangle(cr, borderRadius);

        using (Brush itemBrush = new SolidBrush(BackgroundColor))
        {
            g.FillPath(itemBrush, path2);
        }

        using (Pen itemPen = new Pen(BackgroundBorderColor))
        {
            g.DrawPath(itemPen, path2);
        }

        for (int i = 0; i < Items.Count; i++)
        {
            Rectangle itemRect = GetItemRectangle(i);
            itemRect.Inflate(-4, -2);
            itemRect.Offset(0, 2);
            itemRect.Offset(0, -1);

            int yCenterString = itemRect.Y + (ItemHeight / 2) - (Font.Height)+4;

            GraphicsPath path = Helper.RoundedRectangle(itemRect, borderRadius);

            if (SelectedIndex == i)
            {
                using (Brush itemBrush = new SolidBrush(SelectedItemColor))
                {
                    g.FillPath(itemBrush, path);
                }

                using (Pen itemPen = new Pen(SelectedItemBorderColor))
                {
                    g.DrawPath(itemPen, path);
                }

                string itemText = Items[i].ToString();
                using (Brush textBrush = new SolidBrush(SelectedForeColor))
                {
                    g.DrawString(itemText, Font, textBrush, itemRect.X + 6, yCenterString);
                }
            }
            else
            {
                using (Brush itemBrush = new SolidBrush(UnselectedItemColor))
                {
                    g.FillPath(itemBrush, path);
                }

                using (Pen itemPen = new Pen(UnselectedItemBorderColor))
                {
                    g.DrawPath(itemPen, path);
                }

                string itemText = Items[i].ToString();
                using (Brush textBrush = new SolidBrush(ForeColor))
                {
                    g.DrawString(itemText, Font, textBrush, itemRect.X + 6, yCenterString);
                }
            }
        }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        int clickedIndex = IndexFromPoint(e.Location);

        if (clickedIndex >= 0 && clickedIndex < Items.Count)
        {
            SelectedIndex = clickedIndex;
            Invalidate();
        }

        base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        if (e.Button == MouseButtons.Left)
        {
            OnMouseDown(e);
        }
    }
}
