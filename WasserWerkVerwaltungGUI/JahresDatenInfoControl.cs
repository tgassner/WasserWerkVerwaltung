using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using WasserWerkVerwaltung.BL;
using WasserWerkVerwaltung.CommonObjects;
using System.Globalization;
using WasserWerkVerwaltung.CommonUtilities;

namespace WasserWerkVerwaltung.GUI {
    public partial class JahresDatenInfoControl : UserControl {

        private IWWVBL wwvBLComp;
        private KundenData currentKunde;
        
        public JahresDatenInfoControl() {
            InitializeComponent();
        }

        public void Init(IWWVBL wwvBLComp) {
            this.wwvBLComp = wwvBLComp;
            this.Enabled = true;
            this.Visible = true;

            this.Clear();
        }

        public void SetCurrentCustomer(KundenData kunde) {
            this.currentKunde = kunde;
            this.fillDataFromCurrentCustomer();
        }

        private void fillDataFromCurrentCustomer() {
            this.listBoxJahre.Items.Clear();
            IList<JahresDatenData> jahresDatenList = wwvBLComp.GetJahresdataByKundenID(currentKunde.Id);
            foreach (JahresDatenData jdd in StaticUtilities.SortJahresDataByJahr(jahresDatenList,false)) {
                listBoxJahre.Items.Add(jdd);
            }
            if (listBoxJahre.Items.Count > 0) {
                listBoxJahre.SelectedIndex = 0;
            }
        }

        private void listBoxJahre_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.listBoxJahre.SelectedItem != null) {
                JahresDatenData jdd = (JahresDatenData)this.listBoxJahre.SelectedItem;
                this.labelRechnungssumme.Text = wwvBLComp.calcJahresrechnungBrutto(jdd, this.currentKunde, this.wwvBLComp.GetPreisDataByJahr(jdd.Jahr)).ToString();//((JahresDatenData)this.listBoxJahre.SelectedItem).Rechnungssumme.ToString();
                this.labelRechnungssummehalbe.Text = jdd.HalbJahresBetrag.ToString();
                this.labelZaehlerStandAlt.Text = jdd.ZaehlerStandAlt.ToString();
                this.labelZaehlerstandNeu.Text = jdd.ZaehlerStandNeu.ToString();
                this.labelTauschzaehlerstandAlt.Text = jdd.TauschZaehlerStandAlt.ToString();
                this.labelTauschzaehlerstandNeu.Text = jdd.TauschZaehlerStandNeu.ToString();
                this.labelAbleseDatum.Text = jdd.AbleseDatum.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo);
                this.labelSonstigeForderungenText.Text = jdd.SonstigeForderungenText;
                this.labelSonstigeForderungenValue.Text = jdd.SonstigeForderungenValue.ToString();
                this.labelJahr.Text = jdd.Jahr.ToString();
                this.labelBereitsBezahlt.Text = jdd.BereitsBezahlt.ToString();
                this.labelRechnungsSummeMinusBereitsBezahlt.Text = (wwvBLComp.calcJahresrechnungBrutto(jdd, this.currentKunde, this.wwvBLComp.GetPreisDataByJahr(jdd.Jahr)) - jdd.BereitsBezahlt).ToString(); //(((JahresDatenData)this.listBoxJahre.SelectedItem).Rechnungssumme - ((JahresDatenData)this.listBoxJahre.SelectedItem).BereitsBezahlt).ToString();
            }
        }

        internal void Clear() {
            this.listBoxJahre.Items.Clear();
            this.labelRechnungssumme.Text = String.Empty;
            this.labelRechnungssummehalbe.Text = String.Empty;
            this.labelZaehlerStandAlt.Text = String.Empty;
            this.labelZaehlerstandNeu.Text = String.Empty;
            this.labelTauschzaehlerstandAlt.Text = String.Empty;
            this.labelTauschzaehlerstandNeu.Text = String.Empty;
            this.labelAbleseDatum.Text = String.Empty;
            this.labelSonstigeForderungenText.Text = String.Empty;
            this.labelSonstigeForderungenValue.Text = String.Empty;
            this.labelJahr.Text = String.Empty;
            this.labelBereitsBezahlt.Text = String.Empty;
            this.labelRechnungsSummeMinusBereitsBezahlt.Text = String.Empty;
        }
    }
}
