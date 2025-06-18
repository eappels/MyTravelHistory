using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.Maps;
using MyTravelHistoryApp.Messages;
using MyTravelHistoryApp.Models;
using MyTravelHistoryApp.Services;
using MyTravelHistoryApp.Services.Interfaces;

namespace MyTravelHistoryApp.ViewModels;

public partial class MapViewModel : ObservableObject
{

    private readonly ILocationService locationService;

    public MapViewModel(ILocationService locationService)
    {
        this.locationService = locationService;
        locationService.OnLocationUpdate = OnLocationUpdate;
        StartStopButtonText = "Start";
    }

    private void OnLocationUpdate(CustomLocation clocation)
    {
        Track.Geopath.Add(new Location(clocation.Latitude, clocation.Longitude));
        WeakReferenceMessenger.Default.Send(new LocationUpdateMessage(clocation));
    }

    [RelayCommand]
    public void StartStopRecording()
    {
        if (StartStopButtonText == "Start")
        {
            StartStopButtonText = "Stop";
        }
        else
        {
            StartStopButtonText = "Start";
        }
        OnPropertyChanged(nameof(StartStopButtonText));
    }

    [ObservableProperty]
    private Polyline track;

    [ObservableProperty]
    private string startStopButtonText;

    partial void OnStartStopButtonTextChanged(string value)
    {
        if (value == "Stop")
            locationService.StartTracking();
        else
            locationService.StopTracking();
    }
}