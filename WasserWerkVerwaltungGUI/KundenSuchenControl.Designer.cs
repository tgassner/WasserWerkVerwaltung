namespace WasserWerkVerwaltung.GUI {
    partial class KundenSuchenControl {
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
            this.textBoxNamenSuchen = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSuchen = new System.Windows.Forms.Button();
            this.listBoxKundenSuchenErgebnisse = new System.Windows.Forms.ListBox();
            this.buttonAuswaehlen = new System.Windows.Forms.Button();
            this.buttonObjektSuchen = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxObjektSuchen = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxNamenSuchen
            // 
            this.textBoxNamenSuchen.Location = new System.Drawing.Point(74, 24);
            this.textBoxNamenSuchen.Name = "textBoxNamenSuchen";
            this.textBoxNamenSuchen.Size = new System.Drawing.Size(262, 20);
            this.textBoxNamenSuchen.TabIndex = 0;
            this.textBoxNamenSuchen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSuchen_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Suchen";
            // 
            // buttonSuchen
            // 
            this.buttonSuchen.Location = new System.Drawing.Point(342, 21);
            this.buttonSuchen.Name = "buttonSuchen";
            this.buttonSuchen.Size = new System.Drawing.Size(95, 23);
            this.buttonSuchen.TabIndex = 1;
            this.buttonSuchen.Text = "Suchen Name";
            this.buttonSuchen.UseVisualStyleBackColor = true;
            this.buttonSuchen.Click += new System.EventHandler(this.buttonSuchen_Click);
            // 
            // listBoxKundenSuchenErgebnisse
            // 
            this.listBoxKundenSuchenErgebnisse.FormattingEnabled = true;
            this.listBoxKundenSuchenErgebnisse.Location = new System.Drawing.Point(74, 76);
            this.listBoxKundenSuchenErgebnisse.Name = "listBoxKundenSuchenErgebnisse";
            this.listBoxKundenSuchenErgebnisse.Size = new System.Drawing.Size(262, 121);
            this.listBoxKundenSuchenErgebnisse.TabIndex = 2;
            this.listBoxKundenSuchenErgebnisse.DoubleClick += new System.EventHandler(this.listBoxKundenSuchenErgebnisse_DoubleClick);
            // 
            // buttonAuswaehlen
            // 
            this.buttonAuswaehlen.Location = new System.Drawing.Point(342, 174);
            this.buttonAuswaehlen.Name = "buttonAuswaehlen";
            this.buttonAuswaehlen.Size = new System.Drawing.Size(95, 23);
            this.buttonAuswaehlen.TabIndex = 3;
            this.buttonAuswaehlen.Text = "Auswählen";
            this.buttonAuswaehlen.UseVisualStyleBackColor = true;
            this.buttonAuswaehlen.Click += new System.EventHandler(this.buttonAuswaehlen_Click);
            // 
            // buttonObjektSuchen
            // 
            this.buttonObjektSuchen.Location = new System.Drawing.Point(342, 50);
            this.buttonObjektSuchen.Name = "buttonObjektSuchen";
            this.buttonObjektSuchen.Size = new System.Drawing.Size(95, 23);
            this.buttonObjektSuchen.TabIndex = 4;
            this.buttonObjektSuchen.Text = "Suchen Objekt";
            this.buttonObjektSuchen.UseVisualStyleBackColor = true;
            this.buttonObjektSuchen.Click += new System.EventHandler(this.buttonObjektSuchen_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Objekt:";
            // 
            // textBoxObjektSuchen
            // 
            this.textBoxObjektSuchen.Location = new System.Drawing.Point(74, 50);
            this.textBoxObjektSuchen.Name = "textBoxObjektSuchen";
            this.textBoxObjektSuchen.Size = new System.Drawing.Size(262, 20);
            this.textBoxObjektSuchen.TabIndex = 5;
            this.textBoxObjektSuchen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxObjektSuchErgebnisse_KeyDown);
            // 
            // KundenSuchenControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxObjektSuchen);
            this.Controls.Add(this.buttonObjektSuchen);
            this.Controls.Add(this.buttonAuswaehlen);
            this.Controls.Add(this.listBoxKundenSuchenErgebnisse);
            this.Controls.Add(this.buttonSuchen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxNamenSuchen);
            this.Name = "KundenSuchenControl";
            this.Size = new System.Drawing.Size(459, 206);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxNamenSuchen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSuchen;
        private System.Windows.Forms.ListBox listBoxKundenSuchenErgebnisse;
        private System.Windows.Forms.Button buttonAuswaehlen;
        private System.Windows.Forms.Button buttonObjektSuchen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxObjektSuchen;
    }
}
