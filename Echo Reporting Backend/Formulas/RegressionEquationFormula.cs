using DICOMReporting.Data;
using System;

namespace DICOMReporting.Formulas {
	public class RegressionEquationFormula : IFormula {
		private Constants constants;

		public string ReportAnomaly(double measurement) {
			double ZScore = GetZScore(measurement);
			int bracket;
			if (ZScore < -2) {
				bracket = 0;
			}
			else if (ZScore <= 2) {
				bracket = 1;
			}
			else if (ZScore <= 3) {
				bracket = 2;
			}
			else if (ZScore <= 4) {
				bracket = 3;
			}
			else {
				bracket = 4;
			}

			if (constants.AnomalyPrefix) {
				return constants.Anomalies[bracket] + " " + constants.MeasurementName;
			}
			else {
				return constants.Anomalies[bracket];
			}
		}
		private RegressionEquationFormula(Constants constants) {
			this.constants = constants;
		}
		public double GetZScore(double observed_y) {
			double mean_y = constants.b0 + (constants.b1 * constants.BSA) + (constants.b2 * Math.Pow(constants.BSA, 2)) + (constants.b3 * Math.Pow(constants.BSA, 3));
			return (Math.Log(observed_y) - mean_y) / Math.Sqrt(constants.MSE);
		}
		public bool ZScoreable() => true;
		public static RegressionEquationFormula IVSd(PatientData pd, string name) {
			return new RegressionEquationFormula(new Constants(-1.242, 1.272, -0.762, 0.208, 0.046, pd, name, new[] { "", "", "Mild septal hypertrophy", "Moderate septal hypertrophy", "Severe septal hypertrophy" }, false));
		}
		public static RegressionEquationFormula IVSs(PatientData pd, string name) {
			return new RegressionEquationFormula(new Constants(-1.048, 1.751, -1.177, 0.318, 0.034, pd, name, new[] {"", "", "", "", "" }, false));
		}
		public static RegressionEquationFormula LVIDd(PatientData pd, string name) {
			return new RegressionEquationFormula(new Constants(0.105, 2.859, -2.119, 0.552, 0.01, pd, name, new[] { "Left ventricular hypoplasia", "", "Mild left ventricular dilatation", "Moderate left ventricular dilatation", "Severe left ventricular dilatation" }, false));
		}
		public static RegressionEquationFormula LVIDs(PatientData pd, string name) {
			return new RegressionEquationFormula(new Constants(-0.371, 2.833, -2.081, 0.538, 0.016, pd, name, new[] { "", "", "", "", "" }, false));
		}
		public static RegressionEquationFormula LVPWd(PatientData pd, string name) {
			return new RegressionEquationFormula(new Constants(-1.586, 1.849, -1.188, 0.313, 0.037, pd, name, new[] { "", "", "Mild LV free wall hypertrophy", "Moderate LV free wall hypertrophy", "Severe LV free wall hypertrophy" }, false));
		}
		public static RegressionEquationFormula LVPWs(PatientData pd, string name) {
			return new RegressionEquationFormula(new Constants(-0.947, 1.907, -1.259, 0.33, 0.023, pd, name, new[] { "", "", "", "", "" }, false));
		}
		public static RegressionEquationFormula AorticValveAnnulus(PatientData pd, string name) {
			return new RegressionEquationFormula(new Constants(-0.874, 2.708, -1.841, 0.452, 0.01, pd, name, new[] { "Hypoplastic", "Normal", "Mildly dilated", "Moderately dilated", "Severely dilated" }));
		}
		public static RegressionEquationFormula SinusesOfValsalva(PatientData pd, string name) {
			return new RegressionEquationFormula(new Constants(-0.5, 2.537, -1.707, 0.42, 0.012, pd, name, new[] { "Hypoplastic", "Normal", "Mildly dilated", "Moderately dilated", "Severely dilated" }));
		}
		public static RegressionEquationFormula SinotubularJunction(PatientData pd, string name) {
			return new RegressionEquationFormula(new Constants(-0.759, 2.643, -1.797, 0.442, 0.018, pd, name, new[] { "Hypoplastic", "Normal", "Mildly dilated", "Moderately dilated", "Severely dilated" }));
		}
		public static RegressionEquationFormula TransverseAorticArch(PatientData pd, string name) {
			return new RegressionEquationFormula(new Constants(-0.79, 3.02, -2.484, 0.712, 0.023, pd, name, new[] { "Hypoplastic", "Normal", "Mildly dilated", "Moderately dilated", "Severely dilated" }));
		}
		public static RegressionEquationFormula AorticIsthmus(PatientData pd, string name) {
			return new RegressionEquationFormula(new Constants(-1.072, 2.539, -1.627, 0.368, 0.027, pd, name, new[] { "Hypoplastic", "Normal", "Mildly dilated", "Moderately dilated", "Severely dilated" }));
		}
		public static RegressionEquationFormula DistalAorticArch(PatientData pd, string name) {
			return new RegressionEquationFormula(new Constants(-0.976, 2.469, -1.746, 0.445, 0.026, pd, name, new[] { "Hypoplastic", "Normal", "Mildly dilated", "Moderately dilated", "Severely dilated" }));
		}
		public static RegressionEquationFormula PulmonaryValveAnnulus(PatientData pd, string name) {
			return new RegressionEquationFormula(new Constants(-0.761, 2.774, -1.808, 0.436, 0.023, pd, name, new[] { "Hypoplastic", "Normal", "Mildly dilated", "Moderately dilated", "Severely dilated" }));
		}
		public static RegressionEquationFormula MainPulmonaryArtery(PatientData pd, string name) {
			return new RegressionEquationFormula(new Constants(-0.707, 2.746, -1.807, 0.424, 0.024, pd, name, new[] { "Hypoplastic", "Normal", "Mildly dilated", "Moderately dilated", "Severely dilated" }));
		}
		public static RegressionEquationFormula RightPulmonaryArtery(PatientData pd, string name) {
			return new RegressionEquationFormula(new Constants(-1.36, 3.394, -2.508, 0.66, 0.027, pd, name, new[] { "Hypoplastic", "Normal", "Mildly dilated", "Moderately dilated", "Severely dilated" }));
		}
		public static RegressionEquationFormula LeftPulmonaryArtery(PatientData pd, string name) {
			return new RegressionEquationFormula(new Constants(-1.348, 2.884, -1.954, 0.466, 0.028, pd, name, new[] { "Hypoplastic", "Normal", "Mildly dilated", "Moderately dilated", "Severely dilated" }));
		}
		public static RegressionEquationFormula MitralValveAnnulus(PatientData pd, string name) {
			return new RegressionEquationFormula(new Constants(-0.271, 2.446, -1.7, 0.425, 0.022, pd, name, new[] { "Hypoplastic", "Normal", "Mildly dilated", "Moderately dilated", "Severely dilated" }));
		}
		public static RegressionEquationFormula TricuspidValveAnnulus(PatientData pd, string name) {
			return new RegressionEquationFormula(new Constants(-0.164, 2.341, -1.596, 0.387, 0.036, pd, name, new[] { "Hypoplastic", "Normal", "Mildly dilated", "Moderately dilated", "Severely dilated" }));
		}
		private struct Constants {
			public string[] Anomalies {
				get; private set;
			}
			public bool AnomalyPrefix {
				get; private set;
			}
			public string MeasurementName {
				get; set;
			}
			public double b0 {
				get; private set;
			}
			public double b1 {
				get; private set;
			}
			public double b2 {
				get; private set;
			}
			public double b3 {
				get; private set;
			}
			public double MSE {
				get; private set;
			}
			public double BSA => Pd.BSA;
			private PatientData Pd;
			public Constants(double b0, double b1, double b2, double b3, double mse, PatientData pd, string name, string[] anomalies, bool prefix = true) {
				AnomalyPrefix = prefix;
				MeasurementName = name;
				Anomalies = anomalies;
				this.b0 = b0;
				this.b1 = b1;
				this.b2 = b2;
				this.b3 = b3;
				MSE = mse;
				Pd = pd;
			}
		}
	}
}
