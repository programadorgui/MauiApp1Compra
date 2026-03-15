using MauiApp1.Models;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;

public partial class ListaProduto : ContentPage
{
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>(); 

	public ListaProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = lista;
    }

	protected  async override void OnAppearing()
	{
		List<Produto> tmp = await App.Db.Getall();

		tmp.ForEach( i  => lista.Add(i));

	}//mecanismo paralelo de um construtor

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

    private async void Txt_seach_TextChanged(object sender, TextChangedEventArgs e)
    {
		string q = e.NewTextValue;

		List<Produto> tmp = await App.Db.Search(q);

		lista.Clear();

        tmp.ForEach(i => lista.Add(i));
    }

    private  void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {

		double soma = lista.Sum(i => i.Total);

		string msg = $"O total é {soma:C}";

		DisplayAlertAsync ("Total dos produtos ", msg, "OK");
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {

    }
}