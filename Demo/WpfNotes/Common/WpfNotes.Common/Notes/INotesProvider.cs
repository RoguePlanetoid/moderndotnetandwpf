namespace WpfNotes.Common.Notes;

/// <summary>
/// Notes Provider
/// </summary>
public interface INotesProvider
{
    /// <summary>
    /// Create Storage
    /// </summary>
    /// <returns>True on Success, False if Not</returns>
    Task<bool> CreateAsync();

    /// <summary>
    /// Add
    /// </summary>
    /// <param name="note">Note</param>
    /// <returns>Id</returns>
    Task<int?> AddAsync(NoteModel note);

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>Note</returns>
    Task<NoteModel?> GetAsync(int id);

    /// <summary>
    /// List
    /// </summary>
    /// <returns>IEnumerable of Note</returns>
    Task<IEnumerable<NoteModel>> ListAsync();

    /// <summary>
    /// Edit
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="note">Note</param>
    /// <returns>True on Success, False if Not</returns>
    Task<bool> EditAsync(int id, NoteModel note);

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>True on Success, False if Not</returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Destroy Storage
    /// </summary>
    /// <returns>True on Success, False if Not</returns>
    Task<bool> DestroyAsync();

    /// <summary>
    /// Colours
    /// </summary>
    List<string> Colours { get; }
}
