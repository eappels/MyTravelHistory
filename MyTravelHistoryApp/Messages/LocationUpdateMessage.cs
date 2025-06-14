using CommunityToolkit.Mvvm.Messaging.Messages;
using MyTravelHistoryApp.Models;

namespace MyTravelHistoryApp.Messages;

public class LocationUpdateMessage : ValueChangedMessage<CustomLocation>
{
    public LocationUpdateMessage(CustomLocation location)
        : base(location)
    {
    }
}