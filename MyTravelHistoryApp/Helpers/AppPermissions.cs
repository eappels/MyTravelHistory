namespace MyTravelHistoryApp.Helpers;

internal partial class AppPermissions
{
    internal partial class AppPermission : Permissions.LocationWhenInUse
    {
    }

    public static async Task<PermissionStatus> CheckRequiredPermissionAsync() => await Permissions.CheckStatusAsync<AppPermission>();

    public static async Task<PermissionStatus> CheckAndRequestRequiredPermissionAsync()
    {
        PermissionStatus status = await Permissions.CheckStatusAsync<AppPermission>();

        if (status == PermissionStatus.Granted)
            return status;

        if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            await App.Current.MainPage.DisplayAlert("Required App Permissions", "Please enable all permissions in Settings for this App, it is useless without them.", "Ok");

        if (Permissions.ShouldShowRationale<AppPermission>())
            await App.Current.MainPage.DisplayAlert("Required App Permissions", "This is a location based app, without these permissions it is useless.", "Ok");

        status = await MainThread.InvokeOnMainThreadAsync(Permissions.RequestAsync<AppPermission>);
        return status;
    }

}