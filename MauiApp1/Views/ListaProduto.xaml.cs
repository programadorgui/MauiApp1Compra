using MauiApp1.Models;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;

public partial class ListaProduto : ContentPage
{
	ObservableCollection<Produto> lista; 

	public ListaProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{

			await Navigation.PushAsync(new Views.NovoProduto());

		}catch(Exception ex)
		{
		 await DisplayAlertAsync("Ops", ex.Message, "OK");
        }
    }
}