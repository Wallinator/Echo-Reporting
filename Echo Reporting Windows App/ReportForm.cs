using Echo_Reporting_Backend.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Echo_Reporting_Windows_App {
	public partial class ReportForm : Form {
		private ReportSections _rs;
		public ReportForm(ReportSections rs) {
			InitializeComponent();
			_rs = rs;
			SitusTextbox.Text = rs.Situs;
			SystemicVeinsTextbox.Text = rs.SystemicVeins;
		}

		private void ReportForm_Load(object sender, EventArgs e) {

		}
	}
}
