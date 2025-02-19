using PresentationMaui.ViewModels;

namespace PresentationMaui.Pages;

public partial class EditPage : ContentPage
{
	public EditPage(EditViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}