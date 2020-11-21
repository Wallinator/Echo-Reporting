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
using System.Windows.Forms.VisualStyles;

namespace Echo_Reporting_Windows_App {
	public partial class StringDropDownControl : UserControl {
		private readonly MultipleChoiceResult Result;
		public StringDropDownControl(MultipleChoiceResult result) {
			InitializeComponent();
			Result = result;
			if (result.Name.Length != 0) {
				ResultTitleLabel.Text = result.Name;
			}
			else {
				ResultTitleLabel.Text = result.DisplayName;
			}
			ResultPostfixLabel.Text = result.Postfix;
			ResultValueComboBox.Items.Clear();
			ResultValueComboBox.Width = 1;
			foreach (var x in result.Options) {
				int w = TextRenderer.MeasureText(x, ResultValueComboBox.Font).Width;
				//int w = TextRenderer.MeasureText(x, ResultValueComboBox.Font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.WordEllipsis).Width;
				//w += (int) (w * 0);
				w += 30;
				if (ResultValueComboBox.Width < w) {
					ResultValueComboBox.Width = w;
				}
			}
			ResultValueComboBox.Items.AddRange(result.Options.ToArray());
			ResultValueComboBox.SelectedIndex = 0;
		}

		private void ResultValueComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			Result.Value = (string) ResultValueComboBox.SelectedItem;
		}

		private void panel2_Paint(object sender, PaintEventArgs e) {

		}

		private void ResultTitleLabel_Click(object sender, EventArgs e) {

		}

		private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e) {

		}
	}
}
