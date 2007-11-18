using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace WasserWerkVerwaltung.Print {
    public class PrintTextElement {

        private string text;
        private Font font;
        private Brush brush;
        private float x, y;

        public PrintTextElement(string text, Font font, Brush brush, float x, float y) {
            this.text = text;
            this.font = font;
            this.brush = brush;
            this.x = x;
            this.y = y;
        }

        public string Text {
            get {
                return this.text;
            }
        }

        public Font Font {
            get {
                return this.font;
            }
        }

        public Brush Brush {
            get {
                return this.brush;
            }
        }

        public float X {
            get {
                return this.x;
            }
        }

        public float Y {
            get {
                return this.y;
            }
        }
    }
}
