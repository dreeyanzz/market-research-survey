namespace market_research_survey;

public partial class Window : Form
{
    private LikertScale? likertPage7;
    private bool hasSelectedSportswear = false;

    private void InitPage7()
    {
        likertPage7 = new();

        int normalizedPos = 7 * 1920;

        Label question = new()
        {
            Text = "How often do you use sportswear products?",
            Font = new("Inter", 56f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            Location = new Point(normalizedPos + 205, 83),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        formImg!.Controls.Add(question);

        likertPage7.Location = new Point(normalizedPos + 406, 300);
        likertPage7.Width = 1920 - (406 * 2);
        likertPage7.Height = 100;
        likertPage7.ActiveImage = Image.FromFile("assets\\radioActive.png");
        likertPage7.InactiveImage = Image.FromFile("assets\\radioInactive.png");
        likertPage7.MinLabel = "Not At All Often";
        likertPage7.MaxLabel = "Extremely Often";

        likertPage7.MaxRange = 10;

        likertPage7.SelectedIndexChanged += (sender, e) => hasSelectedSportswear = true;

        formImg.Controls.Add(likertPage7);
    }
}
