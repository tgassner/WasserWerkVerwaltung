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
        private KundenData currentKunde;
        private bool changed = false;

        public long Jahr {
            get {
                return currentJahresData.Jahr;
            }
        }

        public ZaehlerStandElementControl() {
            InitializeComponent();
            this.textBoxNichtGespeichert.BackColor = Color.Red;
            this.textBoxNichtGespeichert.ForeColor = Color.Black;
            this.textBoxNichtGespeichert.Visible = changed;
        }

        internal void Init(IWWVBL wwvBLComp, JahresDatenData currentJahresData, KundenData currentKunde) {
            this.wwvBLComp = wwvBLComp;
            this.currentKunde = currentKunde;
            this.currentJahresData = currentJahresData;
            this.fillDataFromCurrentJahresData();
            if (this.currentJahresData.Id == 0) {
                this.changed = true;
                this.textBoxNichtGespeichert.Visible = this.changed;
            }
        }

        private void fillDataFromCurrentJahresData() {
            this.groupBox1.Text = this.currentJahresData.Jahr.ToString();
            bool changetmp = this.changed;
            this.textBoxZaehlerStandAlt.Text = this.currentJahresData.ZaehlerStandAlt.ToString();
            this.textBoxZaehlerStandNeu.Text = this.currentJahresData.ZaehlerStandNeu.ToString();
            this.textBoxAblesedatum.Text = this.currentJahresData.AbleseDatum.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo);
            this.textBoxBereitsbezahlt.Text = this.currentJahresData.BereitsBezahlt.ToString();
            this.textBoxRechnungssumme.Text = this.currentJahresData.Rechnungssumme.ToString();
            this.changed = changetmp;
            this.textBoxNichtGespeichert.Visible = changed;
        }

        private bool checkZaehlerStandAlt() {
            try {
                long zsa = Int64.Parse(this.textBoxZaehlerStandAlt.Text);
            } catch (FormatException) {
                MessageBox.Show("Bitte ZaehlerStandAlt Überprüfen: Wert ungültig.");
                return false;
            }
            return true;
        }

        private bool checkZaehlerStandNeu() {
            try {
                long zsn = Int64.Parse(this.textBoxZaehlerStandNeu.Text);
            } catch (FormatException) {
                MessageBox.Show("Bitte ZaehlerStandNeu Überprüfen: Wert ungültig.");
                return false;
            }
            return true;
        }

        private bool checkAblesedatum() {
            try {
                DateTime dt = DateTime.Parse(this.textBoxAblesedatum.Text, DateTimeFormatInfo.CurrentInfo);
            } catch (FormatException) {
                MessageBox.Show("Bitte Ablesedatum Überprüfen: Wert ungültig.");
                return false;
            }
            return true;
        }

        private bool checkBereitsBezahlt() {
            try {
                double bb = Double.Parse(this.textBoxBereitsbezahlt.Text.Replace(".", ","));
            } catch (FormatException) {
                MessageBox.Show("Bitte Bereitsbezahlt Überprüfen: Wert ungültig.");
                return false;
            }
            return true;
        }

        private bool checkRechnungssumme() {
            try {
                double rs = Double.Parse(this.textBoxRechnungssumme.Text.Replace(".", ","));
            } catch (FormatException) {
                MessageBox.Show("Bitte Rechnungssumme Überprüfen: Wert ungültig.");
                return false;
            }
            return true;
        }

        private bool checkFields() {
            bool ok;

            ok = checkZaehlerStandAlt();
            if (!ok)
                return false;

            ok = checkZaehlerStandNeu();
            if (!ok)
                return false;

            ok = checkAblesedatum();
            if (!ok)
                return false;

            ok = checkBereitsBezahlt();
            if (!ok)
                return false;

            ok = checkRechnungssumme();
            if (!ok)
                return false;

            return true;
        }

        private void buttonSpeichern_Click(object sender, EventArgs e) {
            if (!checkFields())
                return;

            JahresDatenData jahresdataTemp = new JahresDatenData(this.currentJahresData.Id,
                this.currentJahresData.KundenId,
                Double.Parse(textBoxRechnungssumme.Text.Replace(".",",")),
                Int64.Parse(textBoxZaehlerStandAlt.Text),
                Int64.Parse(textBoxZaehlerStandNeu.Text),
                currentJahresData.Jahr,
                DateTime.Parse(this.textBoxAblesedatum.Text, DateTimeFormatInfo.CurrentInfo),
                Double.Parse(textBoxBereitsbezahlt.Text.Replace(".", ",")));

            if (currentJahresData.Id == 0){ // Neue Jahresdata -> insert
                JahresDatenData jahresdataTemp2 = this.wwvBLComp.InsertJahresDaten(jahresdataTemp);
                if (jahresdataTemp2 == null) {
                    MessageBox.Show("Speichern fehlgeschlagen!");
                    return;
                }
                currentJahresData = jahresdataTemp2;
            } else { // Bestehende Jahresdata -> update
                if (!this.wwvBLComp.UpdateJahresDaten(jahresdataTemp)) {
                    MessageBox.Show("Speichern fehlgeschlagen!");
                    return;
                }
                currentJahresData = jahresdataTemp;
            }
            this.changed = false;
            this.fillDataFromCurrentJahresData();
        }

        private void buttonRestore_Click(object sender, EventArgs e) {
            fillDataFromCurrentJahresData();
            if (currentJahresData.Id != 0) {
                this.changed = false;
                this.textBoxNichtGespeichert.Visible = this.changed;
            }
        }

        private void buttonBerechnen_Click(object sender, EventArgs e) {
            if (!checkZaehlerStandAlt()) return;
            if (!checkZaehlerStandNeu()) return;

            long verbrauch = (Int64.Parse(textBoxZaehlerStandNeu.Text) - Int64.Parse(textBoxZaehlerStandAlt.Text));
            double preis = wwvBLComp.GetPreisDataByJahr(currentJahresData.Jahr).Preis;
            double zaehlermiete = currentKunde.Zaehlermiete;
            double ust = 1.1;

            double rechnungssumme = ((verbrauch * preis) + zaehlermiete) * ust;
            if (rechnungssumme < 0) {
                MessageBox.Show("Rechnungssumme negativ! Bitte Zählerstände kontrollieren");
                return;
            }

            textBoxRechnungssumme.Text = rechnungssumme.ToString();
        }

        private void buttonVomVorjahr_Click(object sender, EventArgs e) {
            IList<JahresDatenData> jahresdataList = this.wwvBLComp.GetJahresdataByKundenID(currentKunde.Id);
            bool found = false;
            long alterStand = 0;
            foreach(JahresDatenData jahresDatum in jahresdataList){
                if (jahresDatum.Jahr == (currentJahresData.Jahr-1)){
                    alterStand = jahresDatum.ZaehlerStandNeu;
                    found = true;
                }
            }

            if (found){
                this.textBoxZaehlerStandAlt.Text = alterStand.ToString();
            }else{
                MessageBox.Show("Stand vom Vorjahr wurde nicht gefunden!");
            }
        }

        private void textChanged(object sender, EventArgs e) {
            this.changed = true;
            this.textBoxNichtGespeichert.Visible = changed;
        }
    }
}
