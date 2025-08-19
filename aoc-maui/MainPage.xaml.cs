namespace aoc_maui;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
    }

    private async void Run(object sender, EventArgs e)
    {
        if (!FolderLabel.Text.StartsWith("Please select"))
        {
            var graphicsView = this.DrawableView;
            ((GraphicsDrawable)graphicsView.Drawable).UpdateBoard(new Board(FolderLabel.Text));
            graphicsView.Invalidate();
        }
    }

    private async void PickFile(object sender, EventArgs e)
    {
        PickOptions options = new()
        {
            PickerTitle = "Please select a file",
        };
        await PickAndShow(options);
    }

    public async Task<FileResult> PickAndShow(PickOptions options)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                FolderLabel.Text = result.FullPath;
            }
            return result;
        }
        catch (Exception ex)
        {
            // The user canceled or something went wrong
        }

        return null;
    }
}

