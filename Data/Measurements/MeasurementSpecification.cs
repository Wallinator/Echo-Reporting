﻿using Accessibility;
using DICOMReporting.Data.Measurements;
using DICOMReporting.Data.Measurements.Units;
using DICOMReporting.Formulas;
using System;
using System.Collections.Generic;
using System.Text;
using UnitsNet;
using UnitsNet.Units;

namespace DICOMReporting.Data.Measurements {
	public class MeasurementSpecification {
		public string Name;
		public string RawMeasurementName;
		public Dictionary<string, string> Properties;
		public string DefaultUnitShorthand;
		public bool IncludeImageMode;
		public Enum UnitEnum;
		public IFormula Formula;

		public MeasurementSpecification(string name, string rawMeasurementName, Dictionary<string, string> properties, string defaultUnitShorthand, IFormula formula = null, bool includeImageMode = false, Enum unitEnum = null) {
			Name = name;
			RawMeasurementName = rawMeasurementName;
			Properties = properties;
			DefaultUnitShorthand = defaultUnitShorthand;
			Formula = formula;
			IncludeImageMode = includeImageMode;
			UnitEnum = unitEnum;
		}

		public Result FindAndRemoveFromGroups(List<MeasurementGroup> groups) {
			if (IncludeImageMode) {
				Properties.Add("Image Mode", "M mode");
			}

			var groupIndex = groups.FindIndex(g => g.Name.Equals(RawMeasurementName) && MeasurementHelpers.CompareProperties(Properties, g));
			if (groupIndex > -1) {
				var group = groups[groupIndex];
				groups.RemoveAt(groupIndex);
				return MeasurementToResult(group.SelectMean());
			}

			else {
				if (IncludeImageMode) {
					Properties["Image Mode"] = "2D mode";
					IncludeImageMode = false;
					return FindAndRemoveFromGroups(groups);
				}
				return MeasurementToResult(null);
			}
		}
		private Result MeasurementToResult(Measurement measurement) {
			if (measurement == null) {
				return new Result(Name, DefaultUnitShorthand, Formula);
			}
			if (UnitEnum != null) {
				SupportedUnitsHelpers.Convert(measurement.Header, UnitEnum);
			}
			return new Result(Name, measurement.Header.UnitShorthand, Formula, false, measurement.Header.Value);

		}


		public static Dictionary<string, List<MeasurementSpecification>> SpecsBySite(double BSA, double AgeInYears) {
			var final = new Dictionary<string, List<MeasurementSpecification>>();

			final["Interventricular septum"] = InterventricularSeptumSpecs(BSA, AgeInYears);
			final["Left Ventricle"] = LeftVentricleSpecs(BSA, AgeInYears);
			final["Aortic Valve"] = AorticValveSpecs(BSA, AgeInYears);
			final["Structure Sinus of Valsalva"] = StructureSinusOfValsalvaSpecs(BSA, AgeInYears);
			final["Aortic sinotubular junction"] = AorticSinotubularJunctionSpecs(BSA, AgeInYears);
			final["Aortic arch"] = AorticArchSpecs(BSA, AgeInYears);

			return final;
		}

