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
        private IList<ZaehlerStandElementControl> zaehlerStandElementControlList = new List<ZaehlerStandElementControl>();
        private IList<JahresDatenData> jahresDataList = new List<JahresDatenData>();
        
        public ZaehlerStaendDetailsControl() {
            InitializeComponent();
        }

        public void Init(IWWVBL wwvBLComp) {
            this.wwvBLComp = wwvBLComp;
        }

        public void Clear() {
            foreach (ZaehlerStandElementControl zsecl in zaehlerStandElementControlList) {
                this.Controls.Remove(zsecl);
            }
            zaehlerStandElementControlList.Clear();
        }

        public void SetCurrentCustomer(KundenData kunde) {
            this.currentKunde = kunde;
            this.fillJahresDataListOfCurrentCustomer();
            this.fillDataFromCurrentCustomer();
        }

        private void fillJahresDataListOfCurrentCustomer() {
            jahresDataList.Clear();
            foreach(JahresDatenData jdd in this.wwvBLComp.GetJahresdataByKundenID(currentKunde.Id)){
                jahresDataList.Add(jdd);
            }
        }

        private void fillDataFromCurrentCustomer() {
            this.Clear();
            foreach (JahresDatenData jahresDaten in jahresDataList) {
                ZaehlerStandElementControl zsc = new ZaehlerStandElementControl();
                zsc.Init(this.wwvBLComp, jahresDaten, currentKunde);
                zaehlerStandElementControlList.Add(zsc);
            }

            placeZaehlerStandElementControl();
        }

        void placeZaehlerStandElementControl() {
            int pos = 50;

            zaehlerStandElementControlList = SortOperationListByDate(zaehlerStandElementControlList);

            foreach (ZaehlerStandElementControl zsc in zaehlerStandElementControlList) {
                zsc.Location = new System.Drawing.Point(0, pos);
                pos += 144;
            }

            this.Controls.AddRange(((List<ZaehlerStandElementControl>)zaehlerStandElementControlList).ToArray());
        }

        private IList<ZaehlerStandElementControl> SortOperationListByDate(IList<ZaehlerStandElementControl> controls) {
            List<ZaehlerStandElementControl> list = new List<ZaehlerStandElementControl>(controls);
            list.Sort(CompareZaehlerStandElementControlByJahr);
            return list;
        }

        private int CompareZaehlerStandElementControlByJahr(ZaehlerStandElementControl x, ZaehlerStandElementControl y) {
            if (x == null) {
                if (y == null) {
                    return 0;
                } else {
                    return -1;
                }
            } else {
                if (y == null) {
                    return 1;
                } else {
                    return (x.Jahr.CompareTo(y.Jahr) * (-1));
                }
            }
        }

        private bool checkJahr(){
            try {
                Int64.Parse(textBoxJahr.Text);
            } catch (FormatException){ 
                MessageBox.Show("Das Jahr scheint ungültig zu sein!") ;
                return false;
            }
            return true;
        }

        private void buttonJahrHinzufuegen_Click(object sender, EventArgs e) {
            long currentJahr;

            if (!checkJahr())
                return;
            currentJahr = Int64.Parse(textBoxJahr.Text);

            PreisData preis = this.wwvBLComp.GetPreisDataByJahr(currentJahr);
            if (preis == null){
                MessageBox.Show("Für das Jahr ist noch kein Preis festgelegt!");
                PreisForm pf = new PreisForm();
                pf.Init(currentJahr);
                pf.ShowDialog();
                if (!pf.OK){
                    MessageBox.Show("Kein Preis für das Jahr festgelegt speichern abgebrochen!");
                    return;
                }
                preis = new PreisData(currentJahr,pf.Preis);
                
                wwvBLComp.InsertPreis(preis);
                preis = new PreisData(currentJahr,pf.Preis);
                if (preis == null){
                    MessageBox.Show("Preis konnte nicht erstellt werden!");
                    return;
                }
            }
            
            JahresDatenData jahresDatenData = new JahresDatenData(0,currentKunde.Id,0,0,0,currentJahr,DateTime.Now,0);
            jahresDataList.Add(jahresDatenData);

            foreach (ZaehlerStandElementControl zsecl in zaehlerStandElementControlList) {
                this.Controls.Remove(zsecl);
            }

            ZaehlerStandElementControl zsc = new ZaehlerStandElementControl();
            zsc.Init(this.wwvBLComp, jahresDatenData, currentKunde);
            zaehlerStandElementControlList.Add(zsc);

            this.placeZaehlerStandElementControl();
        }

        private void buttonJahrLoeschen_Click(object sender, EventArgs e) {
            if (!checkJahr())
                return;

            long currentJahr = Int64.Parse(textBoxJahr.Text);
            JahresDatenData jddel = null;

            foreach (JahresDatenData jdd in jahresDataList) {
                if (jdd.Jahr == currentJahr) {
                    if (jdd.Id == 0) { // noch nicht in der DB
                        jddel = jdd;
                    } else { // muß aus der DB gelöscht werden.
                        if (!wwvBLComp.DeleteJahresDaten(jdd.Id)) {
                            MessageBox.Show("Jahresdaten konnten nicht gelöscht werden.");
                            return;
                        }
                        jddel = jdd;
                    }
                }
            }

            ZaehlerStandElementControl zseldel = null;

            foreach (ZaehlerStandElementControl zsel in zaehlerStandElementControlList) {
                if (zsel.Jahr == currentJahr) {
                    zseldel = zsel;
                }
            }

            if (jddel != null && zseldel != null){
                foreach (ZaehlerStandElementControl zsecl in zaehlerStandElementControlList) {
                    this.Controls.Remove(zsecl);
                }

                jahresDataList.Remove(jddel);
                zaehlerStandElementControlList.Remove(zseldel);
                this.placeZaehlerStandElementControl();
            }
        }

        private void buttonPreisImJahrAendern_Click(object sender, EventArgs e) {
            if (!checkJahr())
                return;

            long currentJahr;
            currentJahr = Int64.Parse(textBoxJahr.Text);

            PreisForm pf = new PreisForm();
            PreisData pd = wwvBLComp.GetPreisDataByJahr(currentJahr);
            if (pd == null) {
                pf.Init(currentJahr);
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
    }
}