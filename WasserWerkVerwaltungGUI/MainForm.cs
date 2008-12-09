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
        private ZaehlerStaendeControl zaehlerStaendeControl;
        private DruckenControl druckenControl;
        private IWWVBL wwvBLComp;

        public MainForm() {
            InitializeComponent();

            this.stammdatenControl = new StammdatenControl();
            this.zaehlerStaendeControl = new ZaehlerStaendeControl();
            this.druckenControl = new DruckenControl();
            wwvBLComp = WWVBLFactory.GetBussinessLogicObject();
            this.stammdatenControl.Init(this.wwvBLComp);
            this.zaehlerStaendeControl.Init(this.wwvBLComp);
            this.druckenControl.Init(this.wwvBLComp);

            this.Text = "Wasser Werk Verwaltung Version " + this.GetType().Assembly.GetName().Version.ToString();
        }

        private void Clear() {
            this.Controls.Clear();
            this.Controls.Add(menuStrip1);
        }

        private void stammdatenpflegeToolStripMenuItem_Click(object sender, EventArgs e) {
            this.stammdatenControl.Location = new System.Drawing.Point(2, 28);
            this.Clear();
            this.Controls.Add(this.stammdatenControl);
            this.stammdatenControl.Init(this.wwvBLComp);
        }

        private void zählerständeToolStripMenuItem_Click(object sender, EventArgs e) {
            this.zaehlerStaendeControl.Location = new System.Drawing.Point(2, 28);
            this.Clear();
            this.Controls.Add(this.zaehlerStaendeControl);
            this.zaehlerStaendeControl.Init(this.wwvBLComp);
        }

        private void druckenToolStripMenuItem_Click(object sender, EventArgs e) {
            this.druckenControl.Location = new System.Drawing.Point(2, 28);
            this.Clear();
            this.Controls.Add(druckenControl);
            this.druckenControl.Init(this.wwvBLComp);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            WWVAboutBox wwvAboutBox = new WWVAboutBox();
            wwvAboutBox.ShowDialog();
        }
    }
}