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

        //private IWWVBL wwvBLComp;

        public DruckenControl() {
            InitializeComponent();
        }

        public void Init(IWWVBL wwvBLComp) {
            this.checkedListBoxKunden.Items.Clear();
            foreach (KundenData kunde in wwvBLComp.GetAllKunden()) {
                this.checkedListBoxKunden.Items.Add(kunde);
            }
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
            MessageBox.Show("Noch nicht implementiert!");
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
    }
}
