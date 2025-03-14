using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeatProductionSystem.Views;
using Avalonia.Controls;

namespace HeatProductionSystem.ViewModels;



public partial class MainWindowViewModel : ViewModelBase
{
     [ObservableProperty]
    private UserControl currentView;


    private FirstScenario _firstView = new FirstScenario{DataContext=new FirstScenarioViewModel()};
    private SecondScenario _secondView = new SecondScenario{DataContext=new SecondScenarioViewModel()};
  

    public MainWindowViewModel() 
    {
        CurrentView = _firstView;
    }

    [RelayCommand]
    public void NavigateToFirstView()
    {
        CurrentView = _firstView;
    }

    [RelayCommand]
    public void NavigateToSecondView()
    {
        CurrentView = _secondView;
    }
}
