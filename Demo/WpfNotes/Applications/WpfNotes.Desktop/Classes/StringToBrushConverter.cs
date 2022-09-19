namespace WpfNotes.Desktop.Classes;

/// <summary>
/// String to Brush Converter
/// </summary>
internal class StringToBrushConverter : IValueConverter
{
    private const string white_smoke = "#F5F5F5";

    /// <summary>
    /// Get Value
    /// </summary>
    /// <param name="value">Value</param>
    /// <returns></returns>
    private static string GetValue(string? value) =>
        string.IsNullOrEmpty(value) ? white_smoke : value;

    /// <summary>
    /// Convert
    /// </summary>
    /// <param name="value">Color String</param>
    /// <param name="targetType">Target Type</param>
    /// <param name="parameter">Parameter</param>
    /// <param name="culture">Culture</param>
    /// <returns>Solid Color Brush</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        new SolidColorBrush((Color)ColorConverter.ConvertFromString(GetValue(value as string)));

    /// <summary>
    /// Convert Back
    /// </summary>
    /// <param name="value">Value</param>
    /// <param name="targetType">Target Type</param>
    /// <param name="parameter">Parameter</param>
    /// <param name="culture">Culture</param>
    /// <returns>Object</returns>
    /// <exception cref="NotImplementedException">Not Implemented</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
