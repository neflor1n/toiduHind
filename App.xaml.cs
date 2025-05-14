using System.Globalization;
using toiduHind.DatabaseModels;

namespace toiduHind
{
    public partial class App : Application
    {

        public static Database Database { get; private set; }
        public static User CurrentUser { get; set; }

        public App()
        {
            var culture = new CultureInfo("et-EE"); 
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;


            InitializeComponent();

            Database = new Database();

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new AvaLeht()));
        }
    }
}