using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WasserWerkVerwaltung.GUI {
    public partial class ZaehlerstandableseText : Form {
        public ZaehlerstandableseText() {
            InitializeComponent();

            this.textBoxText.Text = WasserWerkVerwaltung.GUI.Properties.Settings.Default.TextZaeholerstandformular;
        }

        public string FormularText {
            get {
                return this.textBoxText.Text;
            }
        }

        public DateTime DatumVon {
            get {
                return DateTime.Parse(this.dateTimePickerAblesezeitraumVon.Text);
            }
        }

        public DateTime DatumBis {
            get {
                return DateTime.Parse(this.dateTimePickerAblesezeitraumBis.Text);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            WasserWerkVerwaltung.GUI.Properties.Settings.Default.TextZaeholerstandformular = this.textBoxText.Text;
            WasserWerkVerwaltung.GUI.Properties.Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonAbbrechen_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
