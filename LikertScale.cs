using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace market_research_survey
{
    [DefaultEvent(nameof(SelectedIndexChanged))]
    public class LikertScale : Control
    {
        private int _maxRange = 10;
        private int _selectedIndex = -1;
        private int _hoveredIndex = -1;

        // Event marked as nullable to satisfy CS8618
        public event EventHandler? SelectedIndexChanged;

        public LikertScale()
        {
            // Enable double buffering to prevent flickering during hover/resize
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint
                    | ControlStyles.UserPaint
                    | ControlStyles.DoubleBuffer
                    | ControlStyles.OptimizedDoubleBuffer
                    | ControlStyles.ResizeRedraw,
                true
            );

            this.Size = new Size(600, 80);
            this.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.ForeColor = Color.FromArgb(64, 64, 64);
        }

        #region Properties

        [Category("Appearance")]
        [Description("The number of options to display.")]
        // Fixes WFO1000 for MaxRange
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int MaxRange
        {
            get => _maxRange;
            set
            {
                if (value > 0)
                {
                    _maxRange = value;
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        [Description("The image shown when a number is selected.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image? ActiveImage { get; set; }

        [Category("Appearance")]
        [Description("The default image for unselected numbers.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image? InactiveImage { get; set; }

        [Category("Appearance")]
        [Description("The current 1-based selected value.")]
        // Fixes WFO1000 for SelectedValue - Hidden because we don't want
        // the designer to hardcode a selection by default.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedValue
        {
            get => _selectedIndex + 1;
            set
            {
                int index = value - 1;
                if (index >= -1 && index < _maxRange)
                {
                    _selectedIndex = index;
                    Invalidate();
                }
            }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Designer Safety: if no image is set, provide a visual cue
            if (InactiveImage == null)
            {
                using (Pen p = new Pen(Color.Red, 1) { DashStyle = DashStyle.Dash })
                {
                    g.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
                    g.DrawString("Set InactiveImage in Properties", this.Font, Brushes.Red, 10, 10);
                }
                return;
            }

            float slotWidth = (float)this.Width / _maxRange;
            int imgW = InactiveImage.Width;
            int imgH = InactiveImage.Height;

            for (int i = 0; i < _maxRange; i++)
            {
                // Logic check for state
                bool isSelected = (i == _selectedIndex);
                bool isHovered = (i == _hoveredIndex);

                // Math for even distribution
                float slotCenterX = (i * slotWidth) + (slotWidth / 2f);
                float imgX = slotCenterX - (imgW / 2f);
                float imgY = (this.Height / 2f) - (imgH / 2f);
                RectangleF imgRect = new RectangleF(imgX, imgY, imgW, imgH);

                // 1. Draw Hover Effect (Optional subtle glow)
                if (isHovered && !isSelected)
                {
                    using (SolidBrush sb = new SolidBrush(Color.FromArgb(20, Color.Black)))
                    {
                        g.FillEllipse(sb, RectangleF.Inflate(imgRect, 4, 4));
                    }
                }

                // 2. Draw the Image
                Image drawImg = isSelected ? (ActiveImage ?? InactiveImage) : InactiveImage;
                g.DrawImage(drawImg, imgRect);

                // 3. Draw the Number centered
                string numberText = (i + 1).ToString();
                SizeF textSize = g.MeasureString(numberText, this.Font);
                float textX = slotCenterX - (textSize.Width / 2f);
                float textY = (this.Height / 2f) - (textSize.Height / 2f);

                using (Brush textBrush = new SolidBrush(this.ForeColor))
                {
                    g.DrawString(numberText, this.Font, textBrush, textX, textY);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            float slotWidth = (float)this.Width / _maxRange;
            int index = (int)(e.X / slotWidth);

            if (index >= 0 && index < _maxRange)
            {
                _selectedIndex = index;
                SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            float slotWidth = (float)this.Width / _maxRange;
            int hit = (int)(e.X / slotWidth);

            if (hit != _hoveredIndex)
            {
                _hoveredIndex = (hit >= 0 && hit < _maxRange) ? hit : -1;
                this.Cursor = _hoveredIndex != -1 ? Cursors.Hand : Cursors.Default;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _hoveredIndex = -1;
            this.Cursor = Cursors.Default;
            Invalidate();
        }
    }
}
