using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DICOMReporting.Data;
using DICOMReporting.Data.Results;

namespace Echo_Reporting_Windows_App {
	public partial class BoolCheckControl : UserControl {
		private readonly BoolResult Result;
		public BoolCheckControl(BoolResult result) {
			InitializeComponent();
			Result = result;
			ResultTitleLabel.Text = result.Name;
			ResultCheckBox.Text = result.OptionName;
			ResultCheckBox.Checked = result.Value;
		}

		private void ResultCheckBox_CheckedChanged(object sender, EventArgs e) {
			Result.Value = ResultCheckBox.Checked;
		}

		private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e) {

		}
	}
}
