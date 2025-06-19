using MyTravelHistoryApp.Models;

namespace MyTravelHistoryApp.Services.Interfaces;

public interface IDBService
{
    Task<int> Save(CustomLocation location);
}