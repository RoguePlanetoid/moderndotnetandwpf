namespace WpfNotes.Desktop.Pages;

/// <summary>
/// Pages
/// </summary>
internal static class Pages
{
    private const string kestrel_config = "Kestrel";

    /// <summary>
    /// Add Web Host
    /// </summary>
    /// <param name="builder">Web Host Builder</param>
    /// <returns>Web Host Builder</returns>
    private static IWebHostBuilder AddWebHost(this IWebHostBuilder builder) =>
        builder.UseKestrel((context, options) =>
            context.Configuration.GetSection(kestrel_config))
        .UseStartup<Startup>();

    /// <summary>
    /// Add Pages
    /// </summary>
    /// <param name="builder">Host Builder</param>
    /// <returns>Host Builder</returns>
    public static IHostBuilder AddPages(this IHostBuilder builder) =>
        builder.ConfigureWebHostDefaults(builder => builder.AddWebHost());
}
