namespace WpfNotes.Common.Bindings;

/// <summary>
/// Command Handler
/// </summary>
public class ActionCommandHandler : ICommand
{
    private readonly Action<object?>? _action;
    private readonly Func<object?, bool>? _canExecute;

    /// <summary>
    /// Can Execute Changed
    /// </summary>
    public event EventHandler? CanExecuteChanged;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="action">Action</param>
    /// <param name="canExecute">Can Execute</param>
    public ActionCommandHandler(Action<object?>? action, Func<object?, bool> canExecute) =>
        (_action, _canExecute) = (action, canExecute);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="action">Action</param>
    public ActionCommandHandler(Action<object?> action) =>
        _action = action;

    /// <summary>
    /// Can Execute
    /// </summary>
    /// <param name="parameter">Parameter</param>
    /// <returns>True</returns>
    public bool CanExecute(object? parameter) =>
        CanExecute == null || parameter == null || _canExecute == null || _canExecute.Invoke(parameter);

    /// <summary>
    /// Execute
    /// </summary>
    /// <param name="parameter">Parameter</param>
    public void Execute(object? parameter) =>
        _action?.Invoke(parameter);

    /// <summary>
    /// Update Can Execute
    /// </summary>
    public void UpdateCanExecute() =>
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
