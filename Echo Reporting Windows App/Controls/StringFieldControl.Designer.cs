namespace Echo_Reporting_Windows_App {
	partial class StringFieldControl {
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
			this.ResultTitleLabel = new System.Windows.Forms.Label();
			this.ResultValueTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// ResultTitleLabel
			// 
			this.ResultTitleLabel.AutoSize = true;
			this.ResultTitleLabel.Location = new System.Drawing.Point(-1, 3);
			this.ResultTitleLabel.Name = "ResultTitleLabel";
			this.ResultTitleLabel.Size = new System.Drawing.Size(83, 13);
			this.ResultTitleLabel.TabIndex = 0;
			this.ResultTitleLabel.Text = "ResultTitleLabel";
			// 
			// ResultValueTextBox
			// 
			this.ResultValueTextBox.Location = new System.Drawing.Point(30, 21);
			this.ResultValueTextBox.Name = "ResultValueTextBox";
			this.ResultValueTextBox.Size = new System.Drawing.Size(96, 20);
			this.ResultValueTextBox.TabIndex = 3;
			this.ResultValueTextBox.TextChanged += new System.EventHandler(this.ResultValueTextBox_TextChanged);
			// 
			// StringFieldControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.ResultValueTextBox);
			this.Controls.Add(this.ResultTitleLabel);
			this.Name = "StringFieldControl";
			this.Size = new System.Drawing.Size(254, 54);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label ResultTitleLabel;
		private System.Windows.Forms.TextBox ResultValueTextBox;
	}
}
