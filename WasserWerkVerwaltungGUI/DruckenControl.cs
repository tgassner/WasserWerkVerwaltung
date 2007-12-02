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
    public partial class DruckenControl : UserControl {

        private IWWVBL wwvBLComp;

        public DruckenControl() {
            InitializeComponent();
        }

        public void Init(IWWVBL wwvBLComp) {
            this.wwvBLComp = wwvBLComp;
            
            this.listBoxJahre.Items.Clear();

            foreach (PreisData preis in wwvBLComp.GetAllPreise()) {
                listBoxJahre.Items.Add(preis);
            }
            if (listBoxJahre.Items.Count > 0) {
                listBoxJahre.SelectedIndex = 0;
            }
        }

        private void fillKundenDieJahresDataEintragHaben() {
            this.checkedListBoxKunden.Items.Clear();

            if (listBoxJahre.SelectedItems.Count != 1) {
                MessageBox.Show("Bitte ein Jahr auswählen");
                return;
            }

            PreisData pd = listBoxJahre.SelectedItem as PreisData;

            foreach (KundenData kunde in wwvBLComp.GetAllKunden()) {
                
                if (this.wwvBLComp.hasKundeJahresdataByPreis(kunde,pd)){
                    this.checkedListBoxKunden.Items.Add(kunde);
                }
            }
        }

        private void listBoxJahre_SelectedValueChanged(object sender, EventArgs e) {
            fillKundenDieJahresDataEintragHaben();
        }



        private void buttonAlleSelektieren_Click(object sender, EventArgs e) {
            for (int i = 0; i < checkedListBoxKunden.Items.Count; i++) {
                checkedListBoxKunden.SetItemChecked(i, true);
            }
        }

        private void buttonAlleSelektierungenAufheben_Click(object sender, EventArgs e) {
            for (int i = 0; i < checkedListBoxKunden.Items.Count; i++) {
                checkedListBoxKunden.SetItemChecked(i, false);
            }
        }

        private void buttonAlleSelektierenDieEineJahresRechnungBekommen_Click(object sender, EventArgs e) {
            for (int i = 0; i < checkedListBoxKunden.Items.Count; i++) {
                if (((KundenData)checkedListBoxKunden.Items[i]).BekommtRechnung == Rechnung.Jahres) {
                    checkedListBoxKunden.SetItemChecked(i, true);
                } else {
                    checkedListBoxKunden.SetItemChecked(i, false);
                }
            }
        }

        private void buttonAlleSelektierenDieEineHalbJahresRechnungBekommen_Click(object sender, EventArgs e) {
            for (int i = 0; i < checkedListBoxKunden.Items.Count; i++) {
                if (((KundenData)checkedListBoxKunden.Items[i]).BekommtRechnung == Rechnung.Halbjahres) {
                    checkedListBoxKunden.SetItemChecked(i, true);
                } else {
                    checkedListBoxKunden.SetItemChecked(i, false);
                }
            }
        }

        private void buttonGanzJahresRechnungDrucken_Click(object sender, EventArgs e) {
            if (checkedListBoxKunden.CheckedItems.Count <= 0) {
                MessageBox.Show("Keine Kunden markiert!");
                return;
            }
            PreisData pd = listBoxJahre.SelectedItem as PreisData;

            IList<KundenData> selectedKundenList = new List<KundenData>();
            foreach (KundenData kunde in checkedListBoxKunden.CheckedItems) {
                selectedKundenList.Add(kunde);
            }

            this.wwvBLComp.PrintJahresRechnungen(selectedKundenList, (PreisData)listBoxJahre.SelectedItem);
        }

        private void buttonHalbJahresRechnungDrucken_Click(object sender, EventArgs e) {
            MessageBox.Show("Noch nicht implementiert!");
        }

        private void buttonBezahltchecklisteDrucken_Click(object sender, EventArgs e) {
            MessageBox.Show("Noch nicht implementiert!");
        }

        private void buttonAuszugDrucken_Click(object sender, EventArgs e) {
            MessageBox.Show("Noch nicht implementiert!");
        }

        private void buttonKontrollzettelDrucken_Click(object sender, EventArgs e) {
            MessageBox.Show("Noch nicht implementiert!");
        }

        private void buttonHalbJahresRechnungPart2Drucken_Click(object sender, EventArgs e) {
            MessageBox.Show("Noch nicht implementiert!");
        }
    }
}
