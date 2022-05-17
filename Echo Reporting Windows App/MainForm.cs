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
using Echo_Reporting_Backend.Data;
using System.Data.Odbc;
using System.Net.Http.Headers;

namespace Echo_Reporting_Windows_App {
	public partial class MainForm : Form {
		private StructuredReport report = new StructuredReport();
		private ReportForm reportForm = null;
			
		public MainForm() {
			InitializeComponent();
			foreach(var control in panel3.Controls) {
				if (control is GroupBox g) {
					var font = g.Font;
					g.Font = new Font(Font.FontFamily, 12, Font.Style | FontStyle.Bold | FontStyle.Underline);
					foreach (var child in g.Controls) {
						if (child is Panel p) {
							p.Font = font;
						}
					}
				}
			}
			ShowAllResults();
		}

		private void openFolderToolStripMenuItem_Click(object sender, EventArgs e) {
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
				var folder = folderBrowserDialog1.SelectedPath;
				var enumerator = new DicomFileEnumerator(new DirectoryInfo(folder));
				while (enumerator.MoveNext()) {
					Debug.WriteLine(enumerator.Current.File.Name);
					report = DicomReader.GetStructuredReport(enumerator.Current.Dataset);
					ShowAllResults(true);
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
					ShowAllResults(true);
				}
				else {
					MessageBox.Show("The selected file is invalid or in an unsupported format.", "Error - Invalid DICOM File", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		private void UpdateBSA() {
			report.PatientData.UpdateBSAResult();
			ShowAllResults();
		}
		private void ShowAllResults() {
			ShowAllResults(false);
		}
		private void ShowAllResults(bool showNotFoundError) {
			report.AddCalculatedValues();
			panel3.Visible = false;
			ClearAllPanels();
			// Patient Data
			#region
			PatientIDPanel.Controls.Add(new StringFieldControl(report.PatientData.PatientID));
			PatientNamePanel.Controls.Add(new StringFieldControl(report.PatientData.PatientName));
			PatientSexPanel.Controls.Add(new StringFieldControl(report.PatientData.PatientSex));
			DOBPanel.Controls.Add(new StringFieldControl(report.PatientData.PatientDOB));
			StudyDatePanel.Controls.Add(new StringFieldControl(report.PatientData.StudyDate));
			AgePanel.Controls.Add(new ResultControl(report.PatientData.PatientAge, showNotFoundError, this, ShowAllResults));
			HeightPanel.Controls.Add(new ResultControl(report.PatientData.PatientHeight, showNotFoundError, this, UpdateBSA));
			WeightPanel.Controls.Add(new ResultControl(report.PatientData.PatientWeight, showNotFoundError, this, UpdateBSA));
			BSAPanel.Controls.Add(new ResultControl(report.PatientData.BSA, showNotFoundError, this, ShowAllResults));
			DiastolicBPPanel.Controls.Add(new ResultControl(report.PatientData.DiastolicBloodPressure, showNotFoundError, this));
			SystolicBPPanel.Controls.Add(new ResultControl(report.PatientData.SystolicBloodPressure, showNotFoundError, this));

			ReasonForStudyPanel.Controls.Add(new StringFieldControl(report.PatientData.ReasonForStudy));
			ReferringPhysicianPanel.Controls.Add(new StringFieldControl(report.PatientData.ReferringPhysician));
			EchoTypePanel.Controls.Add(new StringDropDownControl(report.PatientData.EchoType));
			ReportingDoctorPanel.Controls.Add(new StringDropDownControl(report.PatientData.ReportingDoctor));
			#endregion
			// Table
			#region
			IVSdPanel.Controls.Add(new ResultControl(report.Results["IVSd"], showNotFoundError, this));
			LVIDdPanel.Controls.Add(new ResultControl(report.Results["LVIDd"], showNotFoundError, this));
			LVPWdPanel.Controls.Add(new ResultControl(report.Results["LVPWd"], showNotFoundError, this));

			IVSsPanel.Controls.Add(new ResultControl(report.Results["IVSs"], showNotFoundError, this));
			LVIDsPanel.Controls.Add(new ResultControl(report.Results["LVIDs"], showNotFoundError, this));
			LVPWsPanel.Controls.Add(new ResultControl(report.Results["LVPWs"], showNotFoundError, this));

			MVEWavePanel.Controls.Add(new ResultControl(report.Results["Mitral valve E wave"], showNotFoundError, this));
			MVAWavePanel.Controls.Add(new ResultControl(report.Results["Mitral valve A wave"], showNotFoundError, this));
			MVDecelTimePanel.Controls.Add(new ResultControl(report.Results["MV decel time"], showNotFoundError, this));
			MitralAnnulusEPanel.Controls.Add(new ResultControl(report.Results["Mitral annulus E'"], showNotFoundError, this));
			SeptalAnnulusEPanel.Controls.Add(new ResultControl(report.Results["Septal annulus E'"], showNotFoundError, this));
			RVEPanel.Controls.Add(new ResultControl(report.Results["Tricuspid annulus E'"], showNotFoundError, this)); 

			FractionalShorteningPanel.Controls.Add(new ResultControl(report.Results["Fractional Shortening"], showNotFoundError, this));
			LVTeichholzEFPanel.Controls.Add(new ResultControl(report.Results["Left Ventricular Teichholz EF"], showNotFoundError, this));
			LVMassIndexPanel.Controls.Add(new ResultControl(report.Results["LV mass index"], showNotFoundError, this));
			HeartRatePanel.Controls.Add(new ResultControl(report.Results["Heart Rate"], showNotFoundError, this));
			MVCFcPanel.Controls.Add(new ResultControl(report.Results["MVCFc"], showNotFoundError, this));
			LVOutputPanel.Controls.Add(new ResultControl(report.Results["Left ventricular cardiac output"], showNotFoundError, this));
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
			ASDDimensionPanel.Controls.Add(new ResultControl(report.Results["Atrial Septal Defect dimension"], showNotFoundError, this));
			ASDGradientPanel.Controls.Add(new ResultControl(report.Results["Atrial Septal Defect mean gradient"], showNotFoundError, this));
			#endregion
			// AV Valves
			#region
			AVConnectionPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.AVConnection));
			MV1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.MitralValve1));
			MV2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.MitralValve2));
			MV3Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.MitralValve3));
			MitralProlapsePanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.MitralValveProlapse));

			LAVV1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.LAVV1));
			LAVV2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.LAVV2));
			LAVV3Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.LAVV3));

			MVAnnulusPanel.Controls.Add(new ResultControl(report.Results["Mitral valve annulus"], showNotFoundError, this));
			//MVEPanel.Controls.Add(new ResultControl(report.Results["Mitral valve E wave"], showNotFoundError, this));
			//MVAPanel.Controls.Add(new ResultControl(report.Results["Mitral valve A wave"], showNotFoundError, this));
			MVEARatioPanel.Controls.Add(new ResultControl(report.Results["Mitral E/A ratio"], showNotFoundError, this));
			//MVInflowAPanel.Controls.Add(new ResultControl(report.Results["Mitral valve inflow A wave duration"], showNotFoundError, this));
			//MVDecelPanel.Controls.Add(new ResultControl(report.Results["MV decel time"], showNotFoundError, this));
			MVInflowGradPanel.Controls.Add(new ResultControl(report.Results["Mitral valve inflow mean gradient"], showNotFoundError, this));
			MVRegurgVelPanel.Controls.Add(new ResultControl(report.Results["Mitral valve regurgitation peak velocity"], showNotFoundError, this));
			MVRegurgGradPanel.Controls.Add(new ResultControl(report.Results["Mitral valve regurgitation peak gradient"], showNotFoundError, this));

			Tri1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.TriscupidValve1));
			Tri2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.TriscupidValve2));
			Tri3Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.TriscupidValve3));
			RAVV1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.RAVV1));
			RAVV2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.RAVV2));

			InsufficentTRPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.InsufficientTR));

			TVAnnulusPanel.Controls.Add(new ResultControl(report.Results["Tricuspid valve annulus"], showNotFoundError, this));
			TVInflowPanel.Controls.Add(new ResultControl(report.Results["Tricuspid valve inflow mean gradient"], showNotFoundError, this));
			TVRegurgVelPanel.Controls.Add(new ResultControl(report.Results["Tricuspid valve regurgitation peak velocity"], showNotFoundError, this));
			
			//TVRegurgGradPanel.Controls.Add(new ResultControl(report.Results["Tricuspid valve regurgitation peak gradient"], showNotFoundError, this));
			TVRegurgGradPanel.Controls.Add(new ResultControl(report.Results["Estimated RV systolic pressure"], showNotFoundError, this));
			#endregion
			// Ventricles
			#region
			VentricleSizeFunctionPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.VentricleFunction));
			VentricularHypertrophyPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.VentricularHypertrophy));

			//IVSd2Panel.Controls.Add(new ResultControl(report.Results["IVSd"], showNotFoundError, this));
			//LVIDd2Panel.Controls.Add(new ResultControl(report.Results["LVIDd"], showNotFoundError, this));
			//LVPWd2Panel.Controls.Add(new ResultControl(report.Results["LVPWd"], showNotFoundError, this));
			//LVMassIndex2Panel.Controls.Add(new ResultControl(report.Results["LV mass index"], showNotFoundError, this));
			//FractionalShortening2Panel.Controls.Add(new ResultControl(report.Results["Fractional Shortening"], showNotFoundError, this));
			//LVTeichholz2Panel.Controls.Add(new ResultControl(report.Results["Left Ventricular Teichholz EF"], showNotFoundError, this));
			LVBiplaneEFPanel.Controls.Add(new ResultControl(report.Results["Left Ventricular biplane EF"], showNotFoundError, this));
			LVApical42Panel.Controls.Add(new ResultControl(report.Results["Left ventricular Apical 4 chamber EF"], showNotFoundError, this));
			//HeartRate2Panel.Controls.Add(new ResultControl(report.Results["Heart Rate"], showNotFoundError, this));


			DilatedLVPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.DilatedLV));
			HypertrophyLVPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.HypertrophiedLV));
			ReducedLVPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.ReducedLVFunction));

			LVSystolicPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.LVSystolicFunction));
			LVDiastolicPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.NormalDiastolic));


			LVIVRTPanel.Controls.Add(new ResultControl(report.Results["LV IVRT"], showNotFoundError, this));
			MyoPIPanel.Controls.Add(new ResultControl(report.Results["Myocardial Performance Index"], showNotFoundError, this));

			PulmSWavePanel.Controls.Add(new ResultControl(report.Results["Pulm vein S wave"], showNotFoundError, this));
			PulmDWavePanel.Controls.Add(new ResultControl(report.Results["Pulm vein D wave"], showNotFoundError, this));
			PulmAWavePanel.Controls.Add(new ResultControl(report.Results["Pulm vein A wave"], showNotFoundError, this));

			PulmWaveDurationPanel.Controls.Add(new ResultControl(report.Results["Pulmonary vein A wave duration"], showNotFoundError, this));
			MitralWaveDurationPanel.Controls.Add(new ResultControl(report.Results["Mitral valve inflow A wave duration"], showNotFoundError, this));

			//MitralAnnulusE2Panel.Controls.Add(new ResultControl(report.Results["Mitral annulus E'"], showNotFoundError, this));
			MitralAnnulusA2Panel.Controls.Add(new ResultControl(report.Results["Mitral annulus A'"], showNotFoundError, this));
			MitralAnnulusS2Panel.Controls.Add(new ResultControl(report.Results["Mitral annulus S'"], showNotFoundError, this));

			//SeptalAnnulusE2Panel.Controls.Add(new ResultControl(report.Results["Septal annulus E'"], showNotFoundError, this));
			SeptalAnnulusA2Panel.Controls.Add(new ResultControl(report.Results["Septal annulus A'"], showNotFoundError, this));
			SeptalAnnulusS2Panel.Controls.Add(new ResultControl(report.Results["Septal annulus S'"], showNotFoundError, this));

			//TriAnnulusE2Panel.Controls.Add(new ResultControl(report.Results["Tricuspid annulus E'"], showNotFoundError, this));
			TriAnnulusA2Panel.Controls.Add(new ResultControl(report.Results["Tricuspid annulus A'"], showNotFoundError, this));
			TriAnnulusS2Panel.Controls.Add(new ResultControl(report.Results["Tricuspid annulus S'"], showNotFoundError, this));
			TAPSEPanel.Controls.Add(new ResultControl(report.Results["TAPSE"], showNotFoundError, this));


			DilatedRVPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.DilatedRV));
			HypertrophyRVPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.HypertrophiedRV));
			ReducedRVPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.ReducedRVFunction));

			SeptalMotionPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.SeptalMotion));
			VentricularSeptumPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.IntactVentricularSeptum));
			ResidualVSDPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.ResidualVSD));

			VSD1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.VSD1));
			VSD2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.VSD2));
			VSD3Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.VSD3));
			VSDDescPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.VSDDescription));

			VSDDimensionPanel.Controls.Add(new ResultControl(report.Results["Ventricular Septal Defect dimension"], showNotFoundError, this));
			VSDVelPanel.Controls.Add(new ResultControl(report.Results["Ventricular Septal Defect peak velocity"], showNotFoundError, this));
			VSDGradPanel.Controls.Add(new ResultControl(report.Results["Ventricular Septal Defect peak gradient"], showNotFoundError, this));
			#endregion
			// Outlets
			#region
			VentriculoarterialPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.Ventriculoarterial));
			OutflowTractsPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.OutflowTracts));
			SubAorticMembranePanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.SubAorticMembrane));

			LVODimensionPanel.Controls.Add(new ResultControl(report.Results["Left ventricle outflow dimension"], showNotFoundError, this));
			LVOVelocityPanel.Controls.Add(new ResultControl(report.Results["Left ventricle outflow peak velocity"], showNotFoundError, this));
			LVOPeakPanel.Controls.Add(new ResultControl(report.Results["Left ventricle outflow peak gradient"], showNotFoundError, this));
			LVOMeanPanel.Controls.Add(new ResultControl(report.Results["Left ventricle outflow mean gradient"], showNotFoundError, this));

			AorticValve1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.AorticValve1));
			AorticValve2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.AorticValve2));
			AorticValve3Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.AorticValve3));

			AVAnnulusPanel.Controls.Add(new ResultControl(report.Results["Aortic valve annulus"], showNotFoundError, this));
			AVVelocityPanel.Controls.Add(new ResultControl(report.Results["Aortic valve peak velocity"], showNotFoundError, this));
			AVPeakPanel.Controls.Add(new ResultControl(report.Results["Aortic valve peak gradient"], showNotFoundError, this));
			AVMeanPanel.Controls.Add(new ResultControl(report.Results["Aortic valve mean gradient"], showNotFoundError, this));
			PressureHalfTimePanel.Controls.Add(new ResultControl(report.Results["Aortic valve pressure half-time"], showNotFoundError, this));

			AVLeafletsPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.AorticValveLeaflets));
			AVProlapsePanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.AorticValveProlapse));
			AortaVSDPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.AortaVSDOvveride));
			LossSinotubularPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.LossSinotubularJunction));
			SubPulmonaryStenosisPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.SubPulmonaryStenosis));

			RVODimensionPanel.Controls.Add(new ResultControl(report.Results["Right ventricle outflow dimension"], showNotFoundError, this));
			RVOVelocityPanel.Controls.Add(new ResultControl(report.Results["Right ventricle outflow peak velocity"], showNotFoundError, this));
			RVOPeakPanel.Controls.Add(new ResultControl(report.Results["Right ventricle outflow peak gradient"], showNotFoundError, this));
			RVOMeanPanel.Controls.Add(new ResultControl(report.Results["Right ventricle outflow mean gradient"], showNotFoundError, this));

			PV1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.PulmonaryValve1));
			PV2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.PulmonaryValve2));
			PV3Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.PulmonaryValve3));

			PVAnnulusPanel.Controls.Add(new ResultControl(report.Results["Pulmonary valve annulus"], showNotFoundError, this));
			PVVelocityPanel.Controls.Add(new ResultControl(report.Results["Pulmonary valve peak velocity"], showNotFoundError, this));
			PVPeakPanel.Controls.Add(new ResultControl(report.Results["Pulmonary valve peak gradient"], showNotFoundError, this));
			PVMeanPanel.Controls.Add(new ResultControl(report.Results["Pulmonary valve mean gradient"], showNotFoundError, this));
			PAAccelTimePanel.Controls.Add(new ResultControl(report.Results["Pulmonary artery acceleration time"], showNotFoundError, this));
			PVEndDiaVelPanel.Controls.Add(new ResultControl(report.Results["Pulmonary valve end diastolic velocity"], showNotFoundError, this));
			PVEndDiaGradPanel.Controls.Add(new ResultControl(report.Results["Pulmonary valve end diastolic peak gradient"], showNotFoundError, this));
			#endregion
			// Great Arteries
			#region
			LeftAorticArch1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.LeftAorticArch1));
			LeftAorticArch2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.LeftAorticArch2));

			SinusValsavaPanel.Controls.Add(new ResultControl(report.Results["Sinuses of Valsalva"], showNotFoundError, this));
			SinotubularJunctionPanel.Controls.Add(new ResultControl(report.Results["Sinotubular junction"], showNotFoundError, this));
			AscendingAortaPanel.Controls.Add(new ResultControl(report.Results["Ascending aorta"], showNotFoundError, this));
			TransverseArchPanel.Controls.Add(new ResultControl(report.Results["Transverse aortic arch"], showNotFoundError, this));
			DistalArchPanel.Controls.Add(new ResultControl(report.Results["Distal aortic arch"], showNotFoundError, this));
			AorticIsthmusPanel.Controls.Add(new ResultControl(report.Results["Aortic isthmus"], showNotFoundError, this));
			AscendingAortaVelocityPanel.Controls.Add(new ResultControl(report.Results["Ascending aorta peak velocity"], showNotFoundError, this));
			AscendignAortaGradientPanel.Controls.Add(new ResultControl(report.Results["Ascending aorta peak gradient"], showNotFoundError, this));

			RightAorticArch1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.RightAorticArch1));
			RightAorticArch2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.RightAorticArch2));
			NoCoarctationPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.NoCoarctationAorta));
			CoarctationPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.CoarctationAorta));

			CoarctationAortaPanel.Controls.Add(new ResultControl(report.Results["Coarctation of the aorta"], showNotFoundError, this));
			DescendindAortaVelPanel.Controls.Add(new ResultControl(report.Results["Descending aorta peak velocity"], showNotFoundError, this));
			DescendindAortaGradPanel.Controls.Add(new ResultControl(report.Results["Descending aorta peak gradient"], showNotFoundError, this));

			BranchPulmArteryPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.BranchPulmonaryArteries));

			MainPulmArteryPanel.Controls.Add(new ResultControl(report.Results["Main pulmonary artery"], showNotFoundError, this));
			MainPulmArteryVelPanel.Controls.Add(new ResultControl(report.Results["Main pulmonary artery peak velocity"], showNotFoundError, this));
			MainPulmArteryGradPanel.Controls.Add(new ResultControl(report.Results["Main pulmonary artery peak gradient"], showNotFoundError, this));

			RightPulmArteryPanel.Controls.Add(new ResultControl(report.Results["Right pulmonary artery"], showNotFoundError, this));
			RightPulmArteryVelPanel.Controls.Add(new ResultControl(report.Results["Right pulmonary artery peak velocity"], showNotFoundError, this));
			RightPulmArteryGradPanel.Controls.Add(new ResultControl(report.Results["Right pulmonary artery peak gradient"], showNotFoundError, this));

			LeftPulmArteryPanel.Controls.Add(new ResultControl(report.Results["Left pulmonary artery"], showNotFoundError, this));
			LeftPulmArteryVelPanel.Controls.Add(new ResultControl(report.Results["Left pulmonary artery peak velocity"], showNotFoundError, this));
			LeftPulmArteryGradPanel.Controls.Add(new ResultControl(report.Results["Left pulmonary artery peak gradient"], showNotFoundError, this));

			NoPatentDuctusArteriosusPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.NoPatentDuctusArteriosus));
			PatentDuctusArteriosus1Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.PatentDuctusArteriosus1));
			PatentDuctusArteriosus2Panel.Controls.Add(new StringDropDownControl(report.ReportingOptions.PatentDuctusArteriosus2));

			PatentDuctusArteriosusPanel.Controls.Add(new ResultControl(report.Results["Patent Ductus Arteriosus"], showNotFoundError, this));
			PatentDuctusArteriosusSysPanel.Controls.Add(new ResultControl(report.Results["Patent Ductus Arteriosus peak velocity systole"], showNotFoundError, this));
			PDAGradientPanel.Controls.Add(new ResultControl(report.Results["Patent Ductus Arteriosus peak gradient"], showNotFoundError, this));
			PatentDuctusArteriosusDiaPanel.Controls.Add(new ResultControl(report.Results["Patent Ductus Arteriosus peak velocity diastole"], showNotFoundError, this));

			#endregion
			// Pulmonary Veins
			#region
			PulmonaryVeinsPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.PulmonaryVeins));

			//PulmVeinSWavePanel.Controls.Add(new ResultControl(report.Results["Pulm vein S wave"], showNotFoundError, this));
			//PulmVeinDWavePanel.Controls.Add(new ResultControl(report.Results["Pulm vein D wave"], showNotFoundError, this));
			//PulmVeinAWavePanel.Controls.Add(new ResultControl(report.Results["Pulm vein A wave"], showNotFoundError, this));
			//PulmVeinWaveDurationPanel.Controls.Add(new ResultControl(report.Results["Pulmonary vein A wave duration"], showNotFoundError, this));

			#endregion
			// Coronary Arteries
			#region
			CoronaryArteryPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.CoronaryArteries));

			LeftMainCoronaryPanel.Controls.Add(new ResultControl(report.Results["Left Main Coronary Artery"], showNotFoundError, this));
			AnteriorCoronaryPanel.Controls.Add(new ResultControl(report.Results["Anterior Descending Branch of Left Coronary Artery"], showNotFoundError, this));
			RightCoronaryPanel.Controls.Add(new ResultControl(report.Results["Right Coronary Artery"], showNotFoundError, this));
			LeftCircumflexPanel.Controls.Add(new ResultControl(report.Results["Left Circumflex"], showNotFoundError, this));

			#endregion
			// Other
			#region
			NoPerciardialEffusionPanel.Controls.Add(new BoolCheckControl(report.ReportingOptions.NoPerciardialEffusion));
			PerciardialEffusionPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.PerciardialEffusion));

			#endregion
			// Conclusion	
			#region
			ConclusionPanel.Controls.Add(new StringDropDownControl(report.ReportingOptions.Conclusion));
			#endregion
			panel3.AutoScrollPosition = new Point(0, 0);
			panel3.Visible = true;
		}
		private void ClearAllPanels() {
			// Patient Data
			#region
			PatientIDPanel.Controls.Clear();
			PatientNamePanel.Controls.Clear();
			PatientSexPanel.Controls.Clear();
			DOBPanel.Controls.Clear();
			StudyDatePanel.Controls.Clear();
			AgePanel.Controls.Clear();
			HeightPanel.Controls.Clear();
			WeightPanel.Controls.Clear();
			BSAPanel.Controls.Clear();
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
			RVEPanel.Controls.Clear();

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
			//MVEPanel.Controls.Clear();
			//MVAPanel.Controls.Clear();
			MVEARatioPanel.Controls.Clear();
			//MVInflowAPanel.Controls.Clear();
			//MVDecelPanel.Controls.Clear();
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

			//IVSd2Panel.Controls.Clear();
			//LVIDd2Panel.Controls.Clear();
			//LVPWd2Panel.Controls.Clear();
			//LVMassIndex2Panel.Controls.Clear();
			//FractionalShortening2Panel.Controls.Clear();
			//LVTeichholz2Panel.Controls.Clear();
			LVBiplaneEFPanel.Controls.Clear();
			LVApical42Panel.Controls.Clear();
			//HeartRate2Panel.Controls.Clear();


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

			//MitralAnnulusE2Panel.Controls.Clear();
			MitralAnnulusA2Panel.Controls.Clear();
			MitralAnnulusS2Panel.Controls.Clear();

			//SeptalAnnulusE2Panel.Controls.Clear();
			SeptalAnnulusA2Panel.Controls.Clear();
			SeptalAnnulusS2Panel.Controls.Clear();

			//TriAnnulusE2Panel.Controls.Clear();
			TriAnnulusA2Panel.Controls.Clear();
			TriAnnulusS2Panel.Controls.Clear();
			TAPSEPanel.Controls.Clear();

			DilatedRVPanel.Controls.Clear();
			HypertrophyRVPanel.Controls.Clear();
			ReducedRVPanel.Controls.Clear();

			SeptalMotionPanel.Controls.Clear();
			VentricularSeptumPanel.Controls.Clear();
			ResidualVSDPanel.Controls.Clear();

			VSD1Panel.Controls.Clear();
			VSD2Panel.Controls.Clear();
			VSD3Panel.Controls.Clear();
			VSDDescPanel.Controls.Clear();

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
			PAAccelTimePanel.Controls.Clear();
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
			PDAGradientPanel.Controls.Clear();
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
			NoPerciardialEffusionPanel.Controls.Clear();
			PerciardialEffusionPanel.Controls.Clear();

			#endregion
			// Conclusion	
			#region
			ConclusionPanel.Controls.Clear();
			#endregion

		}
		private void MainForm_Load(object sender, EventArgs e) {

		}

		private void publishToolStripMenuItem_Click(object sender, EventArgs e) {
			report.GenerateSections();
			if (reportForm == null) {
				reportForm = new ReportForm(report);
				reportForm.FormClosed += (x, y) => { this.reportForm = null; };
				reportForm.Show(this);
			}
			else {
				reportForm.Activate();
			}
		}

		private void PatientIDPanel_Paint(object sender, PaintEventArgs e) {

		}

		private void groupBox2_Enter(object sender, EventArgs e) {

		}

		private void panel3_Paint(object sender, PaintEventArgs e) {

		}

		private void groupBox7_Enter(object sender, EventArgs e) {

		}

		private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {

		}
	}
}
