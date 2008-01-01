namespace WasserWerkVerwaltung.GUI {
    partial class ZaehlerStaendeControl {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZaehlerStaendeControl));
            this.listBoxKunden = new System.Windows.Forms.ListBox();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.buttonSuchen = new System.Windows.Forms.Button();
            this.textBoxSuchen = new System.Windows.Forms.TextBox();
            this.listBoxSuchen = new System.Windows.Forms.ListBox();
            this.comboBoxJahr = new System.Windows.Forms.ComboBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxJahr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonGeneriereMehrereHalbjahresWerte = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonJahreswasserpreisErstellen = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxJahrWasserpreis = new System.Windows.Forms.TextBox();
            this.buttonPreisImJahrAendern = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxKunden
            // 
            this.listBoxKunden.FormattingEnabled = true;
            this.listBoxKunden.Location = new System.Drawing.Point(0, 9);
            this.listBoxKunden.Name = "listBoxKunden";
            this.listBoxKunden.Size = new System.Drawing.Size(200, 628);
            this.listBoxKunden.Sorted = true;
            this.listBoxKunden.TabIndex = 1;
            this.listBoxKunden.SelectedIndexChanged += new System.EventHandler(this.listBoxKunden_SelectedIndexChanged);
            // 
            // buttonFilter
            // 
            this.buttonFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.buttonFilter.Location = new System.Drawing.Point(6, 136);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(172, 23);
            this.buttonFilter.TabIndex = 2;
            this.buttonFilter.Text = "Filter kein Eintrag im Jahr:";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // buttonSuchen
            // 
            this.buttonSuchen.Location = new System.Drawing.Point(6, 19);
            this.buttonSuchen.Name = "buttonSuchen";
            this.buttonSuchen.Size = new System.Drawing.Size(172, 23);
            this.buttonSuchen.TabIndex = 3;
            this.buttonSuchen.Text = "suchen in aktuellen Liste";
            this.buttonSuchen.UseVisualStyleBackColor = true;
            this.buttonSuchen.Click += new System.EventHandler(this.buttonSuchen_Click);
            // 
            // textBoxSuchen
            // 
            this.textBoxSuchen.Location = new System.Drawing.Point(6, 48);
            this.textBoxSuchen.Name = "textBoxSuchen";
            this.textBoxSuchen.Size = new System.Drawing.Size(172, 20);
            this.textBoxSuchen.TabIndex = 4;
            // 
            // listBoxSuchen
            // 
            this.listBoxSuchen.FormattingEnabled = true;
            this.listBoxSuchen.Location = new System.Drawing.Point(6, 74);
            this.listBoxSuchen.Name = "listBoxSuchen";
            this.listBoxSuchen.Size = new System.Drawing.Size(172, 56);
            this.listBoxSuchen.TabIndex = 5;
            this.listBoxSuchen.SelectedIndexChanged += new System.EventHandler(this.listBoxSuchen_SelectedIndexChanged);
            // 
            // comboBoxJahr
            // 
            this.comboBoxJahr.FormattingEnabled = true;
            this.comboBoxJahr.Location = new System.Drawing.Point(42, 165);
            this.comboBoxJahr.Name = "comboBoxJahr";
            this.comboBoxJahr.Size = new System.Drawing.Size(136, 21);
            this.comboBoxJahr.Sorted = true;
            this.comboBoxJahr.TabIndex = 6;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(3, 192);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(175, 23);
            this.buttonReset.TabIndex = 7;
            this.buttonReset.Text = "Alle Kunden in Liste";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Jahr:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSuchen);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonReset);
            this.groupBox1.Controls.Add(this.textBoxSuchen);
            this.groupBox1.Controls.Add(this.comboBoxJahr);
            this.groupBox1.Controls.Add(this.listBoxSuchen);
            this.groupBox1.Controls.Add(this.buttonFilter);
            this.groupBox1.Location = new System.Drawing.Point(206, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 225);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Suchen";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxJahr);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.buttonGeneriereMehrereHalbjahresWerte);
            this.groupBox2.Location = new System.Drawing.Point(206, 240);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(184, 199);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Halbjahresbetrag für alle";
            // 
            // textBoxJahr
            // 
            this.textBoxJahr.Location = new System.Drawing.Point(36, 19);
            this.textBoxJahr.Name = "textBoxJahr";
            this.textBoxJahr.Size = new System.Drawing.Size(142, 20);
            this.textBoxJahr.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Jahr:";
            // 
            // buttonGeneriereMehrereHalbjahresWerte
            // 
            this.buttonGeneriereMehrereHalbjahresWerte.Location = new System.Drawing.Point(9, 81);
            this.buttonGeneriereMehrereHalbjahresWerte.Name = "buttonGeneriereMehrereHalbjahresWerte";
            this.buttonGeneriereMehrereHalbjahresWerte.Size = new System.Drawing.Size(169, 112);
            this.buttonGeneriereMehrereHalbjahresWerte.TabIndex = 0;
            this.buttonGeneriereMehrereHalbjahresWerte.Text = resources.GetString("buttonGeneriereMehrereHalbjahresWerte.Text");
            this.buttonGeneriereMehrereHalbjahresWerte.UseVisualStyleBackColor = true;
            this.buttonGeneriereMehrereHalbjahresWerte.Click += new System.EventHandler(this.buttonGeneriereMehrereHalbjahresWerte_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonPreisImJahrAendern);
            this.groupBox3.Controls.Add(this.textBoxJahrWasserpreis);
            this.groupBox3.Controls.Add(this.buttonJahreswasserpreisErstellen);
            this.groupBox3.Location = new System.Drawing.Point(206, 445);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(184, 122);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Jahres Wasserpreis / m³";
            // 
            // buttonJahreswasserpreisErstellen
            // 
            this.buttonJahreswasserpreisErstellen.Location = new System.Drawing.Point(9, 49);
            this.buttonJahreswasserpreisErstellen.Name = "buttonJahreswasserpreisErstellen";
            this.buttonJahreswasserpreisErstellen.Size = new System.Drawing.Size(169, 23);
            this.buttonJahreswasserpreisErstellen.TabIndex = 0;
            this.buttonJahreswasserpreisErstellen.Text = "Jahreswasserpreis erstellen";
            this.buttonJahreswasserpreisErstellen.UseVisualStyleBackColor = true;
            this.buttonJahreswasserpreisErstellen.Click += new System.EventHandler(this.buttonJahreswasserpreisErstellen_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Achtung dieser Vorgang kann";
            // 
            // textBoxJahrWasserpreis
            // 
            this.textBoxJahrWasserpreis.Location = new System.Drawing.Point(36, 19);
            this.textBoxJahrWasserpreis.Name = "textBoxJahrWasserpreis";
            this.textBoxJahrWasserpreis.Size = new System.Drawing.Size(142, 20);
            this.textBoxJahrWasserpreis.TabIndex = 1;
            // 
            // buttonPreisImJahrAendern
            // 
            this.buttonPreisImJahrAendern.Location = new System.Drawing.Point(9, 78);
            this.buttonPreisImJahrAendern.Name = "buttonPreisImJahrAendern";
            this.buttonPreisImJahrAendern.Size = new System.Drawing.Size(169, 23);
            this.buttonPreisImJahrAendern.TabIndex = 9;
            this.buttonPreisImJahrAendern.Text = "Preis im Jahr ändern (global)";
            this.buttonPreisImJahrAendern.UseVisualStyleBackColor = true;
            this.buttonPreisImJahrAendern.Click += new System.EventHandler(this.buttonPreisImJahrAendern_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "eine Weile dauern!";
            // 
            // ZaehlerStaendeControl
            // 
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listBoxKunden);
            this.Name = "ZaehlerStaendeControl";
            this.Size = new System.Drawing.Size(1000, 640);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxKunden;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.Button buttonSuchen;
        private System.Windows.Forms.TextBox textBoxSuchen;
        private System.Windows.Forms.ListBox listBoxSuchen;
        private System.Windows.Forms.ComboBox comboBoxJahr;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxJahr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonGeneriereMehrereHalbjahresWerte;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxJahrWasserpreis;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonJahreswasserpreisErstellen;
        private System.Windows.Forms.Button buttonPreisImJahrAendern;
        private System.Windows.Forms.Label label4;

    }
}
