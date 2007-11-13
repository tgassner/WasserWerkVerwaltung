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

        public event ChangeKundeEventHandler ChangeKunde;
        public event NewKundeEventHandler NewKunde;

        public KundenDetailsControl() {
            InitializeComponent();
        }

        public void Init(IWWVBL wwvBLComp) {
            this.wwvBLComp = wwvBLComp;
        }

        public void SetCurrentCustomer(KundenData kunde){
            this.currentKunde = kunde;
            this.fillDataFromCurrentCustomer();
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
            if (currentKunde.BekommtRechnung) {
                this.radioButtonBekommtRechnung.Checked = true;
                this.radioButtonBekommtKeineRechnung.Checked = false;
            } else {
                this.radioButtonBekommtRechnung.Checked = false;
                this.radioButtonBekommtKeineRechnung.Checked = true;
            }
        }

        private bool checkFields() {
            try {
                long l = Int64.Parse(this.textBoxZaehlerStandEinbau.Text);
            } catch (FormatException) {
                MessageBox.Show("Bitte ZaehlerStandEinbau �berpr�fen: Wert ung�ltig.");
                return false;
            }

            try {
                long l = Int64.Parse(this.textBoxZaehlerStandNeu.Text);
            } catch (FormatException) {
                MessageBox.Show("Bitte ZaehlerStandNeu �berpr�fen: Wert ung�ltig.");
                return false;
            }

            try {
                DateTime dt = DateTime.Parse(this.textBoxEichdatum.Text, DateTimeFormatInfo.CurrentInfo);
            } catch (FormatException) {
                MessageBox.Show("Bitte Eichdatum �berpr�fen: Wert ung�ltig.");
                return false;
            }

            try {
                DateTime dt = DateTime.Parse(this.textBoxEinbaudatum.Text, DateTimeFormatInfo.CurrentInfo);
            } catch (FormatException) {
                MessageBox.Show("Bitte Einbaudatum �berpr�fen: Wert ung�ltig.");
                return false;
            }

            try {
                DateTime dt = DateTime.Parse(this.textBoxTauschdatum.Text, DateTimeFormatInfo.CurrentInfo);
            } catch (FormatException) {
                MessageBox.Show("Bitte Tauschdatum �berpr�fen: Wert ung�ltig.");
                return false;
            }

            try {
                double d = Double.Parse(this.textBoxZaehlermiete.Text);
            } catch (FormatException) {
                MessageBox.Show("Bitte Z�hlermiete �berpr�fen: Wert ung�ltig.");
                return false;
            }


            return true;
        }

        private void buttonSpeichern_Click(object sender, EventArgs e) {

            if (Int64.Parse(textBoxID.Text) != currentKunde.Id) {
                MessageBox.Show("Error mit der Kundenid!");
                return;
            }

            KundenData tempKunde = new KundenData(currentKunde.Id, "", "", "", "", "", "", "", false, 0, 0, DateTime.Now, "", DateTime.Now, "", DateTime.Now, 0, "", "");

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
            tempKunde.Zaehlermiete = Double.Parse(this.textBoxZaehlermiete.Text);
            tempKunde.Zahlung = this.textBoxZahlung.Text;
            tempKunde.Bemerkung = this.textBoxBemerkung.Text;
            tempKunde.BekommtRechnung = this.radioButtonBekommtRechnung.Checked;

            if (currentKunde.Id == 0) {  // Neuer Kunde
                KundenData tempKunde2 = this.wwvBLComp.InsertKunde(tempKunde);
                if (tempKunde2 != null) {
                    this.currentKunde = tempKunde2;
                } else {
                    MessageBox.Show("Erstellung des neuen Kunden fehlgeschlagen!");
                    return;
                }
            } else { // �ndern eines bestehenden Kunden

                if (this.wwvBLComp.UpdateKunde(tempKunde)) {
                    this.currentKunde = tempKunde;
                } else {
                    MessageBox.Show("Speichern fehlgeschlagen!");
                    return;
                }
            }
            ChangeKunde(this, new KundeEventArgs(currentKunde));
        }

        private void buttonRestore_Click(object sender, EventArgs e) {
            this.fillDataFromCurrentCustomer();
        }

        private void buttonNewKunde_Click(object sender, EventArgs e) {
            currentKunde = new KundenData(0, "", "", "", "", "", "", "", true, 0, 0, DateTime.Now.Date, "", DateTime.Now.Date, "", DateTime.Now.Date, 0, "", "");
            this.fillDataFromCurrentCustomer();
            this.NewKunde(this, new EventArgs());
        }
    }
}
