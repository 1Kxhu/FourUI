using SkiaSharp;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class FourTextBox : Control
{
    private string text = string.Empty;
    private bool isFocused = false;

    private bool isCaretVisible = true;
    private Timer caretBlinkTimer;

    private Color unfocusedbg = Color.FromArgb(21,21,21);
    private Color unfocusedborder = Color.FromArgb(41,41,41);

    private Color focusedbg = Color.FromArgb(31,31,31);
    private Color focusedborder = Color.FromArgb(51,51,51);

    private Color caretcolor = Color.FromArgb(221,221,221);

    int borderRadius = 5;
    int borderSize = 1;

    [Browsable(true)]
    [Category("FourUI")]
    [Description("The background color when unfocused.")]
    public Color UnfocusedBackgroundColor
    {
        get { return unfocusedbg; }
        set { unfocusedbg = value; Invalidate(); }
    }

    [Browsable(true)]
    [Category("FourUI")]
    [Description("The border color when unfocused.")]
    public Color UnfocusedBorderColor
    {
        get { return unfocusedborder; }
        set { unfocusedborder = value; Invalidate(); }
    }

    [Browsable(true)]
    [Category("FourUI")]
    [Description("The background color when focused.")]
    public Color FocusedBackgroundColor
    {
        get { return focusedbg; }
        set { focusedbg = value; Invalidate(); }
    }

    [Browsable(true)]
    [Category("FourUI")]
    [Description("The border color when focused.")]
    public Color FocusedBorderColor
    {
        get { return focusedborder; }
        set { focusedborder = value; Invalidate(); }
    }

    [Browsable(true)]
    [Category("FourUI")]
    [Description("The border color when focused.")]
    public Color CaretColor
    {
        get { return caretcolor; }
        set { caretcolor = value; Invalidate(); }
    }

    [Browsable(true)]
    [Category("FourUI")]
    [Description("The radius of the rounding.")]
    public int BorderRadius
    {
        get { return borderRadius; }
        set { borderRadius = value; Invalidate(); }
    }

    [Browsable(true)]
    [Category("FourUI")]
    [Description("The size of the border around the control.")]
    public int BorderSize
    {
        get { return borderSize; }
        set { borderSize = value; Invalidate(); }
    }

    public FourTextBox()
    {
        SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);

        this.BackColor = Color.Transparent;

        this.Font = new Font("Segoe UI", 12f);
        this.Size = new Size(200, 30);

        this.Click += CustomTextBox_Click;
        this.GotFocus += CustomTextBox_GotFocus;
        this.LostFocus += CustomTextBox_LostFocus;
        this.KeyPress += CustomTextBox_KeyPress;
        this.MouseLeave += mouseleave;
        this.MouseDown += mousedown;

        this.TextChanged += textchanged;

        caretBlinkTimer = new Timer();
        caretBlinkTimer.Interval = 500; // Set the blinking interval here (e.g., 500ms)
        caretBlinkTimer.Tick += CaretBlinkTimer_Tick;
        caretBlinkTimer.Start(); // Start the timer
    }

    private void CaretBlinkTimer_Tick(object sender, EventArgs e)
    {
        // Toggle the caret visibility
        isCaretVisible = !isCaretVisible;
        Invalidate(); // Force a repaint to update caret visibility
    }

    private void mouseleave(object sender, EventArgs e)
    {
        this.Parent.Focus();
        isFocused = false;
        caretBlinkTimer.Stop();
        this.Invalidate();
    }


    private void textchanged(object sender, EventArgs e)
    {
        this.Invalidate();
    }

    private void mousedown(object sender, EventArgs e)
    {
        this.Focus();
        isFocused = true;
        caretBlinkTimer.Start();
        this.Invalidate();
    }

    private void CustomTextBox_Click(object sender, EventArgs e)
    {
        isFocused = true;
        Focus();
    }

    private void CustomTextBox_GotFocus(object sender, EventArgs e)
    {
        isFocused = true;
        this.Invalidate();
    }

    private void CustomTextBox_LostFocus(object sender, EventArgs e)
    {
        isFocused = false;
        
        this.Invalidate();
    }

    private void CustomTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (isFocused)
        {
            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)Keys.Back && Text.Length > 0)
                {
                    Text = Text.Substring(0, Text.Length - 1);
                }
            }
            else
            {
                Text += e.KeyChar;
            }
        }

        this.Invalidate();
    }

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x20; return cp;
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        //base.OnPaint(e);
 

        Pen caretpen = new Pen(caretcolor);

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;



        // Draw the border with rounded corners
        using (GraphicsPath path = CreateRoundedRectanglePath(new Rectangle(0+1, 0+1, Width - 1, Height - 1), borderRadius))
        {
            e.Graphics.FillPath(new SolidBrush(isFocused ? focusedbg : unfocusedbg), path);
            using (Pen borderPen = new Pen(isFocused ? focusedborder : unfocusedborder, borderSize))
            {
                e.Graphics.DrawPath(borderPen, path);
            }

   
        }


        // Draw the text
        

     

        TextRenderer.DrawText(e.Graphics, Text, Font, new Point(5, (Height - Font.Height) / 2), ForeColor);



        if (isFocused)
        {
            if (isCaretVisible) // Draw the caret if it's visible
            {
                var cursorX = TextRenderer.MeasureText(Text, Font).Width;
                e.Graphics.DrawLine(caretpen, cursorX, (Height / 2) - 10, cursorX, (Height / 2) + 10);
            }
        }
    }



    // Helper method to create a rounded rectangle path
    private GraphicsPath CreateRoundedRectanglePath(Rectangle rectangle, int borderRadius)
    {
        GraphicsPath path = new GraphicsPath();

        int diameter = borderRadius * 2;
        Size size = new Size(diameter, diameter);
        Rectangle arc = new Rectangle(rectangle.Location, size);

        path.AddArc(arc, 180, 90); // Top left arc
        arc.X = rectangle.Right - diameter -1;
        path.AddArc(arc, 270, 90); // Top right arc
        arc.Y = rectangle.Bottom - diameter -1;
        path.AddArc(arc, 0, 90);   // Bottom right arc
        arc.X = rectangle.Left;
        path.AddArc(arc, 90, 90);  // Bottom left arc

        path.CloseFigure();

        return path;
    }


    protected override void WndProc(ref Message m)
    {
        const int WM_MOUSEACTIVATE = 0x0021;
        const int MA_NOACTIVATE = 0x0003;

        if (m.Msg == WM_MOUSEACTIVATE && !this.Focused)
        {
            // Prevent the control from being activated when clicked
            m.Result = (IntPtr)MA_NOACTIVATE;
            return;
        }

        base.WndProc(ref m);
    }

}
