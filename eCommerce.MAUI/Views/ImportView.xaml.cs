using eCommerce.MAUI.ViewModels;
using System.IO;
namespace eCommerce.MAUI.Views;

public partial class ImportView : ContentPage
{
	public ImportView()
	{
		InitializeComponent();
        BindingContext = new ImportViewModel();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//Inventory");
    }

    private void ImportClicked(object sender, EventArgs e)
    {
        (BindingContext as ImportViewModel).ImportFile();

    }

    private async void FileBrowseClicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Select File to Import",
            FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>> { { DevicePlatform.WinUI, new[] { ".csv" } } })
        });

        if (result == null)
            return;

        var stream = await result.OpenReadAsync();

        (BindingContext as ImportViewModel).ImportFile(stream);
    }
}