using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WasserWerkVerwaltung.GUI {
    public partial class ZaehlerstaendeInfoControl : UserControl {
        public ZaehlerstaendeInfoControl() {
            InitializeComponent();
        }

        private void buttonSpeichern_Click(object sender, EventArgs e) {
            MessageBox.Show("Speichern noch nicht implementiert");
        }

        private void buttonNeu_Click(object sender, EventArgs e) {
            MessageBox.Show("Neu noch nicht implementiert");
        }

        private void buttonLoeschen_Click(object sender, EventArgs e) {
            MessageBox.Show("Löschen noch nicht implementiert");
        }
    }
}
