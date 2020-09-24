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
			this.label1 = new System.Windows.Forms.Label();
			this.PatientIDBox = new System.Windows.Forms.TextBox();
			this.PatientNameBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.PatientSexBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.menuStrip1.SuspendLayout();
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
			this.menuStrip1.Size = new System.Drawing.Size(903, 25);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
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
			this.AgePanel.Location = new System.Drawing.Point(188, 184);
			this.AgePanel.Name = "AgePanel";
			this.AgePanel.Size = new System.Drawing.Size(250, 39);
			this.AgePanel.TabIndex = 2;
			// 
			// WeightPanel
			// 
			this.WeightPanel.Location = new System.Drawing.Point(188, 222);
			this.WeightPanel.Name = "WeightPanel";
			this.WeightPanel.Size = new System.Drawing.Size(250, 39);
			this.WeightPanel.TabIndex = 3;
			// 
			// DiastolicBPPanel
			// 
			this.DiastolicBPPanel.Location = new System.Drawing.Point(437, 222);
			this.DiastolicBPPanel.Name = "DiastolicBPPanel";
			this.DiastolicBPPanel.Size = new System.Drawing.Size(250, 39);
			this.DiastolicBPPanel.TabIndex = 3;
			// 
			// SystolicBPPanel
			// 
			this.SystolicBPPanel.Location = new System.Drawing.Point(437, 184);
			this.SystolicBPPanel.Name = "SystolicBPPanel";
			this.SystolicBPPanel.Size = new System.Drawing.Size(250, 39);
			this.SystolicBPPanel.TabIndex = 3;
			// 
			// HeightPanel
			// 
			this.HeightPanel.Location = new System.Drawing.Point(188, 260);
			this.HeightPanel.Name = "HeightPanel";
			this.HeightPanel.Size = new System.Drawing.Size(250, 39);
			this.HeightPanel.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(82, 63);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Patient ID";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// PatientIDBox
			// 
			this.PatientIDBox.Location = new System.Drawing.Point(142, 60);
			this.PatientIDBox.Name = "PatientIDBox";
			this.PatientIDBox.Size = new System.Drawing.Size(100, 20);
			this.PatientIDBox.TabIndex = 6;
			// 
			// PatientNameBox
			// 
			this.PatientNameBox.Location = new System.Drawing.Point(142, 86);
			this.PatientNameBox.Name = "PatientNameBox";
			this.PatientNameBox.Size = new System.Drawing.Size(100, 20);
			this.PatientNameBox.TabIndex = 8;
			this.PatientNameBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(65, 86);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(71, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Patient Name";
			// 
			// PatientSexBox
			// 
			this.PatientSexBox.Location = new System.Drawing.Point(142, 107);
			this.PatientSexBox.Name = "PatientSexBox";
			this.PatientSexBox.Size = new System.Drawing.Size(100, 20);
			this.PatientSexBox.TabIndex = 10;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(75, 110);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Patient Sex";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(903, 450);
			this.Controls.Add(this.PatientSexBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.PatientNameBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.PatientIDBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.HeightPanel);
			this.Controls.Add(this.WeightPanel);
			this.Controls.Add(this.DiastolicBPPanel);
			this.Controls.Add(this.SystolicBPPanel);
			this.Controls.Add(this.AgePanel);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
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
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox PatientIDBox;
		private System.Windows.Forms.TextBox PatientNameBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox PatientSexBox;
		private System.Windows.Forms.Label label3;
	}
}

