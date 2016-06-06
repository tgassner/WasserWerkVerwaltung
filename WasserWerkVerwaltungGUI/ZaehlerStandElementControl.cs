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
        private DateTime rechnungsDatumHalbjahr;
        private DateTime rechnungsDatumJahr;

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
            this.buttonDruckJahresrechnung.Enabled = !changed;
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
            this.buttonDruckJahresrechnung.Enabled = !this.changed;
        }

        private void fillDataFromCurrentJahresData() {
            this.groupBox1.Text = this.currentJahresData.Jahr.ToString();
            bool changetmp = this.changed;
            this.textBoxZaehlerStandAlt.Text = this.currentJahresData.ZaehlerStandAlt.ToString();
            this.textBoxZaehlerStandNeu.Text = this.currentJahresData.ZaehlerStandNeu.ToString();
            this.textBoxAblesedatum.Text = this.currentJahresData.AbleseDatum.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo);
            this.textBoxBereitsbezahlt.Text = this.currentJahresData.BereitsBezahlt.ToString();
            this.textBoxTauschZaehlerstandAlt.Text = this.currentJahresData.TauschZaehlerStandAlt.ToString();
            this.textBoxTauschZaehlerstandNeu.Text = this.currentJahresData.TauschZaehlerStandNeu.ToString();
            this.textBoxSonstigeForderungenText.Text = this.currentJahresData.SonstigeForderungenText;
            this.textBoxSonstigeForderungenWert.Text = this.currentJahresData.SonstigeForderungenValue.ToString();
            this.textBoxHalbJahresBetrag.Text = this.currentJahresData.HalbJahresBetrag.ToString();
            this.textBoxHalbJahresRechnungsNummer.Text = Convert.ToString(this.currentJahresData.RechnungsNummerHalbjahr);
            this.textBoxGanzJahresRechnungsNummer.Text = Convert.ToString(this.currentJahresData.RechnungsNummerJahr);
            this.rechnungsDatumHalbjahr = this.currentJahresData.RechnungsDatumHalbjahr;
            this.rechnungsDatumJahr = this.currentJahresData.RechnungsDatumJahr;

            this.changed = changetmp;
            this.textBoxNichtGespeichert.Visible = changed;
            this.buttonDruckJahresrechnung.Enabled = !this.changed;
        }

        private bool checkHalbJahresRechnungsNummer()
        {
            if (String.IsNullOrEmpty(this.textBoxHalbJahresRechnungsNummer.Text))
            {
                return true;
            }
            try
            {
                Int64.Parse(this.textBoxHalbJahresRechnungsNummer.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Bitte Halbjahres Rechnungs Nummer Überprüfen: Wert ungültig.");
                return false;
            }
            return true;
        }

        private bool checkGanzJahresRechnungsNummer()
        {
            if (String.IsNullOrEmpty(this.textBoxGanzJahresRechnungsNummer.Text))
            {
                return true;
            }
            try
            {
                Int64.Parse(this.textBoxGanzJahresRechnungsNummer.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Bitte Ganzjahres Rechnungs Nummer Überprüfen: Wert ungültig.");
                return false;
            }
            return true;
        }

        private bool checkZaehlerStandAlt() {
            try {
                Int64.Parse(this.textBoxZaehlerStandAlt.Text);
            } catch (FormatException) {
                MessageBox.Show("Bitte ZaehlerStandAlt Überprüfen: Wert ungültig.");
                return false;
            }
            return true;
        }

        private bool checkZaehlerStandNeu() {
            try {
                Int64.Parse(this.textBoxZaehlerStandNeu.Text);
            } catch (FormatException) {
                MessageBox.Show("Bitte ZaehlerStandNeu Überprüfen: Wert ungültig.");
                return false;
            }
            return true;
        }

        private bool checkAblesedatum() {
            try {
                DateTime.Parse(this.textBoxAblesedatum.Text, DateTimeFormatInfo.CurrentInfo);
            } catch (FormatException) {
                MessageBox.Show("Bitte Ablesedatum Überprüfen: Wert ungültig.");
                return false;
            }
            return true;
        }

        private bool checkBereitsBezahlt() {
            try {
                Double.Parse(this.textBoxBereitsbezahlt.Text.Replace(".", ","));
            } catch (FormatException) {
                MessageBox.Show("Bitte Bereitsbezahlt Überprüfen: Wert ungültig.");
                return false;
            }
            return true;
        }

        private bool checkTauschzaehlerstandAlt() {
            try {
                Int64.Parse(this.textBoxTauschZaehlerstandAlt.Text);
            } catch (FormatException) {
                MessageBox.Show("Bitte Tausch Zaehlerstand Alt Überprüfen: Wert ungültig.");
                return false;
            }
            return true;
        }

        private bool checkTauschzaehlerstandNeu() {
            try {
                Int64.Parse(this.textBoxTauschZaehlerstandNeu.Text);
            } catch (FormatException) {
                MessageBox.Show("Bitte Tausch Zaehlerstand Neu Überprüfen: Wert ungültig.");
                return false;
            }
            return true;
        }

        private bool checkSonstigeForderungenWert() {
            try {
                Double.Parse(this.textBoxSonstigeForderungenWert.Text.Replace(".", ","));
            } catch (FormatException) {
                MessageBox.Show("Bitte Sonstige Froderungen Wert Überprüfen: Wert ungültig.");
                return false;
            }
            return true;
        }

        private bool checkHalbJahresBetrag() {
            try {
                Double.Parse(this.textBoxSonstigeForderungenWert.Text.Replace(".", ","));
            } catch (FormatException) {
                MessageBox.Show("Bitte Sonstige Froderungen überprüfen: Wert ungültig.");
                return false;
            }
            return true;
        }

        private bool checkBereitsBezahltSummand() {
            try {
                Double.Parse(this.textBoxBereitsBezahltSummand.Text.Replace(".", ","));
            } catch (FormatException) {
                MessageBox.Show("Bitte BereitsBezahlt Summand überprüfen: Wert ungültig.");
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

            ok = checkTauschzaehlerstandAlt();
            if (!ok)
                return false;

            ok = checkTauschzaehlerstandNeu();
            if (!ok)
                return false;

            ok = checkSonstigeForderungenWert();
            if (!ok)
                return false;

            ok = checkHalbJahresBetrag();
            if (!ok)
                return false;

            ok = checkHalbJahresRechnungsNummer();
            if (!ok)
                return false;

            ok = checkGanzJahresRechnungsNummer();
            if (!ok)
                return false;

            return true;
        }

        private void buttonSpeichern_Click(object sender, EventArgs e) {
            if (!checkFields())
                return;

            if ((currentJahresData.RechnungsNummerJahr != null && !String.IsNullOrEmpty(textBoxGanzJahresRechnungsNummer.Text) && currentJahresData.RechnungsNummerJahr != Int64.Parse(textBoxGanzJahresRechnungsNummer.Text))
                || (String.IsNullOrEmpty(textBoxGanzJahresRechnungsNummer.Text) && currentJahresData.RechnungsNummerJahr != null)) {
                DialogResult dr = MessageBox.Show("Die Ganzjahres Rechnungsnummer wurde geändert. Wollen sie das wirklich?", "Achtung", MessageBoxButtons.OKCancel);
                if (!(dr == DialogResult.OK)) {
                    return;
                }
            }

            if (!String.IsNullOrEmpty(textBoxGanzJahresRechnungsNummer.Text) && currentJahresData.RechnungsNummerJahr != Int64.Parse(textBoxGanzJahresRechnungsNummer.Text)) {
                if (wwvBLComp.ExistsRechnungsnummer(long.Parse(textBoxGanzJahresRechnungsNummer.Text), currentJahresData.Jahr))
                {
                    MessageBox.Show("Die Ganzjahres Rechnungsnummer existiert bereits! Vorgang Abgebrochen!");
                    return;
                }
            }

            if ((currentJahresData.RechnungsNummerHalbjahr != null && !String.IsNullOrEmpty(textBoxHalbJahresRechnungsNummer.Text) && currentJahresData.RechnungsNummerHalbjahr != Int64.Parse(textBoxHalbJahresRechnungsNummer.Text))
                || (String.IsNullOrEmpty(textBoxHalbJahresRechnungsNummer.Text) && currentJahresData.RechnungsNummerHalbjahr != null))
            {
                DialogResult dr = MessageBox.Show("Die Halbjahres Rechnungsnummer wurde geändert. Wollen sie das wirklich?", "Achtung", MessageBoxButtons.OKCancel);
                if (!(dr == DialogResult.OK))
                {
                    return;
                }
            }

            if (!String.IsNullOrEmpty(textBoxHalbJahresRechnungsNummer.Text) && currentJahresData.RechnungsNummerHalbjahr != Int64.Parse(textBoxHalbJahresRechnungsNummer.Text))
            {
                if (wwvBLComp.ExistsRechnungsnummer(long.Parse(textBoxHalbJahresRechnungsNummer.Text), currentJahresData.Jahr))
                {
                    MessageBox.Show("Die Halbjahres Rechnungsnummer existiert bereits! Vorgang Abgebrochen!");
                    return;
                }
            }

            JahresDatenData jahresdataTemp = new JahresDatenData(this.currentJahresData.Id,
                this.currentJahresData.KundenId,
                Int64.Parse(textBoxZaehlerStandAlt.Text),
                Int64.Parse(textBoxZaehlerStandNeu.Text),
                currentJahresData.Jahr,
                DateTime.Parse(this.textBoxAblesedatum.Text, DateTimeFormatInfo.CurrentInfo),
                Double.Parse(textBoxBereitsbezahlt.Text.Replace(".", ",")),
                long.Parse(textBoxTauschZaehlerstandAlt.Text),
                long.Parse(textBoxTauschZaehlerstandNeu.Text),
                textBoxSonstigeForderungenText.Text,
                Double.Parse(textBoxSonstigeForderungenWert.Text.Replace(".", ",")),
                Double.Parse(textBoxHalbJahresBetrag.Text.Replace(".", ",")),
                rechnungsDatumHalbjahr,
                rechnungsDatumJahr,
                String.IsNullOrEmpty(textBoxHalbJahresRechnungsNummer.Text) ? (long?)null : long.Parse(textBoxHalbJahresRechnungsNummer.Text),
                String.IsNullOrEmpty(textBoxGanzJahresRechnungsNummer.Text) ? (long?)null : long.Parse(textBoxGanzJahresRechnungsNummer.Text)
                );

            if (currentJahresData.Id == 0) { // Neue Jahresdata -> insert
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
            this.buttonDruckJahresrechnung.Enabled = !this.changed;
        }

        private void buttonVomVorjahr_Click(object sender, EventArgs e) {
            IList<JahresDatenData> jahresdataList = this.wwvBLComp.GetJahresdataByKundenID(currentKunde.Id);
            bool found = false;
            long alterStand = 0;
            foreach (JahresDatenData jahresDatum in jahresdataList) {
                if (jahresDatum.Jahr == (currentJahresData.Jahr - 1)) {
                    alterStand = jahresDatum.ZaehlerStandNeu;
                    found = true;
                }
            }

            if (found) {
                this.textBoxZaehlerStandAlt.Text = alterStand.ToString();
            } else {
                MessageBox.Show("Stand vom Vorjahr wurde nicht gefunden!");
            }
        }

        private void textChanged(object sender, EventArgs e) {
            this.changed = true;
            this.textBoxNichtGespeichert.Visible = this.changed;
            this.buttonDruckJahresrechnung.Enabled = !this.changed;
        }

        private void buttonDruckJahresrechnung_Click(object sender, EventArgs e) {
            if (changed) {
                MessageBox.Show("Bitte vor dem Drucken speichern!");
                return;
            }
            IList<KundenData> kundenlist = new List<KundenData>();
            kundenlist.Add(this.currentKunde);
            this.wwvBLComp.PrintJahresRechnungen(kundenlist,this.wwvBLComp.GetPreisDataByJahr(this.currentJahresData.Jahr));
            this.fillDataFromCurrentJahresData();
        }

        private void buttonBerechneHalbJahresBetrag_Click(object sender, EventArgs e) {
            JahresDatenData jdd = this.wwvBLComp.GetJahresdataByKundenIDandYear(this.currentKunde.Id,this.currentJahresData.Jahr - 1);
            PreisData pd = this.wwvBLComp.GetPreisDataByJahr(this.currentJahresData.Jahr-1);
            if (jdd == null || pd == null){
                MessageBox.Show("Halbjahreswert konnte leider nicht berechnet werden!");
                return;
            }
            this.textBoxHalbJahresBetrag.Text = ((double)Math.Round(this.wwvBLComp.calcJahresrechnungBrutto(jdd, this.currentKunde, pd) / 2, 2)).ToString();
        }

        private void buttonCalcBereitsBezahlt_Click(object sender, EventArgs e) {
            bool ok;
            ok = checkBereitsBezahltSummand();
            ok = ok && checkBereitsBezahlt();
            if (!ok) {
                return;
            }
            double summe = Double.Parse(textBoxBereitsbezahlt.Text.Replace(".", ",")) + Double.Parse(textBoxBereitsBezahltSummand.Text.Replace(".", ","));
            textBoxBereitsbezahlt.Text = summe.ToString();
            textBoxBereitsBezahltSummand.Text = "";
        }

    }
}
