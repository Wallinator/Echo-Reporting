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
			this.ResultPostfixLabel = new System.Windows.Forms.Label();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.ResultValueComboBox = new Echo_Reporting_Windows_App.Controls.ComboListBox();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ResultTitleLabel
			// 
			this.ResultTitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.ResultTitleLabel.AutoSize = true;
			this.ResultTitleLabel.Location = new System.Drawing.Point(3, 7);
			this.ResultTitleLabel.Name = "ResultTitleLabel";
			this.ResultTitleLabel.Size = new System.Drawing.Size(83, 13);
			this.ResultTitleLabel.TabIndex = 0;
			this.ResultTitleLabel.Text = "ResultTitleLabel";
			this.ResultTitleLabel.Click += new System.EventHandler(this.ResultTitleLabel_Click);
			// 
			// ResultPostfixLabel
			// 
			this.ResultPostfixLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.ResultPostfixLabel.AutoSize = true;
			this.flowLayoutPanel1.SetFlowBreak(this.ResultPostfixLabel, true);
			this.ResultPostfixLabel.Location = new System.Drawing.Point(245, 7);
			this.ResultPostfixLabel.Name = "ResultPostfixLabel";
			this.ResultPostfixLabel.Size = new System.Drawing.Size(94, 13);
			this.ResultPostfixLabel.TabIndex = 1;
			this.ResultPostfixLabel.Text = "ResultPostfixLabel";
			this.ResultPostfixLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.ResultTitleLabel);
			this.flowLayoutPanel1.Controls.Add(this.ResultValueComboBox);
			this.flowLayoutPanel1.Controls.Add(this.ResultPostfixLabel);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(550, 26);
			this.flowLayoutPanel1.TabIndex = 2;
			this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
			// 
			// ResultValueComboBox
			// 
			this.ResultValueComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.ResultValueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ResultValueComboBox.Location = new System.Drawing.Point(92, 3);
			this.ResultValueComboBox.Name = "ResultValueComboBox";
			this.ResultValueComboBox.Size = new System.Drawing.Size(147, 21);
			this.ResultValueComboBox.TabIndex = 1;
			this.ResultValueComboBox.SelectedIndexChanged += new System.EventHandler(this.ResultValueComboBox_SelectedIndexChanged);
			// 
			// StringDropDownControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.flowLayoutPanel1);
			this.Name = "StringDropDownControl";
			this.Size = new System.Drawing.Size(550, 26);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label ResultTitleLabel;
		//private ComboListBox ResultValueComboBox;
		private System.Windows.Forms.Label ResultPostfixLabel;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private ComboListBox ResultValueComboBox;
	}
}
