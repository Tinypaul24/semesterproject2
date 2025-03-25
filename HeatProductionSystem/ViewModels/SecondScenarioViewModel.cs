using System;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace HeatProductionSystem.ViewModels;

public partial class SecondScenarioViewModel : ViewModelBase
{
    public ObservableCollection<ProductionUnitsViewModel> Devices { get; } = new()
    {
        new ProductionUnitsViewModel { Name = "GM1", IsOn = false },
        new ProductionUnitsViewModel { Name = "HP1", IsOn = false },
        new ProductionUnitsViewModel { Name = "GB1", IsOn = false },
        new ProductionUnitsViewModel { Name = "OB1", IsOn = false }
    };
}
