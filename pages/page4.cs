namespace market_research_survey;

public partial class Window : Form
{
    private CustomRadioGroup? radioPage4;
    private bool hasSelectedEmploymentStatus = false;

    private void InitPage4()
    {
        radioPage4 = new();

        int normalizedPos = 4 * 1920;

        Label question = new()
        {
            Text = "What is your employment status?",
            Font = new("Inter", 56f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            Location = new Point(normalizedPos + 205, 83),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        formImg!.Controls.Add(question);

        radioPage4!.ImageTextSpacing = 31;
        radioPage4.CustomFont = new Font("Inter", 36f, FontStyle.Regular, GraphicsUnit.Pixel);

        radioPage4.ActiveImage = Image.FromFile("assets\\radioActive.png");
        radioPage4.InactiveImage = Image.FromFile("assets\\radioInactive.png");

        radioPage4.Location = new Point(normalizedPos + 406, 220);
        radioPage4.Size = new Size(1400, 500);

        radioPage4.AddOption("Employed full time", new Point(0, 0));
        radioPage4.AddOption("Self-employed", new Point(0, 100));
        radioPage4.AddOption("Not looking for a job", new Point(0, 200));
        radioPage4.AddOption("Student", new Point(0, 300));

        radioPage4.AddOption("Employed part time", new Point(708, 0));
        radioPage4.AddOption("Unemployed", new Point(708, 100));
        radioPage4.AddOption("Homemaker", new Point(708, 200));
        radioPage4.AddOption("Prefer not to answer", new Point(708, 300));

        radioPage4.SelectedIndexChanged += (sender, e) => hasSelectedEmploymentStatus = true;

        formImg.Controls.Add(radioPage4);
    }
}