		private static List<MeasurementSpecification> InterventricularSeptumSpecs(double BSA, double AgeInYears) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			props.Add("Cardiac Cycle Point", "End Diastole");
			specs.Add(new MeasurementSpecification("IVSd", "ROI Thickness by US", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.IVSd(BSA), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();

			props.Add("Cardiac Cycle Point", "End Systole");
			specs.Add(new MeasurementSpecification("IVSs", "ROI Thickness by US", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.IVSs(BSA), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();
			return specs;
		}
		private static List<MeasurementSpecification> LeftVentricleSpecs(double BSA, double AgeInYears) {
			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			props.Add("Cardiac Cycle Point", "End Diastole");
			specs.Add(new MeasurementSpecification("LVIDd", "ROI Internal Dimension by US", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.LVIDd(BSA), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();
			props.Add("Cardiac Cycle Point", "End Systole");
			specs.Add(new MeasurementSpecification("LVIDs", "ROI Internal Dimension by US", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.LVIDs(BSA), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();


			props.Add("Finding Site", "Posterior Wall");
			props.Add("Cardiac Cycle Point", "End Diastole");
			specs.Add(new MeasurementSpecification("LVPWd", "ROI Thickness by US", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.LVPWd(BSA), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();

			props.Add("Finding Site", "Posterior Wall");
			props.Add("Cardiac Cycle Point", "End Systole");
			specs.Add(new MeasurementSpecification("LVPWs", "ROI Thickness by US", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.LVPWs(BSA), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();

			props.Add("Measurement Method", "Cube Method");
			props.Add("Index", "Body Surface Area");
			specs.Add(new MeasurementSpecification("LV mass index", "Left Ventricle Mass Index", new Dictionary<string, string>(props), "g/m2", formula: ImpactOfCardiacGrowthFormula.LVMassIndex(AgeInYears), includeImageMode: true));
			props.Clear();

			specs.Add(new MeasurementSpecification("Heart Rate", "Heart Rate", new Dictionary<string, string>(props), "bpm", formula: ImpactOfCardiacGrowthFormula.HeartRate(AgeInYears), includeImageMode: true));
			props.Clear();

			specs.Add(new MeasurementSpecification("Fractional Shortening", "Fractional Shortening", new Dictionary<string, string>(props), "%", formula: ImpactOfCardiacGrowthFormula.FractionalShortening(AgeInYears), includeImageMode: true));
			props.Clear();

			props.Add("Finding Site", "Lateral Mitral Annulus");
			props.Add("Cardiac Cycle Point", "Early Diastole");
			props.Add("Image Mode", "Tissue Doppler Imaging");
			specs.Add(new MeasurementSpecification("Mitral annulus E'", "Peak Tissue Velocity", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.MitralAnnulusE(AgeInYears), unitEnum: SpeedUnit.CentimeterPerSecond));
			props.Clear();
			props.Add("Finding Site", "Lateral Mitral Annulus");
			props.Add("Cardiac Cycle Point", "Atrial Systole");
			props.Add("Image Mode", "Tissue Doppler Imaging");
			specs.Add(new MeasurementSpecification("Mitral annulus A'", "Peak Tissue Velocity", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.MitralAnnulusA(AgeInYears), unitEnum: SpeedUnit.CentimeterPerSecond));
			props.Clear();
			props.Add("Finding Site", "Lateral Mitral Annulus");
			props.Add("Cardiac Cycle Point", "End Systole");
			props.Add("Image Mode", "Tissue Doppler Imaging");
			specs.Add(new MeasurementSpecification("Mitral annulus S'", "Peak Tissue Velocity", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.MitralAnnulusS(AgeInYears), unitEnum: SpeedUnit.CentimeterPerSecond));
			props.Clear();

			props.Add("Finding Site", "Medial Mitral Annulus");
			props.Add("Cardiac Cycle Point", "Early Diastole");
			props.Add("Image Mode", "Tissue Doppler Imaging");
			specs.Add(new MeasurementSpecification("Septal annulus E'", "Peak Tissue Velocity", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.SeptalAnnulusE(AgeInYears), unitEnum: SpeedUnit.CentimeterPerSecond));
			props.Clear();
			props.Add("Finding Site", "Medial Mitral Annulus");
			props.Add("Cardiac Cycle Point", "Atrial Systole");
			props.Add("Image Mode", "Tissue Doppler Imaging");
			specs.Add(new MeasurementSpecification("Septal annulus A'", "Peak Tissue Velocity", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.SeptalAnnulusA(AgeInYears), unitEnum: SpeedUnit.CentimeterPerSecond));
			props.Clear();
			props.Add("Finding Site", "Medial Mitral Annulus");
			props.Add("Cardiac Cycle Point", "End Systole");
			props.Add("Image Mode", "Tissue Doppler Imaging");
			specs.Add(new MeasurementSpecification("Septal annulus S'", "Peak Tissue Velocity", new Dictionary<string, string>(props), "cm/s", formula: ImpactOfCardiacGrowthFormula.SeptalAnnulusS(AgeInYears), unitEnum: SpeedUnit.CentimeterPerSecond));
			props.Clear();

			specs.Add(new MeasurementSpecification("LV IVRT", "Isovolumic Relaxation Time", new Dictionary<string, string>(props), "ms", formula: EchoManualFormula.LVIVRT(AgeInYears), unitEnum: DurationUnit.Millisecond));
			props.Clear();


			props.Add("Finding Site", "Left Ventricle Outflow Tract");
			specs.Add(new MeasurementSpecification("Left ventricle outflow peak velocity", "Peak Velocity", new Dictionary<string, string>(props), "m/s", unitEnum: SpeedUnit.MeterPerSecond));
			specs.Add(new MeasurementSpecification("Left ventricle outflow peak gradient", "Peak Gradient", new Dictionary<string, string>(props), "mm[Hg]"));
			specs.Add(new MeasurementSpecification("Left ventricle outflow mean gradient", "Mean Gradient", new Dictionary<string, string>(props), "mm[Hg]"));
			props.Clear();

			props.Add("Finding Site", "Left Ventricle Outflow Tract");
			specs.Add(new MeasurementSpecification("Left ventricle outflow dimension", "Cardiovascular Orifice Diameter", new Dictionary<string, string>(props), "cm", unitEnum: LengthUnit.Centimeter));
			props.Clear();

			props.Add("Measurement Method", "Method of Disks, Biplane");
			specs.Add(new MeasurementSpecification("Left Ventricular biplane EF", "Cardiac ejection fraction", new Dictionary<string, string>(props), "%", includeImageMode: true));
			props.Clear();

			props.Add("Measurement Method", "Teichholz");
			specs.Add(new MeasurementSpecification("Left Ventricular Teichholz EF", "Cardiac ejection fraction", new Dictionary<string, string>(props), "%", includeImageMode: true));
			props.Clear();

			props.Add("Finding Site", "Left Ventricle Outflow Tract");
			specs.Add(new MeasurementSpecification("Left ventricular cardiac output", "Cardiac Output", new Dictionary<string, string>(props), "l/min"));
			props.Clear();

			props.Add("Measurement Method", "Method of Disks, Single Plane");
			props.Add("Image View", "Apical four chamber");
			specs.Add(new MeasurementSpecification("Left ventricular Apical 4 chamber EF", "Cardiac ejection fraction", new Dictionary<string, string>(props), "%", includeImageMode: true));
			props.Clear();

			specs.Add(new MeasurementSpecification("MVCFc", "HR-Corrected Mean Velocity of Circumferential Fiber Shortening", new Dictionary<string, string>(props), "circ/sec", includeImageMode: true));
			props.Clear();

			return specs;
		}


		private static List<MeasurementSpecification> AorticValveSpecs(double BSA, double AgeInYears) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			props.Add("Finding Site", "Cardiac valve annulus");
			specs.Add(new MeasurementSpecification("Aortic valve annulus", "Diameter", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.AorticValveAnnulus(BSA), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();

			props.Add("Flow Direction", "Antegrade Flow");
			specs.Add(new MeasurementSpecification("Aortic valve mean gradient", "Mean Gradient", new Dictionary<string, string>(props), "mm[Hg]"));
			specs.Add(new MeasurementSpecification("Aortic valve peak gradient", "Peak Gradient", new Dictionary<string, string>(props), "mm[Hg]"));
			specs.Add(new MeasurementSpecification("Aortic valve peak velocity", "Peak Velocity", new Dictionary<string, string>(props), "m/s", unitEnum: SpeedUnit.MeterPerSecond));
			props.Clear();
			return specs;
		}
		private static List<MeasurementSpecification> StructureSinusOfValsalvaSpecs(double BSA, double AgeInYears) {
			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			props.Add("Finding Site", "Cardiac valve annulus");
			specs.Add(new MeasurementSpecification("Sinuses of Valsalva", "Diameter", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.SinusesOfValsalva(BSA), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			return specs;
		}
		private static List<MeasurementSpecification> AorticSinotubularJunctionSpecs(double BSA, double AgeInYears) {
			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			specs.Add(new MeasurementSpecification("Sinotubular junction", "Diameter", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.SinotubularJunction(BSA), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();

			return specs;
		}
		private static List<MeasurementSpecification> AorticArchSpecs(double BSA, double AgeInYears) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			specs.Add(new MeasurementSpecification("Transverse aortic arch", "Diameter", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.TransverseAorticArch(BSA), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();

			props.Add("Topographical modifier", "Distal");
			specs.Add(new MeasurementSpecification("Distal aortic arch", "Diameter", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.DistalAorticArch(BSA), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();
			return specs;
		}
		private static List<MeasurementSpecification> AorticValveSpecs124124(double BSA, double AgeInYears) {

			var specs = new List<MeasurementSpecification>();
			Dictionary<string, string> props = new Dictionary<string, string>();

			props.Add("Finding Site", "Cardiac valve annulus");
			specs.Add(new MeasurementSpecification("Aortic valve annulus", "Diameter", new Dictionary<string, string>(props), "cm", formula: RegressionEquationFormula.AorticValveAnnulus(BSA), includeImageMode: true, unitEnum: LengthUnit.Centimeter));
			props.Clear();

			props.Add("Flow Direction", "Antegrade Flow");
			specs.Add(new MeasurementSpecification("Aortic valve mean gradient", "Mean Gradient", new Dictionary<string, string>(props), "mm[Hg]"));
			specs.Add(new MeasurementSpecification("Aortic valve peak gradient", "Peak Gradient", new Dictionary<string, string>(props), "mm[Hg]"));
			specs.Add(new MeasurementSpecification("Aortic valve peak velocity", "Peak Velocity", new Dictionary<string, string>(props), "m/s", unitEnum: SpeedUnit.MeterPerSecond));
			props.Clear();
			return specs;
		}
	}

}