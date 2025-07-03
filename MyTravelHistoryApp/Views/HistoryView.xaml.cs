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
            var viewModel = (HistoryViewModel)BindingContext;

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