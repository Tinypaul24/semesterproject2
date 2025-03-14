using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace HeatProductionSystem.Converters;

public class BoolToOnOffConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
{
    return value is true ? "ON" : "OFF";
}

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}