using MyTravelHistoryApp.Models;

namespace MyTravelHistoryApp.Services.Interfaces;

public interface IDBService
{
    Task<int> SaveTrackAsync(CustomTrack track);
    Task<int> DeleteTrackAsync(CustomTrack track);
    Task<CustomTrack> ReadLastTracksAsync();
    Task<List<CustomTrack>> ListAllTracksAsync();
    Task<CustomTrack> ReadPreviousTracksAsync(int currentTrackIndex);
    Task<CustomTrack> ReadNextTracksAsync(int currentTrackIndex);
    Task ClearDatabase();
}
