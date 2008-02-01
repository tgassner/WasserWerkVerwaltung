using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using WasserWerkVerwaltung.BL;
using WasserWerkVerwaltung.CommonObjects;

namespace WasserWerkVerwaltung.GUI {

    public delegate void KundeGefundenEventHandler(object sender, KundeEventArgs e);

    public partial class KundenSuchenControl : UserControl {

        private IWWVBL wwvBLComp;

        public event KundeGefundenEventHandler KundeGefunden;

        public KundenSuchenControl() {
            InitializeComponent();
        }

        public void Init(IWWVBL wwvBLComp) {
            this.textBoxNamenSuchen.Clear();
            this.listBoxKundenSuchenErgebnisse.Items.Clear();
            this.wwvBLComp = wwvBLComp;
        }

        private void doSearchName() {
            listBoxKundenSuchenErgebnisse.Items.Clear();
            string pattern = textBoxNamenSuchen.Text.ToLower();
            string[] patterns = pattern.Split(' ');
            foreach (KundenData kunde in wwvBLComp.GetAllKunden()) {
                string toSearchIn = kunde.Vorname.ToLower() + " " + kunde.Nachname.ToLower();
                bool found = true;
                foreach (string patt in patterns) {
                    if (!toSearchIn.Contains(patt)) {
                        found = false;
                    }
                }
                if (found) {
                    listBoxKundenSuchenErgebnisse.Items.Add(kunde);
                }
            }
        }

        private void doSearchObjekt() {
            listBoxKundenSuchenErgebnisse.Items.Clear();
            string pattern = textBoxObjektSuchen.Text.ToLower();
            string[] patterns = pattern.Split(' ');
            foreach (KundenData kunde in wwvBLComp.GetAllKunden()) {
                string toSearchIn = kunde.Objekt.ToLower();//kunde.Vorname.ToLower() + " " + kunde.Nachname.ToLower();
                bool found = true;
                foreach (string patt in patterns) {
                    if (!toSearchIn.Contains(patt)) {
                        found = false;
                    }
                }
                if (found) {
                    listBoxKundenSuchenErgebnisse.Items.Add(kunde);
                }
            }
        }

        private void buttonSuchen_Click(object sender, EventArgs e) {
            doSearchName();
        }

        private void textBoxSuchen_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                doSearchName();
            }
        }

        private void buttonObjektSuchen_Click(object sender, EventArgs e) {
            doSearchObjekt();
        }

        private void textBoxObjektSuchErgebnisse_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                doSearchObjekt();
            }
        }

        private void doAuswaehlen() {
            if (this.listBoxKundenSuchenErgebnisse.Items.Count == 0) {
                MessageBox.Show("Kein Kunde in der Auswahl!");
                return;
            }
            if (!(this.listBoxKundenSuchenErgebnisse.SelectedItems.Count > 0)) {
                MessageBox.Show("Bitte einen Kunden auswählen");
                return;
            }
            this.KundeGefunden(this, new KundeEventArgs((KundenData)this.listBoxKundenSuchenErgebnisse.SelectedItem));
        }

        private void buttonAuswaehlen_Click(object sender, EventArgs e) {
            doAuswaehlen();
        }

        private void listBoxKundenSuchenErgebnisse_DoubleClick(object sender, EventArgs e) {
            doAuswaehlen();
        }        
    }

    public class KundeEventArgs : EventArgs {
        private KundenData kunde;
        public KundeEventArgs(KundenData kunde) {
            this.kunde = kunde;
        }
        public KundenData Kunde {
            get {
                return this.kunde;
            }
        }
    }
}
