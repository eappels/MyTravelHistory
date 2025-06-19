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
    private readonly IDBService dbService;
    private DateTime StartTrackingTime, StopTrackingTime;

    public MapViewModel(ILocationService locationService, IDBService dbService)
    {
        StartStopButtonColor = "Green";
        this.locationService = locationService;        
        locationService.OnLocationUpdate = OnLocationUpdate;
        this.dbService = dbService;
        StartStopButtonText = "Start";
        Track = new Polyline
        {
            StrokeColor = Colors.Blue,
            StrokeWidth = 5
        };
    }

    private async void OnLocationUpdate(CustomLocation location)
    {
        Track.Geopath.Add(new Location(location.Latitude, location.Longitude));
        WeakReferenceMessenger.Default.Send(new LocationUpdateMessage(location));
        await dbService.Save(location);
    }

    [RelayCommand]
    public async void StartStopRecording()
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
            StopTrackingTime = DateTime.Now;
            var result = await Application.Current.MainPage.DisplayAlert("Save Track", "Do you want to save the current track?", "Yes", "No");
            if (result)
            {
                if (result == true)
                {

                }
            }
            StartStopButtonColor = "Green";
        }
    }

    [ObservableProperty]
    private Polyline track;

    [ObservableProperty]
    private string startStopButtonText;

    [ObservableProperty]
    private string startStopButtonColor;
}