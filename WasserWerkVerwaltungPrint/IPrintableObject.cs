using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace WasserWerkVerwaltung.Print {
    public interface IPrintableObject {
        void AddToGraphics(Graphics graphics);
    }
}
