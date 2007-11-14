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
            this.SuspendLayout();
            // 
            // listBoxKunden
            // 
            this.listBoxKunden.FormattingEnabled = true;
            this.listBoxKunden.Location = new System.Drawing.Point(0, 0);
            this.listBoxKunden.Name = "listBoxKunden";
            this.listBoxKunden.Size = new System.Drawing.Size(200, 628);
            this.listBoxKunden.Sorted = true;
            this.listBoxKunden.TabIndex = 1;
            this.listBoxKunden.SelectedIndexChanged += new System.EventHandler(this.listBoxKunden_SelectedIndexChanged);
            // 
            // ZaehlerStaendeControl
            // 
            this.AutoScroll = true;
            this.Controls.Add(this.listBoxKunden);
            this.Name = "ZaehlerStaendeControl";
            this.Size = new System.Drawing.Size(936, 640);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxKunden;

    }
}
