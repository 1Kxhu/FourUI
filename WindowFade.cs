using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FourUI
{
    public partial class FourWindowFade : Component
    {
        private Form selectedForm;

        public FourWindowFade()
        {
            InitializeComponent();
        }

        IntPtr hWnd;
        uint duration = 350;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AnimateWindow(IntPtr hWnd, uint dwTime, uint dwFlags);

        public FourWindowFade(IContainer container)
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

        [Browsable(true)]
        [Category("FourUI")]
        [Description("The duration of the animation.")]
        public uint Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                if (duration > 5000)
                {
                    duration = 5000;
                }
            }
        }

        private void SelectedForm_Load(object sender, EventArgs e)
        {
            if (!AnimateWindow(hWnd, duration, 0x00080000))
            {
                Debug.WriteLine("WindowFade would have crashed, but it was prevented.");
            }
        }
    }
}
