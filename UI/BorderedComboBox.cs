using System.Drawing;
using System.Windows.Forms;

namespace SchoolTimetableWinForm
{
    public class BorderedComboBox : ComboBox
    {
        private Color _borderColor = Color.Black;
        private int _borderThickness = 1;

        public BorderedComboBox()
        {
            FlatStyle = FlatStyle.Flat;
            // Ensure proper painting without interfering with text rendering
            SetStyle(ControlStyles.UserPaint, false); // Disable UserPaint to let default rendering handle text
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        public int BorderThickness
        {
            get => _borderThickness;
            set { _borderThickness = value; Invalidate(); }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // WM_PAINT message
            if (m.Msg == 0xF) // WM_PAINT
            {
                using (Graphics g = CreateGraphics())
                using (Pen pen = new Pen(_borderColor, _borderThickness))
                {
                    // Draw border around the ComboBox
                    g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
                }
            }
        }
    }
}