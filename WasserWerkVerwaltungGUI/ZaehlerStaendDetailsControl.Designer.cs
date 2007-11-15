namespace WasserWerkVerwaltung.GUI {
    partial class ZaehlerStaendDetailsControl {
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
            this.buttonJahrHinzufuegen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxJahr = new System.Windows.Forms.TextBox();
            this.buttonJahrLoeschen = new System.Windows.Forms.Button();
            this.buttonPreisImJahrAendern = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonJahrHinzufuegen
            // 
            this.buttonJahrHinzufuegen.Location = new System.Drawing.Point(261, 5);
            this.buttonJahrHinzufuegen.Name = "buttonJahrHinzufuegen";
            this.buttonJahrHinzufuegen.Size = new System.Drawing.Size(187, 23);
            this.buttonJahrHinzufuegen.TabIndex = 0;
            this.buttonJahrHinzufuegen.Text = "Jahr (beim Kunden) hinzufügen";
            this.buttonJahrHinzufuegen.UseVisualStyleBackColor = true;
            this.buttonJahrHinzufuegen.Click += new System.EventHandler(this.buttonJahrHinzufuegen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Neues Jahr für den Kunden anlegen:";
            // 
            // textBoxJahr
            // 
            this.textBoxJahr.Location = new System.Drawing.Point(181, 7);
            this.textBoxJahr.Name = "textBoxJahr";
            this.textBoxJahr.Size = new System.Drawing.Size(74, 20);
            this.textBoxJahr.TabIndex = 2;
            // 
            // buttonJahrLoeschen
            // 
            this.buttonJahrLoeschen.Location = new System.Drawing.Point(454, 5);
            this.buttonJahrLoeschen.Name = "buttonJahrLoeschen";
            this.buttonJahrLoeschen.Size = new System.Drawing.Size(187, 23);
            this.buttonJahrLoeschen.TabIndex = 0;
            this.buttonJahrLoeschen.Text = "Jahr (beim Kunden) löschen";
            this.buttonJahrLoeschen.UseVisualStyleBackColor = true;
            this.buttonJahrLoeschen.Click += new System.EventHandler(this.buttonJahrLoeschen_Click);
            // 
            // buttonPreisImJahrAendern
            // 
            this.buttonPreisImJahrAendern.Location = new System.Drawing.Point(261, 34);
            this.buttonPreisImJahrAendern.Name = "buttonPreisImJahrAendern";
            this.buttonPreisImJahrAendern.Size = new System.Drawing.Size(187, 23);
            this.buttonPreisImJahrAendern.TabIndex = 0;
            this.buttonPreisImJahrAendern.Text = "Preis im Jahr ändern (global)";
            this.buttonPreisImJahrAendern.UseVisualStyleBackColor = true;
            this.buttonPreisImJahrAendern.Click += new System.EventHandler(this.buttonPreisImJahrAendern_Click);
            // 
            // ZaehlerStaendDetailsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.textBoxJahr);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonPreisImJahrAendern);
            this.Controls.Add(this.buttonJahrLoeschen);
            this.Controls.Add(this.buttonJahrHinzufuegen);
            this.Name = "ZaehlerStaendDetailsControl";
            this.Size = new System.Drawing.Size(680, 640);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonJahrHinzufuegen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxJahr;
        private System.Windows.Forms.Button buttonJahrLoeschen;
        private System.Windows.Forms.Button buttonPreisImJahrAendern;
    }
}
