namespace WpfNotes.Common.Bindings;

/// <summary>
/// Action Command
/// </summary>
public class ActionCommand : ActionCommandObservableBase
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="action">Action</param>
    public ActionCommand(Action<object?> action) :
        base(new ActionCommandHandler((param) =>
        action(param), (param) =>
        (param as ActionCommand)?.IsEnabled ?? true))
    { }
}
