namespace market_research_survey;

public partial class Window : Form
{
    private CustomCheckboxGroup? checkboxPage11;
    private bool hasSelectedSportswearDesign = false;

    private void InitPage11()
    {
        checkboxPage11 = new();

        int normalizedPos = 11 * 1920;

        Label question = new()
        {
            Text = "What is your preferred design for sportswear?",
            Font = new("Inter", 40f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            Location = new Point(normalizedPos + 205, 83),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        formImg!.Controls.Add(question);

        checkboxPage11!.ImageTextSpacing = 31;
        checkboxPage11.CustomFont = new Font("Inter", 36f, FontStyle.Regular, GraphicsUnit.Pixel);

        checkboxPage11.ActiveImage = Image.FromFile("assets\\checkboxActive.png");
        checkboxPage11.InactiveImage = Image.FromFile("assets\\checkboxInactive.png");

        checkboxPage11.Location = new Point(normalizedPos + 406, 220);
        checkboxPage11.Size = new Size(1400, 500);

        checkboxPage11.AddOption("Minimalist (with 1 or 2 colors)", new Point(0, 0));
        checkboxPage11.AddOption("With slogans and pictures on", new Point(0, 100));

        checkboxPage11.AddOption("Bold colors and design", new Point(708, 0));
        checkboxPage11.AddOption("Other", new Point(708, 100));

        checkboxPage11.SelectionChanged += (sender, e) =>
            hasSelectedSportswearDesign = checkboxPage11.SelectedIndices.Count > 0;

        formImg.Controls.Add(checkboxPage11);
    }
}
