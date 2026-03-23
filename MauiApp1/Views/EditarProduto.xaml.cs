using System;
using MauiApp1.Models;
using Microsoft.Maui.Controls;

namespace MauiApp1.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    private  async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto proudto_anexado = BindingContext as Produto ?? new Produto();

            // Tentar localizar controles nomeados (se existirem na XAML)
            var txtDescricao = this.FindByName<Entry>("txt_descricao");
            var txtQuantidade = this.FindByName<Entry>("txt_quantidade");
            var txtPreco = this.FindByName<Entry>("txt_preco");

            // Usar texto dos controles se disponíveis, caso contrário usar valores do BindingContext
            string descricao = txtDescricao?.Text ?? proudto_anexado.Descricao;
            string quantidadeText = txtQuantidade?.Text ?? proudto_anexado.Quantidade.ToString();
            string precoText = txtPreco?.Text ?? proudto_anexado.Preco.ToString();

            // Parsing seguro
            double quantidade;
            if (!double.TryParse(quantidadeText, out quantidade))
            {
                quantidade = proudto_anexado.Quantidade;
            }

            double preco;
            if (!double.TryParse(precoText, out preco))
            {
                preco = proudto_anexado.Preco;
            }

            Produto p = new Produto
            {
                Id = proudto_anexado.Id,
                Descricao = descricao,
                Quantidade = quantidade,
                Preco = preco
            };

            await App.Db.Update(p);
            await DisplayAlertAsync("Sucesso", "Registro atualizado", "OK");
            await Navigation.PopAsync();//regressar a tela de origem
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops", ex.Message, "OK");
        }
    }
}