namespace market_research_survey;

public partial class Window : Form
{
    private readonly Panel content;
    private readonly Label overlay;
    private Panel? header;
    private readonly PictureBox titleBarImg;
    private ImageButton? getStartedButton;
    private ImageButton? prevButton;
    private ImageButton? nextButton;
    private Panel form;
    private PictureBox formImg;
    private Size maximizedSize;

    public Window()
    {
        InitializeComponent();
        WindowState = FormWindowState.Maximized;
        BackColor = Color.FromArgb(217, 217, 217);

        content = new() { };

        InitHeader();

        Image _titleBarImage = Image.FromFile("assets\\TitleBar.png");
        titleBarImg = new()
        {
            Image = _titleBarImage,
            Location = new(0, -_titleBarImage.Height),
            SizeMode = PictureBoxSizeMode.AutoSize,
        };

        Image _formImg = Image.FromFile("assets\\form.png");

        form = new() { Location = new Point(1920, 185), Size = _formImg.Size };

        formImg = new()
        {
            Image = Image.FromFile("assets\\form.png"),
            Location = new(0, 0),
            SizeMode = PictureBoxSizeMode.AutoSize,
        };

        form.Controls.Add(formImg);

        InitNavigationButtons();

        content.Controls.Add(header);
        content.Controls.Add(titleBarImg);
        content.Controls.Add(form);

        content.Controls.SetChildIndex(header!, 1);
        content.Controls.SetChildIndex(titleBarImg, 2);
        content.Controls.SetChildIndex(form, 3);

        overlay = new()
        {
            Font = new Font("Jetbrains Mono", 12f, FontStyle.Regular),
            BackColor = Color.Black,
            ForeColor = Color.White,
            Dock = DockStyle.Fill,
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
        };

        Controls.Add(content);
        Controls.Add(overlay);

        Controls.SetChildIndex(overlay, 0);
        Controls.SetChildIndex(content, 1);

        Load += Window_Load;
        SizeChanged += Window_OnResize;
    }

    private void InitNavigationButtons()
    {
        Image prevButtonImage = Image.FromFile("assets\\buttonprevious.png");
        prevButton = new()
        {
            NormalImage = prevButtonImage,
            Size = new(prevButtonImage.Width, prevButtonImage.Height),
            Location = new Point(-prevButtonImage.Width, 877), // animates to (364, 877)
        };
        prevButton.Click += (sender, e) =>
        {
            Point currentPos = form.Location;
            AnimateControl(form, new Point(currentPos.X + 1920, currentPos.Y), 500);
        };

        Image nextButtonImage = Image.FromFile("assets\\buttonnext.png");
        nextButton = new()
        {
            NormalImage = nextButtonImage,
            Size = new(nextButtonImage.Width, nextButtonImage.Height),
            Location = new Point(1920, 877), // animates to (1358, 877)
        };
        nextButton.Click += (sender, e) =>
        {
            Point currentPos = form.Location;
            AnimateControl(form, new Point(currentPos.X - 1920, currentPos.Y), 500);
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
            AnimateControl(header!, new Point(0, 10), 500);
            AnimateControl(titleBarImg, new Point(0, 0), 500);

            AnimateControl(getStartedButton, new Point(getStartedButton.Location.X, 991), 500);

            Point currentPos = form.Location;
            AnimateControl(form, new Point(currentPos.X - 1920, currentPos.Y), 500);

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
}
