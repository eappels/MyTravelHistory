using MyTravelHistoryApp.Helpers;
using MyTravelHistoryApp.Models;
using MyTravelHistoryApp.Services.Interfaces;
using SQLite;

namespace MyTravelHistoryApp.Services;

public class DBService : IDBService
{

    private SQLiteAsyncConnection database;
    private int CurrentIndex = 0;

    private async Task Init()
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
        CurrentIndex = await database.InsertAsync(track);
        return CurrentIndex;
    }

    public async Task<string> ExportDB()
    {
        await Init();
        await database.CloseAsync();
        return Constants.DatabasePath;
    }

    public async Task<CustomTrack> GetLastTrack()
    {
        await Init();
        return await database.Table<CustomTrack>()
            .OrderByDescending(t => t.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<CustomTrack> GetTrackByID(int id)
    {
        await Init();
        return await database.Table<CustomTrack>()
            .Where(t => t.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task ClearDatabase()
    {
        await database.DropTableAsync<CustomTrack>();
        await database.CreateTableAsync<CustomTrack>();
    }
}