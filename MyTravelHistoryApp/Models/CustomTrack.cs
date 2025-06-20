using SQLite;
using System.Text.Json;

namespace MyTravelHistoryApp.Models;

public class CustomTrack
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public TimeSpan Duration => EndTime - StartTime;

    [Ignore]
    public List<Location> Locations { get; set; } = new();

    public CustomTrack() { }

    public string LocationsJson
    {
        get => JsonSerializer.Serialize(Locations);
        set => Locations = string.IsNullOrEmpty(value) ? new List<Location>() : JsonSerializer.Deserialize<List<Location>>(value);
    }

    public CustomTrack(IList<Location> locations)
    {
        Locations = locations.ToList();
        StartTime = locations.FirstOrDefault().Timestamp;
        EndTime = locations.LastOrDefault().Timestamp;
    }
}