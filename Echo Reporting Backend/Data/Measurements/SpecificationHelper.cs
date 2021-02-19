using DICOMReporting.Formulas;
using Echo_Reporting_Backend.Formulas;
using System.Collections.Generic;
using UnitsNet.Units;

namespace DICOMReporting.Data.Measurements {
	public static class SpecificationHelper {
		public static Dictionary<string, List<MeasurementSpecification>> SpecsBySite(PatientData pd) {
			var final = new Dictionary<string, List<MeasurementSpecification>>();

			final["Interventricular septum"] = InterventricularSeptumSpecs(pd);
			final["Left Ventricle"] = LeftVentricleSpecs(pd);
			final["Aortic Valve"] = AorticValveSpecs(pd);
			final["Structure Sinus of Valsalva"] = StructureSinusOfValsalvaSpecs(pd);
			final["Aortic sinotubular junction"] = AorticSinotubularJunctionSpecs(pd);
			final["Aortic arch"] = AorticArchSpecs(pd);
			final["Aortic isthmus"] = AorticIsthmusSpecs(pd);
			final["Pulmonic Valve"] = PulmonicValveSpecs(pd);
			final["Pulmonary artery"] = PulmonaryArterySpecs(pd);
			final["Mitral Valve"] = MitralValveSpecs(pd);
			final["Tricuspid Valve"] = TricuspidValveSpecs(pd);
			final["Ascending aorta"] = AscendingAortaSpecs(pd);
			final["Left Main Coronary Artery"] = LeftMainCoronaryArterySpecs(pd);
			final["Anterior Descending Branch of Left Coronary Artery"] = AnteriorDescendingBranchOfLeftCoronaryArterySpecs(pd);
			final["Right Coronary Artery"] = RightCoronaryArterySpecs(pd);
			final["Pulmonary Vein"] = PulmonaryVeinSpecs(pd);
			final["Atrial Septal Defect"] = AtrialSeptalDefectSpecs(pd);
			final["Ventricular Septal Defect"] = VentricularSeptalDefectSpecs(pd);
			final["Right Ventricle"] = RightVentricleSpecs(pd);
			final["Pulmonary Trunk"] = PulmonaryTrunkSpecs(pd);
			final["Coarctation of aorta"] = CoarctationOfTheAortaSpecs(pd);
			final["Thoracic aorta"] = ThoracicAortaSpecs(pd);
			final["Circumflex Coronary Artery"] = CircumflexCoronaryArterySpecs(pd);
			final["Patent Ductus Arteriosus"] = PatentDuctusArteriosusSpecs(pd);

			return final;
		}

