using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using WasserWerkVerwaltung.BL;
using WasserWerkVerwaltung.CommonObjects;
using System.Globalization;

namespace WasserWerkVerwaltung.GUI {
    public partial class ZaehlerStandElementControl : UserControl {

        private IWWVBL wwvBLComp;
        private JahresDatenData currentJahresData;

        public ZaehlerStandElementControl() {
            InitializeComponent();
        }

        internal void Init(IWWVBL wwvBLComp, JahresDatenData currentJahresData) {
            this.wwvBLComp = wwvBLComp;
            this.currentJahresData = currentJahresData;
            this.fillDataFromCurrentJahresData();
        }

        private void fillDataFromCurrentJahresData() {
            this.groupBox1.Text = this.currentJahresData.Jahr.ToString();
            this.textBoxZaehlerStandAlt.Text = this.currentJahresData.ZaehlerStandAlt.ToString();
            this.textBoxZaehlerStandNeu.Text = this.currentJahresData.ZaehlerStandNeu.ToString();
            this.textBoxAblesedatum.Text = this.currentJahresData.AbleseDatum.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo);
            this.textBoxBereitsbezahlt.Text = this.textBoxBereitsbezahlt.ToString();
        }

        private void buttonSpeichern_Click(object sender, EventArgs e) {

        }

        private void buttonRestore_Click(object sender, EventArgs e) {

        }

        private void buttonBerechnen_Click(object sender, EventArgs e) {

        }

        private void buttonVomVorjahr_Click(object sender, EventArgs e) {

        }
    }
}
