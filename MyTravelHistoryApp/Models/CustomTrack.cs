using SQLite;
using System.Text.Json;

namespace MyTravelHistoryApp.Models;

public class CustomTrack
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    // Not mapped directly
    [Ignore]
    public List<Location> Locations { get; set; } = new();

    // Backing property for SQLite
    public string LocationsJson
    {
        get => JsonSerializer.Serialize(Locations);
        set => Locations = string.IsNullOrEmpty(value) ? new List<Location>() : JsonSerializer.Deserialize<List<Location>>(value);
    }

    public CustomTrack() { }
    public CustomTrack(IList<Location> locations)
    {
        Locations = locations.ToList();
    }
}