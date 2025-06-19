using MyTravelHistoryApp.Helpers;
using MyTravelHistoryApp.Models;
using MyTravelHistoryApp.Services.Interfaces;
using SQLite;

namespace MyTravelHistoryApp.Services;

public class DBService : IDBService
{

    private SQLiteAsyncConnection database;

    private async Task Init()
    {
        if (database is not null)
            return;

        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await database.CreateTableAsync<CustomLocation>();
    }

    public async Task<int> Save(CustomLocation location)
    {
        await Init();
        if (location == null)
            throw new ArgumentNullException(nameof(location));
        return await database.InsertAsync(location);
    }
}