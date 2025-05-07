using toiduHind.DatabaseModels;

namespace toiduHind.MainToiduFol.AdminPanels;

public partial class UsersPage : ContentPage
{
    private Database _database;

    public UsersPage()
    {
        InitializeComponent();
        _database = new Database();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadUsersAsync();
    }

    private async Task LoadUsersAsync()
    {
        var users = await _database.GetAllUsersAsync();
        UsersCollection.ItemsSource = users;
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
                    await _database.DeleteUserByIdAsync(userId);
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

    private async void OnAddUserClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddUserPage());
    }

    private async void OnUserTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is int userId)
        {
            try
            {
                // Get the user details
                var user = await _database.GetUserByIdAsync(userId);
                if (user != null)
                {
                    // Show user details
                    string details = $"ID: {user.Id}\n" +
                                    $"Name: {user.Name}\n" +
                                    $"Email: {user.Email}\n" +
                                    $"Role: {user.Role}\n";

                    await DisplayAlert($"User Details: {user.Name}", details, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load user details: {ex.Message}", "OK");
            }
        }
    }

    private async void OnEditUserClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.CommandParameter is int userId)
        {
            await Navigation.PushAsync(new EditUserPage(userId));
        }
    }

}
