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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Echo_Reporting_Windows_App {
	public partial class MainForm : Form {
		private StructuredReport report = new StructuredReport();
		public MainForm() {
			InitializeComponent();
			ShowAllResults(false);
		}

		private void openFolderToolStripMenuItem_Click(object sender, EventArgs e) {
			CommonOpenFileDialog dialog = new CommonOpenFileDialog {
				IsFolderPicker = true
			};

			if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
				var folder = dialog.FileName;
				var enumerator = new DicomFileEnumerator(new DirectoryInfo(folder));
				while (enumerator.MoveNext()) {
					Debug.WriteLine(enumerator.Current.File.Name);
					report = DicomReader.GetStructuredReport(enumerator.Current.Dataset);
					ShowAllResults();
					return;
				}
				MessageBox.Show("The selected folder does not contain any supported DICOM files.", "Error - Dicom File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
					MessageBox.Show("The selected file is invalid or in an unsupported format.", "Error - Invalid DICOM File", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		private void ShowAllResults(bool showNotFoundError = true) {
			ClearAllPanels();
			PatientIDPanel.Controls.Add(new StringFieldControl(report.PatientData.PatientID));
			PatientNamePanel.Controls.Add(new StringFieldControl(report.PatientData.PatientName));
			PatientSexPanel.Controls.Add(new StringFieldControl(report.PatientData.PatientSex));
			AgePanel.Controls.Add(new ResultControl(report.PatientData.PatientAge, showNotFoundError));
			HeightPanel.Controls.Add(new ResultControl(report.PatientData.PatientSize, showNotFoundError));
			WeightPanel.Controls.Add(new ResultControl(report.PatientData.PatientWeight, showNotFoundError));
			DiastolicBPPanel.Controls.Add(new ResultControl(report.PatientData.DiastolicBloodPressure, showNotFoundError));
			SystolicBPPanel.Controls.Add(new ResultControl(report.PatientData.SystolicBloodPressure, showNotFoundError));

			IVSdPanel.Controls.Add(new ResultControl(report.Results["IVSd"], showNotFoundError));
			LVIDdPanel.Controls.Add(new ResultControl(report.Results["LVIDd"], showNotFoundError));
			LVPWdPanel.Controls.Add(new ResultControl(report.Results["LVPWd"], showNotFoundError));

			IVSsPanel.Controls.Add(new ResultControl(report.Results["IVSs"], showNotFoundError));
			LVIDsPanel.Controls.Add(new ResultControl(report.Results["LVIDs"], showNotFoundError));
			LVPWsPanel.Controls.Add(new ResultControl(report.Results["LVPWs"], showNotFoundError));

			MVEWavePanel.Controls.Add(new ResultControl(report.Results["Mitral valve E wave"], showNotFoundError));
			MVAWavePanel.Controls.Add(new ResultControl(report.Results["Mitral valve A wave"], showNotFoundError));
			MVDecelTimePanel.Controls.Add(new ResultControl(report.Results["MV decel time"], showNotFoundError));
			MitralAnnulusEPanel.Controls.Add(new ResultControl(report.Results["Mitral annulus E'"], showNotFoundError));
			SeptalAnnulusEPanel.Controls.Add(new ResultControl(report.Results["Septal annulus E'"], showNotFoundError));
			// RV E' LVPWsPanel.Controls.Add(new ResultControl(report.Results["LVPWs"], showNotFoundError)); 

			FractionalShorteningPanel.Controls.Add(new ResultControl(report.Results["Fractional Shortening"], showNotFoundError));
			LVTeichholzEFPanel.Controls.Add(new ResultControl(report.Results["Left Ventricular Teichholz EF"], showNotFoundError));
			LVMassIndexPanel.Controls.Add(new ResultControl(report.Results["LV mass index"], showNotFoundError));
			HeartRatePanel.Controls.Add(new ResultControl(report.Results["Heart Rate"], showNotFoundError));
			MVCFcPanel.Controls.Add(new ResultControl(report.Results["MVCFc"], showNotFoundError));
			LVOutputPanel.Controls.Add(new ResultControl(report.Results["Left ventricular cardiac output"], showNotFoundError));
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

			IVSdPanel.Controls.Clear();
			LVIDdPanel.Controls.Clear();
			LVPWdPanel.Controls.Clear();

			IVSsPanel.Controls.Clear();
			LVIDsPanel.Controls.Clear();
			LVPWsPanel.Controls.Clear();


			MVEWavePanel.Controls.Clear();
			MVAWavePanel.Controls.Clear();
			MVDecelTimePanel.Controls.Clear();
			MitralAnnulusEPanel.Controls.Clear();
			SeptalAnnulusEPanel.Controls.Clear();
			// RV E' panel7.Controls.Clear();

			FractionalShorteningPanel.Controls.Clear();
			LVTeichholzEFPanel.Controls.Clear();
			LVMassIndexPanel.Controls.Clear();
			HeartRatePanel.Controls.Clear();
			MVCFcPanel.Controls.Clear();
			LVOutputPanel.Controls.Clear();
		}

		private void MainForm_Load(object sender, EventArgs e) {

		}
	}
}
