using MyTravelHistoryApp.Models;

namespace MyTravelHistoryApp.Services.Interfaces;

public interface IDBService
{
    Task<int> SaveTrackAsync(CustomTrack track);
    Task<int> DeleteTrackAsync(CustomTrack track);
    Task<CustomTrack> ReadLastTracksAsync();
    Task<List<CustomTrack>> ListAllTracksAsync();
    Task ClearDatabase();
}
