using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HeatProductionSystem.ViewModels;


public partial class ProductionUnitsViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isOn;

    [ObservableProperty]
    private string? _name;
    
    // Remove ToggleCommand and Toggle method
}