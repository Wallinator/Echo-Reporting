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
			SitusTextBox.Text = rs.Situs;
			SitusTextBox.TextChanged += new EventHandler(_TextChanged);

			SystemicVeinsTextbox.Text = rs.SystemicVeins;
			SystemicVeinsTextbox.TextChanged += new EventHandler(_TextChanged);

			AtriaTextBox.Text = rs.Atria;
			AtriaTextBox.TextChanged += new EventHandler(_TextChanged);

			AVValvesTextBox.Text = rs.AVValves;
			AVValvesTextBox.TextChanged += new EventHandler(_TextChanged);

			VentriclesTextBox.Text = rs.Ventricles;
			VentriclesTextBox.TextChanged += new EventHandler(_TextChanged);

			OutletsTextBox.Text = rs.Outlets;
			OutletsTextBox.TextChanged += new EventHandler(_TextChanged);

			GreatArteriesTextBox.Text = rs.GreatArteries;
			GreatArteriesTextBox.TextChanged += new EventHandler(_TextChanged);

			PulmonaryVeinsTextBox.Text = rs.PulmonaryVeins;
			PulmonaryVeinsTextBox.TextChanged += new EventHandler(_TextChanged);

			CoronaryArteriesTextBox.Text = rs.CoronaryArteries;
			CoronaryArteriesTextBox.TextChanged += new EventHandler(_TextChanged);

			OtherTextBox.Text = rs.Other;
			OtherTextBox.TextChanged += new EventHandler(_TextChanged);

			ConclusionTextBox.Text = rs.Conclusion;
			ConclusionTextBox.TextChanged += new EventHandler(_TextChanged);

			ReportingTextBox.Text = rs.SignOff;
			ReportingTextBox.TextChanged += new EventHandler(_TextChanged);
		}

		private void _TextChanged(object sender, EventArgs e) {
			_rs.Situs = SitusTextBox.Text;
			_rs.SystemicVeins = SystemicVeinsTextbox.Text;
			_rs.Atria = AtriaTextBox.Text;
			_rs.AVValves = AVValvesTextBox.Text;
			_rs.Ventricles = VentriclesTextBox.Text;
			_rs.Outlets = OutletsTextBox.Text;
			_rs.GreatArteries = GreatArteriesTextBox.Text;
			_rs.PulmonaryVeins = PulmonaryVeinsTextBox.Text;
			_rs.CoronaryArteries = CoronaryArteriesTextBox.Text;
			_rs.Other = OtherTextBox.Text;
			_rs.Conclusion = ConclusionTextBox.Text;
			_rs.SignOff = ReportingTextBox.Text;
		}

		private void ReportForm_Load(object sender, EventArgs e) {

		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {

		}
	}
}
