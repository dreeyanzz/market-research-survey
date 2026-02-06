namespace market_research_survey;

public partial class Window : Form
{
    private RatingGroup? ratingPage13;
    private bool hasRatedAllMaterials = false;

    private void InitPage13()
    {
        ratingPage13 = new();

        int normalizedPos = 13 * 1920;

        Label question = new()
        {
            Text = "Please rate your preferences regarding the materials used in sportswear",
            Font = new("Inter", 40f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            Location = new Point(normalizedPos + 205, 70), // Moved up slightly from 83
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        formImg!.Controls.Add(question);

        // 1. Configure Visuals
        ratingPage13.ActiveImage = Image.FromFile("assets\\radioActive.png");
        ratingPage13.InactiveImage = Image.FromFile("assets\\radioInactive.png");
        ratingPage13.StarSize = new Size(48, 48);
        ratingPage13.StarSpacing = 12;
        ratingPage13.MaxStars = 5;
        ratingPage13.Font = new Font("Inter", 32f, FontStyle.Regular, GraphicsUnit.Pixel);

        // 2. Set Control Bounds
        // Moved Y up to 200 (from 220) to give more room at the bottom
        ratingPage13.Location = new Point(normalizedPos + 406, 200);
        ratingPage13.Size = new Size(1400, 650);

        // 3. Optimized Points (Tightened to 130px increments)
        // Column 1
        ratingPage13.AddCategory("Water Resistance", new Point(0, 0));
        ratingPage13.AddCategory("Anti Bacteria", new Point(0, 110));
        ratingPage13.AddCategory("Soft and Smooth Material", new Point(0, 220));
        ratingPage13.AddCategory("Cooling", new Point(0, 330));

        // Column 2
        ratingPage13.AddCategory("Anti Odour", new Point(708, 0));
        ratingPage13.AddCategory("Elasticity", new Point(708, 110));
        ratingPage13.AddCategory("Endurance", new Point(708, 220));

        // 4. Validation logic
        ratingPage13.RatingChanged += (sender, e) =>
        {
            hasRatedAllMaterials = ratingPage13.IsEverythingRated;
        };

        formImg.Controls.Add(ratingPage13);
        ratingPage13.BringToFront();
    }
}
