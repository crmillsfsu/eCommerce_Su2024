using eCommerce.MAUI.ViewModels;

namespace eCommerce.MAUI.Views;

[QueryProperty(nameof(ProductId), "productId")]
public partial class ProductView : ContentPage
{
    public int ProductId { get; set; }
	public ProductView()
	{
		InitializeComponent();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//Inventory");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        (BindingContext as ProductViewModel).Add();
        Shell.Current.GoToAsync("//Inventory");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProductViewModel(ProductId);
    }
}