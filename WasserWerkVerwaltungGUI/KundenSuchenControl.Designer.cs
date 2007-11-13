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
            this.textBoxSuchen = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSuchen = new System.Windows.Forms.Button();
            this.listBoxKundenSuchenErgebnisse = new System.Windows.Forms.ListBox();
            this.buttonAuswaehlen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxSuchen
            // 
            this.textBoxSuchen.Location = new System.Drawing.Point(48, 29);
            this.textBoxSuchen.Name = "textBoxSuchen";
            this.textBoxSuchen.Size = new System.Drawing.Size(262, 20);
            this.textBoxSuchen.TabIndex = 0;
            this.textBoxSuchen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSuchen_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 32);
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
            this.buttonSuchen.Location = new System.Drawing.Point(316, 26);
            this.buttonSuchen.Name = "buttonSuchen";
            this.buttonSuchen.Size = new System.Drawing.Size(75, 23);
            this.buttonSuchen.TabIndex = 1;
            this.buttonSuchen.Text = "Suchen";
            this.buttonSuchen.UseVisualStyleBackColor = true;
            this.buttonSuchen.Click += new System.EventHandler(this.buttonSuchen_Click);
            // 
            // listBoxKundenSuchenErgebnisse
            // 
            this.listBoxKundenSuchenErgebnisse.FormattingEnabled = true;
            this.listBoxKundenSuchenErgebnisse.Location = new System.Drawing.Point(48, 55);
            this.listBoxKundenSuchenErgebnisse.Name = "listBoxKundenSuchenErgebnisse";
            this.listBoxKundenSuchenErgebnisse.Size = new System.Drawing.Size(262, 108);
            this.listBoxKundenSuchenErgebnisse.TabIndex = 2;
            this.listBoxKundenSuchenErgebnisse.DoubleClick += new System.EventHandler(this.listBoxKundenSuchenErgebnisse_DoubleClick);
            // 
            // buttonAuswaehlen
            // 
            this.buttonAuswaehlen.Location = new System.Drawing.Point(316, 139);
            this.buttonAuswaehlen.Name = "buttonAuswaehlen";
            this.buttonAuswaehlen.Size = new System.Drawing.Size(75, 23);
            this.buttonAuswaehlen.TabIndex = 3;
            this.buttonAuswaehlen.Text = "Auswählen";
            this.buttonAuswaehlen.UseVisualStyleBackColor = true;
            this.buttonAuswaehlen.Click += new System.EventHandler(this.buttonAuswaehlen_Click);
            // 
            // KundenSuchenControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonAuswaehlen);
            this.Controls.Add(this.listBoxKundenSuchenErgebnisse);
            this.Controls.Add(this.buttonSuchen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSuchen);
            this.Name = "KundenSuchenControl";
            this.Size = new System.Drawing.Size(459, 181);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSuchen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSuchen;
        private System.Windows.Forms.ListBox listBoxKundenSuchenErgebnisse;
        private System.Windows.Forms.Button buttonAuswaehlen;
    }
}
