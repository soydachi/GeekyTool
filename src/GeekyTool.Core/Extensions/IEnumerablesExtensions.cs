using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GeekyTool.Helpers;

namespace GeekyTool.Extensions
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

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            var result = new ObservableCollection<T>();
            foreach (var item in source)
                result.Add(item);
            return result;
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IList<T> source)
        {
            var result = new ObservableCollection<T>();
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

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "Source list cannot be null");
            if (action == null)
                throw new ArgumentNullException(nameof(action), "Action to be applied to source cannot be null");

            foreach (T element in source)
            {
                action(element);
            }
        }
    }
}
