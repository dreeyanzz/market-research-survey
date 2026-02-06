namespace market_research_survey;

public partial class Window : Form
{
    private LikertScale? likertPage6;

    private void InitPage6()
    {
        likertPage6 = new();

        int normalizedPos = 6 * 1920;

        Label question = new()
        {
            Text = "How often do you exercise?",
            Font = new("Inter", 56f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            Location = new Point(normalizedPos + 205, 83),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        formImg!.Controls.Add(question);

        likertPage6.Location = new Point(normalizedPos + 406, 300);
        likertPage6.Width = 1920 - (406 * 2);
        likertPage6.Height = 100;
        likertPage6.ActiveImage = Image.FromFile("assets\\radioActive.png");
        likertPage6.InactiveImage = Image.FromFile("assets\\radioInactive.png");

        likertPage6.MaxRange = 10;

        formImg.Controls.Add(likertPage6);
    }
}
