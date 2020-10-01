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
	public partial class StringFieldControl : UserControl {
		private readonly StringResult Result;
		public StringFieldControl(StringResult result) {
			InitializeComponent();
			Result = result;
			ResultTitleLabel.Text = result.Name;
			ResultValueTextBox.Text = result.Value;
		}
		private void ResultValueTextBox_TextChanged(object sender, EventArgs e) {
			Result.Value = ResultValueTextBox.Text;
		}
	}
}
