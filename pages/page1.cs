namespace market_research_survey;

public partial class Window : Form
{
    private CustomRadioGroup? radioPage1;
    private bool hasSelectedAgeRange = false;

    private void InitPage1()
    {
        radioPage1 = new();

        int normalizedPos = 1 * 1920;

        Label question = new()
        {
            Text = "What is your age range?",
            Font = new("Inter", 56f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            Location = new Point(normalizedPos + 205, 83),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        formImg!.Controls.Add(question);

        radioPage1!.ImageTextSpacing = 31;
        radioPage1.CustomFont = new Font("Inter", 36f, FontStyle.Regular, GraphicsUnit.Pixel);

        radioPage1.ActiveImage = Image.FromFile("assets\\radioActive.png");
        radioPage1.InactiveImage = Image.FromFile("assets\\radioInactive.png");

        radioPage1.Location = new Point(normalizedPos + 406, 220);
        radioPage1.Size = new Size(1400, 500);

        radioPage1.AddOption("0-17", new Point(0, 0));
        radioPage1.AddOption("25-34", new Point(0, 100));
        radioPage1.AddOption("45-54", new Point(0, 200));
        radioPage1.AddOption("65+", new Point(0, 300));
        radioPage1.AddOption("18-24", new Point(708, 0));
        radioPage1.AddOption("35-44", new Point(708, 100));
        radioPage1.AddOption("55-64", new Point(708, 200));

        radioPage1.SelectedIndexChanged += (sender, e) => hasSelectedAgeRange = true;

        formImg.Controls.Add(radioPage1);
    }
}
