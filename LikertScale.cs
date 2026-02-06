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

        // Label Fields
        private string _minLabel = "";
        private string _maxLabel = "";
        private Color _footerColor = Color.Gray;

        public event EventHandler? SelectedIndexChanged;

        public LikertScale()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint
                    | ControlStyles.UserPaint
                    | ControlStyles.DoubleBuffer
                    | ControlStyles.OptimizedDoubleBuffer
                    | ControlStyles.ResizeRedraw,
                true
            );

            this.Size = new Size(600, 120); // Height increased for labels
            this.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.ForeColor = Color.FromArgb(64, 64, 64);
        }

        #region Properties

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string MinLabel
        {
            get => _minLabel;
            set
            {
                _minLabel = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string MaxLabel
        {
            get => _maxLabel;
            set
            {
                _maxLabel = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color FooterColor
        {
            get => _footerColor;
            set
            {
                _footerColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image? ActiveImage { get; set; }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image? InactiveImage { get; set; }

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

            if (InactiveImage == null)
            {
                using (Pen p = new Pen(Color.Red, 1) { DashStyle = DashStyle.Dash })
                {
                    g.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
                    g.DrawString("Set InactiveImage", this.Font, Brushes.Red, 10, 10);
                }
                return;
            }

            float slotWidth = (float)this.Width / _maxRange;
            int imgW = InactiveImage.Width;
            int imgH = InactiveImage.Height;

            for (int i = 0; i < _maxRange; i++)
            {
                bool isSelected = (i == _selectedIndex);
                bool isHovered = (i == _hoveredIndex);

                float slotCenterX = (i * slotWidth) + (slotWidth / 2f);
                float imgX = slotCenterX - (imgW / 2f);

                // Keep circles in upper portion to leave room for footer
                float imgY = (this.Height * 0.4f) - (imgH / 2f);
                RectangleF imgRect = new RectangleF(imgX, imgY, imgW, imgH);

                if (isHovered && !isSelected)
                {
                    using (SolidBrush sb = new SolidBrush(Color.FromArgb(20, Color.Black)))
                        g.FillEllipse(sb, RectangleF.Inflate(imgRect, 4, 4));
                }

                Image drawImg = isSelected ? (ActiveImage ?? InactiveImage) : InactiveImage;
                g.DrawImage(drawImg, imgRect);

                string numberText = (i + 1).ToString();
                SizeF textSize = g.MeasureString(numberText, this.Font);
                g.DrawString(
                    numberText,
                    this.Font,
                    new SolidBrush(this.ForeColor),
                    slotCenterX - (textSize.Width / 2f),
                    imgY + (imgH / 2f) - (textSize.Height / 2f)
                );
            }

            // Draw Footer Labels
            using (var footerFont = new Font(this.Font.FontFamily, this.Font.Size * 0.8f))
            using (var brush = new SolidBrush(_footerColor))
            {
                float footerY = this.Height - g.MeasureString("Ag", footerFont).Height - 5;

                // Left Label (Center under first slot)
                if (!string.IsNullOrEmpty(_minLabel))
                {
                    SizeF size = g.MeasureString(_minLabel, footerFont);
                    g.DrawString(
                        _minLabel,
                        footerFont,
                        brush,
                        (slotWidth / 2f) - (size.Width / 2f),
                        footerY
                    );
                }

                // Right Label (Center under last slot)
                if (!string.IsNullOrEmpty(_maxLabel))
                {
                    SizeF size = g.MeasureString(_maxLabel, footerFont);
                    float lastSlotCenter = ((_maxRange - 1) * slotWidth) + (slotWidth / 2f);
                    g.DrawString(
                        _maxLabel,
                        footerFont,
                        brush,
                        lastSlotCenter - (size.Width / 2f),
                        footerY
                    );
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            float slotWidth = (float)this.Width / _maxRange;
            int index = (int)(e.X / slotWidth);
            if (index >= 0 && index < _maxRange)
            {
                _selectedIndex = index;
                SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
                Invalidate();
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            float slotWidth = (float)this.Width / _maxRange;
            int hit = (int)(e.X / slotWidth);
            if (hit != _hoveredIndex)
            {
                _hoveredIndex = (hit >= 0 && hit < _maxRange) ? hit : -1;
                this.Cursor = _hoveredIndex != -1 ? Cursors.Hand : Cursors.Default;
                Invalidate();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _hoveredIndex = -1;
            this.Cursor = Cursors.Default;
            Invalidate();
            base.OnMouseLeave(e);
        }
    }
}
