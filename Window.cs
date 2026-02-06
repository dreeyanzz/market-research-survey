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
    private Panel? form;
    private PictureBox? formImg;
    private Size maximizedSize;
    private int index = -1;
    private readonly Image _titleBarImage;

    public Window()
    {
        InitializeComponent();
        WindowState = FormWindowState.Maximized;
        BackColor = Color.FromArgb(217, 217, 217);

        content = new() { };

        InitHeader();

        _titleBarImage = Image.FromFile("assets\\TitleBar.png");
        titleBarImg = new()
        {
            Image = _titleBarImage,
            Location = new(0, -_titleBarImage.Height),
            SizeMode = PictureBoxSizeMode.AutoSize,
        };

        InitForm();
        InitNavigationButtons();

        content.Controls.Add(header);
        content.Controls.Add(titleBarImg);
        content.Controls.Add(form);

        content.Controls.SetChildIndex(header!, 1);
        content.Controls.SetChildIndex(titleBarImg, 2);
        content.Controls.SetChildIndex(form!, 3);

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

        InitPage0();
        InitPage1();
        InitPage2();
        InitPage3();
        InitPage4();
        InitPage5();
        InitPage6();
        InitPage7();
        InitPage8();
        InitPage9();
        InitPage10();
        InitPage11();
        InitPage12();
        InitPage13();
        InitPage14();
        InitPage15();
    }
}
