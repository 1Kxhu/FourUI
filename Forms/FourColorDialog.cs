using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FourUI.Forms
{
    public partial class FourColorDialog : Form
    {
        public FourColorDialog()
        {
            InitializeComponent();

        }

        public Color defaultColor { get; set; } 
        public Color selectedColor { get; set; }
        public Color borderColor { get; set; } = Color.FromArgb(45, 45, 45);

        public Color buttonColor { get; set; } = Color.FromArgb(10,10,10);
        public Color buttonHoverColor { get; set; } = Color.FromArgb(14, 14, 14);
        public Color buttonPressColor { get; set; } = Color.FromArgb(21,21,21);


        public int buttonCornerRadius { get; set; } = 2;
        public int cornerRadius { get; set; } = 0;
        public int borderThickness { get; set; } = 1;

        private void FourColorDialog_Load(object sender, EventArgs e)
        {
            selectedColor = defaultColor;

            if (selectedColor == Color.Empty)
            {
                selectedColor = Color.Black;
            }

            fourPanel1.PanelColor = selectedColor;
            fourButton1.CornerRadius = buttonCornerRadius;
            fourButton2.CornerRadius = buttonCornerRadius;

            fourPictureBox1.CornerRadius = buttonCornerRadius;
            fourPanel1.CornerRadius = buttonCornerRadius;

            fourRound1.CornerRadius = cornerRadius + 1;
            fourBorder1.BorderRadius = cornerRadius;
            fourBorder2.BorderRadius = cornerRadius - 1;

            fourButton1.FillColor = buttonColor;
            fourButton2.FillColor = buttonColor;

            fourButton1.HoverColor = buttonHoverColor;
            fourButton2.HoverColor = buttonHoverColor;

            fourButton1.PressColor = buttonPressColor;
            fourButton2.PressColor = buttonPressColor;

            fourPanel1.PanelColor = buttonColor;

            fourTextBox1.Text = selectedColor.R.ToString();
            fourTextBox2.Text = selectedColor.G.ToString();
            fourTextBox3.Text = selectedColor.B.ToString();

            updateTextBoxes();
        }

        private void fourPictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point cursorPosition = Cursor.Position;
            if (e.Button == MouseButtons.Left)
            {
                using (Bitmap screenCapture = new Bitmap(1, 1))
                {
                    using (Graphics g = Graphics.FromImage(screenCapture))
                    {
                        g.CopyFromScreen(cursorPosition, new Point(0, 0), new Size(1, 1));

                        Color pixelColor = screenCapture.GetPixel(0, 0);
                        //so it wont be semi transparent (winforms hates semi transparency)
                        pixelColor = Color.FromArgb(255, pixelColor.R, pixelColor.G, pixelColor.B);
                        selectedColor = pixelColor;
                        fourPanel1.Hide();
                        fourPanel1.PanelColor = selectedColor;

                        fourPanel1.Refresh();
                        fourPanel1.Invalidate();
                        fourPanel1.Update();

                        fourPanel1.Show();

                        updateTextBoxes();

                        Invalidate();
                    }
                }
            }
        }

        private void updateTextBoxes()
        {
            fourTextBox1.Text = selectedColor.R.ToString();
            fourTextBox2.Text = selectedColor.G.ToString();
            fourTextBox3.Text = selectedColor.B.ToString();
        }

        private void fourButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void fourButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void fourPanel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);
            base.OnPaint(e);

            if (this != null)
            {
                if (cornerRadius == 0)
                {
                    fourBorder2.TargetForm = null;
                }
                else
                {
                    fourBorder2.TargetForm = this;
                }
            }
        }

        int R;
        int G;
        int B;

        private void fourTextBox1_TextChanged(object sender, EventArgs e)
        {
            int parsed = 0;

            if (fourTextBox1.Text == "")
            {
                return;
            }

            try 
            {
                parsed = int.Parse(fourTextBox1.Text);
            }
            catch
            {
                parsed = 0;
                fourTextBox1.Text = "0";
                R = 0;
            }

            if (parsed < 0 || parsed > 255)
            { 
                return; 
            }
            else
            { 
                R = (int)parsed;
            }
            fourPanel1.PanelColor = Color.FromArgb(255, R, G, B);
        }

        private void fourTextBox2_TextChanged(object sender, EventArgs e)
        {
            int parsed = 0;

            if (fourTextBox2.Text == "")
            {
                return;
            }

            try
            {
                parsed = int.Parse(fourTextBox2.Text);
            }
            catch
            {
                parsed = 0;
                fourTextBox2.Text = "0";
                G = 0;
            }

            if (parsed < 0 || parsed > 255)
            {
                return;
            }
            else
            {
                G = (int)parsed;
            }
            fourPanel1.PanelColor = Color.FromArgb(255, R, G, B);
        }

        private void fourTextBox3_TextChanged(object sender, EventArgs e)
        {
            int parsed = 0;

            if (fourTextBox3.Text == "")
            {
                return;
            }

            try
            {
                parsed = int.Parse(fourTextBox3.Text);
            }
            catch
            {
                parsed = 0;
                fourTextBox3.Text = "0";
                B = 0;
            }

            if (parsed < 0 || parsed > 255)
            {
                return;
            }
            else
            {
                B = (int)parsed;
            }
            fourPanel1.PanelColor = Color.FromArgb(255, R, G, B);
        }
    }
}
