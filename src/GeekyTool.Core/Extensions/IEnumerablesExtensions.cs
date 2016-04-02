using System.Collections.Generic;
using System.Linq;
using GeekyTool.Core.Helpers;

namespace GeekyTool.Core.Extensions
{
    public static class IEnumerablesExtensions
    {
        public static OptimizedObservableCollection<T> ToOptimizedObservableCollection<T>(this IEnumerable<T> source)
        {
            var result = new OptimizedObservableCollection<T>();
            foreach (var item in source)
                result.Add(item);
            return result;
        }

        public static OptimizedObservableCollection<T> ToOptimizedObservableCollection<T>(this IList<T> source)
        {
            var result = new OptimizedObservableCollection<T>();
            foreach (var item in source)
                result.Add(item);
            return result;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> genericEnumerable)
        {
            return (genericEnumerable == null) || !genericEnumerable.Any();
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> genericCollection)
        {
            if (genericCollection == null)
            {
                return true;
            }
            return genericCollection.Count < 1;
        }
    }
}
