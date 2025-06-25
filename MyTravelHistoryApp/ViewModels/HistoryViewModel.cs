using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using MyTravelHistoryApp.Models;
using MyTravelHistoryApp.Services.Interfaces;
using System.Diagnostics;

namespace MyTravelHistoryApp.ViewModels;

public partial class HistoryViewModel : ObservableObject
{

    public readonly IDBService dbService;

    public HistoryViewModel(IDBService dbService)
    {
        this.dbService = dbService;
        //MainThread.BeginInvokeOnMainThread(async () =>
        //{
        //    var lastTrackid = -1;
        //    try
        //    {
        //         lastTrackid = await dbService.ReadLastTracksIdAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"Error reading last track ID: {ex.Message}");
        //        throw;
        //    }
            
        //    await LoadTrackById(lastTrackid);
        //});
    }

    private async Task LoadTrackById(int id)
    {
        //Track.Clear();
        //var customTrack = new CustomTrack();

        //try
        //{
        //    customTrack = await dbService.ReadTrackByIdAsync(id);
        //}
        //catch (Exception ex)
        //{
        //    Debug.WriteLine($"Error reading last track: {ex.Message}");
        //    throw new Exception();
        //}

        //if (customTrack != null)
        //{            
        //    Track = new Polyline
        //    {
        //        StrokeColor = Colors.Blue,
        //        StrokeWidth = 5
        //    };
        //    foreach (var location in customTrack.Locations)
        //    {
        //        Track.Geopath.Add(location);
        //    }
        //}
        await Task.CompletedTask;
    }

    [RelayCommand]
    private async Task PreviousTrack()
    {
        CurrentTrackIndex--;       
        await LoadTrackById(CurrentTrackIndex);
    }

    [RelayCommand]
    private async Task NextTrack()
    {
        CurrentTrackIndex++;
        await LoadTrackById(CurrentTrackIndex);
    }

    [ObservableProperty]
    private Polyline track;

    [ObservableProperty]
    private int currentTrackIndex;
}