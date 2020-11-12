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
		private ReportSections _rs;
		public ReportForm(StructuredReport report) {
			InitializeComponent();
			_report = report;
			_rs = new ReportSections(report);
			SitusTextBox.Text = _rs.Situs;
			SitusTextBox.TextChanged += new EventHandler(_TextChanged);

			SystemicVeinsTextbox.Text = _rs.SystemicVeins;
			SystemicVeinsTextbox.TextChanged += new EventHandler(_TextChanged);

			AtriaTextBox.Text = _rs.Atria;
			AtriaTextBox.TextChanged += new EventHandler(_TextChanged);

			AVValvesTextBox.Text = _rs.AVValves;
			AVValvesTextBox.TextChanged += new EventHandler(_TextChanged);

			VentriclesTextBox.Text = _rs.Ventricles;
			VentriclesTextBox.TextChanged += new EventHandler(_TextChanged);

			OutletsTextBox.Text = _rs.Outlets;
			OutletsTextBox.TextChanged += new EventHandler(_TextChanged);

			GreatArteriesTextBox.Text = _rs.GreatArteries;
			GreatArteriesTextBox.TextChanged += new EventHandler(_TextChanged);

			PulmonaryVeinsTextBox.Text = _rs.PulmonaryVeins;
			PulmonaryVeinsTextBox.TextChanged += new EventHandler(_TextChanged);

			CoronaryArteriesTextBox.Text = _rs.CoronaryArteries;
			CoronaryArteriesTextBox.TextChanged += new EventHandler(_TextChanged);

			OtherTextBox.Text = _rs.Other;
			OtherTextBox.TextChanged += new EventHandler(_TextChanged);

			ConclusionTextBox.Text = _rs.Conclusion;
			ConclusionTextBox.TextChanged += new EventHandler(_TextChanged);

			ReportingTextBox.Text = _rs.SignOff;
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

		private void generateReportToolStripMenuItem_Click(object sender, EventArgs e) {
			var name = _report.PatientData.PatientID.Value;
			var date = DateTime.Now.Date.ToShortDateString().Replace('/', '-');
			saveFileDialog1.FileName = date + "_generated_report_for_patient_" + name;
			if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
				var stream = saveFileDialog1.OpenFile();
				if (stream != null) {
					var writer = new StreamWriter(stream);
					writer.WriteLine("Patient Data - ");
					writer.WriteLine(_report.PatientData.PatientID.AsString());
					writer.WriteLine(_report.PatientData.PatientName.AsString());
					writer.WriteLine(_report.PatientData.PatientDOB.AsString());
					writer.WriteLine(_report.PatientData.PatientSex.AsString());
					writer.WriteLine(_report.PatientData.ReasonForStudy.AsString());
					writer.WriteLine(_report.PatientData.ReferringPhysician.AsString());
					writer.WriteLine(_report.PatientData.EchoType.AsString());
					writer.WriteLine(_report.PatientData.ReportingDoctor.AsString());
					writer.WriteLine(_report.PatientData.PatientAge.AsString());
					writer.WriteLine(_report.PatientData.PatientWeight.AsString());
					writer.WriteLine(_report.PatientData.PatientHeight.AsString());
					writer.WriteLine(_report.PatientData.SystolicBloodPressure.AsString());
					writer.WriteLine(_report.PatientData.DiastolicBloodPressure.AsString());
					writer.WriteLine(_report.PatientData.BSA.AsString());
					writer.WriteLine("");


					writer.WriteLine("Table - ");
					writer.WriteLine(_report.Results["IVSd"].AsString());
					writer.WriteLine(_report.Results["LVIDd"].AsString());
					writer.WriteLine(_report.Results["LVPWd"].AsString());

					writer.WriteLine(_report.Results["IVSs"].AsString());
					writer.WriteLine(_report.Results["LVIDs"].AsString());
					writer.WriteLine(_report.Results["LVPWs"].AsString());

					writer.WriteLine(_report.Results["Mitral valve E wave"].AsString());
					writer.WriteLine(_report.Results["Mitral valve A wave"].AsString());
					writer.WriteLine(_report.Results["MV decel time"].AsString());
					writer.WriteLine(_report.Results["Mitral annulus E'"].AsString());
					writer.WriteLine(_report.Results["Septal annulus E'"].AsString());

					writer.WriteLine(_report.Results["Fractional Shortening"].AsString());
					writer.WriteLine(_report.Results["Left Ventricular Teichholz EF"].AsString());
					writer.WriteLine(_report.Results["LV mass index"].AsString());
					writer.WriteLine(_report.Results["Heart Rate"].AsString());
					writer.WriteLine(_report.Results["MVCFc"].AsString());
					writer.WriteLine(_report.Results["Left ventricular cardiac output"].AsString());

					writer.WriteLine("");

					writer.WriteLine("Situs - " + _rs.Situs);
					writer.WriteLine("Systemic Veins - " + _rs.SystemicVeins);
					writer.WriteLine("AV Valves - " + _rs.AVValves);
					writer.WriteLine("Atria - " + _rs.Atria);
					writer.WriteLine("Ventricles - " + _rs.Ventricles);
					writer.WriteLine("Outlets - " + _rs.Outlets);
					writer.WriteLine("Great Arteries - " + _rs.GreatArteries);
					writer.WriteLine("Pulmonary Veins - " + _rs.PulmonaryVeins);
					writer.WriteLine("Coronary Arteries - " + _rs.CoronaryArteries);
					writer.WriteLine("Other - " + _rs.Other);
					writer.WriteLine("");
					writer.WriteLine("Conclusions - " + _report.ReportingOptions.Conclusion.Value);
					writer.WriteLine(_rs.Conclusion);
					writer.WriteLine("");
					writer.WriteLine("Reporting - " + _rs.SignOff);
					writer.Close();
					stream.Close();
				}
				else {
					MessageBox.Show("The selected file is invalid or in an unsupported format.", "Error - Invalid DICOM File", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
