using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using WasserWerkVerwaltung.CommonObjects;
using WasserWerkVerwaltung.CommonUtilities;
using WasserWerkVerwaltung.BL;

namespace WasserWerkVerwaltung.GUI {
    public partial class StammdatenControl : UserControl {

        private KundenDetailsControl kundenDetailsControl;
        private KundenSuchenControl kundenSuchenControl;
        //private ZaehlerstaendeControl zaehlerstaendeControl;
        private JahresDatenInfoControl jahresDatenInfoControl;
        private IWWVBL wwvBLComp;

        public StammdatenControl() {
            InitializeComponent();

            this.kundenDetailsControl = new KundenDetailsControl();
            this.kundenDetailsControl.Location = new System.Drawing.Point(200, 200);
            this.Controls.Add(this.kundenDetailsControl);
            this.kundenDetailsControl.ChangeKunde += new ChangeKundeEventHandler(kundenDetailsControl_ChangeKunde);
            this.kundenDetailsControl.NewKunde += new NewKundeEventHandler(kundenDetailsControl_NewKunde);

            this.kundenSuchenControl = new KundenSuchenControl();
            this.kundenSuchenControl.Location = new System.Drawing.Point(200, 0);
            this.Controls.Add(this.kundenSuchenControl);
            this.kundenSuchenControl.KundeGefunden += new KundeGefundenEventHandler(kundenSuchenControl_KundeGefunden);

            //this.zaehlerstaendeControl = new ZaehlerstaendeControl();
            //this.zaehlerstaendeControl.Location = new System.Drawing.Point(580, 0);
            //this.Controls.Add(this.zaehlerstaendeControl);

            this.jahresDatenInfoControl = new JahresDatenInfoControl();
            this.jahresDatenInfoControl.Location = new System.Drawing.Point(720, 0);
            this.Controls.Add(this.jahresDatenInfoControl);
        }

        #region Handler

        void kundenDetailsControl_NewKunde(object sender, EventArgs e) {
            this.listBoxKunden.SelectedItem = null;
            this.jahresDatenInfoControl.Visible = false;
        }

        void kundenDetailsControl_ChangeKunde(object sender, KundeEventArgs e) {
            //Object o = listBoxKunden.SelectedItem;
            updateListBoxKunden();
            listBoxKunden.SelectedItem = e.Kunde;
        }

        void kundenSuchenControl_KundeGefunden(object sender, KundeEventArgs e) {
            this.listBoxKunden.SelectedItem = e.Kunde;
        }

        #endregion

        private void updateListBoxKunden() {
            this.listBoxKunden.Items.Clear();

            this.listBoxKunden.Sorted = false;
            foreach (KundenData kunde in StaticUtilities.SortByNachname(wwvBLComp.GetAllKunden())) {
                this.listBoxKunden.Items.Add(kunde);
            }

            
        }

        public void Init(IWWVBL wwvBLComp) {
            this.wwvBLComp = wwvBLComp;
            this.kundenDetailsControl.Init(wwvBLComp);
            this.kundenSuchenControl.Init(wwvBLComp);
            this.jahresDatenInfoControl.Init(wwvBLComp);
            this.updateListBoxKunden();
        }

        private void listBoxKunden_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.listBoxKunden.SelectedItem != null) {
                this.kundenDetailsControl.SetCurrentCustomer((KundenData)this.listBoxKunden.SelectedItem);
                this.jahresDatenInfoControl.Visible = true;
                this.jahresDatenInfoControl.SetCurrentCustomer((KundenData)this.listBoxKunden.SelectedItem);
            }
        }
    }
}
