using System;
using System.IO;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
using Microsoft.VisualBasic;

namespace DataBaseContext;

public class SourceDataManagerContext : DataContext {
    
    public Table<WinterData>? WinterTimeData;
    public Table<SummerData>? SummerTimeData;
    
    public SourceDataManagerContext(string connectionString) : base(connectionString) {}
}

[Table(Name = "WinterTimeData")]
public class WinterData{

    [Column]
    public string? DataFromTime {get; set;}
    [Column]
    public string? DataToTime {get; set;}
    [Column]
    public double HeatDemand {get; set;} //MWh
    [Column]
    public double EletricityPrice {get; set;} // DKK/MWh
}

[Table(Name = "SummerTimeData")]
public class SummerData{

    [Column]
    public string? DataFromTime {get; set;}
    [Column]
    public string? DataToTime {get; set;}
    [Column]
    public double HeatDemand {get; set;} //MWh
    [Column]
    public double EletricityPrice {get; set;} // DKK/MWh
}


