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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxZaehlerStandNeu = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAblesedatum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxBereitsbezahlt = new System.Windows.Forms.TextBox();
            this.textBoxRechnungssumme = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonBerechnen = new System.Windows.Forms.Button();
            this.buttonSpeichern = new System.Windows.Forms.Button();
            this.buttonRestore = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Zählerstand alt: ";
            // 
            // buttonVomVorjahr
            // 
            this.buttonVomVorjahr.Location = new System.Drawing.Point(98, 16);
            this.buttonVomVorjahr.Name = "buttonVomVorjahr";
            this.buttonVomVorjahr.Size = new System.Drawing.Size(107, 23);
            this.buttonVomVorjahr.TabIndex = 0;
            this.buttonVomVorjahr.Text = "Stand vom Vorjahr";
            this.buttonVomVorjahr.UseVisualStyleBackColor = true;
            this.buttonVomVorjahr.Click += new System.EventHandler(this.buttonVomVorjahr_Click);
            // 
            // textBoxZaehlerStandAlt
            // 
            this.textBoxZaehlerStandAlt.Location = new System.Drawing.Point(98, 45);
            this.textBoxZaehlerStandAlt.Name = "textBoxZaehlerStandAlt";
            this.textBoxZaehlerStandAlt.Size = new System.Drawing.Size(107, 20);
            this.textBoxZaehlerStandAlt.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(212, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Rechnung = (Verbrauch * Preis/m³ + Zählermiete) * UST (1.1)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxRechnungssumme);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxBereitsbezahlt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxAblesedatum);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxZaehlerStandNeu);
            this.groupBox1.Controls.Add(this.buttonRestore);
            this.groupBox1.Controls.Add(this.buttonSpeichern);
            this.groupBox1.Controls.Add(this.buttonBerechnen);
            this.groupBox1.Controls.Add(this.buttonVomVorjahr);
            this.groupBox1.Controls.Add(this.textBoxZaehlerStandAlt);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(594, 136);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // textBoxZaehlerStandNeu
            // 
            this.textBoxZaehlerStandNeu.Location = new System.Drawing.Point(98, 65);
            this.textBoxZaehlerStandNeu.Name = "textBoxZaehlerStandNeu";
            this.textBoxZaehlerStandNeu.Size = new System.Drawing.Size(107, 20);
            this.textBoxZaehlerStandNeu.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Zählerstand neu: ";
            // 
            // textBoxAblesedatum
            // 
            this.textBoxAblesedatum.Location = new System.Drawing.Point(98, 85);
            this.textBoxAblesedatum.Name = "textBoxAblesedatum";
            this.textBoxAblesedatum.Size = new System.Drawing.Size(107, 20);
            this.textBoxAblesedatum.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ablesedatum: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Bereits Bezahlt: ";
            // 
            // textBoxBereitsbezahlt
            // 
            this.textBoxBereitsbezahlt.Location = new System.Drawing.Point(98, 105);
            this.textBoxBereitsbezahlt.Name = "textBoxBereitsbezahlt";
            this.textBoxBereitsbezahlt.Size = new System.Drawing.Size(107, 20);
            this.textBoxBereitsbezahlt.TabIndex = 4;
            // 
            // textBoxRechnungssumme
            // 
            this.textBoxRechnungssumme.Location = new System.Drawing.Point(321, 19);
            this.textBoxRechnungssumme.Name = "textBoxRechnungssumme";
            this.textBoxRechnungssumme.Size = new System.Drawing.Size(74, 20);
            this.textBoxRechnungssumme.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(219, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Rechnungssumme: ";
            // 
            // buttonBerechnen
            // 
            this.buttonBerechnen.Location = new System.Drawing.Point(398, 18);
            this.buttonBerechnen.Name = "buttonBerechnen";
            this.buttonBerechnen.Size = new System.Drawing.Size(107, 23);
            this.buttonBerechnen.TabIndex = 6;
            this.buttonBerechnen.Text = "Berechnen";
            this.buttonBerechnen.UseVisualStyleBackColor = true;
            this.buttonBerechnen.Click += new System.EventHandler(this.buttonBerechnen_Click);
            // 
            // buttonSpeichern
            // 
            this.buttonSpeichern.Location = new System.Drawing.Point(252, 88);
            this.buttonSpeichern.Name = "buttonSpeichern";
            this.buttonSpeichern.Size = new System.Drawing.Size(107, 23);
            this.buttonSpeichern.TabIndex = 7;
            this.buttonSpeichern.Text = "Speichern";
            this.buttonSpeichern.UseVisualStyleBackColor = true;
            this.buttonSpeichern.Click += new System.EventHandler(this.buttonSpeichern_Click);
            // 
            // buttonRestore
            // 
            this.buttonRestore.Location = new System.Drawing.Point(365, 88);
            this.buttonRestore.Name = "buttonRestore";
            this.buttonRestore.Size = new System.Drawing.Size(107, 23);
            this.buttonRestore.TabIndex = 8;
            this.buttonRestore.Text = "Restore";
            this.buttonRestore.UseVisualStyleBackColor = true;
            this.buttonRestore.Click += new System.EventHandler(this.buttonRestore_Click);
            // 
            // ZaehlerStandElementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ZaehlerStandElementControl";
            this.Size = new System.Drawing.Size(600, 144);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonVomVorjahr;
        private System.Windows.Forms.TextBox textBoxZaehlerStandAlt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxZaehlerStandNeu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAblesedatum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxRechnungssumme;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxBereitsbezahlt;
        private System.Windows.Forms.Button buttonBerechnen;
        private System.Windows.Forms.Button buttonRestore;
        private System.Windows.Forms.Button buttonSpeichern;
    }
}
