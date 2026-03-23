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
		try
		{
			List<Produto> tmp = await App.Db.Getall();

			tmp.ForEach(i => lista.Add(i));
		}
		catch (Exception ex)
		{
			await DisplayAlertAsync("Ops", ex.Message, "OK");
		}

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
		try
		{
			string q = e.NewTextValue;

			List<Produto> tmp = await App.Db.Search(q);

			lista.Clear();

			tmp.ForEach(i => lista.Add(i));
			}catch(Exception ex)
		{
			await DisplayAlertAsync("Ops", ex.Message, "OK");
        }
    }

    private  void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {

		double soma = lista.Sum(i => i.Total);

		string msg = $"O total é {soma:C}";

		DisplayAlertAsync ("Total dos produtos ", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			MenuItem selecionado = sender as MenuItem;

			Produto p = selecionado.BindingContext as Produto;// item do objeto que era que era o item em questoa conseguindo resgatar

			bool confirm = await DisplayAlertAsync("Confirma?", $"Remover Produto{p.Descricao}?", "Sim", "Não");

			if (confirm)
			{
				await App.Db.Delete(p.Id);
				lista.Remove(p);
            }
        }
		catch(Exception ex)
		{
			 await DisplayAlertAsync("Ops",ex.Message, "OK");
        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		try{

			Produto p = e.SelectedItem as Produto;


			Navigation.PushAsync( new Views.EditarProduto
			{
					BindingContext = p,

            });

        }
        catch(Exception ex)
		{
			DisplayAlertAsync("Ops", ex.Message, "OK");
        }
    }
}