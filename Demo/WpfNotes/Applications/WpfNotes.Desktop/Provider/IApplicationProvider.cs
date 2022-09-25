namespace WpfNotes.Desktop.Provider;

/// <summary>
/// Application Provider
/// </summary>
public interface IApplicationProvider
{
    /// <summary>
    /// Confirm
    /// </summary>
    Func<DialogModel, Task<bool>> Confirm { get; set; }

    /// <summary>
    /// Upsert
    /// </summary>
    Func<DialogModel, Task<bool>> Upsert { get; set; }

    /// <summary>
    /// Content
    /// </summary>
    ContentModel Content { get; }
}
