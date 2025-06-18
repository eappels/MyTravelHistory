using CoreLocation;
using MyTravelHistoryApp.Models;

namespace MyTravelHistoryApp.Services;

public partial class LocationService
{

    public readonly CLLocationManager locationManager;
    private bool isTracking = false;

    public LocationService()
    {
        locationManager = new CLLocationManager();
        locationManager.PausesLocationUpdatesAutomatically = false;
        locationManager.DesiredAccuracy = CLLocation.AccuracyBestForNavigation;
        locationManager.AllowsBackgroundLocationUpdates = true;
        locationManager.ActivityType = CLActivityType.AutomotiveNavigation;
        locationManager.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) =>
            OnLocationUpdate?.Invoke(new CustomLocation(e.Locations.LastOrDefault().Coordinate.Latitude, e.Locations.LastOrDefault().Coordinate.Longitude));
    }

    partial void ToggleStartStopTrackingInternal()
    {
        if (!isTracking)
        {
            locationManager.StartUpdatingLocation();            
        }
        else
        {
            locationManager.StopUpdatingLocation();
        }
        isTracking = !isTracking;
    }
}