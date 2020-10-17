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
			// Patient Data
			#region
			PatientIDPanel.Controls.Add(new StringFieldControl(report.PatientData.PatientID));
			PatientNamePanel.Controls.Add(new StringFieldControl(report.PatientData.PatientName));
			PatientSexPanel.Controls.Add(new StringFieldControl(report.PatientData.PatientSex));
			AgePanel.Controls.Add(new ResultControl(report.PatientData.PatientAge, showNotFoundError));
			HeightPanel.Controls.Add(new ResultControl(report.PatientData.PatientSize, showNotFoundError));
			WeightPanel.Controls.Add(new ResultControl(report.PatientData.PatientWeight, showNotFoundError));
			DiastolicBPPanel.Controls.Add(new ResultControl(report.PatientData.DiastolicBloodPressure, showNotFoundError));
			SystolicBPPanel.Controls.Add(new ResultControl(report.PatientData.SystolicBloodPressure, showNotFoundError));

			ReasonForStudyPanel.Controls.Add(new StringFieldControl(report.PatientData.ReasonForStudy));
			ReferringPhysicianPanel.Controls.Add(new StringFieldControl(report.PatientData.ReferringPhysician));
			EchoTypePanel.Controls.Add(new StringDropDownControl(report.PatientData.EchoType));
			ReportingDoctorPanel.Controls.Add(new StringDropDownControl(report.PatientData.ReportingDoctor));
			#endregion
			// Table
			#region
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
			#endregion
			// Situs and Systemic Veins
			#region
			SitusPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.Situs));
			SystemicVeinsPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.SystemicVeins));
			#endregion
			// Atria
			#region
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
			#endregion
			// AV Valves
			#region
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
			//MVInflowAPanel.Controls.Add(new ResultControl(report.Results["Mitral valve inflow A wave duration"], showNotFoundError));
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
			
			//TVRegurgGradPanel.Controls.Add(new ResultControl(report.Results["Tricuspid valve regurgitation peak gradient"], showNotFoundError));
			TVRegurgGradPanel.Controls.Add(new ResultControl(report.Results["Estimated RV systolic pressure"], showNotFoundError));
			#endregion
			// Ventricles
			#region
			VentricleSizeFunctionPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.VentricleFunction));
			VentricularHypertrophyPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.VentricularHypertrophy));

			IVSd2Panel.Controls.Add(new ResultControl(report.Results["IVSd"], showNotFoundError));
			LVIDd2Panel.Controls.Add(new ResultControl(report.Results["LVIDd"], showNotFoundError));
			LVPWd2Panel.Controls.Add(new ResultControl(report.Results["LVPWd"], showNotFoundError));
			LVMassIndex2Panel.Controls.Add(new ResultControl(report.Results["LV mass index"], showNotFoundError));
			FractionalShortening2Panel.Controls.Add(new ResultControl(report.Results["Fractional Shortening"], showNotFoundError));
			LVTeichholz2Panel.Controls.Add(new ResultControl(report.Results["Left Ventricular Teichholz EF"], showNotFoundError));
			LVBiplaneEFPanel.Controls.Add(new ResultControl(report.Results["Left Ventricular biplane EF"], showNotFoundError));
			LVApical42Panel.Controls.Add(new ResultControl(report.Results["Left ventricular Apical 4 chamber EF"], showNotFoundError));
			HeartRate2Panel.Controls.Add(new ResultControl(report.Results["Heart Rate"], showNotFoundError));


			DilatedLVPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.LV1));
			HypertrophyLVPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.LV2));
			ReducedLVPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.LV3));

			LVSystolicPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.LVSystolicFunction));
			LVDiastolicPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.NormalDiastolic));


			LVIVRTPanel.Controls.Add(new ResultControl(report.Results["LV IVRT"], showNotFoundError));
			MyoPIPanel.Controls.Add(new ResultControl(report.Results["Myocardial Performance Index"], showNotFoundError));

			PulmSWavePanel.Controls.Add(new ResultControl(report.Results["Pulm vein S wave"], showNotFoundError));
			PulmDWavePanel.Controls.Add(new ResultControl(report.Results["Pulm vein D wave"], showNotFoundError));
			PulmAWavePanel.Controls.Add(new ResultControl(report.Results["Pulm vein A wave"], showNotFoundError));

			PulmWaveDurationPanel.Controls.Add(new ResultControl(report.Results["Pulmonary vein A wave duration"], showNotFoundError));
			MitralWaveDurationPanel.Controls.Add(new ResultControl(report.Results["Mitral valve inflow A wave duration"], showNotFoundError));

			MitralAnnulusE2Panel.Controls.Add(new ResultControl(report.Results["Mitral annulus E'"], showNotFoundError));
			MitralAnnulusA2Panel.Controls.Add(new ResultControl(report.Results["Mitral annulus A'"], showNotFoundError));
			MitralAnnulusS2Panel.Controls.Add(new ResultControl(report.Results["Mitral annulus S'"], showNotFoundError));

			SeptalAnnulusE2Panel.Controls.Add(new ResultControl(report.Results["Septal annulus E'"], showNotFoundError));
			SeptalAnnulusA2Panel.Controls.Add(new ResultControl(report.Results["Septal annulus A'"], showNotFoundError));
			SeptalAnnulusS2Panel.Controls.Add(new ResultControl(report.Results["Septal annulus S'"], showNotFoundError));

			TriAnnulusE2Panel.Controls.Add(new ResultControl(report.Results["Tricuspid annulus E'"], showNotFoundError));
			TriAnnulusA2Panel.Controls.Add(new ResultControl(report.Results["Tricuspid annulus A'"], showNotFoundError));
			TriAnnulusS2Panel.Controls.Add(new ResultControl(report.Results["Tricuspid annulus S'"], showNotFoundError));


			DilatedRVPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.RV1));
			HypertrophyRVPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.RV2));
			ReducedRVPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.RV3));

			SeptalMotionPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.SeptalMotion));
			VentricularSeptumPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.IntactVentricularSeptum));
			ResidualVSDPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.ResidualVSD));

			VSD1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.VSD1));
			VSD2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.VSD2));
			VSD3Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.VSD3));

			VSDDimensionPanel.Controls.Add(new ResultControl(report.Results["Ventricular Septal Defect dimension"], showNotFoundError));
			VSDVelPanel.Controls.Add(new ResultControl(report.Results["Ventricular Septal Defect peak velocity"], showNotFoundError));
			VSDGradPanel.Controls.Add(new ResultControl(report.Results["Ventricular Septal Defect peak gradient"], showNotFoundError));
			#endregion
			// Outlets
			#region
			VentriculoarterialPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.Ventriculoarterial));
			OutflowTractsPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.OutflowTracts));
			SubAorticMembranePanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.SubAorticMembrane));

			LVODimensionPanel.Controls.Add(new ResultControl(report.Results["Left ventricle outflow dimension"], showNotFoundError));
			LVOVelocityPanel.Controls.Add(new ResultControl(report.Results["Left ventricle outflow peak velocity"], showNotFoundError));
			LVOPeakPanel.Controls.Add(new ResultControl(report.Results["Left ventricle outflow peak gradient"], showNotFoundError));
			LVOMeanPanel.Controls.Add(new ResultControl(report.Results["Left ventricle outflow mean gradient"], showNotFoundError));

			AorticValve1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.AorticValve1));
			AorticValve2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.AorticValve2));
			AorticValve3Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.AorticValve3));

			AVAnnulusPanel.Controls.Add(new ResultControl(report.Results["Aortic valve annulus"], showNotFoundError));
			AVVelocityPanel.Controls.Add(new ResultControl(report.Results["Aortic valve peak velocity"], showNotFoundError));
			AVPeakPanel.Controls.Add(new ResultControl(report.Results["Aortic valve peak gradient"], showNotFoundError));
			AVMeanPanel.Controls.Add(new ResultControl(report.Results["Aortic valve mean gradient"], showNotFoundError));
			PressureHalfTimePanel.Controls.Add(new ResultControl(report.Results["Aortic valve pressure half-time"], showNotFoundError));

			AVLeafletsPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.AorticValveLeaflets));
			AVProlapsePanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.AorticValveProlapse));
			AortaVSDPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.AortaVSDOvveride));
			LossSinotubularPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.LossSinotubularJunction));
			SubPulmonaryStenosisPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.SubPulmonaryStenosis));

			RVODimensionPanel.Controls.Add(new ResultControl(report.Results["Right ventricle outflow dimension"], showNotFoundError));
			RVOVelocityPanel.Controls.Add(new ResultControl(report.Results["Right ventricle outflow peak velocity"], showNotFoundError));
			RVOPeakPanel.Controls.Add(new ResultControl(report.Results["Right ventricle outflow peak gradient"], showNotFoundError));
			RVOMeanPanel.Controls.Add(new ResultControl(report.Results["Right ventricle outflow mean gradient"], showNotFoundError));

			PV1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.PulmonaryValve1));
			PV2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.PulmonaryValve2));
			PV3Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.PulmonaryValve3));

			PVAnnulusPanel.Controls.Add(new ResultControl(report.Results["Pulmonary valve annulus"], showNotFoundError));
			PVVelocityPanel.Controls.Add(new ResultControl(report.Results["Pulmonary valve peak velocity"], showNotFoundError));
			PVPeakPanel.Controls.Add(new ResultControl(report.Results["Pulmonary valve peak gradient"], showNotFoundError));
			PVMeanPanel.Controls.Add(new ResultControl(report.Results["Pulmonary valve mean gradient"], showNotFoundError));
			PVEndDiaVelPanel.Controls.Add(new ResultControl(report.Results["Pulmonary valve end diastolic velocity"], showNotFoundError));
			PVEndDiaGradPanel.Controls.Add(new ResultControl(report.Results["Pulmonary valve end diastolic peak gradient"], showNotFoundError));
			#endregion
			// Great Arteries
			#region
			LeftAorticArch1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.LeftAorticArch1));
			LeftAorticArch2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.LeftAorticArch2));

			SinusValsavaPanel.Controls.Add(new ResultControl(report.Results["Sinuses of Valsalva"], showNotFoundError));
			SinotubularJunctionPanel.Controls.Add(new ResultControl(report.Results["Sinotubular junction"], showNotFoundError));
			AscendingAortaPanel.Controls.Add(new ResultControl(report.Results["Ascending aorta"], showNotFoundError));
			TransverseArchPanel.Controls.Add(new ResultControl(report.Results["Transverse aortic arch"], showNotFoundError));
			DistalArchPanel.Controls.Add(new ResultControl(report.Results["Distal aortic arch"], showNotFoundError));
			AorticIsthmusPanel.Controls.Add(new ResultControl(report.Results["Aortic isthmus"], showNotFoundError));
			AscendingAortaVelocityPanel.Controls.Add(new ResultControl(report.Results["Ascending aorta peak velocity"], showNotFoundError));
			AscendignAortaGradientPanel.Controls.Add(new ResultControl(report.Results["Ascending aorta peak gradient"], showNotFoundError));

			RightAorticArch1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.RightAorticArch1));
			RightAorticArch2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.RightAorticArch2));
			NoCoarctationPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.NoCoarctationAorta));
			CoarctationPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.CoarctationAorta));

			CoarctationAortaPanel.Controls.Add(new ResultControl(report.Results["Coarctation of the aorta"], showNotFoundError));
			DescendindAortaVelPanel.Controls.Add(new ResultControl(report.Results["Descending aorta peak velocity"], showNotFoundError));
			DescendindAortaGradPanel.Controls.Add(new ResultControl(report.Results["Descending aorta peak gradient"], showNotFoundError));

			BranchPulmArteryPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.BranchPulmonaryArteries));

			MainPulmArteryPanel.Controls.Add(new ResultControl(report.Results["Main pulmonary artery"], showNotFoundError));
			MainPulmArteryVelPanel.Controls.Add(new ResultControl(report.Results["Main pulmonary artery peak velocity"], showNotFoundError));
			MainPulmArteryGradPanel.Controls.Add(new ResultControl(report.Results["Main pulmonary artery peak gradient"], showNotFoundError));

			RightPulmArteryPanel.Controls.Add(new ResultControl(report.Results["Right pulmonary artery"], showNotFoundError));
			RightPulmArteryVelPanel.Controls.Add(new ResultControl(report.Results["Right pulmonary artery peak velocity"], showNotFoundError));
			RightPulmArteryGradPanel.Controls.Add(new ResultControl(report.Results["Right pulmonary artery peak gradient"], showNotFoundError));

			LeftPulmArteryPanel.Controls.Add(new ResultControl(report.Results["Left pulmonary artery"], showNotFoundError));
			LeftPulmArteryVelPanel.Controls.Add(new ResultControl(report.Results["Left pulmonary artery peak velocity"], showNotFoundError));
			LeftPulmArteryGradPanel.Controls.Add(new ResultControl(report.Results["Left pulmonary artery peak gradient"], showNotFoundError));

			NoPatentDuctusArteriosusPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.NoPatentDuctusArteriosus));
			PatentDuctusArteriosus1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.PatentDuctusArteriosus1));
			PatentDuctusArteriosus2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.PatentDuctusArteriosus2));

			PatentDuctusArteriosusPanel.Controls.Add(new ResultControl(report.Results["Patent Ductus Arteriosus"], showNotFoundError));
			PatentDuctusArteriosusSysPanel.Controls.Add(new ResultControl(report.Results["Patent Ductus Arteriosus peak velocity systole"], showNotFoundError));
			PatentDuctusArteriosusDiaPanel.Controls.Add(new ResultControl(report.Results["Patent Ductus Arteriosus peak velocity diastole"], showNotFoundError));

			#endregion
			// Pulmonary Veins
			#region
			PulmonaryVeinsPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.PulmonaryVeins));

			//PulmVeinSWavePanel.Controls.Add(new ResultControl(report.Results["Pulm vein S wave"], showNotFoundError));
			//PulmVeinDWavePanel.Controls.Add(new ResultControl(report.Results["Pulm vein D wave"], showNotFoundError));
			//PulmVeinAWavePanel.Controls.Add(new ResultControl(report.Results["Pulm vein A wave"], showNotFoundError));
			//PulmVeinWaveDurationPanel.Controls.Add(new ResultControl(report.Results["Pulmonary vein A wave duration"], showNotFoundError));

			#endregion
			// Coronary Arteries
			#region
			CoronaryArteryPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.CoronaryArteries));

			LeftMainCoronaryPanel.Controls.Add(new ResultControl(report.Results["Left Main Coronary Artery"], showNotFoundError));
			AnteriorCoronaryPanel.Controls.Add(new ResultControl(report.Results["Anterior Descending Branch of Left Coronary Artery"], showNotFoundError));
			RightCoronaryPanel.Controls.Add(new ResultControl(report.Results["Right Coronary Artery"], showNotFoundError));
			LeftCircumflexPanel.Controls.Add(new ResultControl(report.Results["Left Circumflex"], showNotFoundError));

			#endregion
			// Other
			#region
			NoPercardialEffusionPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.NoPercardialEffusion));
			PercardialEffusionPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.PercardialEffusion));

			#endregion
			// Conclusion	
			#region
			ConclusionPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.Conclusion));
			#endregion
		}
		private void ClearAllPanels() {
			// Patient Data
			#region
			PatientIDPanel.Controls.Clear();
			PatientNamePanel.Controls.Clear();
			PatientSexPanel.Controls.Clear();
			AgePanel.Controls.Clear();
			HeightPanel.Controls.Clear();
			WeightPanel.Controls.Clear();
			DiastolicBPPanel.Controls.Clear();
			SystolicBPPanel.Controls.Clear();
			ReasonForStudyPanel.Controls.Clear();
			ReferringPhysicianPanel.Controls.Clear();
			EchoTypePanel.Controls.Clear();
			ReportingDoctorPanel.Controls.Clear();
			#endregion
			// Table
			#region
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
			#endregion
			// Situs and Systemic Veins
			#region
			SitusPanel.Controls.Clear();
			SystemicVeinsPanel.Controls.Clear();
			#endregion
			// Atria
			#region
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
			#endregion
			// AV Valves
			#region
			AVConnectionPanel.Controls.Clear();
			MV1Panel.Controls.Clear();
			MV2Panel.Controls.Clear();
			MV3Panel.Controls.Clear();
			MitralProlapsePanel.Controls.Clear();

			LAVV1Panel.Controls.Clear();
			LAVV2Panel.Controls.Clear();
			LAVV3Panel.Controls.Clear();

			MVAnnulusPanel.Controls.Clear();
			MVEPanel.Controls.Clear();
			MVAPanel.Controls.Clear();
			MVEARatioPanel.Controls.Clear();
			//MVInflowAPanel.Controls.Clear();
			MVDecelPanel.Controls.Clear();
			MVInflowGradPanel.Controls.Clear();
			MVRegurgVelPanel.Controls.Clear();
			MVRegurgGradPanel.Controls.Clear();

			Tri1Panel.Controls.Clear();
			Tri2Panel.Controls.Clear();
			Tri3Panel.Controls.Clear();
			RAVV1Panel.Controls.Clear();
			RAVV2Panel.Controls.Clear();

			InsufficentTRPanel.Controls.Clear();
			TVAnnulusPanel.Controls.Clear();
			TVInflowPanel.Controls.Clear();
			TVRegurgVelPanel.Controls.Clear();
			TVRegurgGradPanel.Controls.Clear();

			#endregion
			// Ventricles
			#region
			VentricleSizeFunctionPanel.Controls.Clear();
			VentricularHypertrophyPanel.Controls.Clear();

			IVSd2Panel.Controls.Clear();
			LVIDd2Panel.Controls.Clear();
			LVPWd2Panel.Controls.Clear();
			LVMassIndex2Panel.Controls.Clear();
			FractionalShortening2Panel.Controls.Clear();
			LVTeichholz2Panel.Controls.Clear();
			LVBiplaneEFPanel.Controls.Clear();
			LVApical42Panel.Controls.Clear();
			HeartRate2Panel.Controls.Clear();


			DilatedLVPanel.Controls.Clear();
			HypertrophyLVPanel.Controls.Clear();
			ReducedLVPanel.Controls.Clear();

			LVSystolicPanel.Controls.Clear();
			LVDiastolicPanel.Controls.Clear();

			LVIVRTPanel.Controls.Clear();
			MyoPIPanel.Controls.Clear();

			PulmSWavePanel.Controls.Clear();
			PulmDWavePanel.Controls.Clear();
			PulmAWavePanel.Controls.Clear();

			PulmWaveDurationPanel.Controls.Clear();
			MitralWaveDurationPanel.Controls.Clear();

			MitralAnnulusE2Panel.Controls.Clear();
			MitralAnnulusA2Panel.Controls.Clear();
			MitralAnnulusS2Panel.Controls.Clear();

			SeptalAnnulusE2Panel.Controls.Clear();
			SeptalAnnulusA2Panel.Controls.Clear();
			SeptalAnnulusS2Panel.Controls.Clear();

			TriAnnulusE2Panel.Controls.Clear();
			TriAnnulusA2Panel.Controls.Clear();
			TriAnnulusS2Panel.Controls.Clear();

			DilatedRVPanel.Controls.Clear();
			HypertrophyRVPanel.Controls.Clear();
			ReducedRVPanel.Controls.Clear();

			SeptalMotionPanel.Controls.Clear();
			VentricularSeptumPanel.Controls.Clear();
			ResidualVSDPanel.Controls.Clear();

			VSD1Panel.Controls.Clear();
			VSD2Panel.Controls.Clear();
			VSD3Panel.Controls.Clear();

			VSDDimensionPanel.Controls.Clear();
			VSDVelPanel.Controls.Clear();
			VSDGradPanel.Controls.Clear();
			#endregion
			// Outlets
			#region
			VentriculoarterialPanel.Controls.Clear();
			OutflowTractsPanel.Controls.Clear();
			SubAorticMembranePanel.Controls.Clear();

			LVODimensionPanel.Controls.Clear();
			LVOVelocityPanel.Controls.Clear();
			LVOPeakPanel.Controls.Clear();
			LVOMeanPanel.Controls.Clear();

			AorticValve1Panel.Controls.Clear();
			AorticValve2Panel.Controls.Clear();
			AorticValve3Panel.Controls.Clear();

			AVAnnulusPanel.Controls.Clear();
			AVVelocityPanel.Controls.Clear();
			AVPeakPanel.Controls.Clear();
			AVMeanPanel.Controls.Clear();
			PressureHalfTimePanel.Controls.Clear();

			AVLeafletsPanel.Controls.Clear();
			AVProlapsePanel.Controls.Clear();
			AortaVSDPanel.Controls.Clear();
			LossSinotubularPanel.Controls.Clear();
			SubPulmonaryStenosisPanel.Controls.Clear();

			RVODimensionPanel.Controls.Clear();
			RVOVelocityPanel.Controls.Clear();
			RVOPeakPanel.Controls.Clear();
			RVOMeanPanel.Controls.Clear();

			PV1Panel.Controls.Clear();
			PV2Panel.Controls.Clear();
			PV3Panel.Controls.Clear();

			PVAnnulusPanel.Controls.Clear();
			PVVelocityPanel.Controls.Clear();
			PVPeakPanel.Controls.Clear();
			PVMeanPanel.Controls.Clear();
			PVEndDiaVelPanel.Controls.Clear();
			PVEndDiaGradPanel.Controls.Clear();

			#endregion
			// Great Arteries
			#region
			LeftAorticArch1Panel.Controls.Clear();
			LeftAorticArch2Panel.Controls.Clear();

			SinusValsavaPanel.Controls.Clear();
			SinotubularJunctionPanel.Controls.Clear();
			AscendingAortaPanel.Controls.Clear();
			TransverseArchPanel.Controls.Clear();
			DistalArchPanel.Controls.Clear();
			AorticIsthmusPanel.Controls.Clear();
			AscendingAortaVelocityPanel.Controls.Clear();
			AscendignAortaGradientPanel.Controls.Clear();

			RightAorticArch1Panel.Controls.Clear();
			RightAorticArch2Panel.Controls.Clear();
			NoCoarctationPanel.Controls.Clear();
			CoarctationPanel.Controls.Clear();

			CoarctationAortaPanel.Controls.Clear();
			DescendindAortaVelPanel.Controls.Clear();
			DescendindAortaGradPanel.Controls.Clear();

			BranchPulmArteryPanel.Controls.Clear();

			MainPulmArteryPanel.Controls.Clear();
			MainPulmArteryVelPanel.Controls.Clear();
			MainPulmArteryGradPanel.Controls.Clear();

			RightPulmArteryPanel.Controls.Clear();
			RightPulmArteryVelPanel.Controls.Clear();
			RightPulmArteryGradPanel.Controls.Clear();

			LeftPulmArteryPanel.Controls.Clear();
			LeftPulmArteryVelPanel.Controls.Clear();
			LeftPulmArteryGradPanel.Controls.Clear();

			NoPatentDuctusArteriosusPanel.Controls.Clear();
			PatentDuctusArteriosus1Panel.Controls.Clear();
			PatentDuctusArteriosus2Panel.Controls.Clear();

			PatentDuctusArteriosusPanel.Controls.Clear();
			PatentDuctusArteriosusSysPanel.Controls.Clear();
			PatentDuctusArteriosusDiaPanel.Controls.Clear();

			#endregion
			// Pulmonary Veins
			#region
			PulmonaryVeinsPanel.Controls.Clear();

			//PulmVeinSWavePanel.Controls.Clear();
			//PulmVeinDWavePanel.Controls.Clear();
			//PulmVeinAWavePanel.Controls.Clear();
			//PulmVeinWaveDurationPanel.Controls.Clear();

			#endregion
			// Coronary Arteries
			#region
			CoronaryArteryPanel.Controls.Clear();

			LeftMainCoronaryPanel.Controls.Clear();
			AnteriorCoronaryPanel.Controls.Clear();
			RightCoronaryPanel.Controls.Clear();
			LeftCircumflexPanel.Controls.Clear();

			#endregion
			// Other	
			#region
			NoPercardialEffusionPanel.Controls.Clear();
			PercardialEffusionPanel.Controls.Clear();

			#endregion
			// Conclusion	
			#region
			ConclusionPanel.Controls.Clear();
			#endregion

		}

		private void MainForm_Load(object sender, EventArgs e) {

		}
	}
}
