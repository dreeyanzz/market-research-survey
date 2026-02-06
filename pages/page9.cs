namespace market_research_survey;

public partial class Window : Form
{
    private CustomCheckboxGroup? checkboxPage9;
    private bool hasSelectedSportswearPurpose = false;

    private void InitPage9()
    {
        checkboxPage9 = new();

        int normalizedPos = 9 * 1920;

        Label question = new()
        {
            Text = "Which of the followings would describe your purpose to buy sportswear?",
            Font = new("Inter", 40f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            Location = new Point(normalizedPos + 205, 83),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        formImg!.Controls.Add(question);

        checkboxPage9!.ImageTextSpacing = 31;
        checkboxPage9.CustomFont = new Font("Inter", 36f, FontStyle.Regular, GraphicsUnit.Pixel);

        checkboxPage9.ActiveImage = Image.FromFile("assets\\checkboxActive.png");
        checkboxPage9.InactiveImage = Image.FromFile("assets\\checkboxInactive.png");

        checkboxPage9.Location = new Point(normalizedPos + 406, 220);
        checkboxPage9.Size = new Size(1400, 500);

        checkboxPage9.AddOption("Gym/fitness", new Point(0, 0));
        checkboxPage9.AddOption("Hiking", new Point(0, 100));
        checkboxPage9.AddOption("Sports (basketball, swimming etc)", new Point(0, 200));

        checkboxPage9.AddOption("Running", new Point(708, 0));
        checkboxPage9.AddOption("Outdoor fashion", new Point(708, 100));
        checkboxPage9.AddOption("Other", new Point(708, 200));

        checkboxPage9.SelectionChanged += (sender, e) =>
            hasSelectedSportswearPurpose = checkboxPage9.SelectedIndices.Count > 0;

        formImg.Controls.Add(checkboxPage9);
    }
}
