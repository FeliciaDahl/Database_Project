using PresentationMaui.ViewModels;

namespace PresentationMaui.Pages;

public partial class ListPage : ContentPage
{
	public ListPage(ListViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}