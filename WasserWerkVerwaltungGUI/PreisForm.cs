using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WasserWerkVerwaltung.GUI {
    public partial class PreisForm : Form {

        private double preis;
        private long jahr;
        private bool ok;

        public PreisForm() {
            InitializeComponent();
        }

        public void Init(long jahr){
            this.jahr = jahr;
            this.textBoxJahr.Text = jahr.ToString();
        }

        public void Init(long jahr,double alterPreis) {
            Init(jahr);
            textBoxPreis.Text = alterPreis.ToString();
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            textBoxPreis.Text = textBoxPreis.Text.Replace(".", ",");
            try {
                this.preis = Double.Parse(textBoxPreis.Text);
            } catch (FormatException) {
                MessageBox.Show("Format von Preis inkorrekt!");
                return;
            }
            this.ok = true;
            this.Close();
        }

        public bool OK {
            get {
                return this.ok;
            }
        }

        public double Preis {
            get {
                return this.preis;
            }
        }

        private void buttonAbbrechen_Click(object sender, EventArgs e) {
            this.ok = false;
            this.Close();
        }


    }
}