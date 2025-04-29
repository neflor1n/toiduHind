using toiduHind.MainToiduFol.AdminPanels;

namespace toiduHind.MainToiduFol.AdminPanels;

public partial class AdminTabbedPage : TabbedPage
{
    public AdminTabbedPage()
    {
        InitializeComponent();

        this.Children.Add(new StoresPage());
        // this.Children.Add(new ProductsPage()); // если хочешь
    }
}
