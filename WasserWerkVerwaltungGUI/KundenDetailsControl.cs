using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using WasserWerkVerwaltung.CommonObjects;
using System.Globalization;

namespace WasserWerkVerwaltung.GUI {
    public partial class KundenDetailsControl : UserControl {
        
        private KundenData currentCustomer;

        public KundenDetailsControl() {
            InitializeComponent();
        }

        public void SetCurrentCustomer(KundenData customer){
            this.currentCustomer = customer;
            this.fillDateFromCurrentCustomer();
        }

        private void fillDateFromCurrentCustomer() {
            this.textBoxID.Text = currentCustomer.Id.ToString();
            this.textBoxVorname.Text = currentCustomer.Vorname;
            this.textBoxNachname.Text = currentCustomer.Nachname;
            this.textBoxStrasse.Text = currentCustomer.Strasse;
            this.textBoxOrt.Text = currentCustomer.Ort;
            this.textBoxTel.Text = currentCustomer.Tel;
            this.textBoxBankverbindung.Text = currentCustomer.BankVerbindung;
            this.textBoxZaehlerStandEinbau.Text = currentCustomer.ZaehlerEinbauStand.ToString();
            this.textBoxZaehlerStandNeu.Text = currentCustomer.ZaehlerNeuStand.ToString();
            this.textBoxEichdatum.Text = currentCustomer.EichDatum.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo);
            this.textBoxZaehlerNummer.Text = currentCustomer.ZaehlerNummer;
            this.textBoxEinbaudatum.Text = currentCustomer.EinbauDatum.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo);
            this.textBoxErkl.Text = currentCustomer.Erkl;
            this.textBoxHausbesitzer.Text = currentCustomer.Hausbesitzer;
            this.textBoxTauschdatum.Text = currentCustomer.TauschDatum.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo);
            this.textBoxZaehlermiete.Text = currentCustomer.Zaehlermiete.ToString();
            this.textBoxZahlung.Text = currentCustomer.Zahlung;
            this.textBoxBemerkung.Text = currentCustomer.Bemerkung;
            if (currentCustomer.BekommtRechnung) {
                this.radioButtonBekommtRechnung.Checked = true;
                this.radioButtonBekommtKeineRechnung.Checked = false;
            } else {
                this.radioButtonBekommtRechnung.Checked = false;
                this.radioButtonBekommtKeineRechnung.Checked = true;
            }
        }

        private void buttonSpeichern_Click(object sender, EventArgs e) {
            MessageBox.Show("Speichern noch nicht implementiert");
        }

        private void buttonRestore_Click(object sender, EventArgs e) {
            this.fillDateFromCurrentCustomer();
        }

        private void buttonNewKunde_Click(object sender, EventArgs e) {
            currentCustomer = new KundenData(0, "", "", "", "", "", "", "", true, 0, 0, DateTime.Now.Date, "", DateTime.Now.Date, "", DateTime.Now.Date, 0, "", "");
            this.fillDateFromCurrentCustomer();
        }
    }
}
