namespace eCommerce.MAUI.Views;

public partial class InventoryView : ContentPage
{
	public InventoryView()
	{
		InitializeComponent();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }
}