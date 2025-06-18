using MyTravelHistoryApp.Helpers;
using MyTravelHistoryApp.Models;
using MyTravelHistoryApp.Services.Interfaces;
using SQLite;

namespace MyTravelHistoryApp.Services;

public class DBService : IDBService
{

    private SQLiteAsyncConnection database;

    async Task Init()
    {
        if (database is not null)
            return;

        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await database.CreateTableAsync<CustomLocation>();
        result = await database.CreateTableAsync<Track>();
    }

    public async Task<int> Save(CustomLocation location)
    {
        await Init();
        return await database.InsertAsync(location);
    }

    public async Task<int> SaveTrack(Track track)
    {
        await Init();
        return await database.InsertAsync(track);
    }

    public async Task<List<CustomLocation>> GetTrackLocations(DateTime currentTrackStartTime, DateTime currentTrackStopTime)
    {
        await Init();
        return await database.Table<CustomLocation>()
            .Where(x => x.Timestamp >= currentTrackStartTime && x.Timestamp <= currentTrackStopTime)
            .ToListAsync();
    }

    public async Task<int> ClearCustomLocationTable()
    {
        return await database.DeleteAllAsync<CustomLocation>();
    }
}