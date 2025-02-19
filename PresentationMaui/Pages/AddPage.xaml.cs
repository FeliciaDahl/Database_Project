using PresentationMaui.ViewModels;

namespace PresentationMaui.Pages;

public partial class AddPage : ContentPage
{
	public AddPage(AddViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

}
