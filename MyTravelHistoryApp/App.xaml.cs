using MyTravelHistoryApp.Views;

namespace MyTravelHistoryApp
{
    public partial class App : Application
    {

        private readonly MapView mapView;

        public App(MapView mapView)
        {
            this.mapView = mapView;
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(mapView));
        }
    }
}