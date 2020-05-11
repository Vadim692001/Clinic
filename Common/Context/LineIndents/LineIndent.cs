
namespace Common.Context.LineIndents {

    public abstract class LineIndent {

        private static LineIndent current;

        public static LineIndent Current {
            get {
                if (current == null) {
                    current = new SimpleLineIndent();
                }
                return LineIndent.current;
            }
            set { LineIndent.current = value; }
        }

        public int Length { get; protected set; }

        public abstract string Value { get; }

        public abstract void Increase();
        public abstract void Decrease();
        public abstract void Clear();
    }
}
