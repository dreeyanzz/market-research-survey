namespace market_research_survey;

public partial class Window : Form
{
    private CustomCheckboxGroup? checkboxPage10;
    private bool hasSelectedBuySportswear = false;

    private void InitPage10()
    {
        checkboxPage10 = new();

        int normalizedPos = 10 * 1920;

        Label question = new()
        {
            Text = "Where do you usually buy your sportswear from?",
            Font = new("Inter", 40f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            Location = new Point(normalizedPos + 205, 83),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        formImg!.Controls.Add(question);

        checkboxPage10!.ImageTextSpacing = 31;
        checkboxPage10.CustomFont = new Font("Inter", 36f, FontStyle.Regular, GraphicsUnit.Pixel);

        checkboxPage10.ActiveImage = Image.FromFile("assets\\checkboxActive.png");
        checkboxPage10.InactiveImage = Image.FromFile("assets\\checkboxInactive.png");

        checkboxPage10.Location = new Point(normalizedPos + 406, 220);
        checkboxPage10.Size = new Size(1400, 500);

        checkboxPage10.AddOption("Retail store", new Point(0, 0));
        checkboxPage10.AddOption("Multi brand retail", new Point(0, 100));
        checkboxPage10.AddOption("Other", new Point(0, 200));

        checkboxPage10.AddOption("Departmental store", new Point(708, 0));
        checkboxPage10.AddOption("Online", new Point(708, 100));

        checkboxPage10.SelectionChanged += (sender, e) =>
            hasSelectedBuySportswear = checkboxPage10.SelectedIndices.Count > 0;

        formImg.Controls.Add(checkboxPage10);
    }
}
