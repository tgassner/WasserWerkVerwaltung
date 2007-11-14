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
    public partial class ZaehlerStaendeControl : UserControl {

        private IWWVBL wwvBLComp;
        private ZaehlerStaendDetailsControl zaehlerStaendDetailsControl;

        public ZaehlerStaendeControl() {
            InitializeComponent();
            this.zaehlerStaendDetailsControl = new ZaehlerStaendDetailsControl();
            this.zaehlerStaendDetailsControl.Location = new System.Drawing.Point(200, 0);
            this.Controls.Add(this.zaehlerStaendDetailsControl);
        }

        public void Init(IWWVBL wwvBLComp) {
            this.wwvBLComp = wwvBLComp;
            this.zaehlerStaendDetailsControl.Init(this.wwvBLComp);
            this.updateListBoxKunden();
            this.zaehlerStaendDetailsControl.Clear();
            this.zaehlerStaendDetailsControl.Enabled = false;
        }

        private void updateListBoxKunden() {
            this.listBoxKunden.Items.Clear();
            foreach (KundenData kunde in wwvBLComp.GetAllKunden()) {
                this.listBoxKunden.Items.Add(kunde);
            }
        }

        private void listBoxKunden_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.listBoxKunden.SelectedItem != null) {
                this.zaehlerStaendDetailsControl.SetCurrentCustomer((KundenData)this.listBoxKunden.SelectedItem);
                this.zaehlerStaendDetailsControl.Enabled = true;
            }
        }
    }
}
