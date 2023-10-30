using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzPasswordManager.Converters
{
    public class TranslateButtonConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            Debug.WriteLine(value);

            TranslateTransform transform = new TranslateTransform()
            {
                X = 0,
                Y = 0
            };

            if (value is Button button)
            {
                double multiplier = 1;
                if (parameter is double x)
                    multiplier = x;

                double translateX = button.Bounds.Width * multiplier;

                transform.X = translateX;
            }

            return transform;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
