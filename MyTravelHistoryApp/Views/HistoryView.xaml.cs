using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using MyTravelHistoryApp.Helpers;
using MyTravelHistoryApp.Messages;
using MyTravelHistoryApp.ViewModels;

namespace MyTravelHistoryApp.Views;

public partial class HistoryView : ContentPage
{

    private double zoomLevel = 100;

    public HistoryView(HistoryViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var result = await AppPermissions.CheckAndRequestRequiredPermissionAsync();
        if (result == PermissionStatus.Granted)
        {
            // Get the ViewModel and its track
            var viewModel = (HistoryViewModel)BindingContext;

            // Example: Load the last track from the database
            var lastTrack = await viewModel.dbService.ReadLastTracksAsync();
            if (lastTrack != null && lastTrack.Locations != null && lastTrack.Locations.Count > 0)
            {
                var polyline = new Polyline
                {
                    StrokeColor = Colors.Blue,
                    StrokeWidth = 5
                };

                foreach (var coord in lastTrack.Locations)
                {
                    polyline.Geopath.Add(new Location(coord.Latitude, coord.Longitude));
                }

                viewModel.Track = polyline;
                if (MyMap != null)
                {
                    MyMap.MapElements.Clear();
                    MyMap.MapElements.Add(polyline);

                    // Calculate bounding region for the track
                    var minLat = lastTrack.Locations.Min(c => c.Latitude);
                    var maxLat = lastTrack.Locations.Max(c => c.Latitude);
                    var minLon = lastTrack.Locations.Min(c => c.Longitude);
                    var maxLon = lastTrack.Locations.Max(c => c.Longitude);

                    var centerLat = (minLat + maxLat) / 2;
                    var centerLon = (minLon + maxLon) / 2;
                    var distance = Location.CalculateDistance(minLat, minLon, maxLat, maxLon, DistanceUnits.Kilometers);

                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                        new Location(centerLat, centerLon),
                        Distance.FromKilometers(Math.Max(distance, 0.5)))); // Ensure a minimum zoom
                }
            }

            MyMap.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "VisibleRegion")
                {
                    zoomLevel = MyMap.VisibleRegion.Radius.Meters;
                }
            };

            WeakReferenceMessenger.Default.Register<LocationUpdateMessage>(this, (r, m) =>
            {
                if (MyMap != null && m.Value != null)
                {
                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(m.Value, Distance.FromMeters(zoomLevel)));
                }
            });
        }
        else
        {
            await DisplayAlert("Permission Denied", "This app requires location permissions to function properly.", "OK");
        }
    }
}