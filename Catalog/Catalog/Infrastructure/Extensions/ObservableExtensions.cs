using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Catalog.Infrastructure.Extensions
{
    public static class ObservableExtensions
    {
        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> collections)
        {
            return new ObservableCollection<T>(collections);
        }
    }
}
