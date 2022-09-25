namespace WpfNotes.Desktop.Classes;

/// <summary>
/// Dialog
/// </summary>
internal class Dialog
{
    private const string upsert_template = "UpsertTemplate";
    private const string delete_template = "DeleteTemplate";
    private ContentDialog? _dialog = null;

    /// <summary>
    /// Confirm
    /// </summary>
    /// <param name="title">Title</param>
    /// <param name="content">Content</param>
    /// <param name="primaryButtonText">Primary Button Text</param>
    /// <param name="secondaryButtonText">Secondary Button Text</param>
    /// <returns>True if Primary Button Selected False if Not</returns>
    private async Task<bool> ConfirmAsync(string title, object content,
        string primaryButtonText = "Ok", string secondaryButtonText = "")
    {
        try
        {
            if (_dialog != null)
                _dialog.Hide();
            _dialog = new ContentDialog()
            {
                Title = title,
                Content = content,
                PrimaryButtonText = primaryButtonText,
                SecondaryButtonText = secondaryButtonText
            };
            return await _dialog.ShowAsync() == ContentDialogResult.Primary;
        }
        catch (TaskCanceledException)
        {
            return false;
        }
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="content">Dialog Content</param>
    /// <returns>True if Primary Button Selected False if Not</returns>
    public async Task<bool> DeleteAsync(DialogModel content) =>
        await ConfirmAsync(content.Title, new ContentControl()
        {
            Content = content.Note,
            ContentTemplate = Application.Current.Resources[delete_template] as DataTemplate
        }, content.PrimaryOption, content.SecondaryOption);

    /// <summary>
    /// Upsert
    /// </summary>
    /// <param name="content">Dialog Content</param>
    /// <returns>Note if Primary Button Selected, Null if Not</returns>
    public async Task<bool> UpsertAsync(DialogModel content) =>
       await ConfirmAsync(content.Title, new ContentControl()
       {
           Content = content.Note,
           ContentTemplate = Application.Current.Resources[upsert_template] as DataTemplate
       }, content.PrimaryOption, content.SecondaryOption);
}