using DICOMReporting.Data;
using DICOMReporting.DicomFileReading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Echo_Reporting_Windows_App {
	public partial class MainForm : Form {
		private StructuredReport report = new StructuredReport();
		public MainForm() {
			InitializeComponent();
			ShowAllResults(false);
		}

		private void openFolderToolStripMenuItem_Click(object sender, EventArgs e) {
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
				var folder = folderBrowserDialog1.SelectedPath;
				var enumerator = new DicomFileEnumerator(new DirectoryInfo(folder));
				if (enumerator.MoveNext()) {
					Debug.WriteLine(enumerator.Current.File.Name);
					DicomReader.GetStructuredReport(enumerator.Current.Dataset);
				}
			}
		}

		private void openFileToolStripMenuItem_Click(object sender, EventArgs e) {
			if (openFileDialog1.ShowDialog() == DialogResult.OK) {
				var path = openFileDialog1.FileName;
				var file = DicomFileEnumerator.TryOpen(path);
				if (file != null) {
					report = DicomReader.GetStructuredReport(file.Dataset);
					ShowAllResults();
				}
				else {
					MessageBox.Show("The selected file is invalid or in an unsupported format.", "Error - Invalid Dicom File", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		private void ShowAllResults(bool showNotFoundError = true) {
			ClearAllPanels();
			PatientIDPanel.Controls.Add(new StringFieldControl("Patient ID", report.PatientData.PatientID));
			PatientNamePanel.Controls.Add(new StringFieldControl("Patient Name", report.PatientData.PatientName));
			PatientSexPanel.Controls.Add(new StringFieldControl("Patient Sex", report.PatientData.PatientSex));
			AgePanel.Controls.Add(new ResultControl(report.PatientData.PatientAge, showNotFoundError));
			HeightPanel.Controls.Add(new ResultControl(report.PatientData.PatientSize, showNotFoundError));
			WeightPanel.Controls.Add(new ResultControl(report.PatientData.PatientWeight, showNotFoundError));
			DiastolicBPPanel.Controls.Add(new ResultControl(report.PatientData.DiastolicBloodPressure, showNotFoundError));
			SystolicBPPanel.Controls.Add(new ResultControl(report.PatientData.SystolicBloodPressure, showNotFoundError));
		}
		private void ClearAllPanels() {
			PatientIDPanel.Controls.Clear();
			PatientNamePanel.Controls.Clear();
			PatientSexPanel.Controls.Clear();
			AgePanel.Controls.Clear();
			HeightPanel.Controls.Clear();
			WeightPanel.Controls.Clear();
			DiastolicBPPanel.Controls.Clear();
			SystolicBPPanel.Controls.Clear();
		}

		private void MainForm_Load(object sender, EventArgs e) {

		}

		private void label1_Click(object sender, EventArgs e) {

		}

		private void textBox2_TextChanged(object sender, EventArgs e) {

		}

		private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {

		}
	}
}
