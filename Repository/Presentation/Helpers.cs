using System.Collections.Generic;
using System.Linq;
namespace Repository.Presentation{
    public static class Helpers{
        public static string Format(this string formatting) { 
            return formatting == null?
                string.Empty:
                formatting;
        }

        public static IEnumerable<T> Format<T>(this IEnumerable<T> formatting) {
            return formatting == null ?
                Enumerable.Empty<T>() :
                formatting;
        }
    }
}
