using MyTravelHistoryApp.Models;

namespace MyTravelHistoryApp.Services.Interfaces;

public interface IDBService
{
    Task<int> SaveTrackAsync(CustomTrack track);
    Task<CustomTrack> GetLastTrack();
    Task<CustomTrack> GetTrackByID(int id);
    Task<string> ExportDB();
    Task ClearDatabase();
}