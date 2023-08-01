using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FourUI
{
    public class FourListBox : ListBox
    {
        private Color itemBackgroundColor = Color.FromArgb(240, 240, 240);
        private Color itemTextColor = Color.Black;
        private Color selectedItemBackgroundColor = Color.FromArgb(50, 153, 187);
        private Color selectedItemTextColor = Color.White;
        private int cornerRadius = 5;
        private int selectedIndex = -1;

        public FourListBox()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            DrawMode = DrawMode.OwnerDrawFixed;
            BorderStyle = BorderStyle.None;
            Font = new Font("Microsoft YaHei UI", 12f);
            ItemHeight = 28;
        }

        public Color ItemBackgroundColor
        {
            get => itemBackgroundColor;
            set { itemBackgroundColor = value; Invalidate(); }
        }

        public Color ItemTextColor
        {
            get => itemTextColor;
            set { itemTextColor = value; Invalidate(); }
        }

        public Color SelectedItemBackgroundColor
        {
            get => selectedItemBackgroundColor;
            set { selectedItemBackgroundColor = value; Invalidate(); }
        }

        public Color SelectedItemTextColor
        {
            get => selectedItemTextColor;
            set { selectedItemTextColor = value; Invalidate(); }
        }

        public int CornerRadius
        {
            get => cornerRadius;
            set { cornerRadius = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (SolidBrush backgroundBrush = new SolidBrush(itemBackgroundColor))
            using (GraphicsPath backgroundPath = CreateRoundedRectanglePath(ClientRectangle, cornerRadius))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillPath(backgroundBrush, backgroundPath);
            }

            base.OnPaint(e);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count) return;

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Color backgroundColor = isSelected ? selectedItemBackgroundColor : itemBackgroundColor;
            Color textColor = isSelected ? selectedItemTextColor : itemTextColor;

            if (isSelected && selectedIndex != e.Index)
            {
                backgroundColor = InterpolateColor(itemBackgroundColor, selectedItemBackgroundColor, 0.5f);
                textColor = selectedItemTextColor;
            }

            using (SolidBrush brush = new SolidBrush(backgroundColor))
            using (Pen borderPen = new Pen(Parent.BackColor, 2))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle itemRect = new Rectangle(e.Bounds.X + 1, e.Bounds.Y + 1, e.Bounds.Width - 2, e.Bounds.Height - 2);
                using (GraphicsPath backgroundPath = CreateRoundedRectanglePath(itemRect, cornerRadius))
                {
                    e.Graphics.FillPath(brush, backgroundPath);
                    e.Graphics.DrawPath(borderPen, backgroundPath);
                }

                string itemText = Items[e.Index].ToString();
                using (Brush textBrush = new SolidBrush(textColor))
                {
                    RectangleF textRect = new RectangleF(e.Bounds.X + 2, e.Bounds.Y + (e.Bounds.Height - Font.Height) / 2, e.Bounds.Width, e.Bounds.Height);
                    e.Graphics.DrawString(itemText, Font, textBrush, textRect, StringFormat.GenericDefault);
                }
            }

            base.OnDrawItem(e);
        }

        private GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            RectangleF arcRect;

            arcRect = new RectangleF(rect.X, rect.Y, radius * 2, radius * 2);
            path.AddArc(arcRect, 180, 90);

            arcRect.X = rect.Right - radius * 2;
            path.AddArc(arcRect, 270, 90);

            arcRect.Y = rect.Bottom - radius * 2;
            path.AddArc(arcRect, 0, 90);

            arcRect.X = rect.X;
            path.AddArc(arcRect, 90, 90);

            path.CloseFigure();

            return path;
        }

        private Color InterpolateColor(Color startColor, Color endColor, float progress)
        {
            int r = (int)(startColor.R + (endColor.R - startColor.R) * progress);
            int g = (int)(startColor.G + (endColor.G - startColor.G) * progress);
            int b = (int)(startColor.B + (endColor.B - startColor.B) * progress);
            return Color.FromArgb(r, g, b);
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            InvalidateSelectedItems();
            selectedIndex = SelectedIndex;
            InvalidateSelectedItems();
            base.OnSelectedIndexChanged(e);
        }

        private void InvalidateSelectedItems()
        {
            if (selectedIndex >= 0 && selectedIndex < Items.Count)
                Invalidate(GetItemRectangle(selectedIndex));
        }
    }
}
