namespace market_research_survey;

public partial class Window : Form
{
    private CustomRadioGroup? radioPage5;
    private bool hasSelectedEducation = false;

    private void InitPage5()
    {
        radioPage5 = new();

        int normalizedPos = 5 * 1920;

        Label question = new()
        {
            Text = "What is the highest level of education you have completed?",
            Font = new("Inter", 56f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            Location = new Point(normalizedPos + 205, 83),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        formImg!.Controls.Add(question);

        radioPage5!.ImageTextSpacing = 31;
        radioPage5.CustomFont = new Font("Inter", 36f, FontStyle.Regular, GraphicsUnit.Pixel);

        radioPage5.ActiveImage = Image.FromFile("assets\\radioActive.png");
        radioPage5.InactiveImage = Image.FromFile("assets\\radioInactive.png");

        radioPage5.Location = new Point(normalizedPos + 406, 220);
        radioPage5.Size = new Size(1400, 500);

        radioPage5.AddOption("Less than high school", new Point(0, 0));
        radioPage5.AddOption("Some college / University", new Point(0, 100));
        radioPage5.AddOption("Undergraduate degree", new Point(0, 200));
        radioPage5.AddOption("Doctorate", new Point(0, 300));
        radioPage5.AddOption("Other", new Point(0, 400));

        radioPage5.AddOption("High school", new Point(708, 0));
        radioPage5.AddOption("College diploma / Certificate", new Point(708, 100));
        radioPage5.AddOption("Masters / Graduate degreer", new Point(708, 200));
        radioPage5.AddOption("Prefer not to answer", new Point(708, 300));

        radioPage5.SelectedIndexChanged += (sender, e) => hasSelectedEducation = true;

        formImg.Controls.Add(radioPage5);
    }
}
