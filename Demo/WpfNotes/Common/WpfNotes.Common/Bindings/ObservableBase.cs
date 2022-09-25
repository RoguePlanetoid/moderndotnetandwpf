namespace WpfNotes.Common.Bindings;

/// <summary>
/// Observable
/// </summary>
public abstract class ObservableBase : INotifyPropertyChanged
{
    /// <summary>
    /// Property Changed Event Handler
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Set Property
    /// </summary>
    /// <typeparam name="TItem">Item Type</typeparam>
    /// <param name="field">Field</param>
    /// <param name="value">Value</param>
    /// <param name="propertyName">Property Name</param>
    /// <returns>True if Property was Changed, False if not</returns>
    protected bool SetProperty<TItem>(ref TItem field, TItem value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<TItem>.Default.Equals(field, value))
            return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    /// <summary>
    /// On Property Changed
    /// </summary>
    /// <param name="propertyName">Property Name</param>
    public void OnPropertyChanged(string? propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}