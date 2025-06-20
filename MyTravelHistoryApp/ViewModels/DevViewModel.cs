using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyTravelHistoryApp.Services.Interfaces;

namespace MyTravelHistoryApp.ViewModels;

public partial class DevViewModel : ObservableObject
{

    private readonly IDBService dbService;

    public DevViewModel(IDBService dbService)
    {
        this.dbService = dbService;
    }

    [RelayCommand]
    private async void ResetDatabase()
    {
        await dbService.ClearDatabase();
    }
}