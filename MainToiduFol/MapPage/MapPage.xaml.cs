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

        _allStores = await App.Database.GetAllStoresAsync();

        var storeNames = _allStores
            .Where(s => s.Latitude != 0 && s.Longitude != 0)
            .Select(s => s.Name)
            .Distinct()
            .OrderBy(n => n)
            .ToList();


        storeNames.Insert(0, "Kõik poed");


        UpdateMapPins(storeNames[0]);
    }

    private void UpdateMapPins(string storeName)
    {
        MainMap.Pins.Clear();

        var filteredStores = storeName == "Kõik poed"
            ? _allStores.Where(s => s.Latitude != 0 && s.Longitude != 0).ToList()
            : _allStores.Where(s => s.Name == storeName && s.Latitude != 0 && s.Longitude != 0).ToList();

        foreach (var store in filteredStores)
        {
            var pin = new Pin
            {
                Label = store.Name,
                Location = new Location(store.Latitude, store.Longitude),
                Type = PinType.Place
            };

            MainMap.Pins.Add(pin);
        }

        // Центрируем на Таллинн
        var tallinnCenter = new Location(59.4370, 24.7535);
        MainMap.MoveToRegion(MapSpan.FromCenterAndRadius(tallinnCenter, Distance.FromKilometers(10)));
    }

}
