using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FourUI
{
    [Flags]
    public enum AnimateWindowFlags
    {
        AW_HOR_POSITIVE = 0x00000001,
        AW_HOR_NEGATIVE = 0x00000002,
        AW_VER_POSITIVE = 0x00000004,
        AW_VER_NEGATIVE = 0x00000008,
        AW_CENTER = 0x00000010,
        AW_BLEND = 0x00080000
    }

    public partial class FourWindowAnimate : Component
    {
        private Form selectedForm;

        public FourWindowAnimate()
        {
            InitializeComponent();
        }

        IntPtr hWnd;

        [DefaultValue(200)]
        public int Duration { get; set; } = 200;

        [DefaultValue(AnimateWindowFlags.AW_BLEND)]
        public AnimateWindowFlags AnimationType { get; set; } = AnimateWindowFlags.AW_BLEND;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool AnimateWindow(IntPtr hWnd, int dwTime, AnimateWindowFlags dwFlags);

        public FourWindowAnimate(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The selected form to animate.")]
        public Form SelectedForm
        {
            get { return selectedForm; }
            set
            {
                selectedForm = value;
                hWnd = value.Handle;
                selectedForm.Load += SelectedForm_Load;
            }
        }

        private void SelectedForm_Load(object sender, EventArgs e)
        {
            if (!AnimateWindow(hWnd, Duration, AnimationType))
            {
                Debug.WriteLine("WindowAnimate would have crashed, but it was prevented.");
            }
        }
    }
}
