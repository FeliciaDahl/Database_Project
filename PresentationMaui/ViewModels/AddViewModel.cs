using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace PresentationMaui.ViewModels;

public partial class AddViewModel : ObservableObject 
{

  
    [RelayCommand]
    private async Task NavigateToList()
    {
        await Shell.Current.GoToAsync("ListPage");
    }
}
