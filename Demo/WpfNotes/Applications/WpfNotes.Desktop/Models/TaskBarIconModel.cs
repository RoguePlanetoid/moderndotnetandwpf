namespace WpfNotes.Desktop.Models;

/// <summary>
/// Task Bar Icon Model
/// </summary>
public class TaskBarIconModel
{
    /// <summary>
    /// Show Window
    /// </summary>
    public static ICommand ShowWindowCommand => new ActionCommandHandler(
        (obj) => App.Host?.Services.GetRequiredService<MainWindow>()?.Show(), 
        (obj) => !Application.Current.MainWindow.IsVisible);

    /// <summary>
    /// Hide Window
    /// </summary>
    public static ICommand HideWindowCommand => new ActionCommandHandler(
        (obj) => Application.Current.MainWindow.Hide(),
        (obj) => Application.Current.MainWindow.IsVisible);

    /// <summary>
    /// Exit Application
    /// </summary>
    public static ICommand ExitApplicationCommand => new ActionCommandHandler
        ((obj) => Application.Current.Shutdown());
}
