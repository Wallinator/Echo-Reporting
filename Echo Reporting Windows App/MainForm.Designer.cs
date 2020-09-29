namespace Echo_Reporting_Windows_App {
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
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.AgePanel = new System.Windows.Forms.Panel();
			this.WeightPanel = new System.Windows.Forms.Panel();
			this.DiastolicBPPanel = new System.Windows.Forms.Panel();
			this.SystolicBPPanel = new System.Windows.Forms.Panel();
			this.HeightPanel = new System.Windows.Forms.Panel();
			this.PatientIDPanel = new System.Windows.Forms.Panel();
			this.PatientNamePanel = new System.Windows.Forms.Panel();
			this.PatientSexPanel = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.menuStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 2, 0, 4);
			this.menuStrip1.Size = new System.Drawing.Size(1109, 25);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFolderToolStripMenuItem,
            this.openFileToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// openFolderToolStripMenuItem
			// 
			this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
			this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.openFolderToolStripMenuItem.Text = "Open Folder...";
			this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
			// 
			// openFileToolStripMenuItem
			// 
			this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
			this.openFileToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.openFileToolStripMenuItem.Text = "Open File...";
			this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
			// 
			// AgePanel
			// 
			this.AgePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.AgePanel.Location = new System.Drawing.Point(272, 86);
			this.AgePanel.Name = "AgePanel";
			this.AgePanel.Size = new System.Drawing.Size(250, 50);
			this.AgePanel.TabIndex = 2;
			// 
			// WeightPanel
			// 
			this.WeightPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.WeightPanel.Location = new System.Drawing.Point(535, 37);
			this.WeightPanel.Name = "WeightPanel";
			this.WeightPanel.Size = new System.Drawing.Size(250, 50);
			this.WeightPanel.TabIndex = 3;
			// 
			// DiastolicBPPanel
			// 
			this.DiastolicBPPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.DiastolicBPPanel.Location = new System.Drawing.Point(784, 86);
			this.DiastolicBPPanel.Name = "DiastolicBPPanel";
			this.DiastolicBPPanel.Size = new System.Drawing.Size(250, 50);
			this.DiastolicBPPanel.TabIndex = 3;
			// 
			// SystolicBPPanel
			// 
			this.SystolicBPPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SystolicBPPanel.Location = new System.Drawing.Point(784, 37);
			this.SystolicBPPanel.Name = "SystolicBPPanel";
			this.SystolicBPPanel.Size = new System.Drawing.Size(250, 50);
			this.SystolicBPPanel.TabIndex = 3;
			// 
			// HeightPanel
			// 
			this.HeightPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.HeightPanel.Location = new System.Drawing.Point(535, 86);
			this.HeightPanel.Name = "HeightPanel";
			this.HeightPanel.Size = new System.Drawing.Size(250, 50);
			this.HeightPanel.TabIndex = 4;
			// 
			// PatientIDPanel
			// 
			this.PatientIDPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PatientIDPanel.Location = new System.Drawing.Point(23, 37);
			this.PatientIDPanel.Name = "PatientIDPanel";
			this.PatientIDPanel.Size = new System.Drawing.Size(250, 50);
			this.PatientIDPanel.TabIndex = 3;
			// 
			// PatientNamePanel
			// 
			this.PatientNamePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PatientNamePanel.Location = new System.Drawing.Point(23, 86);
			this.PatientNamePanel.Name = "PatientNamePanel";
			this.PatientNamePanel.Size = new System.Drawing.Size(250, 50);
			this.PatientNamePanel.TabIndex = 4;
			// 
			// PatientSexPanel
			// 
			this.PatientSexPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PatientSexPanel.Location = new System.Drawing.Point(272, 37);
			this.PatientSexPanel.Name = "PatientSexPanel";
			this.PatientSexPanel.Size = new System.Drawing.Size(250, 50);
			this.PatientSexPanel.TabIndex = 5;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.PatientSexPanel);
			this.groupBox1.Controls.Add(this.SystolicBPPanel);
			this.groupBox1.Controls.Add(this.PatientIDPanel);
			this.groupBox1.Controls.Add(this.WeightPanel);
			this.groupBox1.Controls.Add(this.AgePanel);
			this.groupBox1.Controls.Add(this.PatientNamePanel);
			this.groupBox1.Controls.Add(this.DiastolicBPPanel);
			this.groupBox1.Controls.Add(this.HeightPanel);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.groupBox1.Location = new System.Drawing.Point(27, 47);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(1058, 156);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Patient Data";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(1109, 450);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Panel AgePanel;
		private System.Windows.Forms.Panel WeightPanel;
		private System.Windows.Forms.Panel DiastolicBPPanel;
		private System.Windows.Forms.Panel SystolicBPPanel;
		private System.Windows.Forms.Panel HeightPanel;
		private System.Windows.Forms.Panel PatientIDPanel;
		private System.Windows.Forms.Panel PatientNamePanel;
		private System.Windows.Forms.Panel PatientSexPanel;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}

