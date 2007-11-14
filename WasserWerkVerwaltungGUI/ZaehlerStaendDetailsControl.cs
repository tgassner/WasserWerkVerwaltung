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
    public partial class ZaehlerStaendDetailsControl : UserControl {

        private IWWVBL wwvBLComp;
        private KundenData currentKunde;
        private List<ZaehlerStandElementControl> zaehlerStandElementControlList = new List<ZaehlerStandElementControl>();
        
        public ZaehlerStaendDetailsControl() {
            InitializeComponent();
        }

        public void Init(IWWVBL wwvBLComp) {
            this.wwvBLComp = wwvBLComp;
        }

        public void SetCurrentCustomer(KundenData kunde) {
            this.currentKunde = kunde;
            this.fillDataFromCurrentCustomer();
        }

        private void fillDataFromCurrentCustomer() {
            foreach (ZaehlerStandElementControl zsecl in zaehlerStandElementControlList) {
                this.Controls.Remove(zsecl);
            }
            zaehlerStandElementControlList.Clear();
            foreach (JahresDatenData jahresDaten in this.wwvBLComp.GetJahresdataByKundenID(currentKunde.Id)) {
                ZaehlerStandElementControl zsc = new ZaehlerStandElementControl();
                zsc.Init(this.wwvBLComp, jahresDaten);
                zaehlerStandElementControlList.Add(zsc);
            }

            int pos = 40;

            foreach (ZaehlerStandElementControl zsc in zaehlerStandElementControlList) {
                zsc.Location = new System.Drawing.Point(0, pos);
                pos += 40;
            }

            this.Controls.AddRange(zaehlerStandElementControlList.ToArray());
        }

        private void buttonJahrHinzufuegen_Click(object sender, EventArgs e) {
            MessageBox.Show("Hinzufügen nicht implementiert");
        }

        private void buttonJahrLoeschen_Click(object sender, EventArgs e) {
            MessageBox.Show("löschen nicht implementiert");
        }
    }
}