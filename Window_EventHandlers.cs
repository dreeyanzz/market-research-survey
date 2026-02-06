namespace market_research_survey;

public partial class Window : Form
{
    private void Window_Load(object? sender, EventArgs e)
    {
        maximizedSize = ClientSize; // 1920, 991
        content.Size = maximizedSize;
        CheckWindowState();
    }

    private void Window_OnResize(object? sender, EventArgs e)
    {
        CheckWindowState();
    }

    private void CheckWindowState()
    {
        if (this.WindowState == FormWindowState.Maximized)
        {
            overlay.Visible = false;
            content.Visible = true;
            content.BringToFront();
        }
        else
        {
            string dim = $"{ClientSize.Width} x {ClientSize.Height}";
            string maxSize = $"{maximizedSize.Width} x {maximizedSize.Height}";
            string message =
                $"Please maximize the screen.\nCurrent Size: {dim}\nMaximized size: {maxSize}";

            overlay.Text = message;
            overlay.Visible = true;
            content.Visible = false;
            overlay.BringToFront();
        }
    }

    private void AnimateControl(Control target, Point targetLocation, int durationMs)
    {
        Point startLocation = target.Location;
        DateTime startTime = DateTime.Now;

        // Create a local timer so multiple controls can animate at once
        System.Windows.Forms.Timer timer = new();
        timer.Interval = 1;

        timer.Tick += (s, e) =>
        {
            double elapsed = (DateTime.Now - startTime).TotalMilliseconds;
            double t = Math.Min(1.0, elapsed / durationMs);

            // Ease In Out Formula
            double easedT = t < 0.5 ? 2 * t * t : 1 - Math.Pow(-2 * t + 2, 2) / 2;

            int newX = (int)(startLocation.X + (targetLocation.X - startLocation.X) * easedT);
            int newY = (int)(startLocation.Y + (targetLocation.Y - startLocation.Y) * easedT);

            target.Location = new Point(newX, newY);

            if (t >= 1.0)
            {
                timer.Stop();
                timer.Dispose();
            }
        };

        timer.Start();
    }

    private void InitHeader()
    {
        header = new()
        {
            Location = new Point(440, 435),
            AutoSize = true,
            BackColor = Color.Transparent,
        };

        Label title = new()
        {
            Text = "Market Research Survey",
            Font = new Font("Inter", 70f, FontStyle.Bold, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            BackColor = Color.Transparent,
            Location = new Point(0, 0),
            AutoSize = true,
        };

        Label subtitle = new()
        {
            Text = "Please take a few minutes to tell us more about you and your preferences",
            Font = new Font("Inter", 30f, FontStyle.Regular, GraphicsUnit.Pixel),
            ForeColor = Color.Black,
            BackColor = Color.Transparent,
            Location = new Point(
                title.Location.X + 10,
                title.Location.Y + title.PreferredSize.Height
            ),
            AutoSize = true,
        };

        header.Controls.Add(title);
        header.Controls.Add(subtitle);
    }

    private void InitNavigationButtons()
    {
        Image prevButtonImage = Image.FromFile("assets\\buttonprevious.png");
        prevButton = new()
        {
            NormalImage = prevButtonImage,
            Size = new(prevButtonImage.Width, prevButtonImage.Height),
            Location = new Point(-prevButtonImage.Width, 877),
        };
        prevButton.Click += (sender, e) =>
        {
            Point currentPos = formImg!.Location;

            if (formImg.Left == 0)
                return;

            int stopPosX = index * 1920;
            if (formImg.Left != -stopPosX)
                return;

            index--;
            AnimateControl(formImg, new Point(currentPos.X + 1920, currentPos.Y), 500);
        };

        Image nextButtonImage = Image.FromFile("assets\\buttonnext.png");
        nextButton = new()
        {
            NormalImage = nextButtonImage,
            Size = new(nextButtonImage.Width, nextButtonImage.Height),
            Location = new Point(1920, 877),
        };
        nextButton.Click += (sender, e) =>
        {
            if (index == 0)
            {
                if (!hasSelectedGender)
                    return;
            }
            else if (index == 1)
            {
                if (!hasSelectedAgeRange)
                    return;
            }
            else if (index == 2)
            {
                if (!hasSelectedMaritalStatus)
                    return;
            }
            else if (index == 3)
            {
                if (!hasSelectedAnnualIncomeRange)
                    return;
            }

            Point currentPos = formImg!.Location;
            if (formImg.Right == 1920)
                return;

            int stopPosX = index * 1920;
            if (formImg.Left != -stopPosX)
                return;

            index++;
            AnimateControl(formImg, new Point(currentPos.X - 1920, currentPos.Y), 500);
        };

        Image getStartedImage = Image.FromFile("assets\\getStartedButton.png");
        getStartedButton = new()
        {
            NormalImage = getStartedImage,
            Size = new(getStartedImage.Width, getStartedImage.Height),
            Location = new Point(861, 877),
        };

        getStartedButton.Click += (sender, e) =>
        {
            index++;

            AnimateControl(header!, new Point(0, 10), 500);
            AnimateControl(titleBarImg, new Point(0, 0), 500);

            AnimateControl(getStartedButton, new Point(getStartedButton.Location.X, 991), 500);

            Point currentPos = formImg!.Location;
            AnimateControl(formImg, new Point(currentPos.X - 1920, currentPos.Y), 500);

            AnimateControl(prevButton, new Point(364, 877), 500);
            AnimateControl(nextButton, new Point(1358, 877), 500);

            prevButton.BringToFront();
            nextButton.BringToFront();
        };

        content.Controls.Add(prevButton);
        content.Controls.Add(nextButton);
        content.Controls.Add(getStartedButton);

        content.Controls.SetChildIndex(prevButton, 0);
        content.Controls.SetChildIndex(nextButton, 0);
        content.Controls.SetChildIndex(getStartedButton, 0);
    }

    private void InitForm()
    {
        Image _formImg = Image.FromFile("assets\\form.png");

        form = new() { Location = new Point(0, 185), Size = _formImg.Size };

        formImg = new()
        {
            Image = Image.FromFile("assets\\group1.png"),
            Location = new(1920, 0),
            SizeMode = PictureBoxSizeMode.AutoSize,
        };

        form.Controls.Add(formImg);
        form.Controls.SetChildIndex(formImg, 1);
    }
}
