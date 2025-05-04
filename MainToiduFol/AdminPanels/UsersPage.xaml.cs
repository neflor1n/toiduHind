using toiduHind.DatabaseModels;

namespace toiduHind.MainToiduFol.AdminPanels;

public partial class UsersPage : ContentPage
{
    public UsersPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadUsersAsync();
    }

    private async Task LoadUsersAsync()
    {
        var users = await App.Database.GetAllUsersAsync();
        UsersCollection.ItemsSource = users;
    }

    private async void OnAddUserClicked(object sender, EventArgs e)
    {
        string name = await DisplayPromptAsync("Lisa kasutaja", "Sisesta nimi:");
        string email = await DisplayPromptAsync("Lisa kasutaja", "Sisesta email:");
        string password = await DisplayPromptAsync("Lisa kasutaja", "Sisesta parool:");

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Ошибка", "Все поля обязательны для заполнения.", "OK");
            return;
        }

        try
        {
            var user = new User
            {
                Name = name,
                Email = email,
                Password = password,
                Role = "User"
            };

            await App.Database.AddUserAsync(user);
            await LoadUsersAsync();
            await DisplayAlert("Успех", "Пользователь добавлен.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка", ex.Message, "OK");
        }
    }



    private async void OnDeleteUserClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.CommandParameter is int userId)
        {
            bool confirm = await DisplayAlert("Удаление", "Вы уверены, что хотите удалить этого пользователя?", "Да", "Нет");
            if (confirm)
            {
                try
                {
                    await App.Database.DeleteUserByIdAsync(userId);
                    await LoadUsersAsync();
                    await DisplayAlert("Успех", "Пользователь удален.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ошибка", $"Не удалось удалить пользователя: {ex.Message}", "OK");
                }
            }
        }
    }
}