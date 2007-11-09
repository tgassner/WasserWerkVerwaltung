using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using WasserWerkVerwaltung.CommonObjects;

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
            //this.textBoxNachname.Text = currentCustomer.Name;
            this.textBoxStrasse.Text = currentCustomer.Strasse;
            //this.textBoxObjekt.Text = currentCustomer.Objekt;
            this.textBoxOrt.Text = currentCustomer.Ort;
            this.textBoxTel.Text = currentCustomer.Tel;
            this.textBoxBankverbindung.Text = currentCustomer.BankVerbindung;
            //this.textBoxTzEinbau.Text = currentCustomer.TzEinbau.ToString();
            //this.textBoxTzNeu.Text = currentCustomer.TzNeu.ToString();
            this.textBoxEichdatum.Text = currentCustomer.EichDatum.ToString();
            this.textBoxZaehlerNummer.Text = currentCustomer.ZaehlerNummer;
            this.textBoxEinbaudatum.Text = currentCustomer.EinbauDatum.ToString();
            this.textBoxErkl.Text = currentCustomer.Erkl;
            this.textBoxHausbesitzer.Text = currentCustomer.Hausbesitzer;
            this.textBoxTauschdatum.Text = currentCustomer.TauschDatum.ToString();
            this.textBoxZaehlermiete.Text = currentCustomer.Zaehlermiete.ToString();
            //this.textBoxZahlung.Text = currentCustomer.Zahlung;
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
            MessageBox.Show("neuer Kunde noch nicht implementiert");
        }
    }
}
