using MyTravelHistoryApp.Models;

namespace MyTravelHistoryApp.Services.Interfaces;

public interface IDBService
{
    Task<int> Save(CustomLocation location);
    Task<int> SaveTrack(Track track);
    Task<List<CustomLocation>> GetTrackLocations(DateTime currentTrackStartTime, DateTime currentTrackStopTime);
    Task<int> ClearCustomLocationTable();
}