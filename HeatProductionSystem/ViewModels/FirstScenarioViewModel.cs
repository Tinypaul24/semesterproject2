using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace HeatProductionSystem.ViewModels;

public class FirstScenarioViewModel : ViewModelBase
{
    public ObservableCollection<ProductionUnitsViewModel> Devices { get; } = new()
    {
        new ProductionUnitsViewModel { Name = "GB1", IsOn = false },
        new ProductionUnitsViewModel { Name = "GB2", IsOn = false },
        new ProductionUnitsViewModel { Name = "OB1", IsOn = false }
    };
}
