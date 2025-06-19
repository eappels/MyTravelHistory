using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MyTravelHistoryApp.Messages;

public class LocationUpdateMessage : ValueChangedMessage<Location>
{
    public LocationUpdateMessage(Location location)
        : base(location)
    {
    }
}