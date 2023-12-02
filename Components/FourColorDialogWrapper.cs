using FourUI.Forms;
using System.ComponentModel;
using System.Drawing;

namespace FourUI.Components
{
    public partial class FourColorDialogWrapper : Component
    {
        public FourColorDialogWrapper()
        {
            InitializeComponent();
        }

        public FourColorDialog fourColorDialog = new FourColorDialog();

        private Color defaultColor;
        private Color backColor;
        private int buttonCornerRadius = 5;
        private int cornerRadius = 4;

        [Category("Appearance")]
        public Color DefaultColor
        {
            get { return defaultColor; }
            set { defaultColor = value; fourColorDialog.defaultColor = value; }
        }

        [Category("Appearance")]
        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; fourColorDialog.BackColor = value; }
        }

        [Category("Appearance")]
        public int ButtonCornerRadius
        {
            get { return buttonCornerRadius; }
            set { buttonCornerRadius = value; fourColorDialog.buttonCornerRadius = value; }
        }

        [Category("Appearance")]
        public int CornerRadius
        {
            get { return cornerRadius; }
            set { cornerRadius = value; fourColorDialog.cornerRadius = value; }
        }

        private Color buttonColor { get; set; } = Color.FromArgb(10, 10, 10);
        private Color buttonHoverColor { get; set; } = Color.FromArgb(14, 14, 14);
        private Color buttonPressColor { get; set; } = Color.FromArgb(21, 21, 21);

        [Category("Appearance")]
        public Color ButtonColor
        {
            get { return buttonColor; }
            set { buttonColor = value; fourColorDialog.buttonColor = value; }
        }

        [Category("Appearance")]
        public Color ButtonHoverColor
        {
            get { return buttonHoverColor; }
            set { buttonHoverColor = value; fourColorDialog.buttonHoverColor = value; }
        }

        [Category("Appearance")]
        public Color ButtonPressColor
        {
            get { return buttonPressColor; }
            set { buttonPressColor = value; fourColorDialog.buttonPressColor = value; }
        }

        public FourColorDialogWrapper(IContainer container)
        {
            container.Add(this);

            InitializeComponent();


        }
    }
}
