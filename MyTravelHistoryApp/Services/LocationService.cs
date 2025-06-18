using MyTravelHistoryApp.Models;
using MyTravelHistoryApp.Services.Interfaces;

namespace MyTravelHistoryApp.Services;

public partial class LocationService : ILocationService
{

    public Action<CustomLocation> OnLocationUpdate { get; set; }

    //public void StartTracking()
    //{
    //    StartTrackingInternal();
    //}

    //public void StopTracking()
    //{
    //    StopTrackingInternal();
    //}

    //partial void StartTrackingInternal();
    //partial void StopTrackingInternal();

    public void ToggleStartStopTracking()
    {
        ToggleStartStopTrackingInternal();
    }
    partial void ToggleStartStopTrackingInternal();
}