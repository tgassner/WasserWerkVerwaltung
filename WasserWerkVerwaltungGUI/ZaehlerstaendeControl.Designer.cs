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
            this.listBoxKunden = new System.Windows.Forms.ListBox();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.buttonSuchen = new System.Windows.Forms.Button();
            this.textBoxSuchen = new System.Windows.Forms.TextBox();
            this.listBoxSuchen = new System.Windows.Forms.ListBox();
            this.comboBoxJahr = new System.Windows.Forms.ComboBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxKunden
            // 
            this.listBoxKunden.FormattingEnabled = true;
            this.listBoxKunden.Location = new System.Drawing.Point(0, 126);
            this.listBoxKunden.Name = "listBoxKunden";
            this.listBoxKunden.Size = new System.Drawing.Size(200, 511);
            this.listBoxKunden.Sorted = true;
            this.listBoxKunden.TabIndex = 1;
            this.listBoxKunden.SelectedIndexChanged += new System.EventHandler(this.listBoxKunden_SelectedIndexChanged);
            // 
            // buttonFilter
            // 
            this.buttonFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.buttonFilter.Location = new System.Drawing.Point(0, 81);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(134, 23);
            this.buttonFilter.TabIndex = 2;
            this.buttonFilter.Text = "Filter kein Eintrag im Jahr:";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // buttonSuchen
            // 
            this.buttonSuchen.Location = new System.Drawing.Point(0, 1);
            this.buttonSuchen.Name = "buttonSuchen";
            this.buttonSuchen.Size = new System.Drawing.Size(112, 23);
            this.buttonSuchen.TabIndex = 3;
            this.buttonSuchen.Text = "suchen in aktl. Liste";
            this.buttonSuchen.UseVisualStyleBackColor = true;
            this.buttonSuchen.Click += new System.EventHandler(this.buttonSuchen_Click);
            // 
            // textBoxSuchen
            // 
            this.textBoxSuchen.Location = new System.Drawing.Point(111, 3);
            this.textBoxSuchen.Name = "textBoxSuchen";
            this.textBoxSuchen.Size = new System.Drawing.Size(89, 20);
            this.textBoxSuchen.TabIndex = 4;
            // 
            // listBoxSuchen
            // 
            this.listBoxSuchen.FormattingEnabled = true;
            this.listBoxSuchen.Location = new System.Drawing.Point(0, 24);
            this.listBoxSuchen.Name = "listBoxSuchen";
            this.listBoxSuchen.Size = new System.Drawing.Size(200, 56);
            this.listBoxSuchen.TabIndex = 5;
            this.listBoxSuchen.SelectedIndexChanged += new System.EventHandler(this.listBoxSuchen_SelectedIndexChanged);
            // 
            // comboBoxJahr
            // 
            this.comboBoxJahr.FormattingEnabled = true;
            this.comboBoxJahr.Location = new System.Drawing.Point(140, 82);
            this.comboBoxJahr.Name = "comboBoxJahr";
            this.comboBoxJahr.Size = new System.Drawing.Size(60, 21);
            this.comboBoxJahr.Sorted = true;
            this.comboBoxJahr.TabIndex = 6;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(0, 104);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(200, 23);
            this.buttonReset.TabIndex = 7;
            this.buttonReset.Text = "Alle Kunden in Liste";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // ZaehlerStaendeControl
            // 
            this.AutoScroll = true;
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.comboBoxJahr);
            this.Controls.Add(this.listBoxSuchen);
            this.Controls.Add(this.textBoxSuchen);
            this.Controls.Add(this.buttonSuchen);
            this.Controls.Add(this.buttonFilter);
            this.Controls.Add(this.listBoxKunden);
            this.Name = "ZaehlerStaendeControl";
            this.Size = new System.Drawing.Size(936, 640);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxKunden;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.Button buttonSuchen;
        private System.Windows.Forms.TextBox textBoxSuchen;
        private System.Windows.Forms.ListBox listBoxSuchen;
        private System.Windows.Forms.ComboBox comboBoxJahr;
        private System.Windows.Forms.Button buttonReset;

    }
}
