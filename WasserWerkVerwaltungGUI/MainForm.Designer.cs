namespace WasserWerkVerwaltung.GUI {
    partial class MainForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.stammdatenpflegeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zählerständeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.druckenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stammdatenpflegeToolStripMenuItem,
            this.zählerständeToolStripMenuItem,
            this.druckenToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(892, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // stammdatenpflegeToolStripMenuItem
            // 
            this.stammdatenpflegeToolStripMenuItem.Name = "stammdatenpflegeToolStripMenuItem";
            this.stammdatenpflegeToolStripMenuItem.Size = new System.Drawing.Size(109, 20);
            this.stammdatenpflegeToolStripMenuItem.Text = "Stammdatenpflege";
            this.stammdatenpflegeToolStripMenuItem.Click += new System.EventHandler(this.stammdatenpflegeToolStripMenuItem_Click);
            // 
            // zählerständeToolStripMenuItem
            // 
            this.zählerständeToolStripMenuItem.Name = "zählerständeToolStripMenuItem";
            this.zählerständeToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.zählerständeToolStripMenuItem.Text = "Zählerstände";
            this.zählerständeToolStripMenuItem.Click += new System.EventHandler(this.zählerständeToolStripMenuItem_Click);
            // 
            // druckenToolStripMenuItem
            // 
            this.druckenToolStripMenuItem.Name = "druckenToolStripMenuItem";
            this.druckenToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.druckenToolStripMenuItem.Text = "Drucken";
            this.druckenToolStripMenuItem.Click += new System.EventHandler(this.druckenToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 673);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Wasser Werk Verwaltung by Thomas Gassner Version 0.8.7";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem stammdatenpflegeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zählerständeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem druckenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

