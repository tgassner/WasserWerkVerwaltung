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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonJahrLoeschen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonJahrHinzufuegen
            // 
            this.buttonJahrHinzufuegen.Location = new System.Drawing.Point(261, 5);
            this.buttonJahrHinzufuegen.Name = "buttonJahrHinzufuegen";
            this.buttonJahrHinzufuegen.Size = new System.Drawing.Size(92, 23);
            this.buttonJahrHinzufuegen.TabIndex = 0;
            this.buttonJahrHinzufuegen.Text = "Jahr hinzufügen";
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(181, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(74, 20);
            this.textBox1.TabIndex = 2;
            // 
            // buttonJahrLoeschen
            // 
            this.buttonJahrLoeschen.Location = new System.Drawing.Point(359, 5);
            this.buttonJahrLoeschen.Name = "buttonJahrLoeschen";
            this.buttonJahrLoeschen.Size = new System.Drawing.Size(92, 23);
            this.buttonJahrLoeschen.TabIndex = 0;
            this.buttonJahrLoeschen.Text = "Jahr löschen";
            this.buttonJahrLoeschen.UseVisualStyleBackColor = true;
            this.buttonJahrLoeschen.Click += new System.EventHandler(this.buttonJahrLoeschen_Click);
            // 
            // ZaehlerStaendDetailsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonJahrLoeschen;
    }
}
