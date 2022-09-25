namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Add Notes
    /// </summary>
    /// <param name="services">Service Collection</param>
    /// <returns>Service Collection</returns>
    public static IServiceCollection AddNotes(this IServiceCollection services, IConfigurationRoot configuration) =>
        services.AddSingleton<INotesConfig>(configuration.GetSection(nameof(NotesConfig)).Get<NotesConfig>() ?? new())
        .AddSingleton<INotesProvider, NotesProvider>();
}
