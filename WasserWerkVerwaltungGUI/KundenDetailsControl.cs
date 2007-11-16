using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using WasserWerkVerwaltung.CommonObjects;
using System.Globalization;
using WasserWerkVerwaltung.BL;

namespace WasserWerkVerwaltung.GUI {

    public delegate void ChangeKundeEventHandler(object sender, KundeEventArgs e);
    public delegate void NewKundeEventHandler(object sender, EventArgs e);

    public partial class KundenDetailsControl : UserControl {
        
        private KundenData currentKunde;
        private IWWVBL wwvBLComp;
        private bool changed = false;

        public event ChangeKundeEventHandler ChangeKunde;
        public event NewKundeEventHandler NewKunde;

        public KundenDetailsControl() {
            InitializeComponent();
            this.textBoxNichtGespeichert.BackColor = Color.Red;
            this.textBoxNichtGespeichert.ForeColor = Color.Black;
            this.textBoxNichtGespeichert.Visible = changed;
        }

        public void Init(IWWVBL wwvBLComp) {
            this.wwvBLComp = wwvBLComp;
        }

        public void SetCurrentCustomer(KundenData kunde){
            this.currentKunde = kunde;
            this.fillDataFromCurrentCustomer();
            if (this.currentKunde.Id == 0) {
                this.changed = true;
            } else {
                this.changed = false;
            }
            this.textBoxNichtGespeichert.Visible = this.changed;
        }

