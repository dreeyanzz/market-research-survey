namespace market_research_survey;

public partial class Window : Form
{
    private readonly CustomRadioGroup radioPage0;
    private bool hasSelectedGender = false;

    private void InitPage0()
    {
        int normalizedPos = 0 * 1920;

        Label question = new()
        {
            Text = "What is your gender?",
            Font = new("Inter", 56f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            Location = new Point(normalizedPos + 205, 83),
            TextAlign = ContentAlignment.MiddleCenter,
            AutoSize = true,
        };

        formImg!.Controls.Add(question);

        radioPage0.ImageTextSpacing = 10;
        radioPage0.CustomFont = new Font("Inter", 56f, FontStyle.Regular, GraphicsUnit.Pixel);

        radioPage0.ActiveImage = Image.FromFile("assets\\radioActive.png");
        radioPage0.InactiveImage = Image.FromFile("assets\\radioInactive.png");

        radioPage0.Location = new Point(normalizedPos + 370, 246);
        radioPage0.Size = new Size(1540, 300);

        radioPage0.AddOption("Female", new Point(0, 0));
        radioPage0.AddOption("Male", new Point(0, 158));
        radioPage0.AddOption("Non-binary", new Point(708, 0));
        radioPage0.AddOption("Prefer not to say", new Point(708, 158));

        radioPage0.SelectedIndexChanged += (sender, e) => hasSelectedGender = true;

        formImg.Controls.Add(radioPage0);
    }
}
