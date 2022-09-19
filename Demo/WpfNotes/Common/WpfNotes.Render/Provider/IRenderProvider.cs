namespace WpfNotes.Render.Provider;

/// <summary>
/// Render Provider
/// </summary>
public interface IRenderProvider
{
    /// <summary>
    /// Render
    /// </summary>
    /// <param name="model">Note Model</param>
    /// <returns>Image Bytes</returns>
    byte[]? Render(NoteModel model);
}
