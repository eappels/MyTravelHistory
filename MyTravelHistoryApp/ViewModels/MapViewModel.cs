using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.Maps;
using MyTravelHistoryApp.Messages;
using MyTravelHistoryApp.Models;
using MyTravelHistoryApp.Services.Interfaces;
using MyTravelHistoryApp.Views;

namespace MyTravelHistoryApp.ViewModels;

public partial class MapViewModel : ObservableObject
{

    private readonly ILocationService locationService;
    private readonly IDBService dbService;
    private DateTime StartTrackingTime, StopTrackingTime;

    public MapViewModel(ILocationService locationService, IDBService dbService)
    {
        StartStopButtonEnabed = true;
        StartStopButtonColor = "Green";
        StartStopButtonText = "Start";
        this.locationService = locationService;        
        locationService.OnLocationUpdate = OnLocationUpdate;
        this.dbService = dbService;        
        Track = new Polyline
        {
            StrokeColor = Colors.Blue,
            StrokeWidth = 5
        };
    }

    private void OnLocationUpdate(Location location)
    {
        Track.Geopath.Add(location);
        WeakReferenceMessenger.Default.Send(new LocationUpdateMessage(location));
    }

    [RelayCommand]
    public async Task StartStopRecordingAsync()
    {
        StartStopButtonText = StartStopButtonText == "Start" ? "Stop" : "Start";
        if (StartStopButtonText == "Stop")
        {
            Track.Geopath.Clear();
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
                var track = new CustomTrack(Track.Geopath);
                await dbService.SaveTrackAsync(track);
                result = await App.Current.Windows[0].Page.DisplayAlert("Track saved", "Do you want to display the saved track?", "Yes", "No");
                if (result == true)
                {
                    await Shell.Current.GoToAsync("///HistoryView");
                }
                else
                {
                    Track.Geopath.Clear();
                    StartStopButtonColor = "Green";
                    StartStopButtonEnabed = true;
                }                
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