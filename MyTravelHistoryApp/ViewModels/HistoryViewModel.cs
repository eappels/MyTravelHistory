using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using MyTravelHistoryApp.Models;
using MyTravelHistoryApp.Services.Interfaces;

namespace MyTravelHistoryApp.ViewModels;

public partial class HistoryViewModel : ObservableObject
{

    public readonly IDBService dbService;

    public HistoryViewModel(IDBService dbService)
    {
        this.dbService = dbService;
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            CustomTrack track = await dbService.GetLastTrack();
            CurrentTrackIndex = track?.Id ?? 0;
            if (track != null && track.Locations != null && track.Locations.Count > 0)
            {
                Track = new Polyline
                {
                    StrokeColor = Colors.Blue,
                    StrokeWidth = 5
                };
                foreach (var location in track.Locations)
                {
                    Track.Geopath.Add(new Location(location.Latitude, location.Longitude));
                }
            }
        });
    }

    [RelayCommand]
    private async Task PreviousTrack()
    {
        Track.Geopath.Clear();
        CustomTrack track = await dbService.GetTrackByID(CurrentTrackIndex - 1);
        if (track != null && track.Locations != null && track.Locations.Count > 0)
        {
            foreach (var location in track.Locations)
            {
                Track.Geopath.Add(new Location(location.Latitude, location.Longitude));
            }
        }
    }

    [RelayCommand]
    private async Task NextTrack()
    {
        Track.Geopath.Clear();
        CustomTrack track = await dbService.GetTrackByID(CurrentTrackIndex + 1);
        if (track != null && track.Locations != null && track.Locations.Count > 0)
        {
            foreach (var location in track.Locations)
            {
                Track.Geopath.Add(new Location(location.Latitude, location.Longitude));
            }
        }
    }

    [ObservableProperty]
    private Polyline track;

    [ObservableProperty]
    private int currentTrackIndex;
}