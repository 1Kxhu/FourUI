namespace FourUI.Forms
{
    partial class FourColorDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FourColorDialog));
            System.Drawing.Drawing2D.Matrix matrix1 = new System.Drawing.Drawing2D.Matrix();
            this.fourPanel1 = new FourUI.FourPanel();
            this.fourButton2 = new FourUI.FourButton();
            this.fourButton1 = new FourUI.FourButton();
            this.fourPictureBox1 = new FourPictureBox();
            this.fourDrag1 = new FourUI.FourDrag(this.components);
            this.fourRound1 = new FourUI.FourRound(this.components);
            this.fourBorder1 = new FourUI.FourBorder(this.components);
            this.fourBorder2 = new FourUI.FourBorder(this.components);
            this.fourLabel1 = new FourUI.FourLabel();
            this.fourTextBox1 = new FourTextBox();
            this.fourTextBox2 = new FourTextBox();
            this.fourTextBox3 = new FourTextBox();
            this.SuspendLayout();
            // 
            // fourPanel1
            // 
            this.fourPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.fourPanel1.BorderWidth = 2;
            this.fourPanel1.CornerRadius = 5;
            this.fourPanel1.Location = new System.Drawing.Point(12, 317);
            this.fourPanel1.Name = "fourPanel1";
            this.fourPanel1.Opacity = 0;
            this.fourPanel1.PanelColor = System.Drawing.Color.White;
            this.fourPanel1.Size = new System.Drawing.Size(267, 30);
            this.fourPanel1.TabIndex = 3;
            this.fourPanel1.Text = "fourPanel1";
            this.fourPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.fourPanel1_Paint);
            // 
            // fourButton2
            // 
            this.fourButton2.BorderWidth = 1;
            this.fourButton2.ButtonImage = null;
            this.fourButton2.ButtonMode = FourUI.FourButton.ButtonModeEnum.Default;
            this.fourButton2.Checked = false;
            this.fourButton2.CheckedButtonImage = null;
            this.fourButton2.CheckedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(131)))), ((int)(((byte)(255)))));
            this.fourButton2.CheckedForeColor = System.Drawing.Color.White;
            this.fourButton2.CornerRadius = 5;
            this.fourButton2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.fourButton2.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.fourButton2.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.fourButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.fourButton2.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.fourButton2.ImageAlignment = FourUI.FourButton.ImageAlignmentOption.Left;
            this.fourButton2.ImageOffset = new System.Drawing.Point(0, 0);
            this.fourButton2.ImageSize = new System.Drawing.Size(20, 20);
            this.fourButton2.Location = new System.Drawing.Point(148, 466);
            this.fourButton2.Name = "fourButton2";
            this.fourButton2.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.fourButton2.Size = new System.Drawing.Size(130, 31);
            this.fourButton2.StretchImage = false;
            this.fourButton2.TabIndex = 2;
            this.fourButton2.Text = "Cancel";
            this.fourButton2.TextOffset = new System.Drawing.Point(0, 0);
            this.fourButton2.UnfocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.fourButton2.UseVisualStyleBackColor = true;
            this.fourButton2.Click += new System.EventHandler(this.fourButton2_Click);
            // 
            // fourButton1
            // 
            this.fourButton1.BorderWidth = 1;
            this.fourButton1.ButtonImage = null;
            this.fourButton1.ButtonMode = FourUI.FourButton.ButtonModeEnum.Default;
            this.fourButton1.Checked = false;
            this.fourButton1.CheckedButtonImage = null;
            this.fourButton1.CheckedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(131)))), ((int)(((byte)(255)))));
            this.fourButton1.CheckedForeColor = System.Drawing.Color.White;
            this.fourButton1.CornerRadius = 5;
            this.fourButton1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.fourButton1.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.fourButton1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.fourButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.fourButton1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.fourButton1.ImageAlignment = FourUI.FourButton.ImageAlignmentOption.Left;
            this.fourButton1.ImageOffset = new System.Drawing.Point(0, 0);
            this.fourButton1.ImageSize = new System.Drawing.Size(20, 20);
            this.fourButton1.Location = new System.Drawing.Point(12, 466);
            this.fourButton1.Name = "fourButton1";
            this.fourButton1.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.fourButton1.Size = new System.Drawing.Size(130, 31);
            this.fourButton1.StretchImage = false;
            this.fourButton1.TabIndex = 1;
            this.fourButton1.Text = "Done";
            this.fourButton1.TextOffset = new System.Drawing.Point(0, 0);
            this.fourButton1.UnfocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.fourButton1.UseVisualStyleBackColor = true;
            this.fourButton1.Click += new System.EventHandler(this.fourButton1_Click);
            // 
            // fourPictureBox1
            // 
            this.fourPictureBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.fourPictureBox1.BorderWidth = 1;
            this.fourPictureBox1.CornerRadius = 5;
            this.fourPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("fourPictureBox1.Image")));
            this.fourPictureBox1.Location = new System.Drawing.Point(12, 45);
            this.fourPictureBox1.Name = "fourPictureBox1";
            this.fourPictureBox1.RotationAngle = 0F;
            this.fourPictureBox1.Size = new System.Drawing.Size(266, 266);
            this.fourPictureBox1.TabIndex = 0;
            this.fourPictureBox1.Text = "fourPictureBox1";
            this.fourPictureBox1.TranslationMatrix = matrix1;
            this.fourPictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.fourPictureBox1_MouseMove);
            // 
            // fourDrag1
            // 
            this.fourDrag1.Smoothness = 4F;
            this.fourDrag1.TargetControl = this;
            // 
            // fourRound1
            // 
            this.fourRound1.CornerRadius = 1;
            this.fourRound1.TargetForm = this;
            // 
            // fourBorder1
            // 
            this.fourBorder1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.fourBorder1.BorderRadius = 0;
            this.fourBorder1.BorderWidth = 1;
            this.fourBorder1.TargetForm = this;
            // 
            // fourBorder2
            // 
            this.fourBorder2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.fourBorder2.BorderRadius = 0;
            this.fourBorder2.BorderWidth = 1;
            this.fourBorder2.TargetForm = this;
            // 
            // fourLabel1
            // 
            this.fourLabel1.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.fourLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.fourLabel1.Location = new System.Drawing.Point(12, 353);
            this.fourLabel1.Name = "fourLabel1";
            this.fourLabel1.Size = new System.Drawing.Size(266, 23);
            this.fourLabel1.TabIndex = 4;
            this.fourLabel1.TextAlign = FourUI.FourLabel.TextAlignment.Center;
            this.fourLabel1.TextLines = new string[] {
        "RGB Input"};
            // 
            // fourTextBox1
            // 
            this.fourTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.fourTextBox1.BorderRadius = 5;
            this.fourTextBox1.BorderSize = 1;
            this.fourTextBox1.CaretColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.fourTextBox1.FocusedBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.fourTextBox1.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.fourTextBox1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.fourTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.fourTextBox1.Location = new System.Drawing.Point(34, 375);
            this.fourTextBox1.Name = "fourTextBox1";
            this.fourTextBox1.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.fourTextBox1.PlaceholderText = "R";
            this.fourTextBox1.Size = new System.Drawing.Size(69, 30);
            this.fourTextBox1.TabIndex = 5;
            this.fourTextBox1.UnfocusedBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.fourTextBox1.UnfocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.fourTextBox1.UnfocusedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.fourTextBox1.TextChanged += new System.EventHandler(this.fourTextBox1_TextChanged);
            // 
            // fourTextBox2
            // 
            this.fourTextBox2.BackColor = System.Drawing.Color.Transparent;
            this.fourTextBox2.BorderRadius = 5;
            this.fourTextBox2.BorderSize = 1;
            this.fourTextBox2.CaretColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.fourTextBox2.FocusedBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.fourTextBox2.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.fourTextBox2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.fourTextBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.fourTextBox2.Location = new System.Drawing.Point(109, 375);
            this.fourTextBox2.Name = "fourTextBox2";
            this.fourTextBox2.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.fourTextBox2.PlaceholderText = "G";
            this.fourTextBox2.Size = new System.Drawing.Size(69, 30);
            this.fourTextBox2.TabIndex = 6;
            this.fourTextBox2.UnfocusedBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.fourTextBox2.UnfocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.fourTextBox2.UnfocusedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.fourTextBox2.TextChanged += new System.EventHandler(this.fourTextBox2_TextChanged);
            // 
            // fourTextBox3
            // 
            this.fourTextBox3.BackColor = System.Drawing.Color.Transparent;
            this.fourTextBox3.BorderRadius = 5;
            this.fourTextBox3.BorderSize = 1;
            this.fourTextBox3.CaretColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.fourTextBox3.FocusedBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.fourTextBox3.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.fourTextBox3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.fourTextBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.fourTextBox3.Location = new System.Drawing.Point(184, 375);
            this.fourTextBox3.Name = "fourTextBox3";
            this.fourTextBox3.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.fourTextBox3.PlaceholderText = "B";
            this.fourTextBox3.Size = new System.Drawing.Size(69, 30);
            this.fourTextBox3.TabIndex = 7;
            this.fourTextBox3.UnfocusedBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.fourTextBox3.UnfocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.fourTextBox3.UnfocusedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.fourTextBox3.TextChanged += new System.EventHandler(this.fourTextBox3_TextChanged);
            // 
            // FourColorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.ClientSize = new System.Drawing.Size(290, 509);
            this.Controls.Add(this.fourTextBox3);
            this.Controls.Add(this.fourTextBox2);
            this.Controls.Add(this.fourTextBox1);
            this.Controls.Add(this.fourLabel1);
            this.Controls.Add(this.fourPanel1);
            this.Controls.Add(this.fourButton2);
            this.Controls.Add(this.fourButton1);
            this.Controls.Add(this.fourPictureBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Name = "FourColorDialog";
            this.Text = "ColorDialog";
            this.Load += new System.EventHandler(this.FourColorDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FourPictureBox fourPictureBox1;
        private FourDrag fourDrag1;
        private FourRound fourRound1;
        private FourPanel fourPanel1;
        private FourButton fourButton2;
        private FourButton fourButton1;
        private FourBorder fourBorder1;
        private FourBorder fourBorder2;
        private FourTextBox fourTextBox1;
        private FourLabel fourLabel1;
        private FourTextBox fourTextBox3;
        private FourTextBox fourTextBox2;
    }
}