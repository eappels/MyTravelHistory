using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.Maps;
using MyTravelHistoryApp.Messages;
using MyTravelHistoryApp.Models;
using MyTravelHistoryApp.Services.Interfaces;
using System.Diagnostics;

namespace MyTravelHistoryApp.ViewModels;

public partial class MapViewModel : ObservableObject
{

    private readonly ILocationService locationService;

    public MapViewModel(ILocationService locationService)
    {
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

    private void OnLocationUpdate(CustomLocation clocation)
    {
        Track.Geopath.Add(new Location(clocation.Latitude, clocation.Longitude));
        WeakReferenceMessenger.Default.Send(new LocationUpdateMessage(clocation));
    }

    [RelayCommand]
    public async void StartStopRecording()
    {
        StartStopButtonText = StartStopButtonText == "Start" ? "Stop" : "Start";
        if (StartStopButtonText == "Start")
        {
            locationService.StopTracking();
            var result = await Application.Current.MainPage.DisplayAlert("Save Track", "Do you want to save the current track?", "Yes", "No");
            if (result)
            {
                Debug.WriteLine($"result = {result}");
            }
            StartStopButtonColor = "Green";
        }
        else
        {
            locationService.StartTracking();
            StartStopButtonColor = "Red";
        }
    }

    [ObservableProperty]
    private Polyline track;

    [ObservableProperty]
    private string startStopButtonText;

    [ObservableProperty]
    private string startStopButtonColor;
}