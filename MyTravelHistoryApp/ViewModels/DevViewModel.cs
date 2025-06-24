using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyTravelHistoryApp.Services.Interfaces;
using System.Diagnostics;

namespace MyTravelHistoryApp.ViewModels;

public partial class DevViewModel : ObservableObject
{

    private readonly IDBService dbService;

    public DevViewModel(IDBService dbService)
    {
        this.dbService = dbService;
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var pausesLocationUpdatesAutomatically = await SecureStorage.Default.GetAsync("PausesLocationUpdatesAutomatically");
            PausesLocationUpdatesAutomaticallyIsToggled = pausesLocationUpdatesAutomatically == "True";
        });
    }

    [RelayCommand]
    private async void ResetDatabase()
    {
        await dbService.ClearDatabase();
    }

    [ObservableProperty]    
    private bool pausesLocationUpdatesAutomaticallyIsToggled;

    partial void OnPausesLocationUpdatesAutomaticallyIsToggledChanged(bool value)
    {
        Debug.WriteLine($"PausesLocationUpdatesAutomatically is set to {value}.");
    }
}