        private void fillDataFromCurrentCustomer() {
            this.textBoxID.Text = currentKunde.Id.ToString();
            this.textBoxVorname.Text = currentKunde.Vorname;
            this.textBoxNachname.Text = currentKunde.Nachname;
            this.textBoxStrasse.Text = currentKunde.Strasse;
            this.textBoxOrt.Text = currentKunde.Ort;
            this.textBoxTel.Text = currentKunde.Tel;
            this.textBoxBankverbindung.Text = currentKunde.BankVerbindung;
            this.textBoxZaehlerStandEinbau.Text = currentKunde.ZaehlerEinbauStand.ToString();
            this.textBoxZaehlerStandNeu.Text = currentKunde.ZaehlerNeuStand.ToString();
            this.textBoxEichdatum.Text = currentKunde.EichDatum.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo);
            this.textBoxZaehlerNummer.Text = currentKunde.ZaehlerNummer;
            this.textBoxEinbaudatum.Text = currentKunde.EinbauDatum.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo);
            this.textBoxErkl.Text = currentKunde.Erkl;
            this.textBoxHausbesitzer.Text = currentKunde.Hausbesitzer;
            this.textBoxTauschdatum.Text = currentKunde.TauschDatum.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo);
            this.textBoxZaehlermiete.Text = currentKunde.Zaehlermiete.ToString();
            this.textBoxZahlung.Text = currentKunde.Zahlung;
            this.textBoxBemerkung.Text = currentKunde.Bemerkung;
            this.textBoxLeitungskreis.Text = currentKunde.Leitungskreis.ToString();
            switch (currentKunde.BekommtRechnung) {
                case Rechnung.Keine:
                    this.radioButtonKeine.Checked = true;
                    this.radioButtonJahres.Checked = false;
                    this.radioButtonHalbJahres.Checked = false;
                    break;
                case Rechnung.Jahres:
                    this.radioButtonKeine.Checked = false;
                    this.radioButtonJahres.Checked = true;
                    this.radioButtonHalbJahres.Checked = false;
                    break;
                case Rechnung.Halbjahres:
                    this.radioButtonKeine.Checked = false;
                    this.radioButtonJahres.Checked = false;
                    this.radioButtonHalbJahres.Checked = true;
                    break;
                default:
                    break;
            }
        }

        private bool checkFields() {
            try {
                long l = Int64.Parse(this.textBoxZaehlerStandEinbau.Text);
            } catch (FormatException) {
                MessageBox.Show("Bitte ZaehlerStandEinbau überprüfen: Wert ungültig.");
                return false;
            }

            try {
                long l = Int64.Parse(this.textBoxZaehlerStandNeu.Text);
            } catch (FormatException) {
                MessageBox.Show("Bitte ZaehlerStandNeu überprüfen: Wert ungültig.");
                return false;
            }

            try {
                DateTime dt = DateTime.Parse(this.textBoxEichdatum.Text, DateTimeFormatInfo.CurrentInfo);
            } catch (FormatException) {
                MessageBox.Show("Bitte Eichdatum Überprüfen: Wert ungültig.");
                return false;
            }

            try {
                DateTime dt = DateTime.Parse(this.textBoxEinbaudatum.Text, DateTimeFormatInfo.CurrentInfo);
            } catch (FormatException) {
                MessageBox.Show("Bitte Einbaudatum Überprüfen: Wert ungültig.");
                return false;
            }

            try {
                DateTime dt = DateTime.Parse(this.textBoxTauschdatum.Text, DateTimeFormatInfo.CurrentInfo);
            } catch (FormatException) {
                MessageBox.Show("Bitte Tauschdatum Überprüfen: Wert ungültig.");
                return false;
            }

            try {
                double d = Double.Parse(this.textBoxZaehlermiete.Text.Replace(".", ","));
            } catch (FormatException) {
                MessageBox.Show("Bitte Zählermiete Überprüfen: Wert ungültig.");
                return false;
            }

            try {
                long l = Int64.Parse(this.textBoxLeitungskreis.Text);
            } catch (FormatException) {
                MessageBox.Show("Bitte Leitungskreis Überprüfen: Wert ungültig.");
                return false;
            }

            return true;
        }

        private void buttonSpeichern_Click(object sender, EventArgs e) {

            if (Int64.Parse(textBoxID.Text) != currentKunde.Id) {
                MessageBox.Show("Error mit der Kundenid!");
                return;
            }

            KundenData tempKunde = new KundenData(currentKunde.Id, "", "", "", "", "", "", "", Rechnung.Keine, 0, 0, DateTime.Now, "", DateTime.Now, "", DateTime.Now, 0, "", "",0);

            if (!checkFields())
                return;
            tempKunde.Vorname = this.textBoxVorname.Text;
            tempKunde.Nachname = this.textBoxNachname.Text;
            tempKunde.Strasse = this.textBoxStrasse.Text;
            tempKunde.Ort = this.textBoxOrt.Text;
            tempKunde.Tel = this.textBoxTel.Text;
            tempKunde.BankVerbindung = this.textBoxBankverbindung.Text;
            tempKunde.ZaehlerEinbauStand = Int64.Parse(this.textBoxZaehlerStandEinbau.Text);
            tempKunde.ZaehlerNeuStand = Int64.Parse(this.textBoxZaehlerStandNeu.Text);
            tempKunde.EichDatum = DateTime.Parse(this.textBoxEichdatum.Text, DateTimeFormatInfo.CurrentInfo);
            tempKunde.ZaehlerNummer = this.textBoxZaehlerNummer.Text;
            tempKunde.EinbauDatum = DateTime.Parse(this.textBoxEinbaudatum.Text, DateTimeFormatInfo.CurrentInfo);
            tempKunde.Erkl = this.textBoxErkl.Text;
            tempKunde.Hausbesitzer = this.textBoxHausbesitzer.Text;
            tempKunde.TauschDatum = DateTime.Parse(this.textBoxTauschdatum.Text, DateTimeFormatInfo.CurrentInfo);
            tempKunde.Zaehlermiete = Double.Parse(this.textBoxZaehlermiete.Text.Replace(".", ","));
            tempKunde.Zahlung = this.textBoxZahlung.Text;
            tempKunde.Bemerkung = this.textBoxBemerkung.Text;
            if (this.radioButtonKeine.Checked)
                tempKunde.BekommtRechnung = Rechnung.Keine;
            if (this.radioButtonJahres.Checked)
                tempKunde.BekommtRechnung = Rechnung.Jahres;
            if (this.radioButtonHalbJahres.Checked)
                tempKunde.BekommtRechnung = Rechnung.Halbjahres;
            tempKunde.Leitungskreis = Int64.Parse(this.textBoxLeitungskreis.Text);

            if (currentKunde.Id == 0) {  // Neuer Kunde
                KundenData tempKunde2 = this.wwvBLComp.InsertKunde(tempKunde);
                if (tempKunde2 != null) {
                    this.currentKunde = tempKunde2;
                } else {
                    MessageBox.Show("Erstellung des neuen Kunden fehlgeschlagen!");
                    return;
                }
            } else { // ändern eines bestehenden Kunden

                if (this.wwvBLComp.UpdateKunde(tempKunde)) {
                    this.currentKunde = tempKunde;
                } else {
                    MessageBox.Show("Speichern fehlgeschlagen!");
                    return;
                }
            }
            this.changed = true;
            this.textBoxNichtGespeichert.Visible = changed;
            ChangeKunde(this, new KundeEventArgs(currentKunde));
        }

        private void buttonRestore_Click(object sender, EventArgs e) {
            this.fillDataFromCurrentCustomer();
            if (currentKunde.Id != 0) {
                this.changed = false;
                this.textBoxNichtGespeichert.Visible = this.changed;
            }
        }

        private void buttonNewKunde_Click(object sender, EventArgs e) {
            currentKunde = new KundenData(0, "", "", "", "", "", "", "", Rechnung.Keine, 0, 0, DateTime.Now.Date, "", DateTime.Now.Date, "", DateTime.Now.Date, 0, "", "",0);
            this.fillDataFromCurrentCustomer();
            this.changed = true;
            this.textBoxNichtGespeichert.Visible = this.changed;
            this.NewKunde(this, new EventArgs());
        }

        private void textChanged(object sender, EventArgs e) {
            this.changed = true;
            this.textBoxNichtGespeichert.Visible = changed;
        }
    }
}
