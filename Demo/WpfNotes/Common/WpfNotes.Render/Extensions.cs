namespace WpfNotes.Render;

/// <summary>
/// Render
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Add Render
    /// </summary>
    /// <param name="services">Service Collection</param>
    /// <returns>Service Collection</returns>
    public static IServiceCollection AddRender(this IServiceCollection services) =>
        services.AddSingleton<IRenderProvider, RenderProvider>();
}
