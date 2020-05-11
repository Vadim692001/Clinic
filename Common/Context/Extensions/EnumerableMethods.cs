using Common.Context.LineIndents;
using System.Collections.Generic;

namespace Common.Context.Extensions {
    public static class EnumerableMethods {
        public static string ToLineList<T>(
            this IEnumerable<T> collection, string prompt) {
                string s = string.Format("{0}{1}:\n", 
                    LineIndent.Current.Value, prompt);
                LineIndent.Current.Increase();
                s += string.Join("", collection) + "\n";
                LineIndent.Current.Decrease();
                return s;
        }
    }
}
