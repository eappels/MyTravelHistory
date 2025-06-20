using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using MyTravelHistoryApp.Services.Interfaces;
using System.Diagnostics;

namespace MyTravelHistoryApp.ViewModels;

public partial class HistoryViewModel : ObservableObject
{

    public readonly IDBService dbService;
    private int CurrentTrackIndex = 0;

    public HistoryViewModel(IDBService dbService)
    {
        this.dbService = dbService;
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var customTrack = await dbService.ReadLastTracksAsync();
            CurrentTrackIndex = customTrack?.Id ?? 0;
            if (customTrack != null) {
                Track = new Polyline
                {
                    StrokeColor = Colors.Blue,
                    StrokeWidth = 5
                };
                foreach (var location in customTrack.Locations)
                {
                    Track.Geopath.Add(location);
                }
            }
        });
        Debug.WriteLine($"CurrentTrackIndex: {CurrentTrackIndex}");
    }

    [ObservableProperty]
    private Polyline track;

    [RelayCommand]
    private void PreviousTrack()
    {
        Track.Clear();
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var customTrack = await dbService.ReadPreviousTracksAsync(CurrentTrackIndex);
            if (customTrack != null)
            {
                CurrentTrackIndex = customTrack?.Id ?? 0;
                foreach (var location in customTrack.Locations)
                {
                    Track.Geopath.Add(location);
                }
            }
        });
        Debug.WriteLine($"CurrentTrackIndex: {CurrentTrackIndex}");
    }

    [RelayCommand]
    private void NextTrack()
    {
        Track.Clear();
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var customTrack = await dbService.ReadNextTracksAsync(CurrentTrackIndex);
            if (customTrack != null)
            {
                CurrentTrackIndex = customTrack?.Id ?? 0;
                foreach (var location in customTrack.Locations)
                {
                    Track.Geopath.Add(location);
                }
            }
        });
        Debug.WriteLine($"CurrentTrackIndex: {CurrentTrackIndex}");
    }
}