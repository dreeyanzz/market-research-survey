namespace market_research_survey;

public partial class Window : Form
{
    private CustomRadioGroup? radioPage2;
    private bool hasSelectedMaritalStatus = false;

    private void InitPage2()
    {
        radioPage2 = new();

        int normalizedPos = 2 * 1920;

        Label question = new()
        {
            Text = "What is your marital status?",
            Font = new("Inter", 56f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            Location = new Point(normalizedPos + 205, 83),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        formImg!.Controls.Add(question);

        radioPage2!.ImageTextSpacing = 31;
        radioPage2.CustomFont = new Font("Inter", 36f, FontStyle.Regular, GraphicsUnit.Pixel);

        radioPage2.ActiveImage = Image.FromFile("assets\\radioActive.png");
        radioPage2.InactiveImage = Image.FromFile("assets\\radioInactive.png");

        radioPage2.Location = new Point(normalizedPos + 406, 220);
        radioPage2.Size = new Size(1400, 500);

        radioPage2.AddOption("Single", new Point(0, 0));
        radioPage2.AddOption("Divorced", new Point(0, 100));
        radioPage2.AddOption("Prefer not to answer", new Point(0, 200));
        radioPage2.AddOption("Married", new Point(708, 0));
        radioPage2.AddOption("Widowed", new Point(708, 100));

        radioPage2.SelectedIndexChanged += (sender, e) => hasSelectedMaritalStatus = true;

        formImg.Controls.Add(radioPage2);
    }
}
