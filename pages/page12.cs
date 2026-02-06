namespace market_research_survey;

public partial class Window : Form
{
    private CustomCheckboxGroup? checkboxPage12;
    private bool hasSelectedInfluence = false;

    private void InitPage12()
    {
        checkboxPage12 = new();

        int normalizedPos = 12 * 1920;

        Label question = new()
        {
            Text = "Which of the followings would influence your decision to buy a certain brand?",
            Font = new("Inter", 40f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            Location = new Point(normalizedPos + 205, 83),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        formImg!.Controls.Add(question);

        checkboxPage12!.ImageTextSpacing = 31;
        checkboxPage12.CustomFont = new Font("Inter", 36f, FontStyle.Regular, GraphicsUnit.Pixel);

        checkboxPage12.ActiveImage = Image.FromFile("assets\\checkboxActive.png");
        checkboxPage12.InactiveImage = Image.FromFile("assets\\checkboxInactive.png");

        checkboxPage12.Location = new Point(normalizedPos + 406, 220);
        checkboxPage12.Size = new Size(1400, 500);

        checkboxPage12.AddOption("Price", new Point(0, 0));
        checkboxPage12.AddOption("Value", new Point(0, 100));
        checkboxPage12.AddOption("Other", new Point(0, 200));

        checkboxPage12.AddOption("Quality", new Point(708, 0));
        checkboxPage12.AddOption("Brand", new Point(708, 100));

        checkboxPage12.SelectionChanged += (sender, e) =>
            hasSelectedInfluence = checkboxPage12.SelectedIndices.Count > 0;

        formImg.Controls.Add(checkboxPage12);
    }
}
