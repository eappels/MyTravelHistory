using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using MyTravelHistoryApp.Helpers;
using MyTravelHistoryApp.Messages;
using MyTravelHistoryApp.ViewModels;

namespace MyTravelHistoryApp.Views;

public partial class MapView : ContentPage
{

    private double zoomLevel = 100;

	public MapView(MapViewModel viewModel)
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
            ((MapViewModel)BindingContext).Track = new Polyline()
            {
                StrokeColor = Colors.Blue,
                StrokeWidth = 5
            };
            if (MyMap != null)
            {
                MyMap.MapElements.Add(((MapViewModel)(BindingContext)).Track);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(51.16597303515816, 3.8475330605909166), Distance.FromMeters(zoomLevel)));
                MyMap.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "VisibleRegion")
                    {
                        zoomLevel = MyMap.VisibleRegion.Radius.Meters;
                    }
                };
            }

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