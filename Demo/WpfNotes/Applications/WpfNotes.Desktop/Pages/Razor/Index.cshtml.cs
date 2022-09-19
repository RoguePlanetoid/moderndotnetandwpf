namespace WpfNotes.Desktop.Pages;

/// <summary>
/// Index Model
/// </summary>
public class IndexModel : PageModel
{
    private const string content_type = "image/png";
    private const string file_extension = ".png";

    private readonly INotesProvider _notes;
    private readonly IRenderProvider _render;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="notes">Notes</param>
    /// <param name="render">Render</param>
    public IndexModel(INotesProvider notes, IRenderProvider render) =>
        (_notes, _render) = (notes, render);

    /// <summary>
    /// Note
    /// </summary>
    [BindProperty]
    public NoteModel Note { get; set; } = new();

    /// <summary>
    /// Notes
    /// </summary>
    public IEnumerable<NoteModel> Notes { get; set; } = Enumerable.Empty<NoteModel>();

    /// <summary>
    /// Get
    /// </summary>
    /// <returns></returns>
    public async Task OnGetAsync() =>
        Notes = await _notes.ListAsync();

    /// <summary>
    /// Note Image
    /// </summary>
    /// <param name="noteId">Note Id</param>
    /// <returns>Note Preview</returns>
    public async Task<IActionResult> OnGetNoteImageAsync(int noteId)
    {
        var model = await _notes.GetAsync(noteId);
        if (model != null)
        {
            var render = _render.Render(model);
            if (render != null)
                return File(render, content_type, $"{model.Title}{file_extension}");
        }
        return NotFound();
    }

    /// <summary>
    /// Submit
    /// </summary>
    public async Task OnPostAsync()
    {
        if(ModelState.IsValid)
        {
            await _notes.CreateAsync();
            await _notes.AddAsync(Note);
        }
        Notes = await _notes.ListAsync();
    }
}
