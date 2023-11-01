using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Media.Transformation;
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
        private int count = 0;

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            count++;

            Debug.WriteLine(count);
            if (value is Button button)
            {
                double multiplier = 1;

                if (parameter is string x)
                    double.TryParse(x, out multiplier);

                double translateX = -button.Bounds.Width * multiplier;

                var transform = TransformOperations.CreateBuilder(1);
                transform.AppendTranslate(translateX, 0);

                return transform.Build();
            } else if (value is double width)
            {

                double multiplier = 1;

                if (parameter is string x)
                    double.TryParse(x, out multiplier);

                double translateX = -width * multiplier;

                var transform = TransformOperations.CreateBuilder(1);
                transform.AppendTranslate(translateX, 0);

                return transform.Build();
            }
            
            
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
