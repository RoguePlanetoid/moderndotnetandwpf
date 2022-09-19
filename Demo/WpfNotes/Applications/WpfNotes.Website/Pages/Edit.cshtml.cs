namespace WpfNotes.Website;

/// <summary>
/// Edit Model
/// </summary>
public class EditModel : PageModel
{
    private readonly INotesProvider _notes;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="notes">Notes</param>
    /// <param name="render">Render</param>
    public EditModel(INotesProvider notes) =>
        _notes = notes;

    /// <summary>
    /// Note Id
    /// </summary>
    [BindProperty(SupportsGet = true)]
    public int NoteId { get; set; } = new();

    /// <summary>
    /// Note
    /// </summary>
    [BindProperty]
    public NoteModel Note { get; set; } = new();

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="noteId">Note Id</param>
    /// <returns></returns>
    public async Task OnGetAsync(int noteId)
    {
        var note = await _notes.GetAsync(noteId);
        if(note != null)
            Note = note;
    }

    /// <summary>
    /// Edit
    /// </summary>
    public async Task OnPostEditAsync()
    {
        if (ModelState.IsValid)
            await _notes.EditAsync(NoteId, Note);
        Response.Redirect("/");
    }

    /// <summary>
    /// Delete
    /// </summary>
    public async Task OnPostDeleteAsync()
    {
        await _notes.DeleteAsync(NoteId);
        Response.Redirect("/");
    }
}
