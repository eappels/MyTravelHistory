using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.Maps;
using MyTravelHistoryApp.Models;
using MyTravelHistoryApp.Services.Interfaces;

namespace MyTravelHistoryApp.ViewModels;

public partial class MapViewModel : ObservableObject
{

    private readonly ILocationService locationService;
    private readonly IDBService dbService;
    private DateTime CurrentTrackStartTime;
    private DateTime CurrentTrackStopTime;

    public MapViewModel(ILocationService locationService, IDBService dbService)
    {
        StartStopButtonColor = "Green";
        this.locationService = locationService;
        this.locationService.OnLocationUpdate = OnLocationUpdate;
        track = new Polyline
        {
            StrokeColor = Colors.Blue,
            StrokeWidth = 5
        };
        this.dbService = dbService;
        StartStopButtonText = "Start";
    }

    private async void OnLocationUpdate(CustomLocation location)
    {
        WeakReferenceMessenger.Default.Send(new Location(location.Latitude, location.Longitude));
        Track.Geopath.Add(new Location(location.Latitude, location.Longitude));
        await dbService.Save(location);
    }

    [RelayCommand]
    public async void ToggleTracxking()
    {
        locationService.ToggleStartStopTracking();
        StartStopButtonText = StartStopButtonText == "Start" ? "Stop" : "Start";
        if (StartStopButtonText == "Start")
        {
            //display an alert to the user asking if we need to save the current track ?
            CurrentTrackStopTime = DateTime.Now;
            StartStopButtonColor = "Gray";
            var result = await Application.Current.MainPage.DisplayAlert("Save Track", "Do you want to save the current track?", "Yes", "No");
            if (result)
            {
                var track = new Track
                {
                    StartTime = CurrentTrackStartTime,
                    StopTime = CurrentTrackStopTime,
                    Locations = new List<CustomLocation>()
                };

                track.Locations = await dbService.GetTrackLocations(CurrentTrackStartTime, CurrentTrackStopTime);
                await dbService.SaveTrack(track);
                await dbService.ClearCustomLocationTable();
            }
            StartStopButtonColor = "Green";
        }
        if (StartStopButtonText == "Stop")
        {
            CurrentTrackStartTime = DateTime.Now;
            StartStopButtonColor = "Red";
        }
    }

    [RelayCommand]
    private void ShowHistory()
    {

    }

    [ObservableProperty]
    private Polyline track;

    [ObservableProperty]
    private string startStopButtonText;

    [ObservableProperty]
    private string startStopButtonColor;
}