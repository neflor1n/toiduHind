using toiduHind.DatabaseModels;

namespace toiduHind.MainToiduFol.BasketPage;

public partial class BasketPage : ContentPage
{
    public BasketPage(User user)
    {
        InitializeComponent();
        LoadBasketItems();
    }

    private async void LoadBasketItems()
    {
        var items = await App.Database.GetAllBasketItemsAsync();
        BasketCollection.ItemsSource = items;

        UpdateTotalSum(items); 
    }


    private async void OnIncreaseQuantityClicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.CommandParameter is BasketItem item)
        {
            item.Quantity++;
            await App.Database.UpdateBasketItemAsync(item);
            LoadBasketItems();
        }
    }


    private async void OnDecreaseQuantityClicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.CommandParameter is BasketItem item)
        {
            if (item.Quantity > 1)
            {
                item.Quantity--;
                await App.Database.UpdateBasketItemAsync(item);
            }
            else
            {
                await App.Database.DeleteBasketItemAsync(item.Id);
            }

            LoadBasketItems();
        }
    }

    private void UpdateTotalSum(List<BasketItem> items)
    {
        double total = items.Sum(item => item.Price * item.Quantity);
        TotalLabel.Text = $"Kokku: {total:0.00} €";
    }


    private async void OnClearBasketClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Kinnita", "Kas soovid eemaldada kõik tooted ostukorvist?", "Jah", "Ei");
        if (confirm)
        {
            await App.Database.ClearBasketAsync();
            LoadBasketItems();
        }
    }


}
