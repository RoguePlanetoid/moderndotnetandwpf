namespace WpfNotes.Common.Bindings;

/// <summary>
/// Action Command Observable
/// </summary>
public abstract class ActionCommandObservableBase : ObservableBase
{
    private bool _isEnabled;

    /// <summary>
    /// Command
    /// </summary>
    public ICommand Command { get; }

    /// <summary>
    /// Handler
    /// </summary>
    public ActionCommandHandler Handler { get; }

    /// <summary>
    /// Enabled
    /// </summary>
    public bool IsEnabled
    {
        get => _isEnabled;
        set
        {
            SetProperty(ref _isEnabled, value);
            Handler?.UpdateCanExecute();
        }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="handler">Action Command Handler</param>
    /// <param name="isEnabled">Is Enabled?</param>
    public ActionCommandObservableBase(ActionCommandHandler handler, bool isEnabled = false) =>
        (Command, IsEnabled) = (Handler = handler, isEnabled);
}