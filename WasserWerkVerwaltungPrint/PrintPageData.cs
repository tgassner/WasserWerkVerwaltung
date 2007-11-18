using System;
using System.Collections.Generic;
using System.Text;

namespace WasserWerkVerwaltung.Print {

    public class PrintPageData {
        private IList<PrintTextElement> printTextElements;

        public IList<PrintTextElement> PrintTextElements {
            get {
                return this.printTextElements;
            }
        }

        public PrintPageData() {
            printTextElements = new List<PrintTextElement>();
        }

        public void AddTextElements(PrintTextElement textElement) {
            this.printTextElements.Add(textElement);
        }
    }
}
