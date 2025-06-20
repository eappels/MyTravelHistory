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
        var result = await database.CreateTableAsync<CustomTrack>();
    }

    public async Task<int> SaveTrackAsync(CustomTrack track)
    {
        await Init();
        if (track == null || track.Locations == null || track.Locations.Count == 0)
            return 0;
        return await database.InsertAsync(track);
    }

    public async Task<CustomTrack> ReadLastTracksAsync()
    {
        await Init();
        return await database.Table<CustomTrack>().OrderByDescending(t => t.Id).FirstOrDefaultAsync();
    }

    public async Task<List<CustomTrack>> ListAllTracksAsync()
    {
        await Init();
        return await database.Table<CustomTrack>().ToListAsync();
    }

    public async Task<int> DeleteTrackAsync(CustomTrack track)
    {
        await Init();
        if (track == null)
            return 0;
        return await database.DeleteAsync(track);
    }
}