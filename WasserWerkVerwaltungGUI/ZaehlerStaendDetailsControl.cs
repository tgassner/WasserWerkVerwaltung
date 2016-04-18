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
            this.textBoxJahr.Text = "";
            this.button_QuickJahrHinzufuegen.Text = "Jahr " + DateTime.Now.Year + " (beim Kunden) hinzuf�gen";
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
                pos += 165;
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
                MessageBox.Show("Das Jahr scheint ung�ltig zu sein!") ;
                return false;
            }
            return true;
        }

        private void button_QuickJahrHinzufuegen_Click(object sender, EventArgs e)
        {
            jahrHinzufuegen(DateTime.Now.Year);
        }

        private void buttonJahrHinzufuegen_Click(object sender, EventArgs e) {
            long currentJahr;

            if (!checkJahr())
                return;
            currentJahr = Int64.Parse(textBoxJahr.Text);
            jahrHinzufuegen(currentJahr);
        }

        private void jahrHinzufuegen(long currentJahr) {
            PreisData preis = this.wwvBLComp.GetPreisDataByJahr(currentJahr);
            if (preis == null)
            {
                MessageBox.Show("F�r das Jahr ist noch kein Preis festgelegt!");
                PreisForm pf = new PreisForm();
                pf.Init(currentJahr);
                pf.ShowDialog();
                if (!pf.OK)
                {
                    MessageBox.Show("Kein Preis f�r das Jahr festgelegt speichern abgebrochen!");
                    return;
                }
                preis = new PreisData(currentJahr, pf.Preis);

                wwvBLComp.InsertPreis(preis);
                preis = new PreisData(currentJahr, pf.Preis);
                if (preis == null)
                {
                    MessageBox.Show("Preis konnte nicht erstellt werden!");
                    return;
                }
            }

            JahresDatenData jddOld = this.wwvBLComp.GetJahresdataByKundenIDandYear(currentKunde.Id, currentJahr);
            if (jddOld != null) {
                MessageBox.Show("F�r das Jahr " + currentJahr + " und den Kunden " + currentKunde.Vorname + " " + currentKunde.Nachname + " ist bereits ein Jahr angelegt!");
                return;
            }

            foreach (ZaehlerStandElementControl zscLoop in zaehlerStandElementControlList) {
                if (zscLoop.Jahr == currentJahr) {
                    MessageBox.Show("F�r das Jahr " + currentJahr + " und den Kunden " + currentKunde.Vorname + " " + currentKunde.Nachname + " ist bereits ein Jahr hinzugef�gt!\r\nBitte speicherm!!");
                    return;
                }
            }

            JahresDatenData jahresDatenData = new JahresDatenData(0, currentKunde.Id, 0, 0, currentJahr, DateTime.Now, 0.0, 0, 0, "", 0.0, 0.0, new DateTime(1901, 1, 1), new DateTime(1901, 1, 1), null, null);  // FIXME TODO
            jahresDataList.Add(jahresDatenData);

            foreach (ZaehlerStandElementControl zsecl in zaehlerStandElementControlList)
            {
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
                    } else { // mu� aus der DB gel�scht werden.
                        if (!wwvBLComp.DeleteJahresDaten(jdd.Id)) {
                            MessageBox.Show("Jahresdaten konnten nicht gel�scht werden.");
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
    }
}