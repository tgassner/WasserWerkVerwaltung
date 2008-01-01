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
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonJahrHinzufuegen
            // 
            this.buttonJahrHinzufuegen.Location = new System.Drawing.Point(163, 5);
            this.buttonJahrHinzufuegen.Name = "buttonJahrHinzufuegen";
            this.buttonJahrHinzufuegen.Size = new System.Drawing.Size(163, 23);
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
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Neues Jahr für";
            // 
            // textBoxJahr
            // 
            this.textBoxJahr.Location = new System.Drawing.Point(96, 13);
            this.textBoxJahr.Name = "textBoxJahr";
            this.textBoxJahr.Size = new System.Drawing.Size(61, 20);
            this.textBoxJahr.TabIndex = 2;
            // 
            // buttonJahrLoeschen
            // 
            this.buttonJahrLoeschen.Location = new System.Drawing.Point(163, 34);
            this.buttonJahrLoeschen.Name = "buttonJahrLoeschen";
            this.buttonJahrLoeschen.Size = new System.Drawing.Size(163, 23);
            this.buttonJahrLoeschen.TabIndex = 0;
            this.buttonJahrLoeschen.Text = "Jahr (beim Kunden) löschen";
            this.buttonJahrLoeschen.UseVisualStyleBackColor = true;
            this.buttonJahrLoeschen.Click += new System.EventHandler(this.buttonJahrLoeschen_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = " Kunden anlegen:";
            // 
            // ZaehlerStaendDetailsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.textBoxJahr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonJahrLoeschen);
            this.Controls.Add(this.buttonJahrHinzufuegen);
            this.Name = "ZaehlerStaendDetailsControl";
            this.Size = new System.Drawing.Size(580, 640);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonJahrHinzufuegen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxJahr;
        private System.Windows.Forms.Button buttonJahrLoeschen;
        private System.Windows.Forms.Label label2;
    }
}
