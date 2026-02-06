namespace market_research_survey;

public partial class Window : Form
{
    private CustomRadioGroup? radioPage8;
    private bool hasSelectedLastBuySportswear = false;

    private void InitPage8()
    {
        radioPage8 = new();

        int normalizedPos = 8 * 1920;

        Label question = new()
        {
            Text = "When did you last buy a sportswear?",
            Font = new("Inter", 56f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            Location = new Point(normalizedPos + 205, 83),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        formImg!.Controls.Add(question);

        radioPage8!.ImageTextSpacing = 31;
        radioPage8.CustomFont = new Font("Inter", 36f, FontStyle.Regular, GraphicsUnit.Pixel);

        radioPage8.ActiveImage = Image.FromFile("assets\\radioActive.png");
        radioPage8.InactiveImage = Image.FromFile("assets\\radioInactive.png");

        radioPage8.Location = new Point(normalizedPos + 406, 220);
        radioPage8.Size = new Size(1400, 500);

        radioPage8.AddOption("Less than a 1 month ago", new Point(0, 0));
        radioPage8.AddOption("Between 6 months and 1 year ago", new Point(0, 100));
        radioPage8.AddOption("I do not remember", new Point(0, 200));

        radioPage8.AddOption("Between 1 and 6 months ago", new Point(708, 0));
        radioPage8.AddOption("More than 1 year ago", new Point(708, 100));

        radioPage8.SelectedIndexChanged += (sender, e) => hasSelectedLastBuySportswear = true;

        formImg.Controls.Add(radioPage8);
    }
}
