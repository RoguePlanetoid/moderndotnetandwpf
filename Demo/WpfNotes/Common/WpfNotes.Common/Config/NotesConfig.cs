namespace WpfNotes.Common.Config;

/// <summary>
/// Notes Config
/// </summary>
public class NotesConfig : INotesConfig
{
    private const string connection_string = "Filename=notes.db";

    /// <summary>
    /// Connection String
    /// </summary>
    public string ConnectionString { get; set; } = connection_string;
}