using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace QualityOfServiceApp.Converters
{
    public class ColorBarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double rating = 0;
            if (double.TryParse(value.ToString(), out rating))
            {
                if (rating > 3.00 && rating < 4.00) return "#ffc107";
                if (rating<=3.00) return "#ef5350";
                if(rating<=5.00 && rating>=4.00 ) return "#4caf50";
                if(rating==5.00) return "#4caf50";
            }
            return "#4caf50";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
