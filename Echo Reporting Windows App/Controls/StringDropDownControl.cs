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
	public partial class StringDropDownControl : UserControl {
		private readonly MultipleChoiceResult Result;
		public StringDropDownControl(MultipleChoiceResult result) {
			InitializeComponent();
			Result = result;
			ResultTitleLabel.Text = result.Name;
			ResultValueComboBox.SelectedIndex = 0;
			ResultValueComboBox.Items.AddRange(result.Options.ToArray());
		}

		private void ResultValueComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			Result.Value = (string) ResultValueComboBox.SelectedItem;
		}
	}
}
