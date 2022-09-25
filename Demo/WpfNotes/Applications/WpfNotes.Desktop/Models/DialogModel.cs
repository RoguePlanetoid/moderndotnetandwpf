namespace WpfNotes.Desktop.Models;

/// <summary>
/// Dialog Content Base
/// </summary>
public class DialogModel
{
    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Note
    /// </summary>
    public NoteModel? Note { get; set; } = null;

    /// <summary>
    /// Primary Option
    /// </summary>
    public string PrimaryOption { get; set; } = string.Empty;

    /// <summary>
    /// Secondary Option
    /// </summary>
    public string SecondaryOption { get; set; } = string.Empty;
}
