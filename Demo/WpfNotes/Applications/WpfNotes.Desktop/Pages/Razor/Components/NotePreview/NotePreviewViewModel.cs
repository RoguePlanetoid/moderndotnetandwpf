namespace WpfNotes.Desktop;

/// <summary>
/// Note Preview View Model
/// </summary>
public class NotePreviewViewModel
{
    /// <summary>
    /// Constructor
    /// </summary>
    public NotePreviewViewModel() { }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="background">Background</param>
    public NotePreviewViewModel(string background) =>
        Background = background;

    /// <summary>
    /// Background
    /// </summary>
    public string Background { get; set; } = "#F5F5F5";

    /// <summary>
    /// Height
    /// </summary>
    public int Height { get; set; } = 32;

    /// <summary>
    /// Width
    /// </summary>
    public int Width { get; set; } = 32;
}
