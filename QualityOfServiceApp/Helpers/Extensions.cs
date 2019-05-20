using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace QualityOfServiceApp.Helpers
{
    public static class Extensions
    {
        public static ObservableCollection<T> ConvertToObservable<T>(this ICollection<T> list)
        {
            if (list == null) return new ObservableCollection<T> ();
            var convertList = new ObservableCollection<T>();
            foreach (var item in list)
            {
                convertList.Add(item);
            }
            return convertList;
        }
    }
}
