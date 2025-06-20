using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
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
            var customTrack = await dbService.ReadLastTracksAsync();
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
    }

    [ObservableProperty]
    private Polyline track;

    [RelayCommand]
    private async void ClearDB()
    {
        dbService.ClearDatabase();
    }
}