using Echo_Reporting_Backend.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using DICOMReporting.Data;

namespace Echo_Reporting_Windows_App {
	public partial class ReportForm : Form {
		private StructuredReport _report;
		public ReportForm(StructuredReport report) {
			InitializeComponent();
			_report = report;
			SitusTextBox.Text = _report.Sections.Situs;
			SitusTextBox.TextChanged += new EventHandler(_TextChanged);

			SystemicVeinsTextbox.Text = _report.Sections.SystemicVeins;
			SystemicVeinsTextbox.TextChanged += new EventHandler(_TextChanged);

			AtriaTextBox.Text = _report.Sections.Atria;
			AtriaTextBox.TextChanged += new EventHandler(_TextChanged);

			AVValvesTextBox.Text = _report.Sections.AVValves;
			AVValvesTextBox.TextChanged += new EventHandler(_TextChanged);

			VentriclesTextBox.Text = _report.Sections.Ventricles;
			VentriclesTextBox.TextChanged += new EventHandler(_TextChanged);

			OutletsTextBox.Text = _report.Sections.Outlets;
			OutletsTextBox.TextChanged += new EventHandler(_TextChanged);

			GreatArteriesTextBox.Text = _report.Sections.GreatArteries;
			GreatArteriesTextBox.TextChanged += new EventHandler(_TextChanged);

			PulmonaryVeinsTextBox.Text = _report.Sections.PulmonaryVeins;
			PulmonaryVeinsTextBox.TextChanged += new EventHandler(_TextChanged);

			CoronaryArteriesTextBox.Text = _report.Sections.CoronaryArteries;
			CoronaryArteriesTextBox.TextChanged += new EventHandler(_TextChanged);

			OtherTextBox.Text = _report.Sections.Other;
			OtherTextBox.TextChanged += new EventHandler(_TextChanged);

			ConclusionTextBox.Text = _report.Sections.Conclusion;
			ConclusionTextBox.TextChanged += new EventHandler(_TextChanged);

			ReportingTextBox.Text = _report.Sections.SignOff;
			ReportingTextBox.TextChanged += new EventHandler(_TextChanged);
		}

		private void _TextChanged(object sender, EventArgs e) {
			_report.Sections.Situs = SitusTextBox.Text;
			_report.Sections.SystemicVeins = SystemicVeinsTextbox.Text;
			_report.Sections.Atria = AtriaTextBox.Text;
			_report.Sections.AVValves = AVValvesTextBox.Text;
			_report.Sections.Ventricles = VentriclesTextBox.Text;
			_report.Sections.Outlets = OutletsTextBox.Text;
			_report.Sections.GreatArteries = GreatArteriesTextBox.Text;
			_report.Sections.PulmonaryVeins = PulmonaryVeinsTextBox.Text;
			_report.Sections.CoronaryArteries = CoronaryArteriesTextBox.Text;
			_report.Sections.Other = OtherTextBox.Text;
			_report.Sections.Conclusion = ConclusionTextBox.Text;
			_report.Sections.SignOff = ReportingTextBox.Text;
		}

		private void ReportForm_Load(object sender, EventArgs e) {

		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {

		}

		private void generateReportToolStripMenuItem_Click(object sender, EventArgs e) {
			var name = _report.PatientData.PatientID.Value;
			var date = DateTime.Now.Date.ToShortDateString().Replace('/', '-');
			saveFileDialog1.FileName = date + "_generated_report_for_patient_" + name;
			if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
				var stream = saveFileDialog1.OpenFile();
				if (stream != null) {
					_report.WriteGeneratedReport(stream);
					stream.Close();
				}
				else {
					MessageBox.Show("The selected file is invalid or in an unsupported format.", "Error - Invalid DICOM File", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
