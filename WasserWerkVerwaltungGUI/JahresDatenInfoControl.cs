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

            this.labelRechnungssumme.Text = "";
            this.labelRechnungssummehalbe.Text = "";
            this.labelZaehlerStandAlt.Text = "";
            this.labelZaehlerstandNeu.Text = "";
            this.labelAbleseDatum.Text = "";
            this.labelJahr.Text = "";
            this.labelBereitsBezahlt.Text = "";
            this.labelRechnungsSummeMinusBereitsBezahlt.Text = "";
        }

        public void SetCurrentCustomer(KundenData kunde) {
            this.currentKunde = kunde;
            this.fillDataFromCurrentCustomer();
        }

        private void fillDataFromCurrentCustomer() {
            this.listBoxJahre.Items.Clear();
            IList<JahresDatenData> jahresDatenList = wwvBLComp.GetJahresdataByKundenID(currentKunde.Id);
            foreach (JahresDatenData jdd in jahresDatenList) {
                listBoxJahre.Items.Add(jdd);
            }
        }

        private void listBoxJahre_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.listBoxJahre.SelectedItem != null) {

                //this.labelRechnungssumme.Text = wwvBLComp.calcJahresrechnungBrutto().ToString();//((JahresDatenData)this.listBoxJahre.SelectedItem).Rechnungssumme.ToString();
                //this.labelRechnungssummehalbe.Text = (((JahresDatenData)this.listBoxJahre.SelectedItem).Rechnungssumme / 2).ToString();
                this.labelZaehlerStandAlt.Text = ((JahresDatenData)this.listBoxJahre.SelectedItem).ZaehlerStandAlt.ToString();
                this.labelZaehlerstandNeu.Text = ((JahresDatenData)this.listBoxJahre.SelectedItem).ZaehlerStandNeu.ToString();
                this.labelAbleseDatum.Text = ((JahresDatenData)this.listBoxJahre.SelectedItem).AbleseDatum.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo);
                this.labelJahr.Text = ((JahresDatenData)this.listBoxJahre.SelectedItem).Jahr.ToString();
                this.labelBereitsBezahlt.Text = ((JahresDatenData)this.listBoxJahre.SelectedItem).BereitsBezahlt.ToString();
                //this.labelRechnungsSummeMinusBereitsBezahlt.Text = (((JahresDatenData)this.listBoxJahre.SelectedItem).Rechnungssumme - ((JahresDatenData)this.listBoxJahre.SelectedItem).BereitsBezahlt).ToString();

            }
        }
    }
}
