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
			if (result.Name.Length == 0) {
				ResultTitleLabel.Text = result.Name;
			}
			else {
				ResultTitleLabel.Text = result.DisplayName;
			}
			ResultPostfixLabel.Text = result.Postfix;
			ResultValueComboBox.Items.AddRange(result.Options.ToArray());
			ResultValueComboBox.SelectedIndex = 0;
			ResultValueComboBox.Width = 0;
			foreach (var x in ResultValueComboBox.Items) {
				int w = TextRenderer.MeasureText((string) x, Font).Width + 38;
				if (ResultValueComboBox.Width < w) {
					ResultValueComboBox.Width = w;
				}
			}
		}

		private void ResultValueComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			Result.Value = (string) ResultValueComboBox.SelectedItem;
		}

		private void panel2_Paint(object sender, PaintEventArgs e) {

		}
	}
}
