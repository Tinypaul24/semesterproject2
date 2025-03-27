using Avalonia;
using System;
using System.Collections.Generic;

namespace HeatProductionSystem
{
    public class HeatingArea // This class represents a heating grid, such as a district heating network
    {
        public string Arhitecture { get; set; }
        public int Size { get; set; }
        public string City { get; set; }
        public string Image { get; set; }
    }

    public class ProductionUnit // This class represents a production unit, such as a boiler or a wind turbine
    {
        public string Name { get; set; }
        public double ProducedHeat { get; set; }
        public double MaxHeat { get; set; }
        public double GasConsumption { get; set; }
        public double ProducedElectricity { get; set; }
        public double ConsumedElectricity { get; set; }
        public double PrimaryEnergyConsumption { get; set; }
        public double ProductionCosts { get; set; }
        public double ProducedCO2Emissions { get; set; }

        public int MaintenanceDate { get; set; }

    }

    public class AssetManager // This class manages heating grids and production units
    {
        public List<HeatingArea> HeatingAreas { get; set; }
        public List<ProductionUnit> ProductionUnits { get; set; }

        public AssetManager(List<HeatingArea> heatingAreas, List<ProductionUnit> productionUnits)
        {
            HeatingAreas = heatingAreas ?? new List<HeatingArea>();
            ProductionUnits = productionUnits ?? new List<ProductionUnit>();
        }

        public void DisplayHeatingAreas()
        {
            Console.WriteLine("Heating Areas:");
            foreach (var area in HeatingAreas)
            {
                Console.WriteLine($"- {area.Arhitecture} ({area.Image})");
            }
        }

        public void DisplayProductionUnits()
        {
            Console.WriteLine("Production Units:");
            foreach (var unit in ProductionUnits)
            {
                Console.WriteLine($"- {unit.Name} ({unit.Image})");
            }
        }
    }

    class AssetManagerProgram
    {
        static void ProductionUnits(string[] args)
        {
            List<HeatingArea> heatingAreas = new List<HeatingArea>
            {
                new HeatingArea { Arhitecture = "Single District Heating Network", Size = 1600, City = "Heatington", Image = "HeatingtonArea.png" },
            };

            List<ProductionUnit> productionUnits = new List<ProductionUnit>
            {
                new ProductionUnit
                {
                    Name = "Gas Boiler 1",
                    ProducedHeat = 100,
                    ProducedElectricity = 50,
                    ConsumedElectricity = 20,
                    PrimaryEnergyConsumption = 150,
                    ProductionCosts = 200,
                    ProducedCO2Emissions = 300
                    MaintenanceDate = 7
                },
                new ProductionUnit
                {
                    Name = "Unit2",
                    ProducedHeat = 200,
                    ProducedElectricity = 100,
                    ConsumedElectricity = 40,
                    PrimaryEnergyConsumption = 300,
                    ProductionCosts = 400,
                    ProducedCO2Emissions = 600
                    MaintenanceDate = 7
                }
            };
        }
    }
}