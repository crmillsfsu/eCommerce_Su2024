using Amazon.Library.Models;
using Amazon.Library.Services;

namespace eCommerce.MAUI.Views;

public partial class CartView : ContentPage
{
	public CartView()
	{
		InitializeComponent();
	}

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
		BindingContext = new ShoppingCart();
    }

    private void OkClicked(object sender, EventArgs e)
    {
        ShoppingCartServiceProxy.Current.AddCart(BindingContext as ShoppingCart);
        Shell.Current.GoToAsync("//Shop");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Shop");
    }
}