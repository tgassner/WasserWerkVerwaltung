using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WasserWerkVerwaltung.BL;

namespace WasserWerkVerwaltung.GUI {
    public partial class MainForm : Form {

        private StammdatenControl stammdatenControl;
        private DruckenControl druckenControl;
        private IWWVBL wwvBLComp;

        public MainForm() {
            InitializeComponent();

            this.stammdatenControl = new StammdatenControl();
            this.druckenControl = new DruckenControl();
            wwvBLComp = WWVBLFactory.GetBussinessLogicObject();
            this.stammdatenControl.Init(wwvBLComp);
            this.druckenControl.Init(wwvBLComp);
        }

        private void Clear() {
            this.Controls.Clear();
            this.Controls.Add(menuStrip1);
        }

        private void stammdatenpflegeToolStripMenuItem_Click(object sender, EventArgs e) {
            this.stammdatenControl.Location = new System.Drawing.Point(2, 28);
            this.Clear();
            this.Controls.Add(stammdatenControl);
        }

        private void zählerständeToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show("Zählerstände noch nicht implementiert");
        }

        private void druckenToolStripMenuItem_Click(object sender, EventArgs e) {
            this.druckenControl.Location = new System.Drawing.Point(2, 28);
            this.Clear();
            this.Controls.Add(druckenControl);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show("About noch nicht implementiert");
        }
    }
}