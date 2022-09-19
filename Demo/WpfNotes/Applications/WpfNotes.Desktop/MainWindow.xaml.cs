namespace WpfNotes.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly Dialog _dialog = new();
    private readonly IApplicationProvider _application;

    /// <summary>
    /// Contructor
    /// </summary>
    /// <param name="application">Application</param>
    /// <param name="services">Services</param>
    public MainWindow(IApplicationProvider application, IServiceProvider services)
    {
        InitializeComponent();
        Resources.Add(nameof(services), services);
        _application = application;
        Display.DataContext = _application.Content;
        _application.Confirm = async (DialogModel confirm) =>
            await _dialog.DeleteAsync(confirm);
        _application.Upsert = async (DialogModel upsert) =>
            await _dialog.UpsertAsync(upsert);
    }

    /// <summary>
    /// Closing
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event Args</param>
    private void Window_Closing(object sender, CancelEventArgs e)
    {
        e.Cancel = true;
        Hide();
    }
}
