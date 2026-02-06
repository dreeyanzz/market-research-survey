using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace market_research_survey;

public class CheckboxOption
{
    public string Label { get; set; }
    public Point Position { get; set; }
    public object? Value { get; set; }

    public CheckboxOption(string label, Point position, object? value = null)
    {
        Label = label;
        Position = position;
        Value = value ?? label;
    }
}

public class CustomCheckboxGroup : Control
{
    private List<CheckboxOption> _options = [];
    private HashSet<int> _selectedIndices = new();
    private Image? _activeImage;
    private Image? _inactiveImage;
    private Font _customFont;
    private int _hoveredIndex = -1;
    private int _imageTextSpacing = 8;
    private Color _hoverColor = Color.FromArgb(40, Color.Gray);

    // Event name changed to SelectionChanged for multi-select context
    public event EventHandler? SelectionChanged;

    public CustomCheckboxGroup()
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
    public List<int> SelectedIndices => _selectedIndices.ToList();

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<object?> SelectedValues => _selectedIndices.Select(i => _options[i].Value).ToList();

    // Public property to access options for summary page
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<CheckboxOption> Options => _options;

    #endregion

    #region Public Methods
    public void AddOption(string label, Point position, object? value = null)
    {
        _options.Add(new CheckboxOption(label, position, value));
        Invalidate();
    }

    public void ClearOptions()
    {
        _options.Clear();
        _selectedIndices.Clear();
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
            DrawCheckboxOption(g, _options[i], _selectedIndices.Contains(i), i == _hoveredIndex, i);
        }
    }

    private void DrawCheckboxOption(
        Graphics g,
        CheckboxOption option,
        bool isChecked,
        bool isHovered,
        int index
    )
    {
        Image? img = isChecked ? _activeImage : _inactiveImage;
        int indicatorW = img?.Width ?? 16;
        int indicatorH = img?.Height ?? 16;

        SizeF textSize = g.MeasureString(option.Label, _customFont);

        float rowHeight = Math.Max(indicatorH, textSize.Height);
        float indicatorY = option.Position.Y + (rowHeight - indicatorH) / 2f;
        float textY = option.Position.Y + (rowHeight - textSize.Height) / 2f;
        float currentX = option.Position.X;

        // 1. Draw Indicator
        if (img != null)
        {
            g.DrawImage(img, currentX, indicatorY, indicatorW, indicatorH);
        }
        else
        {
            RectangleF rect = new RectangleF(currentX, indicatorY, indicatorW, indicatorH);
            using Pen p = new Pen(this.ForeColor, 2);
            g.DrawRectangle(p, rect.X, rect.Y, rect.Width, rect.Height);
            if (isChecked)
            {
                RectangleF inner = rect;
                inner.Inflate(-4, -4);
                using Brush b = new SolidBrush(this.ForeColor);
                g.FillRectangle(b, inner);
            }
        }

        // 2. Draw Label
        using (Brush brush = new SolidBrush(this.ForeColor))
        {
            g.DrawString(
                option.Label,
                _customFont,
                brush,
                currentX + indicatorW + _imageTextSpacing,
                textY
            );
        }

        // 3. Draw Hover State
        if (isHovered)
        {
            Rectangle bounds = GetOptionBounds(option, index);
            using SolidBrush hb = new SolidBrush(_hoverColor);
            g.FillRectangle(hb, bounds);
        }
    }

    private Rectangle GetOptionBounds(CheckboxOption option, int index)
    {
        bool isChecked = _selectedIndices.Contains(index);
        int indicatorW = (isChecked ? _activeImage?.Width : _inactiveImage?.Width) ?? 16;
        int indicatorH = (isChecked ? _activeImage?.Height : _inactiveImage?.Height) ?? 16;

        using Graphics g = this.CreateGraphics();
        SizeF textSize = g.MeasureString(option.Label, _customFont);
        int width = indicatorW + _imageTextSpacing + (int)textSize.Width + 4;
        int height = (int)Math.Max(indicatorH, textSize.Height) + 4;
        return new Rectangle(option.Position.X - 2, option.Position.Y - 2, width, height);
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
                // Toggle logic
                if (_selectedIndices.Contains(i))
                    _selectedIndices.Remove(i);
                else
                    _selectedIndices.Add(i);

                SelectionChanged?.Invoke(this, EventArgs.Empty);
                Invalidate();
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