		private static List<MeasurementSpecification> InterventricularSeptumSpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			props.Add("Cardiac Cycle Point", "End Diastole");
			specs.Add(new MeasurementSpecification("IVSd", "ROI Thickness by US", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.IVSd(pd, ""), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();

			props.Add("Cardiac Cycle Point", "End Systole");
			specs.Add(new MeasurementSpecification("IVSs", "ROI Thickness by US", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.IVSs(pd, ""), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();
			return specs;
		}
		private static List<MeasurementSpecification> LeftVentricleSpecs(PatientData pd) {
			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			props.Add("Cardiac Cycle Point", "End Diastole");
			specs.Add(new MeasurementSpecification("LVIDd", "ROI Internal Dimension by US", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.LVIDd(pd, ""), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();
			props.Add("Cardiac Cycle Point", "End Systole");
			specs.Add(new MeasurementSpecification("LVIDs", "ROI Internal Dimension by US", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.LVIDs(pd, ""), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();


			props.Add("Finding Site", "Posterior Wall");
			props.Add("Cardiac Cycle Point", "End Diastole");
			specs.Add(new MeasurementSpecification("LVPWd", "ROI Thickness by US", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.LVPWd(pd, ""), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();

			props.Add("Finding Site", "Posterior Wall");
			props.Add("Cardiac Cycle Point", "End Systole");
			specs.Add(new MeasurementSpecification("LVPWs", "ROI Thickness by US", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.LVPWs(pd, ""), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();

			props.Add("Measurement Method", "Cube Method");
			props.Add("Index", "Body Surface Area");
			specs.Add(new MeasurementSpecification("LV mass index", "Left Ventricle Mass Index", new Dictionary<string, string>(props), "g/m2", formula: ImpactOfCardiacGrowthFormula.LVMassIndex(pd, "LV mass index"), includeImageMode: true));
			props.Clear();

			specs.Add(new MeasurementSpecification("Heart Rate", "Heart Rate", new Dictionary<string, string>(props), "bpm", formula: ImpactOfCardiacGrowthFormula.HeartRate(pd, ""), includeImageMode: true));
			props.Clear();

			specs.Add(new MeasurementSpecification("Fractional Shortening", "Fractional Shortening", new Dictionary<string, string>(props), "%", formula: ImpactOfCardiacGrowthFormula.FractionalShortening(pd, ""), includeImageMode: true));
			props.Clear();

			props.Add("Finding Site", "Lateral Mitral Annulus");
			props.Add("Cardiac Cycle Point", "Early Diastole");
			props.Add("Image Mode", "Tissue Doppler Imaging");
			specs.Add(new MeasurementSpecification("Mitral annulus E'", "Peak Tissue Velocity", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.MitralAnnulusE(pd, "Mitral annulus E' velocity"), unitEnum: SpeedUnit.CentimeterPerSecond));
			props.Clear();
			props.Add("Finding Site", "Lateral Mitral Annulus");
			props.Add("Cardiac Cycle Point", "Atrial Systole");
			props.Add("Image Mode", "Tissue Doppler Imaging");
			specs.Add(new MeasurementSpecification("Mitral annulus A'", "Peak Tissue Velocity", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.MitralAnnulusA(pd, "Mitral annulus A' velocity"), unitEnum: SpeedUnit.CentimeterPerSecond));
			props.Clear();
			props.Add("Finding Site", "Lateral Mitral Annulus");
			props.Add("Cardiac Cycle Point", "End Systole");
			props.Add("Image Mode", "Tissue Doppler Imaging");
			specs.Add(new MeasurementSpecification("Mitral annulus S'", "Peak Tissue Velocity", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.MitralAnnulusS(pd, "Mitral annulus S' velocity"), unitEnum: SpeedUnit.CentimeterPerSecond));
			props.Clear();

			props.Add("Finding Site", "Medial Mitral Annulus");
			props.Add("Cardiac Cycle Point", "Early Diastole");
			props.Add("Image Mode", "Tissue Doppler Imaging");
			specs.Add(new MeasurementSpecification("Septal annulus E'", "Peak Tissue Velocity", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.SeptalAnnulusE(pd, "Septal annulus E' velocity"), unitEnum: SpeedUnit.CentimeterPerSecond));
			props.Clear();
			props.Add("Finding Site", "Medial Mitral Annulus");
			props.Add("Cardiac Cycle Point", "Atrial Systole");
			props.Add("Image Mode", "Tissue Doppler Imaging");
			specs.Add(new MeasurementSpecification("Septal annulus A'", "Peak Tissue Velocity", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.SeptalAnnulusA(pd, "Septal annulus A' velocity"), unitEnum: SpeedUnit.CentimeterPerSecond));
			props.Clear();
			props.Add("Finding Site", "Medial Mitral Annulus");
			props.Add("Cardiac Cycle Point", "End Systole");
			props.Add("Image Mode", "Tissue Doppler Imaging");
			specs.Add(new MeasurementSpecification("Septal annulus S'", "Peak Tissue Velocity", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.SeptalAnnulusS(pd, "Septal annulus S' velocity"), unitEnum: SpeedUnit.CentimeterPerSecond));
			props.Clear();

			specs.Add(new MeasurementSpecification("LV IVRT", "Isovolumic Relaxation Time", new Dictionary<string, string>(props), "ms", formula: EchoManualFormula.LVIVRT(pd, "LV IVRT"), unitEnum: DurationUnit.Millisecond));
			props.Clear();

			props.Add("Finding Site", "Left Ventricle Outflow Tract");
			specs.Add(new MeasurementSpecification("Left ventricle outflow peak velocity", "Peak Velocity", new Dictionary<string, string>(props), "m/s", unitEnum: SpeedUnit.MeterPerSecond));
			specs.Add(new MeasurementSpecification("Left ventricle outflow peak gradient", "Peak Gradient", new Dictionary<string, string>(props), "mmHg"));
			specs.Add(new MeasurementSpecification("Left ventricle outflow mean gradient", "Mean Gradient", new Dictionary<string, string>(props), "mmHg"));
			specs.Add(new MeasurementSpecification("Left ventricle outflow dimension", "Cardiovascular Orifice Diameter", new Dictionary<string, string>(props), "cm", unitEnum: LengthUnit.Centimeter));
			props.Clear();

			props.Add("Measurement Method", "Method of Disks, Biplane");
			specs.Add(new MeasurementSpecification("Left Ventricular biplane EF", "Cardiac ejection fraction", new Dictionary<string, string>(props), "%", includeImageMode: true, formula: LVEFFormula.LVBiplaneEF()));
			props.Clear();

			props.Add("Measurement Method", "Teichholz");
			specs.Add(new MeasurementSpecification("Left Ventricular Teichholz EF", "Cardiac ejection fraction", new Dictionary<string, string>(props), "%", includeImageMode: true));
			props.Clear();

			props.Add("Finding Site", "Left Ventricle Outflow Tract");
			specs.Add(new MeasurementSpecification("Left ventricular cardiac output", "Cardiac Output", new Dictionary<string, string>(props), "l/min"));
			props.Clear();

			props.Add("Measurement Method", "Method of Disks, Single Plane");
			props.Add("Image View", "Apical four chamber");
			specs.Add(new MeasurementSpecification("Left ventricular Apical 4 chamber EF", "Cardiac ejection fraction", new Dictionary<string, string>(props), "%", includeImageMode: true, formula: LVEFFormula.LVBiplaneEF()));
			props.Clear();

			specs.Add(new MeasurementSpecification("MVCFc", "HR-Corrected Mean Velocity of Circumferential Fiber Shortening", new Dictionary<string, string>(props), "circ/sec", includeImageMode: true));
			props.Clear();

			return specs;
		}
		private static List<MeasurementSpecification> AorticValveSpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			props.Add("Finding Site", "Cardiac valve annulus");
			specs.Add(new MeasurementSpecification("Aortic valve annulus", "Diameter", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.AorticValveAnnulus(pd, "Aortic valve annulus"), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();

			props.Add("Flow Direction", "Antegrade Flow");
			specs.Add(new MeasurementSpecification("Aortic valve mean gradient", "Mean Gradient", new Dictionary<string, string>(props), "mmHg"));
			specs.Add(new MeasurementSpecification("Aortic valve peak gradient", "Peak Gradient", new Dictionary<string, string>(props), "mmHg"));
			specs.Add(new MeasurementSpecification("Aortic valve peak velocity", "Peak Velocity", new Dictionary<string, string>(props), "m/s", unitEnum: SpeedUnit.MeterPerSecond));
			props.Clear();

			props.Add("Flow Direction", "Regurgitant Flow");
			specs.Add(new MeasurementSpecification("Aortic valve pressure half-time", "Pressure Half-Time", new Dictionary<string, string>(props), "ms", unitEnum: DurationUnit.Millisecond));
			props.Clear();

			return specs;
		}
		private static List<MeasurementSpecification> StructureSinusOfValsalvaSpecs(PatientData pd) {
			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			specs.Add(new MeasurementSpecification("Sinuses of Valsalva", "Diameter", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.SinusesOfValsalva(pd, "Sinuses of Valsalva"), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			return specs;
		}
		private static List<MeasurementSpecification> AorticSinotubularJunctionSpecs(PatientData pd) {
			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			specs.Add(new MeasurementSpecification("Sinotubular junction", "Diameter", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.SinotubularJunction(pd, "Sinotubular junction"), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();

			return specs;
		}
		private static List<MeasurementSpecification> AorticArchSpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			props.Add("Selection Status", "User chosen value");
			specs.Add(new MeasurementSpecification("Transverse aortic arch", "Diameter", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.TransverseAorticArch(pd, "Transverse aortic arch"), includeImageMode: true, unitEnum: LengthUnit.Centimeter));

			props.Add("Topographical modifier", "Distal");
			specs.Add(new MeasurementSpecification("Distal aortic arch", "Diameter", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.DistalAorticArch(pd, "Distal aortic arch"), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();
			return specs;
		}
		private static List<MeasurementSpecification> AorticIsthmusSpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			specs.Add(new MeasurementSpecification("Aortic isthmus", "Diameter", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.AorticIsthmus(pd, "Aortic isthmus"), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();
			return specs;
		}
		private static List<MeasurementSpecification> PulmonicValveSpecs(PatientData pd) {
			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			props.Add("Finding Site", "Cardiac valve annulus");
			specs.Add(new MeasurementSpecification("Pulmonary valve annulus", "Diameter", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.PulmonaryValveAnnulus(pd, "Pulmonary valve annulus"), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();

			props.Add("Flow Direction", "Antegrade Flow");
			specs.Add(new MeasurementSpecification("Pulmonary valve mean gradient", "Mean Gradient", new Dictionary<string, string>(props), "mmHg"));
			specs.Add(new MeasurementSpecification("Pulmonary valve peak gradient", "Peak Gradient", new Dictionary<string, string>(props), "mmHg"));
			specs.Add(new MeasurementSpecification("Pulmonary valve peak velocity", "Peak Velocity", new Dictionary<string, string>(props), "m/s", unitEnum: SpeedUnit.MeterPerSecond));
			props.Clear();

			props.Add("Flow Direction", "Regurgitant Flow");
			specs.Add(new MeasurementSpecification("Pulmonary valve end diastolic velocity", "End Diastolic Velocity", new Dictionary<string, string>(props), "m/s", unitEnum: SpeedUnit.MeterPerSecond));
			props.Clear();
			return specs;
		}
		private static List<MeasurementSpecification> PulmonaryArterySpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			specs.Add(new MeasurementSpecification("Main pulmonary artery", "Diameter", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.MainPulmonaryArtery(pd, "Main pulmonary artery"), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();

			props.Add("Finding Site", "Left pulmonary artery");
			specs.Add(new MeasurementSpecification("Left pulmonary artery", "Diameter", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.LeftPulmonaryArtery(pd, "Left pulmonary artery"), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			specs.Add(new MeasurementSpecification("Left pulmonary artery peak gradient", "Peak Gradient", new Dictionary<string, string>(props), "mmHg"));
			specs.Add(new MeasurementSpecification("Left pulmonary artery peak velocity", "Peak Velocity", new Dictionary<string, string>(props), "m/s", unitEnum: SpeedUnit.MeterPerSecond));
			props.Clear();

			props.Add("Finding Site", "Right pulmonary artery");
			specs.Add(new MeasurementSpecification("Right pulmonary artery", "Diameter", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.RightPulmonaryArtery(pd, "Right pulmonary artery"), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			specs.Add(new MeasurementSpecification("Right pulmonary artery peak gradient", "Peak Gradient", new Dictionary<string, string>(props), "mmHg"));
			specs.Add(new MeasurementSpecification("Right pulmonary artery peak velocity", "Peak Velocity", new Dictionary<string, string>(props), "m/s", unitEnum: SpeedUnit.MeterPerSecond));
			props.Clear();

			return specs;
		}
		private static List<MeasurementSpecification> MitralValveSpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			specs.Add(new MeasurementSpecification("Mitral valve annulus", "Cardiovascular Orifice Diameter", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.MitralValveAnnulus(pd, "Mitral valve annulus"), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();

			props.Add("Flow Direction", "Antegrade Flow");
			specs.Add(new MeasurementSpecification("Mitral valve E wave", "E-Wave Peak Velocity", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.MitralValveEWave(pd, "Mitral valve E wave velocity"), unitEnum: SpeedUnit.CentimeterPerSecond));
			specs.Add(new MeasurementSpecification("Mitral valve A wave", "A-Wave Peak Velocity", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.MitralValveAWave(pd, "Mitral valve A wave velocity"), unitEnum: SpeedUnit.CentimeterPerSecond));
			props.Clear();

			props.Add("Image Mode", "Tissue Doppler Imaging");
			specs.Add(new MeasurementSpecification("Mitral E/A ratio", "Peak E wave/Peak A wave by US", new Dictionary<string, string>(props), "", formula: ImpactOfCardiacGrowthFormula.MitralEA_Ratio(pd, "Mitral E/A ratio")));
			props.Clear();


			props.Add("Flow Direction", "Antegrade Flow");
			specs.Add(new MeasurementSpecification("MV decel time", "Deceleration Time", new Dictionary<string, string>(props), "ms", formula: EchoManualFormula.MVDecelTime(pd, "mitral valve deceleration time"), unitEnum: DurationUnit.Millisecond));
			props.Clear();


			props.Add("Flow Direction", "Antegrade Flow");
			specs.Add(new MeasurementSpecification("Mitral valve inflow mean gradient", "Mean Gradient", new Dictionary<string, string>(props), "mmHg"));
			props.Clear();

			props.Add("Flow Direction", "Regurgitant Flow");
			specs.Add(new MeasurementSpecification("Mitral valve regurgitation peak gradient", "Peak Gradient", new Dictionary<string, string>(props), "mmHg"));
			specs.Add(new MeasurementSpecification("Mitral valve regurgitation peak velocity", "Peak Velocity", new Dictionary<string, string>(props), "m/s", unitEnum: SpeedUnit.MeterPerSecond));
			props.Clear();

			specs.Add(new MeasurementSpecification("Mitral valve inflow A wave duration", "A-Wave Duration", new Dictionary<string, string>(props), "ms", unitEnum: DurationUnit.Millisecond));
			specs.Add(new MeasurementSpecification("Myocardial Performance Index", "Myocardial Performance Index (Tei)", new Dictionary<string, string>(props), ""));
			props.Clear();

			return specs;
		}
		private static List<MeasurementSpecification> TricuspidValveSpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			props.Add("Finding Site", "Cardiac valve annulus");
			specs.Add(new MeasurementSpecification("Tricuspid valve annulus", "Diameter", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.TricuspidValveAnnulus(pd, "Tricuspid valve annulus"), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();

			props.Add("Flow Direction", "Antegrade Flow");
			specs.Add(new MeasurementSpecification("Tricuspid valve inflow mean gradient", "Mean Gradient", new Dictionary<string, string>(props), "mmHg"));
			props.Clear();

			props.Add("Flow Direction", "Regurgitant Flow");
			specs.Add(new MeasurementSpecification("Estimated RV systolic pressure", "Peak Gradient", new Dictionary<string, string>(props), "mmHg + RA pressure"));
			specs.Add(new MeasurementSpecification("Tricuspid valve regurgitation peak velocity", "Peak Velocity", new Dictionary<string, string>(props), "m/s", unitEnum: SpeedUnit.MeterPerSecond));
			props.Clear();

			specs.Add(new MeasurementSpecification("Tricuspid annulus E'", "", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.TricuspidAnnulusE(pd, "Tricuspid annulus E' velocity"), unitEnum: SpeedUnit.CentimeterPerSecond));
			specs.Add(new MeasurementSpecification("Tricuspid annulus A'", "", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.TricuspidAnnulusA(pd, "Tricuspid annulus A' velocity"), unitEnum: SpeedUnit.CentimeterPerSecond));
			specs.Add(new MeasurementSpecification("Tricuspid annulus S'", "", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.TricuspidAnnulusS(pd, "Tricuspid annulus S' velocity"), unitEnum: SpeedUnit.CentimeterPerSecond));
			props.Clear();

			return specs;
		}
		private static List<MeasurementSpecification> AscendingAortaSpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			specs.Add(new MeasurementSpecification("Ascending aorta", "Diameter", new Dictionary<string, string>(props), "mm", formula: DilationOfAscendingAortaFormula.AscendingAorta(pd, "ascending aorta"), includeImageMode: true, unitEnum: LengthUnit.Millimeter));
			props.Clear();

			specs.Add(new MeasurementSpecification("Ascending aorta peak velocity", "Peak Velocity", new Dictionary<string, string>(props), "m/s", unitEnum: SpeedUnit.MeterPerSecond));
			specs.Add(new MeasurementSpecification("Ascending aorta peak gradient", "Peak Gradient", new Dictionary<string, string>(props), "mmHg"));
			props.Clear();

			return specs;
		}
		private static List<MeasurementSpecification> LeftMainCoronaryArterySpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			specs.Add(new MeasurementSpecification("Left Main Coronary Artery", "Diameter", new Dictionary<string, string>(props), "mm", formula: CoronaryArteryInvolvementFormula.LeftMainCoronary(pd, "left main coronary artery"), includeImageMode: true, unitEnum: LengthUnit.Millimeter));
			props.Clear();

			return specs;
		}
		private static List<MeasurementSpecification> AnteriorDescendingBranchOfLeftCoronaryArterySpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			specs.Add(new MeasurementSpecification("Anterior Descending Branch of Left Coronary Artery", "Diameter", new Dictionary<string, string>(props), "mm", formula: CoronaryArteryInvolvementFormula.LeftAnteriorDescending(pd, "left anterior descending artery"), includeImageMode: true, unitEnum: LengthUnit.Millimeter, altName: "Left Anterior Descending Artery"));
			props.Clear();

			return specs;
		}
		private static List<MeasurementSpecification> RightCoronaryArterySpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			specs.Add(new MeasurementSpecification("Right Coronary Artery", "Diameter", new Dictionary<string, string>(props), "mm", formula: CoronaryArteryInvolvementFormula.RightCoronaryArtery(pd, "right coronary artery"), includeImageMode: true, unitEnum: LengthUnit.Millimeter));
			props.Clear();

			return specs;
		}
		private static List<MeasurementSpecification> PulmonaryVeinSpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			props.Add("Cardiac Cycle Point", "End Systole");
			specs.Add(new MeasurementSpecification("Pulm vein S wave", "Peak Velocity", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.PulmVeinSWave(pd, "pulmonary venous S wave velocity"), unitEnum: SpeedUnit.CentimeterPerSecond));
			props.Clear();

			props.Add("Cardiac Cycle Point", "End Diastole");
			specs.Add(new MeasurementSpecification("Pulm vein D wave", "Peak Velocity", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.PulmVeinDWave(pd, "pulmonary venous D wave velocity"), unitEnum: SpeedUnit.CentimeterPerSecond));
			props.Clear();

			specs.Add(new MeasurementSpecification("Pulm vein A wave", "Peak Reversal Velocity during Atrial Contraction", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.PulmVeinAWave(pd, "pulmonary venous A wave velocity"), unitEnum: SpeedUnit.CentimeterPerSecond));
			specs.Add(new MeasurementSpecification("Pulmonary vein A wave duration", "A-Wave Duration", new Dictionary<string, string>(props), "ms", unitEnum: DurationUnit.Millisecond));
			props.Clear();

			return specs;
		}
		private static List<MeasurementSpecification> AtrialSeptalDefectSpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			specs.Add(new MeasurementSpecification("Atrial Septal Defect dimension", "Major Axis", new Dictionary<string, string>(props), "cm", includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			specs.Add(new MeasurementSpecification("Atrial Septal Defect mean gradient", "Mean Gradient", new Dictionary<string, string>(props), "mmHg"));

			return specs;
		}
		private static List<MeasurementSpecification> VentricularSeptalDefectSpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			specs.Add(new MeasurementSpecification("Ventricular Septal Defect peak gradient", "Peak Gradient", new Dictionary<string, string>(props), "mmHg"));
			specs.Add(new MeasurementSpecification("Ventricular Septal Defect peak velocity", "Peak Velocity", new Dictionary<string, string>(props), "m/s", unitEnum: SpeedUnit.MeterPerSecond));
			specs.Add(new MeasurementSpecification("Ventricular Septal Defect dimension", "Major Axis", new Dictionary<string, string>(props), "cm", includeImageMode: true, unitEnum: LengthUnit.Centimeter));

			return specs;
		}
		private static List<MeasurementSpecification> RightVentricleSpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			props.Add("Finding Site", "Right Ventricle Outflow Tract");
			specs.Add(new MeasurementSpecification("Right ventricle outflow peak velocity", "Peak Velocity", new Dictionary<string, string>(props), "m/s", unitEnum: SpeedUnit.MeterPerSecond));
			specs.Add(new MeasurementSpecification("Right ventricle outflow peak gradient", "Peak Gradient", new Dictionary<string, string>(props), "mmHg"));
			specs.Add(new MeasurementSpecification("Right ventricle outflow mean gradient", "Mean Gradient", new Dictionary<string, string>(props), "mmHg"));
			specs.Add(new MeasurementSpecification("Right ventricle outflow dimension", "Cardiovascular Orifice Diameter", new Dictionary<string, string>(props), "cm", includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();
			//specs.Add(new MeasurementSpecification("RV E'", "", new Dictionary<string, string>(props), "cm/s", unitEnum: SpeedUnit.CentimeterPerSecond));
			props.Clear();

			return specs;
		}
		private static List<MeasurementSpecification> PulmonaryTrunkSpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			specs.Add(new MeasurementSpecification("Main pulmonary artery peak gradient", "Peak Gradient", new Dictionary<string, string>(props), "mmHg"));
			specs.Add(new MeasurementSpecification("Main pulmonary artery peak velocity", "Peak Velocity", new Dictionary<string, string>(props), "m/s", unitEnum: SpeedUnit.MeterPerSecond));
			props.Clear();

			return specs;
		}
		private static List<MeasurementSpecification> CoarctationOfTheAortaSpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			specs.Add(new MeasurementSpecification("Coarctation of the aorta", "Diameter", new Dictionary<string, string>(props), "cm", includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();

			return specs;
		}
		private static List<MeasurementSpecification> ThoracicAortaSpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			specs.Add(new MeasurementSpecification("Descending aorta peak velocity", "Peak Velocity", new Dictionary<string, string>(props), "m/s", unitEnum: SpeedUnit.MeterPerSecond));
			specs.Add(new MeasurementSpecification("Descending aorta peak gradient", "Peak Gradient", new Dictionary<string, string>(props), "mmHg"));
			props.Clear();

			return specs;
		}
		private static List<MeasurementSpecification> CircumflexCoronaryArterySpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			specs.Add(new MeasurementSpecification("Left Circumflex", "Diameter", new Dictionary<string, string>(props), "mm", includeImageMode: true, unitEnum: LengthUnit.Millimeter));
			props.Clear();

			return specs;
		}
		private static List<MeasurementSpecification> PatentDuctusArteriosusSpecs(PatientData pd) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			specs.Add(new MeasurementSpecification("Patent Ductus Arteriosus", "Diameter", new Dictionary<string, string>(props), "cm", includeImageMode: true, unitEnum: LengthUnit.Centimeter));

			props.Add("Cardiac Cycle Point", "End Systole");
			specs.Add(new MeasurementSpecification("Patent Ductus Arteriosus peak velocity systole", "Peak Velocity", new Dictionary<string, string>(props), "m/s", unitEnum: SpeedUnit.MeterPerSecond));
			props.Clear();

			props.Add("Cardiac Cycle Point", "End Diastole");
			specs.Add(new MeasurementSpecification("Patent Ductus Arteriosus peak velocity diastole", "Peak Velocity", new Dictionary<string, string>(props), "m/s", unitEnum: SpeedUnit.MeterPerSecond));
			props.Clear();

			return specs;
		}
	}
}
