using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.Maps;
using MyTravelHistoryApp.Messages;
using MyTravelHistoryApp.Models;
using MyTravelHistoryApp.Services.Interfaces;

namespace MyTravelHistoryApp.ViewModels;

public partial class MapViewModel : ObservableObject
{

    private readonly ILocationService locationService;
    private DateTime StartTrackingTime, StopTrackingTime;

    public MapViewModel(ILocationService locationService)
    {
        StartStopButtonEnabed = true;
        StartStopButtonColor = "Green";
        this.locationService = locationService;        
        locationService.OnLocationUpdate = OnLocationUpdate;

        StartStopButtonText = "Start";
        Track = new Polyline
        {
            StrokeColor = Colors.Blue,
            StrokeWidth = 5
        };
    }

    private void OnLocationUpdate(CustomLocation location)
    {
        Track.Geopath.Add(new Location(location.Latitude, location.Longitude));
        WeakReferenceMessenger.Default.Send(new LocationUpdateMessage(location));
    }

    [RelayCommand]
    public async Task StartStopRecordingAsync()
    {
        StartStopButtonText = StartStopButtonText == "Start" ? "Stop" : "Start";
        if (StartStopButtonText == "Stop")
        {
            locationService.StartTracking();
            StartTrackingTime = DateTime.Now;
            StartStopButtonColor = "Red";
        }
        else
        {
            locationService.StopTracking();
            StartStopButtonEnabed = false;
            StopTrackingTime = DateTime.Now;
            var result = await Application.Current.MainPage.DisplayAlert("Save Track", "Do you want to save the current track?", "Yes", "No");
            if (result == true)
            {

            }
            else
            {
                Track.Geopath.Clear();
            }
            StartStopButtonColor = "Green";
            StartStopButtonEnabed = true;
        }
    }

    [ObservableProperty]
    private Polyline track;

    [ObservableProperty]
    private string startStopButtonText;

    [ObservableProperty]
    private string startStopButtonColor;

    [ObservableProperty]
    private bool startStopButtonEnabed;
}