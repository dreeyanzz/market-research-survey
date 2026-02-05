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
            prevButton!.BackColor = Color.Transparent;
            nextButton!.BackColor = Color.Transparent;
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
}
