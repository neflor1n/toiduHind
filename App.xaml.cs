using toiduHind.DatabaseModels;

namespace toiduHind
{
    public partial class App : Application
    {

        public static Database Database { get; private set; }

        public App()
        {
            InitializeComponent();

            Database = new Database();

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new AvaLeht()));
        }
    }
}