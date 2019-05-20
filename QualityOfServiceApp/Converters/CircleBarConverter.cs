using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace QualityOfServiceApp.Converters
{
    public class CircleBarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double rating = 0;
            if(double.TryParse(value.ToString(), out rating))
            {
                rating = rating * 20;
                if (rating >= 100) return 99;
            }
            return rating;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
