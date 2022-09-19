

namespace WpfNotes.Common.Notes;

/// <summary>
/// Notes Provider
/// </summary>
internal class NotesProvider : INotesProvider
{
    private const string background_red = "#ff4947";
    private const string background_orange = "#f57900";
    private const string background_yellow = "#ffc476";
    private const string background_green = "#6dc0a4";
    private const string background_blue = "#6f9acd";
    private const string background_indigo = "#833db8";
    private const string background_violet = "#c693c2";

    private const string field_id = "@Id";
    private const string field_title = "@Title";
    private const string field_content = "@Content";
    private const string field_background = "@Background";
    private const string table_create = "CREATE TABLE IF NOT EXISTS Notes (Id INTEGER PRIMARY KEY AUTOINCREMENT, Title NVARCHAR(50) NULL, Content NVARCHAR(255) NULL, Background NVARCHAR(10) NULL);";
    private const string table_destroy = "DROP TABLE IF EXISTS Notes;";
    private const string table_insert = "INSERT INTO Notes VALUES (NULL, @Title, @Content, @Background); SELECT last_insert_rowid();";
    private const string table_update = "UPDATE Notes SET Title = @Title, Content = @Content, Background = @Background WHERE Id = @Id;";
    private const string table_delete = "DELETE FROM Notes WHERE Id = @Id;";
    private const string table_list = "SELECT Id, Title, Content, Background FROM Notes;";
    private const string table_get = "SELECT Id, Title, Content, Background FROM Notes WHERE Id = @Id;";

    private readonly INotesConfig _config;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="config">Config</param>
    public NotesProvider(INotesConfig config) =>
        _config = config;

    /// <summary>
    /// Create Storage
    /// </summary>
    /// <returns>True on Success, False if Not</returns>
    public async Task<bool> CreateAsync()
    {
        using SqliteConnection connection = new(_config.ConnectionString);
        try
        {
            await connection.OpenAsync();
            SqliteCommand create = new()
            {
                Connection = connection,
                CommandText = table_create
            };
            await create.ExecuteNonQueryAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    /// <summary>
    /// Add
    /// </summary>
    /// <param name="note">Note</param>
    /// <returns>Id</returns>
    public async Task<int?> AddAsync(NoteModel note)
    {
        using SqliteConnection connection = new(_config.ConnectionString);
        try
        {
            await connection.OpenAsync();
            SqliteCommand insert = new()
            {
                Connection = connection,
                CommandText = table_insert
            };
            insert.Parameters.AddWithValue(field_title, note.Title);
            insert.Parameters.AddWithValue(field_content, note.Content);
            insert.Parameters.AddWithValue(field_background, note.Background);
            return Convert.ToInt32(await insert.ExecuteScalarAsync());
        }
        catch(Exception)
        {
            return null;
        }
        finally
        {
            connection.Close();
        }
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>Note</returns>
    public async Task<NoteModel?> GetAsync(int id)
    {
        using SqliteConnection connection = new(_config.ConnectionString);
        try
        {
            await connection.OpenAsync();
            SqliteCommand get = new()
            {
                Connection = connection,
                CommandText = table_get
            };
            get.Parameters.AddWithValue(field_id, id);
            SqliteDataReader query = await get.ExecuteReaderAsync();
            NoteModel? note = null;
            while(await query.ReadAsync())
            {
                note = new()
                {
                    Id = query.GetInt32(0),
                    Title = query.GetString(1),
                    Content = query.GetString(2),
                    Background = query.GetString(3)
                };
            }
            return note;
        }
        catch (Exception)
        {
            return null;
        }
        finally
        {
            connection.Close();
        }
    }

    /// <summary>
    /// List
    /// </summary>
    /// <returns>IEnumerable of Note</returns>
    public async Task<IEnumerable<NoteModel>> ListAsync()
    {
        using SqliteConnection connection = new(_config.ConnectionString);
        try
        {
            await connection.OpenAsync();
            SqliteCommand list = new()
            {
                Connection = connection,
                CommandText = table_list
            };
            SqliteDataReader query = await list.ExecuteReaderAsync();
            List<NoteModel> results = new();
            while (await query.ReadAsync())
            {
                results.Add(new()
                {
                    Id = query.GetInt32(0),
                    Title = query.GetString(1),
                    Content = query.GetString(2),
                    Background = query.GetString(3)
                });
            }
            return results;
        }
        catch (Exception)
        {
            return Enumerable.Empty<NoteModel>();
        }
        finally
        {
            connection.Close();
        }
    }

    /// <summary>
    /// Edit
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="note">Note</param>
    /// <returns>True on Success, False if Not</returns>
    public async Task<bool> EditAsync(int id, NoteModel note)
    {
        using SqliteConnection connection = new(_config.ConnectionString);
        try
        {
            await connection.OpenAsync();
            SqliteCommand update = new()
            {
                Connection = connection,
                CommandText = table_update
            };
            update.Parameters.AddWithValue(field_id, id);
            update.Parameters.AddWithValue(field_title, note.Title);
            update.Parameters.AddWithValue(field_content, note.Content);
            update.Parameters.AddWithValue(field_background, note.Background);
            await update.ExecuteScalarAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>True on Success, False if Not</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        using SqliteConnection connection = new(_config.ConnectionString);
        try
        {
            await connection.OpenAsync();
            SqliteCommand delete = new()
            {
                Connection = connection,
                CommandText = table_delete
            };
            delete.Parameters.AddWithValue(field_id, id);
            await delete.ExecuteScalarAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    /// <summary>
    /// Destroy Storage
    /// </summary>
    /// <returns>True on Success, False if Not</returns>
    public async Task<bool> DestroyAsync()
    {
        using SqliteConnection connection = new(_config.ConnectionString);
        try
        {
            await connection.OpenAsync();
            SqliteCommand create = new()
            {
                Connection = connection,
                CommandText = table_destroy
            };
            await create.ExecuteNonQueryAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    /// <summary>
    /// Colours
    /// </summary>
    public List<string> Colours => new()
    {
        background_red,
        background_orange,
        background_yellow,
        background_green,
        background_blue,
        background_indigo,
        background_violet
    };
}