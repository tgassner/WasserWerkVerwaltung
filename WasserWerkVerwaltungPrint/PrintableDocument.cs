using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;

namespace WasserWerkVerwaltung.Print {
    public class PrintableDocument {

        private IList<PrintablePage> printPageList;
        private PrintDocument docToPrint = new PrintDocument();
        private string documentName = "";
        private int pages;
        private int currentPage;

        public string DocumentName {
            get {
                return this.documentName;
            }
            set {
                this.documentName = value;
            }
        }

        public PrintableDocument() {
            printPageList = new List<PrintablePage>();
        }

        public void AddPrintPage(PrintablePage page) {
            this.printPageList.Add(page);
        }

        public void DoPrint() {
            PrintDialog printDialog = new PrintDialog();

            docToPrint.DocumentName = this.documentName;
            printDialog.AllowSomePages = true;

            printDialog.ShowHelp = true;

            printDialog.Document = docToPrint;

            DialogResult result = printDialog.ShowDialog();

            docToPrint.PrintPage += new PrintPageEventHandler(document_PrintPage);

            pages = this.printPageList.Count;
            currentPage = 0;
            if (this.printPageList.Count <= 0)
                return;

            if (result == DialogResult.OK) {
                try {
                    docToPrint.Print();
                } catch (Exception e) {
                    MessageBox.Show("Fehler beim Drucken: " + e.ToString());
                }
            }
            
        }

        private void document_PrintPage(object sender,
            PrintPageEventArgs e) {

            PrintablePage ppd = this.printPageList[currentPage];
            foreach (IPrintableObject po in ppd.PrintableObjectList) {
                po.AddToGraphics(e.Graphics);
            }

            currentPage++;
            if (--pages > 0)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }
    }
}
