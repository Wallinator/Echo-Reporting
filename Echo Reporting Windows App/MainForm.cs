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

			SitusPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.Situs));
			SystemicVeinsPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.SystemicVeins));

			// Atria
			NormalAtriaPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.AtriaSize));
			IntactAtrialSeptumPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.AtriaSeptum));
			DilatedLeftAtriumPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.AtriaLeft));
			DilatedRightAtriumPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.AtriaRight));
			DilatedBilaterallyAtriumPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.AtriaBilateral));
			PatentForamenOvalePanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.AtriaPatentForamenOvale));
			ASD1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.ASD1));
			ASD2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.ASD2));
			ASD3Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.ASD3));
			ASDDimensionPanel.Controls.Add(new ResultControl(report.Results["Atrial Septal Defect dimension"], showNotFoundError));
			ASDGradientPanel.Controls.Add(new ResultControl(report.Results["Atrial Septal Defect mean gradient"], showNotFoundError));

			// AV Valves
			AVConnectionPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.AVValves));
			MV1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.MitralValve1));
			MV2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.MitralValve2));
			MV3Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.MitralValve3));
			MitralProlapsePanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.MitralValveProlapse));

			LAVV1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.LAVV1));
			LAVV2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.LAVV2));
			LAVV3Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.LAVV3));

			MVAnnulusPanel.Controls.Add(new ResultControl(report.Results["Mitral valve annulus"], showNotFoundError));
			MVEPanel.Controls.Add(new ResultControl(report.Results["Mitral valve E wave"], showNotFoundError));
			MVAPanel.Controls.Add(new ResultControl(report.Results["Mitral valve A wave"], showNotFoundError));
			MVEARatioPanel.Controls.Add(new ResultControl(report.Results["Mitral E/A ratio"], showNotFoundError));
			MVInflowAPanel.Controls.Add(new ResultControl(report.Results["Mitral valve inflow A wave duration"], showNotFoundError));
			MVDecelPanel.Controls.Add(new ResultControl(report.Results["MV decel time"], showNotFoundError));
			MVInflowGradPanel.Controls.Add(new ResultControl(report.Results["Mitral valve inflow mean gradient"], showNotFoundError));
			MVRegurgVelPanel.Controls.Add(new ResultControl(report.Results["Mitral valve regurgitation peak velocity"], showNotFoundError));
			MVRegurgGradPanel.Controls.Add(new ResultControl(report.Results["Mitral valve regurgitation peak gradient"], showNotFoundError));

			Tri1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.TriscupidValve1));
			Tri2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.TriscupidValve2));
			Tri3Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.TriscupidValve3));
			RAVV1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.RAVV1));
			RAVV2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.RAVV2));

			InsufficentTRPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.InsufficientTR));

			TVAnnulusPanel.Controls.Add(new ResultControl(report.Results["Tricuspid valve annulus"], showNotFoundError));
			TVInflowPanel.Controls.Add(new ResultControl(report.Results["Tricuspid valve inflow mean gradient"], showNotFoundError));
			TVRegurgVelPanel.Controls.Add(new ResultControl(report.Results["Tricuspid valve regurgitation peak velocity"], showNotFoundError));
			TVRegurgGradPanel.Controls.Add(new ResultControl(report.Results["Tricuspid valve regurgitation peak gradient"], showNotFoundError));
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

			NormalAtriaPanel.Controls.Clear();
			IntactAtrialSeptumPanel.Controls.Clear();
			DilatedLeftAtriumPanel.Controls.Clear();
			DilatedRightAtriumPanel.Controls.Clear();
			DilatedBilaterallyAtriumPanel.Controls.Clear();
			PatentForamenOvalePanel.Controls.Clear();
			ASD1Panel.Controls.Clear();
			ASD2Panel.Controls.Clear();
			ASD3Panel.Controls.Clear();
			ASDDimensionPanel.Controls.Clear();
			ASDGradientPanel.Controls.Clear();

			//AV Valves

			AVConnectionPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.AVValves));
			MV1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.MitralValve1));
			MV2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.MitralValve2));
			MV3Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.MitralValve3));
			MitralProlapsePanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.MitralValveProlapse));

			LAVV1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.LAVV1));
			LAVV2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.LAVV2));
			LAVV3Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.LAVV3));

			MVAnnulusPanel.Controls.Clear();
			MVEPanel.Controls.Clear();
			MVAPanel.Controls.Clear();
			MVEARatioPanel.Controls.Clear();
			MVInflowAPanel.Controls.Clear();
			MVDecelPanel.Controls.Clear();
			MVInflowGradPanel.Controls.Clear();
			MVRegurgVelPanel.Controls.Clear();
			MVRegurgGradPanel.Controls.Clear();

			Tri1Panel.Controls.Clear();
			Tri2Panel.Controls.Clear();
			Tri3Panel.Controls.Clear();
			RAVV1Panel.Controls.Clear();
			RAVV2Panel.Controls.Clear();

			InsufficentTRPanel.Controls.Clear()
				;
			TVAnnulusPanel.Controls.Clear();
			TVInflowPanel.Controls.Clear();
			TVRegurgVelPanel.Controls.Clear();
			TVRegurgGradPanel.Controls.Clear();
		}

		private void MainForm_Load(object sender, EventArgs e) {

		}
	}
}
