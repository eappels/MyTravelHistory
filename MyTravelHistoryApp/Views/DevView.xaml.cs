using MyTravelHistoryApp.ViewModels;

namespace MyTravelHistoryApp.Views;

public partial class DevView : ContentPage
{
	public DevView(DevViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
    }
}