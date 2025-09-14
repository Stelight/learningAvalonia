using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AvaloniaDemo.Converters;

public class BooleanToClassConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is bool boolValue && boolValue ? "true" : "false";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}