using MyTravelHistoryApp.Models;

namespace MyTravelHistoryApp.Services.Interfaces;

public interface ILocationService
{
    Action<CustomLocation> OnLocationUpdate { get; set; }
    //void StartTracking();
    //void StopTracking();

    void ToggleStartStopTracking();
}