using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using WasserWerkVerwaltung.BL;
using WasserWerkVerwaltung.CommonObjects;
using WasserWerkVerwaltung.CommonUtilities;
using System.Collections;

namespace WasserWerkVerwaltung.GUI {
    public partial class ZaehlerStaendeControl : UserControl {

        private IWWVBL wwvBLComp;
        private ZaehlerStaendDetailsControl zaehlerStaendDetailsControl;

        public ZaehlerStaendeControl() {
            InitializeComponent();
            this.zaehlerStaendDetailsControl = new ZaehlerStaendDetailsControl();
            this.zaehlerStaendDetailsControl.Location = new System.Drawing.Point(400, 0);
            this.Controls.Add(this.zaehlerStaendDetailsControl);
        }

        public void Init(IWWVBL wwvBLComp) {
            this.wwvBLComp = wwvBLComp;
            this.zaehlerStaendDetailsControl.Init(this.wwvBLComp);
            this.updateListBoxKunden();
            this.zaehlerStaendDetailsControl.Clear();
            this.zaehlerStaendDetailsControl.Enabled = false;
            this.listBoxSuchen.Items.Clear();
            this.textBoxSuchen.Text = "";
        }

        private void updateListBoxKunden() {
            this.listBoxKunden.Items.Clear();
            this.listBoxKunden.Sorted = false;
            foreach (KundenData kunde in StaticUtilities.SortByNachname(wwvBLComp.GetAllKunden(),true)) {
                this.listBoxKunden.Items.Add(kunde);
            }
            this.comboBoxJahr.Items.Clear();
            long hoechstesJahr = Int64.MinValue;
            foreach (PreisData preis in this.wwvBLComp.GetAllPreise()) {
                this.comboBoxJahr.Items.Add(preis.Jahr);
                if (preis.Jahr > hoechstesJahr) {
                    hoechstesJahr = preis.Jahr;
                }
            }
            this.comboBoxJahr.Text = hoechstesJahr.ToString();
            this.listBoxSuchen.Items.Clear();
            this.textBoxSuchen.Text = "";
        }

        private void listBoxKunden_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.listBoxKunden.SelectedItem != null) {
                this.zaehlerStaendDetailsControl.SetCurrentCustomer((KundenData)this.listBoxKunden.SelectedItem);
                this.zaehlerStaendDetailsControl.Enabled = true;
            }
        }

        private void buttonSuchen_Click(object sender, EventArgs e) {
            if (textBoxSuchen.Text.Equals("")){
                MessageBox.Show("Bitte einen Suchpattern eintragen!");
                return;
            }
            this.listBoxSuchen.Items.Clear();

            foreach (KundenData kunde in this.listBoxKunden.Items) {
                if (kunde.Vorname.ToLower().Contains(textBoxSuchen.Text.ToLower()) || (kunde.Nachname.ToLower().Contains(textBoxSuchen.Text.ToLower()))){
                    this.listBoxSuchen.Items.Add(kunde);
                }
            }
        }

        private void buttonFilter_Click(object sender, EventArgs e) {
            this.listBoxKunden.Items.Clear();
            this.listBoxKunden.Sorted = false;

            long jahr;
            try {
                jahr = Int64.Parse(this.comboBoxJahr.Text);
            } catch (FormatException) {
                MessageBox.Show("Bitte Jahr überprüfen: Wert ungültig.");
                return;
            }

            foreach (KundenData kunde in StaticUtilities.SortByNachname(wwvBLComp.GetAllKunden(),true)) {
                if (!wwvBLComp.hasKundeJahresdataByJahr(kunde, jahr)) {
                    this.listBoxKunden.Items.Add(kunde);
                }
            }
        }

        private void listBoxSuchen_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.listBoxSuchen.Items.Count == 0) {
                MessageBox.Show("Kein Kunde in der Auswahl!");
                return;
            }
            this.listBoxKunden.SelectedItem = (KundenData)this.listBoxSuchen.SelectedItem;
        }

        private void buttonReset_Click(object sender, EventArgs e) {
            this.listBoxKunden.Items.Clear();
            this.listBoxSuchen.Items.Clear();
            this.textBoxSuchen.Text = "";
            foreach (KundenData kunde in StaticUtilities.SortByNachname(wwvBLComp.GetAllKunden(),true)) {
                this.listBoxKunden.Items.Add(kunde);
            }
        }

        private void buttonGeneriereMehrereHalbjahresWerte_Click(object sender, EventArgs e) {
            long jahr;
            try {
                jahr = Int64.Parse(textBoxJahr.Text);
            } catch (Exception){
                MessageBox.Show("Das Format des Jahres ist ungültig");
                return;
            }
            this.wwvBLComp.GenerateHalbJahresBetragFuerJahr(jahr);
        }

        private void buttonPreisImJahrAendern_Click(object sender, EventArgs e) {
            long currentJahr;

            try {
                currentJahr = Int64.Parse(textBoxJahrWasserpreis.Text);
            } catch (FormatException) {
                MessageBox.Show("Das Format vom Jahr scheint ungültig zu sein!");
                return;
            }            

            PreisForm pf = new PreisForm();
            PreisData pd = wwvBLComp.GetPreisDataByJahr(currentJahr);
            if (pd == null) {
                MessageBox.Show("Für dieses Jahr gibt es noch keinen Preis - bitte vor dem ändern anlegen");
                return;
            } else {
                pf.Init(currentJahr, wwvBLComp.GetPreisDataByJahr(currentJahr).Preis);
            }
            pf.ShowDialog();
            if (!pf.OK) {
                MessageBox.Show("Kein neuer Preis für das Jahr festgelegt speichern abgebrochen!");
                return;
            }
            PreisData preis = new PreisData(currentJahr, pf.Preis);

            if (!wwvBLComp.UpdatePreis(preis)) {
                MessageBox.Show("Preis konnte nicht gespeichert werden!");
                return;
            }
        }

        private void buttonJahreswasserpreisErstellen_Click(object sender, EventArgs e) {
            long currentJahr;

            try {
                currentJahr = Int64.Parse(textBoxJahrWasserpreis.Text);
            } catch (FormatException) {
                MessageBox.Show("Das Format vom Jahr scheint ungültig zu sein!");
                return;
            }   
            
            PreisData preis = this.wwvBLComp.GetPreisDataByJahr(currentJahr);
            if (preis == null) {
                PreisForm pf = new PreisForm();
                pf.Init(currentJahr);
                pf.ShowDialog();
                if (!pf.OK) {
                    MessageBox.Show("Kein Preis für das Jahr festgelegt speichern abgebrochen!");
                    return;
                }
                preis = new PreisData(currentJahr, pf.Preis);

                wwvBLComp.InsertPreis(preis);
                preis = new PreisData(currentJahr, pf.Preis);
                if (preis == null) {
                    MessageBox.Show("Preis konnte nicht erstellt werden!");
                }
            }
        }
    }
}
