using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using toiduHind.DatabaseModels;

namespace toiduHind.MainToiduFol.MapPage;

public partial class MapPage : ContentPage
{
    private List<Store> _allStores;

    public MapPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await App.Database.ImportStoresFromCsvAsync();
        _allStores = await App.Database.GetAllStoresAsync();


        if (_allStores == null || !_allStores.Any(s => s.Latitude != 0 && s.Longitude != 0))
        {
            await DisplayAlert("Teade", "Ühtegi poodi ei leitud kaardile lisamiseks.", "OK");
            return;
        }

        // Отображаем сразу все магазины
        UpdateMapPins();
    }

    private void UpdateMapPins()
    {
        MainMap.Pins.Clear();

        var storesWithCoords = _allStores
            .Where(s => s.Latitude != 0 && s.Longitude != 0)
            .ToList();

        foreach (var store in storesWithCoords)
        {
            var pin = new Pin
            {
                Label = store.Name,
                Location = new Location(store.Latitude, store.Longitude),
                Type = PinType.Place
            };

            MainMap.Pins.Add(pin);
        }

        // Центрируем карту на Таллинне
        var tallinnCenter = new Location(59.4370, 24.7535);
        MainMap.MoveToRegion(MapSpan.FromCenterAndRadius(tallinnCenter, Distance.FromKilometers(10)));
    }
}
