using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace market_research_survey;

public class RadioOption
{
    public string Label { get; set; }
    public Point Position { get; set; }
    public object? Value { get; set; }

    public RadioOption(string label, Point position, object? value = null)
    {
        Label = label;
        Position = position;
        Value = value ?? label;
    }
}

public class CustomRadioGroup : Control
{
    private List<RadioOption> _options = [];
    private Image? _activeImage;
    private Image? _inactiveImage;
    private Font _customFont;
    private int _selectedIndex = -1;
    private int _hoveredIndex = -1;
    private int _imageTextSpacing = 8;
    private Color _hoverColor = Color.FromArgb(40, Color.Gray);

    public CustomRadioGroup()
    {
        this.DoubleBuffered = true;
        this.ResizeRedraw = true;
        this.ForeColor = Color.Black;
        _customFont = new Font("Segoe UI", 10F);
    }

    #region Properties

    [
        Category("Appearance"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
    ]
    public Font CustomFont
    {
        get => _customFont;
        set
        {
            _customFont = value ?? new Font("Segoe UI", 10F);
            Invalidate();
        }
    }

    [
        Category("Appearance"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
    ]
    public Image? ActiveImage
    {
        get => _activeImage;
        set
        {
            _activeImage = value;
            Invalidate();
        }
    }

    [
        Category("Appearance"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
    ]
    public Image? InactiveImage
    {
        get => _inactiveImage;
        set
        {
            _inactiveImage = value;
            Invalidate();
        }
    }

    [
        Category("Appearance"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
    ]
    public int ImageTextSpacing
    {
        get => _imageTextSpacing;
        set
        {
            _imageTextSpacing = value;
            Invalidate();
        }
    }

    [
        Category("Appearance"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
    ]
    public Color HoverColor
    {
        get => _hoverColor;
        set
        {
            _hoverColor = value;
            Invalidate();
        }
    }

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<RadioOption> Options
    {
        get => _options;
        set
        {
            _options = value ?? [];
            Invalidate();
        }
    }

    [Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            if (value >= -1 && value < _options.Count && _selectedIndex != value)
            {
                _selectedIndex = value;
                OnSelectedIndexChanged(EventArgs.Empty);
                Invalidate();
            }
        }
    }

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string? SelectedLabel
    {
        get =>
            (_selectedIndex >= 0 && _selectedIndex < _options.Count)
                ? _options[_selectedIndex].Label
                : null;
        set { SelectedIndex = _options.FindIndex(o => o.Label == value); }
    }

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? SelectedValue
    {
        get =>
            (_selectedIndex >= 0 && _selectedIndex < _options.Count)
                ? _options[_selectedIndex].Value
                : null;
        set { SelectedIndex = _options.FindIndex(o => Equals(o.Value, value)); }
    }

    #endregion

    #region Events
    public event EventHandler? SelectedIndexChanged;

    protected virtual void OnSelectedIndexChanged(EventArgs e) =>
        SelectedIndexChanged?.Invoke(this, e);
    #endregion

    #region Public Methods
    public void AddOption(string label, Point position, object? value = null)
    {
        _options.Add(new RadioOption(label, position, value));
        Invalidate();
    }

    public void ClearOptions()
    {
        _options.Clear();
        _selectedIndex = -1;
        Invalidate();
    }
    #endregion

    #region Rendering

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

        for (int i = 0; i < _options.Count; i++)
        {
            DrawRadioOption(g, _options[i], i == _selectedIndex, i == _hoveredIndex, i);
        }
    }

    private void DrawRadioOption(
        Graphics g,
        RadioOption option,
        bool isSelected,
        bool isHovered,
        int index
    )
    {
        Image? img = isSelected ? _activeImage : _inactiveImage;
        int indicatorSize = 16; // Default size if no image

        int currentX = option.Position.X;
        int currentY = option.Position.Y;

        // 1. Draw Indicator (Image or Vector fallback)
        if (img != null)
        {
            g.DrawImage(img, currentX, currentY, img.Width, img.Height);
            currentX += img.Width + _imageTextSpacing;
        }
        else
        {
            // Draw a standard-looking radio button if images are missing
            Rectangle rect = new Rectangle(currentX, currentY, indicatorSize, indicatorSize);
            using (Pen p = new Pen(this.ForeColor, 2))
                g.DrawEllipse(p, rect);
            if (isSelected)
            {
                Rectangle inner = rect;
                inner.Inflate(-4, -4);
                using (Brush b = new SolidBrush(this.ForeColor))
                    g.FillEllipse(b, inner);
            }
            currentX += indicatorSize + _imageTextSpacing;
        }

        // 2. Draw Label
        using (Brush brush = new SolidBrush(this.ForeColor))
        {
            SizeF textSize = g.MeasureString(option.Label, _customFont);
            float textY = currentY + (img?.Height ?? indicatorSize) / 2f - textSize.Height / 2f;
            g.DrawString(option.Label, _customFont, brush, currentX, textY);
        }

        // 3. Draw Hover State
        if (isHovered)
        {
            Rectangle bounds = GetOptionBounds(option, index);
            using (SolidBrush hb = new SolidBrush(_hoverColor))
            {
                g.FillRectangle(hb, bounds);
            }
        }
    }

    private Rectangle GetOptionBounds(RadioOption option, int index)
    {
        int indicatorW =
            (index == _selectedIndex ? _activeImage?.Width : _inactiveImage?.Width) ?? 16;
        int indicatorH =
            (index == _selectedIndex ? _activeImage?.Height : _inactiveImage?.Height) ?? 16;

        using (Graphics g = this.CreateGraphics())
        {
            SizeF textSize = g.MeasureString(option.Label, _customFont);
            int width = indicatorW + _imageTextSpacing + (int)textSize.Width + 4;
            int height = Math.Max(indicatorH, (int)textSize.Height) + 4;
            return new Rectangle(option.Position.X - 2, option.Position.Y - 2, width, height);
        }
    }

    #endregion

    #region Mouse Handling
    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);
        for (int i = 0; i < _options.Count; i++)
        {
            if (GetOptionBounds(_options[i], i).Contains(e.Location))
            {
                SelectedIndex = i;
                break;
            }
        }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        int hit = -1;
        for (int i = 0; i < _options.Count; i++)
        {
            if (GetOptionBounds(_options[i], i).Contains(e.Location))
            {
                hit = i;
                break;
            }
        }

        if (hit != _hoveredIndex)
        {
            _hoveredIndex = hit;
            Cursor = hit >= 0 ? Cursors.Hand : Cursors.Default;
            Invalidate();
        }
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        _hoveredIndex = -1;
        Invalidate();
    }
    #endregion

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            _customFont?.Dispose();
        base.Dispose(disposing);
    }
}
