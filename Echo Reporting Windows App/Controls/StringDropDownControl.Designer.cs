using Echo_Reporting_Windows_App.Controls;
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.ResultValueComboBox = new Echo_Reporting_Windows_App.Controls.ComboListBox();
			this.ResultPostfixLabel = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
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
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel1.Controls.Add(this.ResultTitleLabel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(89, 26);
			this.panel1.TabIndex = 2;
			// 
			// panel2
			// 
			this.panel2.AutoSize = true;
			this.panel2.Controls.Add(this.ResultPostfixLabel);
			this.panel2.Controls.Add(this.ResultValueComboBox);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(89, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(394, 26);
			this.panel2.TabIndex = 3;
			// 
			// ResultValueComboBox
			// 
			this.ResultValueComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.ResultValueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ResultValueComboBox.FormattingEnabled = true;
			this.ResultValueComboBox.Location = new System.Drawing.Point(6, 2);
			this.ResultValueComboBox.Name = "ResultValueComboBox";
			this.ResultValueComboBox.Size = new System.Drawing.Size(147, 21);
			this.ResultValueComboBox.TabIndex = 1;
			// 
			// ResultPostfixLabel
			// 
			this.ResultPostfixLabel.AutoSize = true;
			this.ResultPostfixLabel.Location = new System.Drawing.Point(279, 5);
			this.ResultPostfixLabel.Name = "ResultPostfixLabel";
			this.ResultPostfixLabel.Size = new System.Drawing.Size(94, 13);
			this.ResultPostfixLabel.TabIndex = 1;
			this.ResultPostfixLabel.Text = "ResultPostfixLabel";
			// 
			// StringDropDownControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "StringDropDownControl";
			this.Size = new System.Drawing.Size(483, 26);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label ResultTitleLabel;
		private ComboListBox ResultValueComboBox;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label ResultPostfixLabel;
	}
}
