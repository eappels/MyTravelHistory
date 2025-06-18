using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Maps;
using MyTravelHistoryApp.Helpers;
using MyTravelHistoryApp.ViewModels;

namespace MyTravelHistoryApp.Views;

public partial class MapView : ContentPage
{

	public MapView(MapViewModel viewModel)
	{
        InitializeComponent();

        BindingContext = viewModel;

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var result = await AppPermissions.CheckAndRequestRequiredPermissionAsync();
            if (result == PermissionStatus.Granted)
            {
                MyMap.MapElements.Add(viewModel.Track);
            }
        });

        WeakReferenceMessenger.Default.Register<Location>(this, (r, m) =>
        {
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(m.Latitude, m.Longitude), Distance.FromMeters(250)));
        });
    }
}