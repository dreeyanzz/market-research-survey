namespace market_research_survey;

public partial class Window : Form
{
    private RatingGroup? ratingPage13;

    // Track if all categories have been rated (optional validation)
    private bool hasRatedAllMaterials = false;

    private void InitPage13()
    {
        ratingPage13 = new();

        // Page 13 starts at 13 * 1920
        int normalizedPos = 13 * 1920;

        Label question = new()
        {
            Text = "Please rate your preferences regarding the materials used in sportswear",
            Font = new("Inter", 40f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            Location = new Point(normalizedPos + 205, 83),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        formImg!.Controls.Add(question);

        // 1. Configure the Star Visuals
        ratingPage13.ActiveImage = Image.FromFile("assets\\radioActive.png"); // Your active star PNG
        ratingPage13.InactiveImage = Image.FromFile("assets\\radioInactive.png"); // Your inactive star PNG

        ratingPage13.StarSize = new Size(48, 48); // Scaled size for the stars
        ratingPage13.StarSpacing = 12;
        ratingPage13.MaxStars = 5;
        ratingPage13.Font = new Font("Inter", 32f, FontStyle.Regular, GraphicsUnit.Pixel);

        // 2. Set Control Bounds
        // Spaced similarly to Page 12
        ratingPage13.Location = new Point(normalizedPos + 406, 220);
        ratingPage13.Size = new Size(1400, 700);

        // --- Update these points in InitPage13 ---
        // Column 1
        ratingPage13.AddCategory("Water Resistance", new Point(0, 0));
        ratingPage13.AddCategory("Anti Bacteria", new Point(0, 150));
        ratingPage13.AddCategory("Soft and Smooth Material", new Point(0, 300));
        ratingPage13.AddCategory("Cooling", new Point(0, 450));

        // Column 2
        ratingPage13.AddCategory("Anti Odour", new Point(708, 0));
        ratingPage13.AddCategory("Elasticity", new Point(708, 150));
        ratingPage13.AddCategory("Endurance", new Point(708, 300));

        // 5. Listen for Rating Changes
        // Inside InitPage13
        ratingPage13.RatingChanged += (sender, e) =>
        {
            // This checks if EVERY category has a star selected
            hasRatedAllMaterials = ratingPage13.IsEverythingRated;
        };

        formImg.Controls.Add(ratingPage13);
        ratingPage13.BringToFront();
    }
}
