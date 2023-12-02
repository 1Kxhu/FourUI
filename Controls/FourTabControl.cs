using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

public class FourTabControl : TabControl
{
    //did regions because this is too much at once

    #region Colors
    private Color backColor = Color.White;
    [Browsable(true)]
    [Category("FourUI")]
    [Description("The variable name explains itself")]
    public Color UnselectedTabBackgroundColor { get; set; } = Color.FromArgb(10, 10, 10);
    [Browsable(true)]
    [Category("FourUI")]
    [Description("The variable name explains itself")]
    public Color SelectedTabBackgroundColor { get; set; } = Color.FromArgb(24, 24, 24);
    [Browsable(true)]
    [Category("FourUI")]
    [Description("The variable name explains itself")]
    public Color HoveredTabBackgroundColor { get; set; } = Color.FromArgb(14, 14, 14);

    [Browsable(true)]
    [Category("FourUI")]
    [Description("The variable name explains itself")]
    public Color UnselectedTabBorderColor { get; set; } = Color.FromArgb(24, 24, 24);
    [Browsable(true)]
    [Category("FourUI")]
    [Description("The variable name explains itself")]
    public Color SelectedTabBorderColor { get; set; } = Color.FromArgb(45, 45, 45);
    [Browsable(true)]
    [Category("FourUI")]
    [Description("The variable name explains itself")]
    public Color HoveredTabBorderColor { get; set; } = Color.FromArgb(34, 34, 34);

    [Browsable(true)]
    [Category("FourUI")]
    [Description("The variable name explains itself")]
    public Color UnselectedTabTextColor { get; set; } = Color.Silver;
    [Browsable(true)]
    [Category("FourUI")]
    [Description("The variable name explains itself")]
    public Color SelectedTabTextColor { get; set; } = Color.Silver;
    [Browsable(true)]
    [Category("FourUI")]
    [Description("The variable name explains itself")]
    public Color HoveredTabTextColor { get; set; } = Color.Silver;

    [Browsable(true)]
    [Category("FourUI")]
    [Description("The variable name explains itself")]
    public Color UnselectedCloseButtonColor { get; set; } = Color.FromArgb(76, 76, 76);
    [Browsable(true)]
    [Category("FourUI")]
    [Description("The variable name explains itself")]
    public Color SelectedCloseButtonColor { get; set; } = Color.FromArgb(173, 173, 173);
    [Browsable(true)]
    [Category("FourUI")]
    [Description("The variable name explains itself")]
    public Color HoveredCloseButtonColor { get; set; } = Color.FromArgb(100, 100, 100);

    [Browsable(true)]
    [Category("FourUI")]
    [Description("The color of the line inbetween tab rectangles.")]
    public Color InlineColor { get; set; } = Color.FromArgb(21, 21, 21);
    #endregion

    #region Misc
    [Browsable(true)]
    [Category("FourUI")]
    [Description("Rounding on tab rectangles.")]
    public int CornerRadius { get; set; } = 5;
    [Browsable(true)]
    [Category("FourUI")]
    [Description("Offset the position of the tab rectangle.")]
    public int WidthExtend { get; set; } = 3;
    [Browsable(true)]
    [Category("FourUI")]
    [Description("Expand the size of the tab rectangle.")]
    public int WidthInflate { get; set; } = 2;
    [Browsable(true)]
    [Category("FourUI")]
    [Description("Displays a line inbetween each tab.")]
    public bool ShowInline { get; set; } = false;
    #endregion


    private int hoveredTabIndex = -1;
    private int _tabopacity = 0;

    [Browsable(true)]
    [Category("FourUI")]
    [Description("The background color. (tabcontrol disabled backcolor)")]
    public Color BackgroundColor
    {
        get { return backColor; }
        set
        {
            if (backColor != value)
            {
                backColor = value;
                OnBackColorChanged(EventArgs.Empty);
                Invalidate();
            }
        }
    }

    [Browsable(true)]
    [Category("FourUI")]
    public int TabOpacity
    {
        get => _tabopacity;
        set
        {
            _tabopacity = Math.Max(0, Math.Min(100, value));
            Invalidate();
        }
    }

