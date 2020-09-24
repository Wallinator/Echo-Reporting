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

namespace Echo_Reporting_Windows_App {
	public partial class ResultControl : UserControl {
		private readonly Result result;

		public ResultControl(string name, string unitShortHand, double value = 0) : this(new Result(name, unitShortHand, value: value)) {
		}
		public ResultControl(Result result) {
			InitializeComponent();
			this.result = result;
			ResultTitleLabel.Text = result.Name;
			ResultUnitLabel.Text = result.UnitShorthand;
			ZScoreLabel.Text = "";
			if (!result.Empty) {
				UpdateValue(result.Value);
			}
			else {
				errorProvider1.SetError(ResultValueTextBox, result.Name + " value not found in file.");
			}
		}

		private void ValidateValue(object sender, EventArgs e) {
			try {
				double value = double.Parse(ResultValueTextBox.Text);
				errorProvider1.SetError(ResultValueTextBox, "");
				UpdateValue(value);
			}
			catch (Exception) {
				errorProvider1.SetError(ResultValueTextBox, "Not an decimal value.");
			}
		}
		private void UpdateValue(double value) {
			result.Value = value;
			ResultValueTextBox.Text = value.ToString();
			if (result.ZScoreable) {
				ZScoreLabel.Text = "Z Score: " + result.ZScore.ToString("N3");
			}
		}
		private void TextBoxKeyUp(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				ValidateValue(sender, e);
				e.Handled = true;
			}
		}
	}
}
