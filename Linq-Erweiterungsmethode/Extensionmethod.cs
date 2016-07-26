using System;
using System.Collections.Generic;

namespace Linq_Erweiterungsmethode
{
    static class Extensionmethod
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> liste, Func<T, bool> filter)
        {
            List<T> result = new List<T>();
            foreach (T name in liste)
                if (filter(name))
                    result.Add(name);
            return result;
        }
    }
}
