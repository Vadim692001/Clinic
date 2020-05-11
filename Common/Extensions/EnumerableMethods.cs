using System.Collections.Generic;

namespace Common.Extensions {
    public static class EnumerableMethods {
        public static string ToLineList<T>(this IEnumerable<T> collection, 
                                                     string prompt) {
            return prompt + ":\n" + string.Join("\n", collection) + "\n";
        }

        public static string ToLine<T>(this IEnumerable<T> collection, 
                            string prompt, string separator = ", ") {
            return prompt + ":\t" + string.Join(separator, collection) + "\n";
        }
    }
}
