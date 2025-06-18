namespace MyTravelHistoryApp.Models;

public class Track
{
    public int Id { get; set; }
    public string TrackName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime StopTime { get; set; }
    public List<CustomLocation> Locations { get; set; }
}