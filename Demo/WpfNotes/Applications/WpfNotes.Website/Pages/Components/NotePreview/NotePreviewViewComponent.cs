namespace WpfNotes.Website;

/// <summary>
/// Note Preview View Component
/// </summary>
[ViewComponent]
public class NotePreviewViewComponent : ViewComponent
{
    /// <summary>
    /// Invoke Component
    /// </summary>
    /// <param name="background">Background</param>
    /// <returns>View Component Result</returns>
    public IViewComponentResult Invoke(string background) => 
        View("Default", new NotePreviewViewModel(background));
}