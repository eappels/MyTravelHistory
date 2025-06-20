using MyTravelHistoryApp.Models;
using MyTravelHistoryApp.ViewModels;

namespace MyTravelHistoryApp.Views;

public partial class HistoryListView : ContentPage
{
	public HistoryListView(HistoryListViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
    }

    private void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is CustomTrack track)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await ((HistoryListViewModel)BindingContext).DeleteTrackAsync(track);
            });
        }
        ((ListView)sender).SelectedItem = null;
    }
}