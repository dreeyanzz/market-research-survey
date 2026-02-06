namespace market_research_survey;

public partial class Window : Form
{
    private CustomCheckboxGroup? checkboxPage15;
    private bool hasSelectedInterests = false;

    private void InitPage15()
    {
        checkboxPage15 = new();

        // Page 15 starts at 15 * 1920
        int normalizedPos = 15 * 1920;

        Label question = new()
        {
            Text = "What are your other interests? (Select all that apply)",
            Font = new("Inter", 40f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            Location = new Point(normalizedPos + 205, 83),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        formImg!.Controls.Add(question);

        // 1. Configure Checkbox Visuals
        checkboxPage15.ImageTextSpacing = 25; // Slightly tighter spacing for large list
        checkboxPage15.CustomFont = new Font("Inter", 30f, FontStyle.Regular, GraphicsUnit.Pixel);

        checkboxPage15.ActiveImage = Image.FromFile("assets\\checkboxActive.png");
        checkboxPage15.InactiveImage = Image.FromFile("assets\\checkboxInactive.png");

        // 2. Set Control Area (Tall enough for 9 rows)
        checkboxPage15.Location = new Point(normalizedPos + 205, 200);
        checkboxPage15.Size = new Size(1600, 750);

        // 3. Add Options in 3 Columns (X: 0, 520, 1040)
        // Column 1
        checkboxPage15.AddOption("Arts & Entertainment", new Point(0, 0));
        checkboxPage15.AddOption("Books & Literature", new Point(0, 70));
        checkboxPage15.AddOption("Finance", new Point(0, 140));
        checkboxPage15.AddOption("Hobbies & Leisure", new Point(0, 210));
        checkboxPage15.AddOption("Jobs & Education", new Point(0, 280));
        checkboxPage15.AddOption("Online Communities", new Point(0, 350));
        checkboxPage15.AddOption("Real Estate", new Point(0, 420));
        checkboxPage15.AddOption("Shopping", new Point(0, 490));
        checkboxPage15.AddOption("Motorsports", new Point(0, 560));

        // Column 2
        checkboxPage15.AddOption("Autos & Vehicles", new Point(520, 0));
        checkboxPage15.AddOption("Business & Industrial", new Point(520, 70));
        checkboxPage15.AddOption("Food & Drink", new Point(520, 140));
        checkboxPage15.AddOption("Home & Garden", new Point(520, 210));
        checkboxPage15.AddOption("Law & Government", new Point(520, 280));
        checkboxPage15.AddOption("People & Society", new Point(520, 350));
        checkboxPage15.AddOption("Reference", new Point(520, 420));
        checkboxPage15.AddOption("Travel", new Point(520, 490));
        checkboxPage15.AddOption("E-sports", new Point(520, 560));

        // Column 3
        checkboxPage15.AddOption("Beauty & Fitness", new Point(1040, 0));
        checkboxPage15.AddOption("Computers & Electronics", new Point(1040, 70));
        checkboxPage15.AddOption("Games", new Point(1040, 140));
        checkboxPage15.AddOption("Internet & Telecom", new Point(1040, 210));
        checkboxPage15.AddOption("News", new Point(1040, 280));
        checkboxPage15.AddOption("Pets & Animals", new Point(1040, 350));
        checkboxPage15.AddOption("Science", new Point(1040, 420));
        checkboxPage15.AddOption("World Localities", new Point(1040, 490));

        // 4. Handle Selection Logic
        checkboxPage15.SelectionChanged += (sender, e) =>
        {
            hasSelectedInterests = checkboxPage15.SelectedIndices.Count > 0;
        };

        formImg.Controls.Add(checkboxPage15);
        checkboxPage15.BringToFront();
    }
}
