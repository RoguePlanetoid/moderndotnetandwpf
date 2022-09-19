namespace WpfNotes.Desktop;

/// <summary>
/// Note Input View Component
/// </summary>
[ViewComponent]
public class NoteInputViewComponent : ViewComponent
{
    /// <summary>
    /// Invoke Component
    /// </summary>
    /// <param name="note">Note Model</param>
    /// <returns>View Component Result</returns>
    public IViewComponentResult Invoke(NoteModel note) => 
        View("Default", note);
}