namespace WasserWerkVerwaltung.GUI {
    partial class ZaehlerstandableseText {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.buttonAbbrechen = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerAblesezeitraumVon = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePickerAblesezeitraumBis = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // buttonAbbrechen
            // 
            this.buttonAbbrechen.Location = new System.Drawing.Point(93, 265);
            this.buttonAbbrechen.Name = "buttonAbbrechen";
            this.buttonAbbrechen.Size = new System.Drawing.Size(75, 23);
            this.buttonAbbrechen.TabIndex = 0;
            this.buttonAbbrechen.Text = "Abbrechen";
            this.buttonAbbrechen.UseVisualStyleBackColor = true;
            this.buttonAbbrechen.Click += new System.EventHandler(this.buttonAbbrechen_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(12, 265);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ablesezeitruam von:";
            // 
            // textBoxText
            // 
            this.textBoxText.Location = new System.Drawing.Point(12, 25);
            this.textBoxText.Multiline = true;
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(500, 177);
            this.textBoxText.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(374, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Text:                            Achtung! Zeilenumbrüche werden 1 zu 1 übernommen" +
                "!";
            // 
            // dateTimePickerAblesezeitraumVon
            // 
            this.dateTimePickerAblesezeitraumVon.Location = new System.Drawing.Point(120, 210);
            this.dateTimePickerAblesezeitraumVon.Name = "dateTimePickerAblesezeitraumVon";
            this.dateTimePickerAblesezeitraumVon.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerAblesezeitraumVon.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ablesezeitruam bis:";
            // 
            // dateTimePickerAblesezeitraumBis
            // 
            this.dateTimePickerAblesezeitraumBis.Location = new System.Drawing.Point(120, 236);
            this.dateTimePickerAblesezeitraumBis.Name = "dateTimePickerAblesezeitraumBis";
            this.dateTimePickerAblesezeitraumBis.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerAblesezeitraumBis.TabIndex = 4;
            // 
            // ZaehlerstandableseText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 300);
            this.ControlBox = false;
            this.Controls.Add(this.dateTimePickerAblesezeitraumBis);
            this.Controls.Add(this.dateTimePickerAblesezeitraumVon);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonAbbrechen);
            this.Name = "ZaehlerstandableseText";
            this.Text = "Zählerstandableseformular Daten";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAbbrechen;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePickerAblesezeitraumVon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerAblesezeitraumBis;
    }
}