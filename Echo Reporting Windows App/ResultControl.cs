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

		public ResultControl(string name, string unitShortHand, double value = 0, bool showNotFoundError = true) : this(new Result(name, unitShortHand, value: value), showNotFoundError) {
		}
		public ResultControl(Result result, bool showNotFoundError = true) {
			InitializeComponent();
			this.result = result;
			ResultTitleLabel.Text = result.Name;
			ResultUnitLabel.Text = result.UnitShorthand;
			ZScoreLabel.Text = "";
			if (showNotFoundError && result.Empty) {
				errorProvider1.SetError(this, result.Name + " value not found in file.");
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
				errorProvider1.SetError(this, "");
				result.Empty = false;
				ResultValueTextBox.Text = value.ToString();
				UpdateValue(value);
			}
			catch (Exception) {
				errorProvider1.SetError(this, "Not a decimal value.");
				result.Empty = true;
				UpdateValue();
			}
		}
		public void UpdateValue() {
			UpdateValue(result.Value);
		}
		private void UpdateValue(double value) {
			result.Value = value;
			if (result.ZScoreable) {
				if (result.Empty) {
					ZScoreLabel.Text = "Z Score: Unavailable.";
				}
				else {
					ZScoreLabel.Text = "Z Score: " + result.ZScore.ToString("N3");
				}
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
