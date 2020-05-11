using Common.Context.LineIndents;

namespace Common.Context.StringFormatters {

    public abstract class StringFormatter {

        private static StringFormatter _current;

        public static StringFormatter Current {
            get {
                if (_current == null) {
                    _current = new SimpleStringFormatter() {
                        LineLength = 79
                    };
                }
                return _current;
            }
            set { _current = value; }
        }

        public virtual int LineLength { get; set; }

        public abstract string FormatWithLineBreaks(
            string text, int indentLength);

        public string FormatWithLineBreaks(string text) {
            return FormatWithLineBreaks(text,
                LineIndent.Current.Length);
        }
    }
}
