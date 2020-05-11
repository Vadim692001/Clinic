using System;

namespace Common.Context.LineIndents {

    public class SimpleLineIndent : LineIndent {

        //private int length = 0;
        private int step = 2;
        private char fillChar = ' ';

        public SimpleLineIndent() { }

        public SimpleLineIndent(int step, char fillChar) {
            this.step = step > 0 ? step : 2;
            this.fillChar = fillChar;
        }

        private string value = "";

        public override string Value {
            get {
                return value;
            }
        }

        public override void Increase() {
            value = new String(fillChar, Length += step);
        }

        public override void Decrease() {
            if (Length < step) return;
            value = new String(fillChar, Length -= step);
        }

        public override void Clear() {
            Length = 0;
            value = "";
        }
    }
}
