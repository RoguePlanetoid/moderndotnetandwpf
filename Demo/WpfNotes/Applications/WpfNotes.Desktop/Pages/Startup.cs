namespace WpfNotes.Desktop.Pages;

/// <summary>
/// Startup
/// </summary>
internal class Startup
{
    private const string notes_id = "/notes/{id}";
    private const string notes = "/notes";

    /// <summary>
    /// Add Endpoints
    /// </summary>
    /// <param name="builder">Endpoint Route Builder</param>
    /// <returns>Endpoint Route Builder</returns>
    private static IEndpointRouteBuilder AddEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapGet(notes, async (INotesProvider provider) =>
            await provider.ListAsync());
        builder.MapPost(notes, async (NoteModel note, INotesProvider provider) =>
            await provider.AddAsync(note) is int result ?
                Results.Created($"{notes}/{result}", result) : Results.NotFound());
        builder.MapGet(notes_id, async (int id, INotesProvider provider) =>
            await provider.GetAsync(id) is NoteModel note ? Results.Ok(note) : Results.NotFound());
        builder.MapPut(notes_id, async (int id, NoteModel note, INotesProvider provider) =>
            await provider.EditAsync(id, note) ? Results.NoContent() : Results.NotFound());
        builder.MapDelete(notes_id, async (int id, INotesProvider provider) =>
            await provider.DeleteAsync(id) ? Results.NoContent() : Results.NotFound());
        return builder;
    }

    /// <summary>
    /// Configure Services
    /// </summary>
    /// <param name="services">Service Collection</param>
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddSwaggerGen().AddEndpointsApiExplorer();
        services.AddWpfBlazorWebView();
        services.AddRazorPages();
    }

    /// <summary>
    /// Configure Application
    /// </summary>
    /// <param name="app">App Builder</param>
    public static void Configure(IApplicationBuilder app) =>
        app.UseRouting()
        .UseSwagger()
        .UseSwaggerUI()
        .UseEndpoints(routes =>
            AddEndpoints(routes)
            .MapRazorPages());
}
