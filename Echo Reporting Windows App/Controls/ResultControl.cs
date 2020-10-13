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
	public partial class ResultControl : UserControl {
		private readonly Result result;

		public ResultControl(string name, string unitShortHand, double value = 0, bool showNotFoundError = true) : this(new Result(name, unitShortHand, value: value), showNotFoundError) {
		}
		public ResultControl(Result result, bool showNotFoundError = true) {
			InitializeComponent();
			this.result = result;
			ResultTitleLabel.Text = result.Name;
			ResultUnitLabel.Text = result.UnitShorthand;
			ZScoreLabel.Text = "";
			Anomaly.Text = "";
			if (showNotFoundError && result.Empty) {
				errorProvider1.SetError(ResultUnitLabel, result.Name + " value not found in file.");
			}
			else {
				ResultValueTextBox.Text = result.Value.ToString();
				UpdateValue(result.Value);
			}
		}

		private void ValidateValue(object sender, EventArgs e) {
			try {
				double value = double.Parse(ResultValueTextBox.Text);
				errorProvider1.Clear();
				errorProvider1.SetError(ResultUnitLabel, "");
				result.Empty = false;
				UpdateValue(value);
			}
			catch (Exception) {
				errorProvider1.SetError(ResultUnitLabel, "Not a decimal value.");
				result.Empty = true;
				UpdateValue();
			}
		}
		public void UpdateValue() {
			UpdateValue(result.Value);
		}
		private void UpdateValue(double value) {
			result.Value = value;
			Anomaly.Text = result.AnomalyText;
			if (result.Empty) {
				ResultValueTextBox.Text = "";
			}
			else {
				ResultValueTextBox.Text = Math.Round(value, 3).ToString();
			}
			if (result.ZScoreable) {
				if (result.Empty) {
					ZScoreLabel.Text = "Z Score: Unavailable.";
				}
				else {
					ZScoreLabel.Text = "Z Score: " + result.ZScore.ToString("N2");
				}
			}
		}
		private void TextBoxKeyUp(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				ValidateValue(sender, e);
				e.Handled = true;
			}
		}

		private void ResultValueTextBox_TextChanged(object sender, EventArgs e) {

		}

		private void ResultControl_Load(object sender, EventArgs e) {

		}
	}
}
