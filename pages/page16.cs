namespace market_research_survey;

public partial class Window : Form
{
    private Panel? summaryPanel;
    private ImageButton? submitButton;
    private ImageButton? backToEditButton;

    private void InitPage16()
    {
        int normalizedPos = 16 * 1920;

        Label title = new()
        {
            Text = "Survey Summary",
            Font = new("Inter", 56f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            Location = new Point(normalizedPos + 700, 50),
            AutoSize = true,
        };

        formImg!.Controls.Add(title);

        // Create scrollable summary panel - centered
        summaryPanel = new()
        {
            Location = new Point(normalizedPos + 210, 150),
            Size = new Size(1500, 620),
            AutoScroll = true,
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
        };

        formImg.Controls.Add(summaryPanel);

        // Create Submit and Back buttons
        CreateSummaryButtons(normalizedPos);
    }

    private void PopulateSummaryPanel()
    {
        if (summaryPanel == null)
            return;

        // Clear existing controls if any
        summaryPanel.Controls.Clear();

        int yPosition = 20;
        int leftMargin = 30;
        int lineHeight = 45;
        int sectionSpacing = 60;

        Font questionFont = new("Inter", 18f, FontStyle.Bold, GraphicsUnit.Pixel);
        Font answerFont = new("Inter", 16f, FontStyle.Regular, GraphicsUnit.Pixel);
        Font sectionFont = new("Inter", 22f, FontStyle.Bold, GraphicsUnit.Pixel);

        // Helper method to add a section
        void AddSection(string sectionTitle)
        {
            Label sectionLabel = new()
            {
                Text = sectionTitle,
                Font = sectionFont,
                ForeColor = Color.FromArgb(0, 102, 204),
                Location = new Point(leftMargin, yPosition),
                AutoSize = true,
            };
            summaryPanel.Controls.Add(sectionLabel);
            yPosition += sectionSpacing;
        }

        // Helper method to add question and answer
        void AddQuestionAnswer(string question, string answer)
        {
            Label questionLabel = new()
            {
                Text = question,
                Font = questionFont,
                ForeColor = Color.Black,
                Location = new Point(leftMargin, yPosition),
                AutoSize = true,
                MaximumSize = new Size(1400, 0),
            };
            summaryPanel.Controls.Add(questionLabel);
            yPosition += lineHeight;

            Label answerLabel = new()
            {
                Text = "→ " + answer,
                Font = answerFont,
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(leftMargin + 20, yPosition),
                AutoSize = true,
                MaximumSize = new Size(1400, 0),
            };
            summaryPanel.Controls.Add(answerLabel);
            yPosition += lineHeight + 15;
        }

        // === DEMOGRAPHICS SECTION ===
        AddSection("📊 Demographics");

        // Page 0: Gender
        string genderAnswer =
            radioPage0?.SelectedIndex >= 0
                ? radioPage0.Options[radioPage0.SelectedIndex].Label
                : "Not answered";
        AddQuestionAnswer("What is your gender?", genderAnswer);

        // Page 1: Age Range
        string ageAnswer =
            radioPage1?.SelectedIndex >= 0
                ? radioPage1.Options[radioPage1.SelectedIndex].Label
                : "Not answered";
        AddQuestionAnswer("What is your age range?", ageAnswer);

        // Page 2: Marital Status
        string maritalAnswer =
            radioPage2?.SelectedIndex >= 0
                ? radioPage2.Options[radioPage2.SelectedIndex].Label
                : "Not answered";
        AddQuestionAnswer("What is your marital status?", maritalAnswer);

        // Page 3: Annual Income
        string incomeAnswer =
            radioPage3?.SelectedIndex >= 0
                ? radioPage3.Options[radioPage3.SelectedIndex].Label
                : "Not answered";
        AddQuestionAnswer("What is your annual household income range?", incomeAnswer);

        // Page 4: Employment Status
        string employmentAnswer =
            radioPage4?.SelectedIndex >= 0
                ? radioPage4.Options[radioPage4.SelectedIndex].Label
                : "Not answered";
        AddQuestionAnswer("What is your employment status?", employmentAnswer);

        // Page 5: Education
        string educationAnswer =
            radioPage5?.SelectedIndex >= 0
                ? radioPage5.Options[radioPage5.SelectedIndex].Label
                : "Not answered";
        AddQuestionAnswer(
            "What is the highest level of education you have completed?",
            educationAnswer
        );

        yPosition += 20;

        // === SPORTSWEAR BEHAVIOR SECTION ===
        AddSection("👟 Sportswear Behavior & Usage");

        // Page 6: Exercise Frequency (Likert Scale)
        string exerciseAnswer =
            likertPage6?.SelectedValue > 0
                ? $"{likertPage6.SelectedValue}/10 (Not At All Often → Extremely Often)"
                : "Not answered";
        AddQuestionAnswer("How often do you exercise?", exerciseAnswer);

        // Page 7: Sportswear Usage (Likert Scale)
        string usageAnswer =
            likertPage7?.SelectedValue > 0
                ? $"{likertPage7.SelectedValue}/10 (Not At All Often → Extremely Often)"
                : "Not answered";
        AddQuestionAnswer("How often do you use sportswear products?", usageAnswer);

        // Page 8: Last Purchase
        string lastPurchaseAnswer =
            radioPage8?.SelectedIndex >= 0
                ? radioPage8.Options[radioPage8.SelectedIndex].Label
                : "Not answered";
        AddQuestionAnswer("When did you last buy sportswear?", lastPurchaseAnswer);

        // Page 9: Purpose (Checkbox - multiple answers)
        string purposeAnswer =
            checkboxPage9?.SelectedIndices.Count > 0
                ? string.Join(
                    ", ",
                    checkboxPage9.SelectedIndices.Select(i => checkboxPage9.Options[i].Label)
                )
                : "Not answered";
        AddQuestionAnswer("What do you primarily use sportswear for?", purposeAnswer);

        // Page 10: Purchase Location (Checkbox)
        string locationAnswer =
            checkboxPage10?.SelectedIndices.Count > 0
                ? string.Join(
                    ", ",
                    checkboxPage10.SelectedIndices.Select(i => checkboxPage10.Options[i].Label)
                )
                : "Not answered";
        AddQuestionAnswer("Where do you usually buy your sportswear from?", locationAnswer);

        // Page 11: Design Preferences (Checkbox)
        string designAnswer =
            checkboxPage11?.SelectedIndices.Count > 0
                ? string.Join(
                    ", ",
                    checkboxPage11.SelectedIndices.Select(i => checkboxPage11.Options[i].Label)
                )
                : "Not answered";
        AddQuestionAnswer("What is your preferred design for sportswear?", designAnswer);

        // Page 12: Brand Influence (Checkbox)
        string influenceAnswer =
            checkboxPage12?.SelectedIndices.Count > 0
                ? string.Join(
                    ", ",
                    checkboxPage12.SelectedIndices.Select(i => checkboxPage12.Options[i].Label)
                )
                : "Not answered";
        AddQuestionAnswer(
            "Which of the following would influence your decision to buy a certain brand?",
            influenceAnswer
        );

        yPosition += 20;

        // === PREFERENCES & PSYCHOGRAPHICS SECTION ===
        AddSection("⭐ Preferences & Psychographics");

        // Page 13: Material Ratings (Star ratings)
        AddQuestionAnswer("Material Preferences (1-5 stars):", "");
        if (ratingPage13?.Categories != null && ratingPage13.Categories.Count > 0)
        {
            foreach (var category in ratingPage13.Categories)
            {
                string stars =
                    new string('★', category.CurrentRating)
                    + new string('☆', 5 - category.CurrentRating);
                Label ratingLabel = new()
                {
                    Text = $"  • {category.Label}: {stars} ({category.CurrentRating}/5)",
                    Font = answerFont,
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Location = new Point(leftMargin + 40, yPosition),
                    AutoSize = true,
                };
                summaryPanel.Controls.Add(ratingLabel);
                yPosition += 35;
            }
        }
        else
        {
            Label noRating = new()
            {
                Text = "  • Not answered",
                Font = answerFont,
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(leftMargin + 40, yPosition),
                AutoSize = true,
            };
            summaryPanel.Controls.Add(noRating);
            yPosition += 35;
        }
        yPosition += 10;

        // Page 14: Brand Personality (Checkbox)
        string brandPersonalityAnswer =
            checkboxPage14?.SelectedIndices.Count > 0
                ? string.Join(
                    ", ",
                    checkboxPage14.SelectedIndices.Select(i => checkboxPage14.Options[i].Label)
                )
                : "Not answered";
        AddQuestionAnswer(
            "If your favorite sports brand was a person, how would you describe it?",
            brandPersonalityAnswer
        );

        // Page 15: Other Interests (Checkbox)
        string interestsAnswer =
            checkboxPage15?.SelectedIndices.Count > 0
                ? string.Join(
                    ", ",
                    checkboxPage15.SelectedIndices.Select(i => checkboxPage15.Options[i].Label)
                )
                : "Not answered";
        AddQuestionAnswer("What are your other interests?", interestsAnswer);

        yPosition += 30;

        // Add a thank you message at the bottom
        Label thankYouLabel = new()
        {
            Text = "Thank you for completing this survey! 🎉",
            Font = new Font("Inter", 20f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.FromArgb(0, 153, 76),
            Location = new Point(leftMargin, yPosition),
            AutoSize = true,
        };
        summaryPanel.Controls.Add(thankYouLabel);
    }

    private void CreateSummaryButtons(int normalizedPos)
    {
        // Create a "Submit Survey" button
        Image buttonImage = Image.FromFile("assets\\buttonnext.png");

        submitButton = new()
        {
            NormalImage = buttonImage,
            Size = new Size(buttonImage.Width, buttonImage.Height),
            Location = new Point(normalizedPos + 1000, 780),
            ButtonText = "Submit",
            ShowText = false,
        };

        submitButton.Click += (sender, e) =>
        {
            MessageBox.Show(
                "Survey submitted successfully!\n\nThank you for your participation.",
                "Survey Complete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            // TODO: Add actual submission logic here (save to database, export to file, etc.)
        };

        formImg!.Controls.Add(submitButton);

        // Create a "Back to Edit" button
        Image backImage = Image.FromFile("assets\\buttonprevious.png");

        backToEditButton = new()
        {
            NormalImage = backImage,
            Size = new Size(backImage.Width, backImage.Height),
            Location = new Point(normalizedPos + 720, 780),
            ButtonText = "Edit",
            ShowText = false,
        };

        backToEditButton.Click += (sender, e) =>
        {
            // Show Next button again
            if (nextButton != null)
            {
                nextButton.Visible = true;
            }
            
            // Go back to page 0 so user can edit their answers
            Point currentPos = formImg!.Location;
            AnimateControl(formImg, new Point(currentPos.X + (index * 1920), currentPos.Y), 500);
            index = 0;
        };

        formImg.Controls.Add(backToEditButton);
    }
}
