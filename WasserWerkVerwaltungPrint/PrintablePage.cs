using System;
using System.Collections.Generic;
using System.Text;

namespace WasserWerkVerwaltung.Print {

    public class PrintablePage {
        private IList<IPrintableObject> printableObjectList;

        internal IList<IPrintableObject> PrintableObjectList {
            get {
                return this.printableObjectList;
            }
        }

        public PrintablePage() {
            printableObjectList = new List<IPrintableObject>();
        }

        public void AddPrintableObject(IPrintableObject printableObject) {
            this.printableObjectList.Add(printableObject);
        }
    }
}