    public FourTabControl()
    {
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.DoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        Appearance = TabAppearance.FlatButtons;
        ItemSize = new Size(240, 40);
        Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
        BackgroundColor = Color.Transparent;
    }

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        if (m.Msg == 0x114 || m.Msg == 0x115)
        {
            this.Invalidate();
        }
    }

    private TabTypes dchoice = TabTypes.Disconnected;

    public enum TabTypes
    {
        Disconnected,
        Connected,
        Visual
    }


    [Browsable(true)]
    [Category("FourUI")]
    [Description("The style in which the control displays.")]
    public TabTypes TabType
    {
        get { return dchoice; }
        set
        {
            dchoice = value;
            Invalidate();
        }
    }

    private int privateTabHeight;

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (Parent != null)
        {
            using (var brush = new SolidBrush(Parent.BackColor))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }
        }
        else
        {
            using (var brush = new SolidBrush(BackgroundColor))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }
        }

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        if (dchoice == TabTypes.Disconnected)
        {

            for (int i = 0; i < TabCount; i++)
            {
                var tabRect = GetTabRect(i);
                tabRect.Inflate(WidthInflate, 0);
                tabRect.Offset(WidthExtend + 1, 0);

                privateTabHeight = tabRect.Height;

                var path = RoundedRectangle(tabRect, CornerRadius);
                Brush tabBrush;
                Pen tabBorderPen;
                Brush tabTextBrush;
                Brush closeBrush;

                if (i == SelectedIndex)
                {
                    tabBrush = new SolidBrush(SelectedTabBackgroundColor);
                    tabBorderPen = new Pen(SelectedTabBorderColor);
                    tabTextBrush = new SolidBrush(SelectedTabTextColor);
                    closeBrush = new SolidBrush(SelectedCloseButtonColor);
                }
                else if (i == hoveredTabIndex)
                {
                    tabBrush = new SolidBrush(HoveredTabBackgroundColor);
                    tabBorderPen = new Pen(HoveredTabBorderColor);
                    tabTextBrush = new SolidBrush(HoveredTabTextColor);
                    closeBrush = new SolidBrush(HoveredCloseButtonColor);
                }
                else
                {
                    tabBrush = new SolidBrush(UnselectedTabBackgroundColor);
                    tabBorderPen = new Pen(UnselectedTabBorderColor);
                    tabTextBrush = new SolidBrush(UnselectedTabTextColor);
                    closeBrush = new SolidBrush(UnselectedCloseButtonColor);
                }


                if (ShowInline && tabRect.Height > 12 && i + 1 < TabCount)
                {
                    int x = tabRect.Right + WidthExtend;
                    int y1 = tabRect.Top + 6;
                    int y2 = tabRect.Bottom - 6;
                    e.Graphics.DrawLine(new Pen(new SolidBrush(InlineColor)), new Point(x, y1), new Point(x, y2));
                }

                e.Graphics.FillPath(tabBrush, path);
                e.Graphics.DrawPath(tabBorderPen, path);
                Rectangle textRect = new Rectangle(tabRect.X - 6, tabRect.Y, tabRect.Width, tabRect.Height);
                e.Graphics.DrawString(TabPages[i].Text, Font, tabTextBrush, textRect, new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                });


                // close button code
                Point loc = new Point(tabRect.Right - 18, -1 + (tabRect.Height / 3));
                Rectangle close = new Rectangle(loc, new Size(1 + GetTabRect(i).Height / 3, 1 + GetTabRect(i).Height / 3));

                GraphicsPath cross = Cross(close, 2);
                e.Graphics.FillPath(closeBrush, cross);

            }
        }
        else if (dchoice == TabTypes.Connected)
        {

            for (int i = 0; i < TabCount; i++)
            {
                var tabRect = GetTabRect(i);
                tabRect.Inflate(WidthInflate, 0);
                tabRect.Offset(WidthExtend + 1, 3);

                privateTabHeight = tabRect.Height;

                var path = HalfRoundedRectangle(tabRect, CornerRadius);
                Brush tabBrush;
                Pen tabBorderPen;
                Brush tabTextBrush;
                Brush closeBrush;

                if (i == SelectedIndex)
                {
                    tabBrush = new SolidBrush(SelectedTabBackgroundColor);
                    tabBorderPen = new Pen(SelectedTabBorderColor);
                    tabTextBrush = new SolidBrush(SelectedTabTextColor);
                    closeBrush = new SolidBrush(SelectedCloseButtonColor);
                }
                else if (i == hoveredTabIndex)
                {
                    tabBrush = new SolidBrush(HoveredTabBackgroundColor);
                    tabBorderPen = new Pen(HoveredTabBorderColor);
                    tabTextBrush = new SolidBrush(HoveredTabTextColor);
                    closeBrush = new SolidBrush(HoveredCloseButtonColor);
                }
                else
                {
                    tabBrush = new SolidBrush(UnselectedTabBackgroundColor);
                    tabBorderPen = new Pen(UnselectedTabBorderColor);
                    tabTextBrush = new SolidBrush(UnselectedTabTextColor);
                    closeBrush = new SolidBrush(UnselectedCloseButtonColor);
                }


                if (ShowInline && tabRect.Height > 12 && i + 1 < TabCount)
                {
                    int x = tabRect.Right + WidthExtend;
                    int y1 = tabRect.Top + 6;
                    int y2 = tabRect.Bottom - 6;
                    e.Graphics.DrawLine(new Pen(new SolidBrush(InlineColor)), new Point(x, y1), new Point(x, y2));
                }

                e.Graphics.FillPath(tabBrush, path);
                e.Graphics.DrawPath(tabBorderPen, path);
                Rectangle textRect = new Rectangle(tabRect.X - 6, tabRect.Y, tabRect.Width, tabRect.Height);
                e.Graphics.DrawString(TabPages[i].Text, Font, tabTextBrush, textRect, new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                });


                // close button code
                Point loc = new Point(tabRect.Right - 18, 1 + (tabRect.Height / 3));
                Rectangle close = new Rectangle(loc, new Size(1 + GetTabRect(i).Height / 3, 1 + GetTabRect(i).Height / 3));

                GraphicsPath cross = Cross(close, 2);
                e.Graphics.FillPath(closeBrush, cross);

            }


        }
        else if (dchoice == TabTypes.Visual)
        {
            for (int i = 0; i < TabCount; i++)
            {
                var tabRect = GetTabRect(i);
                tabRect.Inflate(WidthInflate, 0);
                tabRect.Offset(WidthExtend + 1, (-tabRect.Height) + 3);

                privateTabHeight = tabRect.Height;

                var path = RoundedRectangle(tabRect, CornerRadius);
                Brush tabBrush = new SolidBrush(SelectedTabBackgroundColor);
                Pen tabBorderPen = new Pen(UnselectedTabBorderColor);
                Brush tabTextBrush = new SolidBrush(UnselectedTabTextColor);
                Brush closeBrush = new SolidBrush(UnselectedCloseButtonColor);

                if (i == SelectedIndex)
                {
                    tabBrush = new SolidBrush(SelectedTabBackgroundColor);
                    tabBorderPen = new Pen(SelectedTabBorderColor);
                    tabTextBrush = new SolidBrush(SelectedTabTextColor);
                    closeBrush = new SolidBrush(SelectedCloseButtonColor);
                }

                if (i != SelectedIndex)
                {
                    if (i == hoveredTabIndex)
                    {
                        tabBrush = new SolidBrush(BackgroundColor);
                        tabBorderPen = new Pen(BackgroundColor);
                        tabTextBrush = new SolidBrush(HoveredTabTextColor);
                        closeBrush = new SolidBrush(HoveredCloseButtonColor);
                    }
                    else
                    {
                        tabBrush = new SolidBrush(BackgroundColor);
                        tabBorderPen = new Pen(BackgroundColor);
                        tabTextBrush = new SolidBrush(UnselectedTabTextColor);
                        closeBrush = new SolidBrush(UnselectedCloseButtonColor);
                    }
                }


                if (ShowInline && tabRect.Height > 12 && i + 1 < TabCount)
                {
                    int x = tabRect.Right + WidthExtend;
                    int y1 = tabRect.Top + 6;
                    int y2 = tabRect.Bottom - 6;
                    e.Graphics.DrawLine(new Pen(new SolidBrush(InlineColor)), new Point(x, y1), new Point(x, y2));
                }

                e.Graphics.FillPath(tabBrush, path);
                e.Graphics.DrawPath(tabBorderPen, path);
                Rectangle textRect = new Rectangle(tabRect.X - 6, tabRect.Y, tabRect.Width, tabRect.Height * 3 - ((ItemSize.Height / 4)) + 8);
                e.Graphics.DrawString(TabPages[i].Text, Font, tabTextBrush, textRect, new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                });


                // close button code
                Point loc = new Point(tabRect.Right - 18, 1 + (tabRect.Height / 3));
                Rectangle close = new Rectangle(loc, new Size(1 + GetTabRect(i).Height / 3, 1 + GetTabRect(i).Height / 3));

                GraphicsPath cross = Cross(close, 2);
                e.Graphics.FillPath(closeBrush, cross);
            }
        }
        Color semiTransparentColor = Color.FromArgb((int)(TabOpacity * 2.55), BackgroundColor);

        Rectangle contentrectangle = this.ClientRectangle;

        //contentrectangle.Offset(0, privateTabHeight);

        using (SolidBrush semiTransparentBrush = new SolidBrush(semiTransparentColor))
        {
            e.Graphics.FillRectangle(semiTransparentBrush, contentrectangle);
        }
    }

    public void AddTabPage(string rawTabName, bool respectParentWidth)
    {
        int totaltabwidth = 0;

        for (int i = 3; i < TabCount; i++)
        {
            Rectangle rect = GetTabRect(i);
            totaltabwidth = totaltabwidth + (i * rect.Width);
        }

        if (respectParentWidth)
        {
            if (totaltabwidth < Parent.Width)
            {
                TabPage tb = new TabPage();
                tb.Text = UniqueTabName(rawTabName);
                TabPages.Add(tb);
            }
        }
        else
        {
            TabPage tb = new TabPage();
            tb.Text = UniqueTabName(rawTabName);
            TabPages.Add(tb);
        }
    }

    private string UniqueTabName(string rawTabName)
    {
        try
        {
            int count = 1;
            string tabName;

            while (true)
            {
                tabName = rawTabName + count;

                bool tabExists = false;
                foreach (TabPage tabPage in TabPages)
                {
                    if (tabPage.Text == tabName)
                    {
                        tabExists = true;
                        break;
                    }
                }

                if (!tabExists)
                {
                    return tabName;
                }

                count++;
            }

            // goes to fallback if it somehow escapes the loop
            throw new Exception();
        }
        catch
        {
            try
            {
                Random random = new Random();
                string fallbackstring = random.Next().ToString("x2");
                if (fallbackstring.Length > 5)
                {
                    fallbackstring = fallbackstring.Substring(0, 5);
                }
                return "fallback" + fallbackstring;
            }
            catch
            {
                // how
                return string.Empty;
            }
        }
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);

        if (e.Button == MouseButtons.Left && SelectedIndex != -1)
        {
            Point loc = new Point(GetTabRect(SelectedIndex).Right - 18, -1 + (GetTabRect(SelectedIndex).Height / 3));
            Rectangle close = new Rectangle(loc, new Size(4 + GetTabRect(SelectedIndex).Height / 4, 4 + GetTabRect(SelectedIndex).Height / 4));

            if (close.Contains(e.Location))
            {
                if (TabCount > 1)
                {
                    TabPages.RemoveAt(SelectedIndex);

                    if (SelectedIndex == SelectedIndex)
                    {
                        if (SelectedIndex > 0)
                            SelectedIndex = SelectedIndex - 1;
                        else
                            SelectedIndex = 0;
                    }
                }
            }
        }
        Invalidate();
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

    private GraphicsPath HalfRoundedRectangle(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();

        path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);               //left top
        path.AddArc(rect.Right - 2 * radius, rect.Y, radius * 2, radius * 2, 270, 90);      //right top
        path.AddArc(rect.Right - 2 * 1, rect.Bottom - 1 * 1, 1 * 2, 1 * 2, 0, 90); //right bottom
        path.AddArc(rect.X, rect.Bottom - 0 * 1, 1, 1, 90, 90); //left bottom

        path.CloseFigure();

        return path;
    }

    public static GraphicsPath Cross(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();

        int x = (rect.Left + rect.Right) / 2;
        int yCenter = (rect.Top + rect.Bottom) / 2;
        int width = rect.Width - 4;
        int height = rect.Height - 4;

        Rectangle horizontalRect = new Rectangle(x - width / 2, yCenter - height / 10, width, height / 5);
        path.AddRectangle(horizontalRect);
        Rectangle verticalRect = new Rectangle(x - height / 10, yCenter - height / 2, width / 5, height);
        path.AddRectangle(verticalRect);
        Rectangle centerRect = new Rectangle(x - width / 10, yCenter - height / 10, width / 5, height / 5);
        path.AddRectangle(centerRect);

        Matrix matrix = new Matrix();
        matrix.RotateAt(45, new Point(x, yCenter));
        path.Transform(matrix);

        return path;
    }
}
