namespace WpfNotes.Desktop.Provider;

/// <summary>
/// Application Provider
/// </summary>
internal class ApplicationProvider : IApplicationProvider
{
    private const string delete_note_title = "Delete Note?";
    private const string edit_note_title = "Edit Note";
    private const string new_note_title = "New Note";
    private const string cancel = "Cancel";
    private const string save = "Save";
    private const string add = "Add";
    private const string yes = "Yes";
    private const string no = "No";

    private readonly INotesProvider _notes;

    /// <summary>
    /// Get Confirm
    /// </summary>
    /// <param name="title"></param>
    /// <param name="note">note</param>
    /// <returns>Dialog Model</returns>
    private static DialogModel GetConfirm(string title, NoteModel note) => new()
    {
        Title = title,
        Note = note,
        PrimaryOption = yes,
        SecondaryOption = no
    };

    /// <summary>
    /// Get Upsert
    /// </summary>
    /// <param name="title"></param>
    /// <param name="note">Note</param>
    /// <returns></returns>
    private static DialogModel GetUpsert(string title) => new()
    {
        Title = title,
        Note = null,
        PrimaryOption = save,
        SecondaryOption = cancel
    };

    /// <summary>
    /// Is Valid
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="results">Results</param>
    /// <returns>True if Valid, False if Not</returns>
    private bool IsValid(NoteModel model, List<ValidationResult> results) =>
        Validator.TryValidateObject(model, new ValidationContext(model), results, true);

    /// <summary>
    /// List
    /// </summary>
    private async void List()
    {
        var notes = await _notes.ListAsync();
        Content.Notes = new ObservableCollection<NoteModel>(notes);
    }

    /// <summary>
    /// New
    /// </summary>
    private async Task NewAsync()
    {
        Content.Note = new();
        var result = await Upsert(GetUpsert(new_note_title));
        if (result && IsValid(Content.Note, new()) && await _notes.CreateAsync())
        {
            var id = await _notes.AddAsync(Content.Note);
            if (id != null)
            {
                Content.Note.Id = id;
                Content.Notes.Add(Content.Note);
            }
        }
    }

    /// <summary>
    /// Edit
    /// </summary>
    /// <param name="model">Note Model</param>
    private async Task EditAsync(NoteModel? model)
    {
        if (model != null)
        {
            Content.Note = model;
            var title = Content.Note.Title;
            var content = Content.Note.Content;
            var background = Content.Note.Background;
            var result = await Upsert(GetUpsert(edit_note_title));
            if (!(result && IsValid(Content.Note, new()) && 
                Content.Note.Id != null &&
                await _notes.EditAsync(Content.Note.Id.Value, Content.Note)))
                (Content.Note.Title, Content.Note.Content, Content.Note.Background) =
                    (title, content, background);
        }
    }

    /// <summary>
    /// Delete Note
    /// </summary>
    /// <param name="note">Note</param>
    private async Task<bool> DeleteAsync(NoteModel? note) => 
        note?.Id != null &&
        await Confirm(GetConfirm(delete_note_title, note)) &&
        await _notes.DeleteAsync(note.Id.Value) && 
        Content.Notes.Remove(note);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="notes">Notes</param>
    public ApplicationProvider(INotesProvider notes)
    {
        _notes = notes;
        Content = new ContentModel(
            (p) => List(),
            async (p) => await NewAsync(),
            async (p) => await EditAsync(p as NoteModel),
            async (p) => await DeleteAsync(p as NoteModel)
        );
        List();
    }

    /// <summary>
    /// Confirm
    /// </summary>
    public Func<DialogModel, Task<bool>> Confirm { get; set; } =
        (confirm) => Task.FromResult(false);

    /// <summary>
    /// Upsert
    /// </summary>
    public Func<DialogModel, Task<bool>> Upsert { get; set; } =
        (confirm) => Task.FromResult(false);

    /// <summary>
    /// List
    /// </summary>
    /// <returns>List of Notes</returns>
    public async Task ListAsync()
    {
        Content.Notes.Clear();
        foreach(var note in await _notes.ListAsync())
            Content.Notes.Add(note);
    }

    /// <summary>
    /// Content
    /// </summary>
    public ContentModel Content { get; private set; }
}

