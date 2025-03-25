using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using HeatProductionSystem.ViewModels;


namespace HeatProductionSystem.Views;

public partial class FirstScenario : UserControl
{
    public FirstScenario()
    {
        InitializeComponent();
        DataContext = new FirstScenarioViewModel();
    }
}