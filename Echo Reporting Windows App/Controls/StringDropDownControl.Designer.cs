using System.Collections.Generic;

namespace Echo_Reporting_Windows_App {
	partial class StringDropDownControl {
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
			this.ResultValueComboBox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// ResultTitleLabel
			// 
			this.ResultTitleLabel.AutoSize = true;
			this.ResultTitleLabel.Location = new System.Drawing.Point(3, 6);
			this.ResultTitleLabel.Name = "ResultTitleLabel";
			this.ResultTitleLabel.Size = new System.Drawing.Size(83, 13);
			this.ResultTitleLabel.TabIndex = 0;
			this.ResultTitleLabel.Text = "ResultTitleLabel";
			// 
			// ResultValueComboBox
			// 
			this.ResultValueComboBox.FormattingEnabled = true;
			this.ResultValueComboBox.Location = new System.Drawing.Point(88, 3);
			this.ResultValueComboBox.Name = "ResultValueComboBox";
			this.ResultValueComboBox.Size = new System.Drawing.Size(121, 21);
			this.ResultValueComboBox.TabIndex = 1;
			this.ResultValueComboBox.SelectedIndexChanged += new System.EventHandler(this.ResultValueComboBox_SelectedIndexChanged);
			// 
			// StringDropDownControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.ResultValueComboBox);
			this.Controls.Add(this.ResultTitleLabel);
			this.Name = "StringDropDownControl";
			this.Size = new System.Drawing.Size(254, 26);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label ResultTitleLabel;
		private System.Windows.Forms.ComboBox ResultValueComboBox;
	}
}
