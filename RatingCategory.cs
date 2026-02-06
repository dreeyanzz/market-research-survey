using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace market_research_survey;

public class RatingCategory
{
    public string Label { get; set; }
    public Point Position { get; set; }
    public int CurrentRating { get; set; }
    public object? Tag { get; set; }

    public RatingCategory(string label, Point position, object? tag = null)
    {
        Label = label;
        Position = position;
        CurrentRating = 0;
        Tag = tag;
    }
}

public class RatingGroup : Control
{
    private List<RatingCategory> _categories = [];
    private Image? _activeImage;
    private Image? _inactiveImage;
    private int _maxStars = 5;
    private int _starSpacing = 12;
    private int _labelStarPadding = 10;
    private Size _starSize = new Size(40, 40);

    private int _hoveredStarIndex = -1;
    private RatingCategory? _hoveredCategory = null;
    private float _hoverOpacity = 0.6f;

    public event EventHandler? RatingChanged;

    public RatingGroup()
    {
        this.DoubleBuffered = true;
        this.ResizeRedraw = true;
        this.Font = new Font("Inter", 12F, FontStyle.Regular);
    }

    #region Properties
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
    public Size StarSize
    {
        get => _starSize;
        set
        {
            _starSize = value;
            Invalidate();
        }
    }

    [
        Category("Appearance"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
    ]
    public int StarSpacing
    {
        get => _starSpacing;
        set
        {
            _starSpacing = value;
            Invalidate();
        }
    }

    [Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int MaxStars
    {
        get => _maxStars;
        set
        {
            _maxStars = value;
            Invalidate();
        }
    }

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsEverythingRated =>
        _categories.Count > 0 && _categories.All(cat => cat.CurrentRating > 0);
    #endregion

    public void AddCategory(string label, Point position, object? tag = null) =>
        _categories.Add(new RatingCategory(label, position, tag));

    private float GetStarRowY(Graphics g, RatingCategory cat) =>
        cat.Position.Y + g.MeasureString(cat.Label, this.Font).Height + _labelStarPadding;

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

        if (InactiveImage == null)
            return;

        foreach (var cat in _categories)
        {
            using Brush textBrush = new SolidBrush(this.ForeColor);
            g.DrawString(cat.Label, this.Font, textBrush, cat.Position);

            float starRowY = GetStarRowY(g, cat);
            for (int i = 0; i < _maxStars; i++)
            {
                float x = cat.Position.X + (i * (_starSize.Width + _starSpacing));
                RectangleF starRect = new RectangleF(
                    x,
                    starRowY,
                    _starSize.Width,
                    _starSize.Height
                );

                bool isSelected = i < cat.CurrentRating;
                bool isHovered = (cat == _hoveredCategory && i <= _hoveredStarIndex);

                if (isHovered && !isSelected)
                {
                    using var ia = new System.Drawing.Imaging.ImageAttributes();
                    ia.SetColorMatrix(
                        new System.Drawing.Imaging.ColorMatrix { Matrix33 = _hoverOpacity }
                    );
                    g.DrawImage(
                        ActiveImage ?? InactiveImage,
                        new PointF[]
                        {
                            new PointF(starRect.X, starRect.Y),
                            new PointF(starRect.Right, starRect.Y),
                            new PointF(starRect.X, starRect.Bottom),
                        },
                        new RectangleF(
                            0,
                            0,
                            (ActiveImage ?? InactiveImage).Width,
                            (ActiveImage ?? InactiveImage).Height
                        ),
                        GraphicsUnit.Pixel,
                        ia
                    );
                }
                else
                {
                    g.DrawImage(
                        isSelected ? (ActiveImage ?? InactiveImage) : InactiveImage,
                        starRect
                    );
                }
            }
        }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        // Force a hit check on click to ensure we aren't relying on old hover data
        using Graphics g = this.CreateGraphics();
        foreach (var cat in _categories)
        {
            float starRowY = GetStarRowY(g, cat);
            for (int i = 0; i < _maxStars; i++)
            {
                float x = cat.Position.X + (i * (_starSize.Width + _starSpacing));
                if (
                    new RectangleF(x, starRowY, _starSize.Width, _starSize.Height).Contains(
                        e.Location
                    )
                )
                {
                    cat.CurrentRating = i + 1;
                    RatingChanged?.Invoke(this, EventArgs.Empty);
                    Invalidate();
                    return;
                }
            }
        }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        int oldH = _hoveredStarIndex;
        RatingCategory? oldC = _hoveredCategory;
        _hoveredStarIndex = -1;
        _hoveredCategory = null;

        using Graphics g = this.CreateGraphics();
        foreach (var cat in _categories)
        {
            float starRowY = GetStarRowY(g, cat);
            for (int i = 0; i < _maxStars; i++)
            {
                float x = cat.Position.X + (i * (_starSize.Width + _starSpacing));
                if (
                    new RectangleF(x, starRowY, _starSize.Width, _starSize.Height).Contains(
                        e.Location
                    )
                )
                {
                    _hoveredStarIndex = i;
                    _hoveredCategory = cat;
                    this.Cursor = Cursors.Hand;
                    goto endLoop;
                }
            }
        }
        this.Cursor = Cursors.Default;
        endLoop:
        if (oldH != _hoveredStarIndex || oldC != _hoveredCategory)
            Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        _hoveredStarIndex = -1;
        _hoveredCategory = null;
        Invalidate();
        base.OnMouseLeave(e);
    }
}
