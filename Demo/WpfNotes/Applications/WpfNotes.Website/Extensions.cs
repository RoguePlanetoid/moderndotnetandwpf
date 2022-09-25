namespace Microsoft.Extensions.Configuration;

/// <summary>
/// Extensions
/// </summary>
internal static class Extensions
{
    private const bool reload = true;
    private const bool optional = true;
    private const string app_settings = "appsettings.json";

    /// <summary>
    /// Add Config
    /// </summary>
    /// <param name="builder">Configuration Builder</param>
    /// <returns>Configuration Builder</returns>
    private static IConfigurationBuilder AddConfig(this IConfigurationBuilder builder) =>
        builder.AddJsonFile(app_settings, optional, reload);

    /// <summary>
    /// Add Services
    /// </summary>
    /// <param name="services">Services</param>
    /// <param name="configuration">Configuration</param>
    /// <returns>Service Collection</returns>
    private static IServiceCollection AddServices(this IServiceCollection services, IConfigurationRoot configuration) =>
        services.AddNotes(configuration)
        .AddRender();

    /// <summary>
    /// Add Services
    /// </summary>
    /// <param name="services">Service Collection</param>
    /// <returns>Service Collection</returns>
    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services
        .AddServices(new ConfigurationBuilder()
        .AddConfig()
        .Build());
}
