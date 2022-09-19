namespace WpfNotes.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private const string mutex_name = @"Global\WpfNotes";

    /// <summary>
    /// Taskbar Icon
    /// </summary>
    private TaskbarIcon? _tray;

    /// <summary>
    /// Host
    /// </summary>
    public static IHost? Host { get; private set; }

    /// <summary>
    /// Enforce Single instance
    /// </summary>
    private static void EnforceSingleInstance()
    {
        _ = new Mutex(true, mutex_name, out var isNewInstance);
        if (!isNewInstance)
            Current.Shutdown();
    }

    /// <summary>
    /// Show Main Window
    /// </summary>
    private static void ShowMainWindow() =>
        Host?.Services.GetRequiredService<MainWindow>()?.Show();

    /// <summary>
    /// Start Service Host
    /// </summary>
    /// <returns></returns>
    private static async Task StartServiceHostAsync()
    {
        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
        .ConfigureServices(services => services.AddServices())
        .AddPages()
        .Build();
        await Host!.StartAsync();
    }

    /// <summary>
    /// Stop Service Host
    /// </summary>
    private static async Task StopServiceHostAsync() =>
        await Host!.StopAsync();

    /// <summary>
    /// Setup Tray
    /// </summary>
    private void SetupTray()
    {
        _tray = (TaskbarIcon)FindResource(nameof(TaskbarIcon));
        _tray.DataContext = new TaskBarIconModel();
    }

    /// <summary>
    /// Dispose Tray Icon
    /// </summary>
    private void DisposeTray() =>
        _tray?.Dispose();

    /// <summary>
    /// On Startup
    /// </summary>
    /// <param name="e">Startup Event Args</param>
    protected override async void OnStartup(StartupEventArgs e)
    {
        EnforceSingleInstance();
        await StartServiceHostAsync();
        SetupTray();
        ShowMainWindow();
        base.OnStartup(e);
    }

    /// <summary>
    /// On Exit
    /// </summary>
    /// <param name="e">Exit Event Args</param>
    protected override async void OnExit(ExitEventArgs e)
    {
        DisposeTray();
        await StopServiceHostAsync();
        base.OnExit(e);
    }
}
