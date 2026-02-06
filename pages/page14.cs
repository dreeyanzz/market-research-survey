namespace market_research_survey;

public partial class Window : Form
{
    private CustomCheckboxGroup? checkboxPage14;
    private bool hasSelectedBrandPersonality = false;

    private void InitPage14()
    {
        checkboxPage14 = new();

        // Page 14 starts at 14 * 1920
        int normalizedPos = 14 * 1920;

        Label question = new()
        {
            Text = "If your favorite sports brand was a person, how would you describe it?",
            Font = new("Inter", 40f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            Location = new Point(normalizedPos + 205, 83),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        formImg!.Controls.Add(question);

        // 1. Configure Checkbox Visuals
        checkboxPage14.ImageTextSpacing = 31;
        checkboxPage14.CustomFont = new Font("Inter", 36f, FontStyle.Regular, GraphicsUnit.Pixel);

        checkboxPage14.ActiveImage = Image.FromFile("assets\\checkboxActive.png");
        checkboxPage14.InactiveImage = Image.FromFile("assets\\checkboxInactive.png");

        // 2. Set Control Area
        checkboxPage14.Location = new Point(normalizedPos + 406, 220);
        checkboxPage14.Size = new Size(1400, 700);

        // 3. Add Options in 3 Columns
        // Column 1
        checkboxPage14.AddOption("Imaginative", new Point(0, 0));
        checkboxPage14.AddOption("Colorful", new Point(0, 80));
        checkboxPage14.AddOption("Cheerful", new Point(0, 160));
        checkboxPage14.AddOption("Daring", new Point(0, 240));
        checkboxPage14.AddOption("Intelligent", new Point(0, 320));
        checkboxPage14.AddOption("Tough", new Point(0, 400));

        // Column 2
        checkboxPage14.AddOption("Serious", new Point(450, 0));
        checkboxPage14.AddOption("Spirited", new Point(450, 80));
        checkboxPage14.AddOption("Outdoorsy", new Point(450, 160));
        checkboxPage14.AddOption("Reliable", new Point(450, 240));
        checkboxPage14.AddOption("Honest", new Point(450, 320));
        checkboxPage14.AddOption("Other", new Point(450, 400));

        // Column 3
        checkboxPage14.AddOption("Funny", new Point(900, 0));
        checkboxPage14.AddOption("Successful", new Point(900, 80));
        checkboxPage14.AddOption("Introvert", new Point(900, 160));
        checkboxPage14.AddOption("Sophisticated", new Point(900, 240));
        checkboxPage14.AddOption("Classy", new Point(900, 320));

        // 4. Handle Selection Logic
        checkboxPage14.SelectionChanged += (sender, e) =>
        {
            hasSelectedBrandPersonality = checkboxPage14.SelectedIndices.Count > 0;
        };

        formImg.Controls.Add(checkboxPage14);
        checkboxPage14.BringToFront();
    }
}
