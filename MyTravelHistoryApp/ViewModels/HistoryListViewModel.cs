using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyTravelHistoryApp.Models;
using MyTravelHistoryApp.Services.Interfaces;
using System.Diagnostics;

namespace MyTravelHistoryApp.ViewModels;

public partial class HistoryListViewModel : ObservableObject
{

    private readonly IDBService dbService;

    public HistoryListViewModel(IDBService dbService)
    {
        this.dbService = dbService;
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            Tracks = await dbService.ListAllTracksAsync();
        });
    }

    public async Task<int> DeleteTrackAsync(CustomTrack track)
    {
        if (track == null) return 0;
        try
        {
            var result = await dbService.DeleteTrackAsync(track);
            if (result > 0)
            {
                Tracks.Remove(track);
            }
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting track: {ex.Message}");
            return 0;
        }
    }

    [ObservableProperty]
    private List<CustomTrack> tracks;
}