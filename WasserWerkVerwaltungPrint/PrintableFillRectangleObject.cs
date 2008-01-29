using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace WasserWerkVerwaltung.Print {
    public class PrintableFillRectangleObject : IPrintableObject{

        private Brush brush;
        private int x, y, width, height;
        private constructorType constrType = constructorType.none;

        private enum constructorType {
            BrushXintYintWidthintHeightint,
            none
        }

        public PrintableFillRectangleObject(Brush brush,int x,int y,int width,int height) {
            this.brush = brush;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.constrType = constructorType.BrushXintYintWidthintHeightint;

        }

        public void AddToGraphics(Graphics graphics) {
            switch (this.constrType) {
                case constructorType.BrushXintYintWidthintHeightint:
                    graphics.FillRectangle(brush, x, y, width, height);
                    break;
                default:
                    break;
            }
        }

        
    }
}
