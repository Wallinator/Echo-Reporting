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
	public partial class StringFieldControl : UserControl {

		public StringFieldControl(string name, string value) {
			InitializeComponent();
			ResultTitleLabel.Text = name;
			ResultValueTextBox.Text = value;
		}
	}
}
