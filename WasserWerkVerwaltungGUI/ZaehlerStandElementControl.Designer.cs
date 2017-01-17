namespace WasserWerkVerwaltung.GUI {
    partial class ZaehlerStandElementControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label6 = new System.Windows.Forms.Label();
            this.buttonVomVorjahr = new System.Windows.Forms.Button();
            this.textBoxZaehlerStandAlt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxHalbJahresRechnungsNummer = new System.Windows.Forms.TextBox();
            this.textBoxGanzJahresRechnungsNummer = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonCalcBereitsBezahlt = new System.Windows.Forms.Button();
            this.textBoxBereitsBezahltSummand = new System.Windows.Forms.TextBox();
            this.buttonBerechneHalbJahresBetrag = new System.Windows.Forms.Button();
            this.buttonDruckJahresrechnung = new System.Windows.Forms.Button();
            this.textBoxNichtGespeichert = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxBereitsbezahlt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxAblesedatum = new System.Windows.Forms.TextBox();
            this.textBoxSonstigeForderungenWert = new System.Windows.Forms.TextBox();
            this.textBoxHalbJahresBetrag = new System.Windows.Forms.TextBox();
            this.textBoxTauschZaehlerstandNeu = new System.Windows.Forms.TextBox();
            this.textBoxZaehlerStandNeu = new System.Windows.Forms.TextBox();
            this.buttonRestore = new System.Windows.Forms.Button();
            this.buttonSpeichern = new System.Windows.Forms.Button();
            this.textBoxSonstigeForderungenText = new System.Windows.Forms.TextBox();
            this.textBoxTauschZaehlerstandAlt = new System.Windows.Forms.TextBox();
            this.labelHalbJahresRechnungJah = new System.Windows.Forms.Label();
            this.labelGanzJahresRechnungJah = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Zählerstand alt:";
            // 
            // buttonVomVorjahr
            // 
            this.buttonVomVorjahr.Location = new System.Drawing.Point(64, 8);
            this.buttonVomVorjahr.Name = "buttonVomVorjahr";
            this.buttonVomVorjahr.Size = new System.Drawing.Size(107, 20);
            this.buttonVomVorjahr.TabIndex = 0;
            this.buttonVomVorjahr.Text = "Stand vom Vorjahr";
            this.buttonVomVorjahr.UseVisualStyleBackColor = true;
            this.buttonVomVorjahr.Click += new System.EventHandler(this.buttonVomVorjahr_Click);
            // 
            // textBoxZaehlerStandAlt
            // 
            this.textBoxZaehlerStandAlt.Location = new System.Drawing.Point(89, 30);
            this.textBoxZaehlerStandAlt.Name = "textBoxZaehlerStandAlt";
            this.textBoxZaehlerStandAlt.Size = new System.Drawing.Size(98, 20);
            this.textBoxZaehlerStandAlt.TabIndex = 0;
            this.textBoxZaehlerStandAlt.TextChanged += new System.EventHandler(this.textChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelGanzJahresRechnungJah);
            this.groupBox1.Controls.Add(this.labelHalbJahresRechnungJah);
            this.groupBox1.Controls.Add(this.textBoxHalbJahresRechnungsNummer);
            this.groupBox1.Controls.Add(this.textBoxGanzJahresRechnungsNummer);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.buttonCalcBereitsBezahlt);
            this.groupBox1.Controls.Add(this.textBoxBereitsBezahltSummand);
            this.groupBox1.Controls.Add(this.buttonBerechneHalbJahresBetrag);
            this.groupBox1.Controls.Add(this.buttonDruckJahresrechnung);
            this.groupBox1.Controls.Add(this.textBoxNichtGespeichert);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxBereitsbezahlt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxAblesedatum);
            this.groupBox1.Controls.Add(this.textBoxSonstigeForderungenWert);
            this.groupBox1.Controls.Add(this.textBoxHalbJahresBetrag);
            this.groupBox1.Controls.Add(this.textBoxTauschZaehlerstandNeu);
            this.groupBox1.Controls.Add(this.textBoxZaehlerStandNeu);
            this.groupBox1.Controls.Add(this.buttonRestore);
            this.groupBox1.Controls.Add(this.buttonSpeichern);
            this.groupBox1.Controls.Add(this.textBoxSonstigeForderungenText);
            this.groupBox1.Controls.Add(this.buttonVomVorjahr);
            this.groupBox1.Controls.Add(this.textBoxTauschZaehlerstandAlt);
            this.groupBox1.Controls.Add(this.textBoxZaehlerStandAlt);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 160);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // textBoxHalbJahresRechnungsNummer
            // 
            this.textBoxHalbJahresRechnungsNummer.Location = new System.Drawing.Point(178, 133);
            this.textBoxHalbJahresRechnungsNummer.Name = "textBoxHalbJahresRechnungsNummer";
            this.textBoxHalbJahresRechnungsNummer.Size = new System.Drawing.Size(100, 20);
            this.textBoxHalbJahresRechnungsNummer.TabIndex = 18;
            this.textBoxHalbJahresRechnungsNummer.TextChanged += new System.EventHandler(this.textChanged);
            // 
            // textBoxGanzJahresRechnungsNummer
            // 
            this.textBoxGanzJahresRechnungsNummer.Location = new System.Drawing.Point(464, 133);
            this.textBoxGanzJahresRechnungsNummer.Name = "textBoxGanzJahresRechnungsNummer";
            this.textBoxGanzJahresRechnungsNummer.Size = new System.Drawing.Size(100, 20);
            this.textBoxGanzJahresRechnungsNummer.TabIndex = 17;
            this.textBoxGanzJahresRechnungsNummer.TextChanged += new System.EventHandler(this.textChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(291, 136);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(130, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Ganzjahres Rechnungsnr:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 136);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(127, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Halbjahres Rechnungsnr:";
            // 
            // buttonCalcBereitsBezahlt
            // 
            this.buttonCalcBereitsBezahlt.Location = new System.Drawing.Point(193, 90);
            this.buttonCalcBereitsBezahlt.Name = "buttonCalcBereitsBezahlt";
            this.buttonCalcBereitsBezahlt.Size = new System.Drawing.Size(24, 20);
            this.buttonCalcBereitsBezahlt.TabIndex = 5;
            this.buttonCalcBereitsBezahlt.Text = "=";
            this.buttonCalcBereitsBezahlt.UseVisualStyleBackColor = true;
            this.buttonCalcBereitsBezahlt.Click += new System.EventHandler(this.buttonCalcBereitsBezahlt_Click);
            // 
            // textBoxBereitsBezahltSummand
            // 
            this.textBoxBereitsBezahltSummand.Location = new System.Drawing.Point(152, 90);
            this.textBoxBereitsBezahltSummand.Name = "textBoxBereitsBezahltSummand";
            this.textBoxBereitsBezahltSummand.Size = new System.Drawing.Size(42, 20);
            this.textBoxBereitsBezahltSummand.TabIndex = 4;
            // 
            // buttonBerechneHalbJahresBetrag
            // 
            this.buttonBerechneHalbJahresBetrag.Location = new System.Drawing.Point(435, 70);
            this.buttonBerechneHalbJahresBetrag.Name = "buttonBerechneHalbJahresBetrag";
            this.buttonBerechneHalbJahresBetrag.Size = new System.Drawing.Size(75, 21);
            this.buttonBerechneHalbJahresBetrag.TabIndex = 10;
            this.buttonBerechneHalbJahresBetrag.Text = "Berechnen";
            this.buttonBerechneHalbJahresBetrag.UseVisualStyleBackColor = true;
            this.buttonBerechneHalbJahresBetrag.Click += new System.EventHandler(this.buttonBerechneHalbJahresBetrag_Click);
            // 
            // buttonDruckJahresrechnung
            // 
            this.buttonDruckJahresrechnung.Location = new System.Drawing.Point(456, 104);
            this.buttonDruckJahresrechnung.Name = "buttonDruckJahresrechnung";
            this.buttonDruckJahresrechnung.Size = new System.Drawing.Size(107, 23);
            this.buttonDruckJahresrechnung.TabIndex = 14;
            this.buttonDruckJahresrechnung.Text = "Drucken Jahresr.";
            this.buttonDruckJahresrechnung.UseVisualStyleBackColor = true;
            this.buttonDruckJahresrechnung.Click += new System.EventHandler(this.buttonDruckJahresrechnung_Click);
            // 
            // textBoxNichtGespeichert
            // 
            this.textBoxNichtGespeichert.Location = new System.Drawing.Point(456, 48);
            this.textBoxNichtGespeichert.Name = "textBoxNichtGespeichert";
            this.textBoxNichtGespeichert.ReadOnly = true;
            this.textBoxNichtGespeichert.Size = new System.Drawing.Size(107, 20);
            this.textBoxNichtGespeichert.TabIndex = 10;
            this.textBoxNichtGespeichert.Text = "nicht gespeichert";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(139, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "+";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Bereits Bezahlt:";
            // 
            // textBoxBereitsbezahlt
            // 
            this.textBoxBereitsbezahlt.Location = new System.Drawing.Point(89, 90);
            this.textBoxBereitsbezahlt.Name = "textBoxBereitsbezahlt";
            this.textBoxBereitsbezahlt.Size = new System.Drawing.Size(50, 20);
            this.textBoxBereitsbezahlt.TabIndex = 3;
            this.textBoxBereitsbezahlt.TextChanged += new System.EventHandler(this.textChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ablesedatum:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(234, 92);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Sonstige Ford Wert:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(241, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "HalbJahresBetrag:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(235, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "TauschZstand neu:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Zählerstand neu:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Sonstige F. Text:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(241, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "TauschZstand alt:";
            // 
            // textBoxAblesedatum
            // 
            this.textBoxAblesedatum.Location = new System.Drawing.Point(89, 70);
            this.textBoxAblesedatum.Name = "textBoxAblesedatum";
            this.textBoxAblesedatum.Size = new System.Drawing.Size(98, 20);
            this.textBoxAblesedatum.TabIndex = 2;
            this.textBoxAblesedatum.TextChanged += new System.EventHandler(this.textChanged);
            // 
            // textBoxSonstigeForderungenWert
            // 
            this.textBoxSonstigeForderungenWert.Location = new System.Drawing.Point(335, 89);
            this.textBoxSonstigeForderungenWert.Name = "textBoxSonstigeForderungenWert";
            this.textBoxSonstigeForderungenWert.Size = new System.Drawing.Size(98, 20);
            this.textBoxSonstigeForderungenWert.TabIndex = 11;
            this.textBoxSonstigeForderungenWert.TextChanged += new System.EventHandler(this.textChanged);
            // 
            // textBoxHalbJahresBetrag
            // 
            this.textBoxHalbJahresBetrag.Location = new System.Drawing.Point(335, 69);
            this.textBoxHalbJahresBetrag.Name = "textBoxHalbJahresBetrag";
            this.textBoxHalbJahresBetrag.Size = new System.Drawing.Size(98, 20);
            this.textBoxHalbJahresBetrag.TabIndex = 9;
            this.textBoxHalbJahresBetrag.TextChanged += new System.EventHandler(this.textChanged);
            // 
            // textBoxTauschZaehlerstandNeu
            // 
            this.textBoxTauschZaehlerstandNeu.Location = new System.Drawing.Point(335, 50);
            this.textBoxTauschZaehlerstandNeu.Name = "textBoxTauschZaehlerstandNeu";
            this.textBoxTauschZaehlerstandNeu.Size = new System.Drawing.Size(98, 20);
            this.textBoxTauschZaehlerstandNeu.TabIndex = 8;
            this.textBoxTauschZaehlerstandNeu.TextChanged += new System.EventHandler(this.textChanged);
            // 
            // textBoxZaehlerStandNeu
            // 
            this.textBoxZaehlerStandNeu.Location = new System.Drawing.Point(89, 50);
            this.textBoxZaehlerStandNeu.Name = "textBoxZaehlerStandNeu";
            this.textBoxZaehlerStandNeu.Size = new System.Drawing.Size(98, 20);
            this.textBoxZaehlerStandNeu.TabIndex = 1;
            this.textBoxZaehlerStandNeu.TextChanged += new System.EventHandler(this.textChanged);
            // 
            // buttonRestore
            // 
            this.buttonRestore.Location = new System.Drawing.Point(456, 28);
            this.buttonRestore.Name = "buttonRestore";
            this.buttonRestore.Size = new System.Drawing.Size(107, 21);
            this.buttonRestore.TabIndex = 13;
            this.buttonRestore.Text = "Restore";
            this.buttonRestore.UseVisualStyleBackColor = true;
            this.buttonRestore.Click += new System.EventHandler(this.buttonRestore_Click);
            // 
            // buttonSpeichern
            // 
            this.buttonSpeichern.Location = new System.Drawing.Point(456, 8);
            this.buttonSpeichern.Name = "buttonSpeichern";
            this.buttonSpeichern.Size = new System.Drawing.Size(107, 20);
            this.buttonSpeichern.TabIndex = 12;
            this.buttonSpeichern.Text = "Speichern";
            this.buttonSpeichern.UseVisualStyleBackColor = true;
            this.buttonSpeichern.Click += new System.EventHandler(this.buttonSpeichern_Click);
            // 
            // textBoxSonstigeForderungenText
            // 
            this.textBoxSonstigeForderungenText.Location = new System.Drawing.Point(89, 110);
            this.textBoxSonstigeForderungenText.Name = "textBoxSonstigeForderungenText";
            this.textBoxSonstigeForderungenText.Size = new System.Drawing.Size(345, 20);
            this.textBoxSonstigeForderungenText.TabIndex = 6;
            this.textBoxSonstigeForderungenText.TextChanged += new System.EventHandler(this.textChanged);
            // 
            // textBoxTauschZaehlerstandAlt
            // 
            this.textBoxTauschZaehlerstandAlt.Location = new System.Drawing.Point(335, 30);
            this.textBoxTauschZaehlerstandAlt.Name = "textBoxTauschZaehlerstandAlt";
            this.textBoxTauschZaehlerstandAlt.Size = new System.Drawing.Size(98, 20);
            this.textBoxTauschZaehlerstandAlt.TabIndex = 7;
            this.textBoxTauschZaehlerstandAlt.TextChanged += new System.EventHandler(this.textChanged);
            // 
            // labelHalbJahresRechnungJah
            // 
            this.labelHalbJahresRechnungJah.AutoSize = true;
            this.labelHalbJahresRechnungJah.Location = new System.Drawing.Point(141, 136);
            this.labelHalbJahresRechnungJah.Name = "labelHalbJahresRechnungJah";
            this.labelHalbJahresRechnungJah.Size = new System.Drawing.Size(31, 13);
            this.labelHalbJahresRechnungJah.TabIndex = 19;
            this.labelHalbJahresRechnungJah.Text = "1900";
            // 
            // labelGanzJahresRechnungJah
            // 
            this.labelGanzJahresRechnungJah.AutoSize = true;
            this.labelGanzJahresRechnungJah.Location = new System.Drawing.Point(427, 136);
            this.labelGanzJahresRechnungJah.Name = "labelGanzJahresRechnungJah";
            this.labelGanzJahresRechnungJah.Size = new System.Drawing.Size(31, 13);
            this.labelGanzJahresRechnungJah.TabIndex = 20;
            this.labelGanzJahresRechnungJah.Text = "1900";
            // 
            // ZaehlerStandElementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ZaehlerStandElementControl";
            this.Size = new System.Drawing.Size(576, 168);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonVomVorjahr;
        private System.Windows.Forms.TextBox textBoxZaehlerStandAlt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxZaehlerStandNeu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAblesedatum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxBereitsbezahlt;
        private System.Windows.Forms.Button buttonRestore;
        private System.Windows.Forms.Button buttonSpeichern;
        private System.Windows.Forms.TextBox textBoxNichtGespeichert;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxTauschZaehlerstandNeu;
        private System.Windows.Forms.TextBox textBoxTauschZaehlerstandAlt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxSonstigeForderungenWert;
        private System.Windows.Forms.TextBox textBoxSonstigeForderungenText;
        private System.Windows.Forms.Button buttonDruckJahresrechnung;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxHalbJahresBetrag;
        private System.Windows.Forms.Button buttonBerechneHalbJahresBetrag;
        private System.Windows.Forms.Button buttonCalcBereitsBezahlt;
        private System.Windows.Forms.TextBox textBoxBereitsBezahltSummand;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxHalbJahresRechnungsNummer;
        private System.Windows.Forms.TextBox textBoxGanzJahresRechnungsNummer;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labelGanzJahresRechnungJah;
        private System.Windows.Forms.Label labelHalbJahresRechnungJah;
    }
}
