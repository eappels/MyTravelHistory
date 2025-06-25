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

    public string ExportDB()
    {
        return Constants.DatabasePath;
    }

    public async Task ClearDatabase()
    {
        await database.DropTableAsync<CustomTrack>();
        await database.CreateTableAsync<CustomTrack>();
    }
}