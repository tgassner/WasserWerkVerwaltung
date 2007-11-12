using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using WasserWerkVerwaltung.CommonObjects;
using WasserWerkVerwaltung.BL;

namespace WasserWerkVerwaltung.GUI {
    public partial class StammdatenControl : UserControl {

        private KundenDetailsControl kundenDetailsControl;
        private KundenSuchenControl kundenSuchenControl;
        private ZaehlerstaendeControl zaehlerstaendeControl;
        private KostenInfoControl kostenInfoControl;

        public StammdatenControl() {
            InitializeComponent();
            
            this.kundenDetailsControl = new KundenDetailsControl();
            this.kundenDetailsControl.Location = new System.Drawing.Point(200, 200);
            this.Controls.Add(this.kundenDetailsControl);

            this.kundenSuchenControl = new KundenSuchenControl();
            this.kundenSuchenControl.Location = new System.Drawing.Point(200, 0);
            this.Controls.Add(this.kundenSuchenControl);

            this.zaehlerstaendeControl = new ZaehlerstaendeControl();
            this.zaehlerstaendeControl.Location = new System.Drawing.Point(580, 0);
            this.Controls.Add(this.zaehlerstaendeControl);

            this.kostenInfoControl = new KostenInfoControl();
            this.kostenInfoControl.Location = new System.Drawing.Point(720,200);
            this.Controls.Add(this.kostenInfoControl);

            Init();
        }

        public void Init() {
            this.listBoxKunden.Items.Clear();
            WWVBusinessComponent bo = new WWVBusinessComponent();
            bo.Trallala();
            //for (int i = 0; i < 300; i++ )
                //this.listBoxKunden.Items.Add(new KundenData(i+1, "Max", "Mustermann"+i.ToString(),"Straße","Ort", "Tel", "Besitzer","Bank",123,456,DateTime.Now,"ZählerNummer", DateTime.Now, "erkl", "hausbesitzer", DateTime.Now, 987, "Zahlung", true));
        }

        private void listBoxKunden_SelectedIndexChanged(object sender, EventArgs e) {
            this.kundenDetailsControl.SetCurrentCustomer((KundenData) this.listBoxKunden.SelectedItem);
        }
    }
}
