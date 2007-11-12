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
        public DruckenControl() {
            InitializeComponent();
        }

        public void Init(IWWVBL wwvBLComp) {
            this.checkedListBoxKunden.Items.Clear();
            foreach (KundenData kunde in wwvBLComp.GetAllKunden()) {
                this.checkedListBoxKunden.Items.Add(kunde);
            }
        }
    }
}
