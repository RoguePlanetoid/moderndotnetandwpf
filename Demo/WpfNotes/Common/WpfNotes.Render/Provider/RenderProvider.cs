namespace WpfNotes.Render.Provider;

/// <summary>
/// Render Provider
/// </summary>
internal class RenderProvider : IRenderProvider
{
    private const int dpi = 96;
    private const int path_size = 466;
    private const int note_size = 250;

    /// <summary>
    /// Get Viewbox for Scaled Content
    /// </summary>
    /// <param name="content">Note Model</param>
    /// <returns>Viewbox</returns>
    private static Viewbox GetViewbox(NoteModel content)
    {
        var note = new NoteControl()
        {
            Width = path_size,
            Height = path_size,
            Title = content.Title,
            Content = content.Content,
            Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(content.Background))
        };
        var viewbox = new Viewbox()
        {
            Height = note_size,
            Width = note_size,
            Child = note
        };
        // Set Layout
        var size = new Size(viewbox.Width, viewbox.Height);
        viewbox.Measure(size);
        viewbox.Arrange(new Rect(size));
        return viewbox;
    }

    /// <summary>
    /// Get Image Bytes
    /// </summary>
    /// <param name="model">Note Model</param>
    /// <returns>Byte Array</returns>
    private static byte[] GetImageBytes(NoteModel model)
    {
        byte[]? buffer = null;
        var viewbox = GetViewbox(model);
        var bounds = VisualTreeHelper.GetDescendantBounds(viewbox);
        var bitmap = new RenderTargetBitmap(
            (int)(bounds.Width * dpi / dpi),
            (int)(bounds.Height * dpi / dpi),
            dpi, dpi, PixelFormats.Pbgra32);
        var visual = new DrawingVisual();
        using (var context = visual.RenderOpen())
        {
            var brush = new VisualBrush(viewbox);
            context.DrawRectangle(brush, null, new Rect(new Point(), bounds.Size));
        }
        bitmap.Render(visual);
        using (var stream = new MemoryStream())
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(stream);
            buffer = stream.ToArray();
        }
        return buffer;
    }

    /// <summary>
    /// Render
    /// </summary>
    /// <param name="model">Note Model</param>
    /// <returns>Image Bytes</returns>
    public byte[]? Render(NoteModel model)
    {
        byte[]? result = null;
        var thread = new Thread(() => result = GetImageBytes(model))
        {
            IsBackground = true
        };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join();
        return result;
    }
}
