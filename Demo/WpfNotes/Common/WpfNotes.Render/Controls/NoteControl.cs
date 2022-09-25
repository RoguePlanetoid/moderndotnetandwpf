namespace WpfNotes.Render.Controls;

/// <summary>
/// Note Control
/// </summary>
public class NoteControl : Grid
{
    private const string note_path = "M 0,233.33333 V 0 H 233.33333 466.66666 V 183.69805 367.3961 l -57.66667,49.60273 -57.66666,49.60274 -175.66667,0.0325 L 0,466.66666 Z";
    private const string corner_path = "m 351.70982,366.69855 114.94703,0.77358 -115.33333,99.12108 z";
    private const int path_size = 466;
    private const int note_size = 250;

    internal Grid _grid;

    /// <summary>
    /// String to Path
    /// </summary>
    /// <param name="value">String</param>
    /// <returns>Path</returns>
    private static Path StringToPath(string value) => (Path)XamlReader.Parse(
        $"<Path xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'><Path.Data>{value}</Path.Data></Path>");

    /// <summary>
    /// Layout
    /// </summary>
    private void Layout()
    {
        var path = StringToPath(note_path);
        path.Width = path_size;
        path.Height = path_size;
        path.SetBinding(Shape.FillProperty, new Binding()
        {
            Path = new PropertyPath(nameof(Fill)),
            Mode = BindingMode.TwoWay,
            Source = this
        });
        var corner = StringToPath(corner_path);
        corner.Width = path_size;
        corner.Height = path_size;
        corner.Fill = new SolidColorBrush(Color.FromArgb(80, 255, 255, 255));
        var canvas = new Canvas()
        {
            Height = path_size,
            Width = path_size
        };
        canvas.Children.Add(path);
        canvas.Children.Add(corner);
        var note = new Viewbox()
        {
            Height = note_size,
            Width = note_size,
            Child = canvas
        };
        note.SetValue(RowSpanProperty, 2);
        _grid.Children.Add(note);
        var rectangle = new Rectangle()
        {
            Fill = new SolidColorBrush(Color.FromArgb(40, 0, 0, 0)),
            Stretch = Stretch.UniformToFill,
            Width = note_size
        };
        rectangle.SetValue(RowProperty, 0);
        _grid.Children.Add(rectangle);
        var title = new TextBlock()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            FontFamily = new FontFamily("Segoe Script"),
            Margin = new Thickness(2),
            FontSize = 20
        };
        title.SetBinding(TextBlock.TextProperty, new Binding()
        {
            Path = new PropertyPath(nameof(Title)),
            Mode = BindingMode.TwoWay,
            Source = this
        });
        title.SetValue(RowProperty, 0);
        _grid.Children.Add(title);
        var content = new TextBlock()
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            TextWrapping = TextWrapping.WrapWithOverflow,
            FontFamily = new FontFamily("Segoe Script"),
            Margin = new Thickness(5),
            FontSize = 16
        };
        content.SetBinding(TextBlock.TextProperty, new Binding()
        {
            Path = new PropertyPath(nameof(Content)),
            Mode = BindingMode.TwoWay,
            Source = this
        });
        content.SetValue(RowProperty, 1);
        _grid.Children.Add(content);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public NoteControl()
    {
        _grid = new Grid()
        {
            Height = note_size,
            Width = note_size,
        };
        _grid.RowDefinitions.Add(new RowDefinition() 
        { 
            Height = new GridLength(0.15, GridUnitType.Star) 
        });
        _grid.RowDefinitions.Add(new RowDefinition()
        {
            Height = new GridLength(0.85, GridUnitType.Star)
        });
        Layout();
        Viewbox viewbox = new()
        {
            Child = _grid
        };
        Children.Add(viewbox);
    }

    /// <summary>
    /// Title Property
    /// </summary>
    public static readonly DependencyProperty TitleProperty =
    DependencyProperty.Register(nameof(Title), typeof(string),
    typeof(NoteControl), new PropertyMetadata(string.Empty));

    /// <summary>
    /// Title
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Content Property
    /// </summary>
    public static readonly DependencyProperty ContentProperty =
    DependencyProperty.Register(nameof(Content), typeof(string),
    typeof(NoteControl), new PropertyMetadata(string.Empty));

    /// <summary>
    /// Content
    /// </summary>
    public string Content
    {
        get => (string)GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    /// <summary>
    /// Fill Property
    /// </summary>
    public static readonly DependencyProperty FillProperty =
    DependencyProperty.Register(nameof(Fill), typeof(Brush),
    typeof(NoteControl), new PropertyMetadata(new SolidColorBrush(Colors.WhiteSmoke)));

    /// <summary>
    /// Fill
    /// </summary>
    public Brush Fill
    {
        get => (Brush)GetValue(FillProperty);
        set => SetValue(FillProperty, value);
    }
}
