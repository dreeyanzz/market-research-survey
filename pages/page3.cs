namespace market_research_survey;

public partial class Window : Form
{
    private readonly CustomRadioGroup? radioPage3;
    private bool hasSelectedAnnualIncomeRange = false;

    private void InitPage3()
    {
        int normalizedPos = 3 * 1920;

        Label question = new()
        {
            Text = "What is your annual income range?",
            Font = new("Inter", 56f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            Location = new Point(normalizedPos + 205, 83),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        formImg!.Controls.Add(question);

        radioPage3!.ImageTextSpacing = 31;
        radioPage3.CustomFont = new Font("Inter", 36f, FontStyle.Regular, GraphicsUnit.Pixel);

        radioPage3.ActiveImage = Image.FromFile("assets\\radioActive.png");
        radioPage3.InactiveImage = Image.FromFile("assets\\radioInactive.png");

        radioPage3.Location = new Point(normalizedPos + 406, 220);
        radioPage3.Size = new Size(1400, 500);

        radioPage3.AddOption("$9,999 or less", new Point(0, 0));
        radioPage3.AddOption("25,000-$49,999", new Point(0, 100));
        radioPage3.AddOption("$75,000-$99,999", new Point(0, 200));
        radioPage3.AddOption("Prefer Not to Answer", new Point(0, 300));
        radioPage3.AddOption("$10,000-$24,999", new Point(708, 0));
        radioPage3.AddOption("$50,000-$74,999", new Point(708, 100));
        radioPage3.AddOption("$100,000 or more", new Point(708, 200));

        radioPage3.SelectedIndexChanged += (sender, e) => hasSelectedAnnualIncomeRange = true;

        formImg.Controls.Add(radioPage3);
    }
}
