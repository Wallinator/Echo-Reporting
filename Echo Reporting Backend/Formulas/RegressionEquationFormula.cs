﻿using DICOMReporting.Data;
using System;

namespace DICOMReporting.Formulas {
	public class RegressionEquationFormula : IFormula {
		private Constants constants;

		private RegressionEquationFormula(Constants constants) {
			this.constants = constants;
		}
		public double GetZScore(double observed_y) {
			double mean_y = constants.b0 + (constants.b1 * constants.BSA) + (constants.b2 * Math.Pow(constants.BSA, 2)) + (constants.b3 * Math.Pow(constants.BSA, 3));
			return (Math.Log(observed_y) - mean_y) / Math.Sqrt(constants.MSE);
		}
		public bool ZScoreable() => true;
		public static RegressionEquationFormula IVSd(PatientData pd) {
			return new RegressionEquationFormula(new Constants(-1.242, 1.272, -0.762, 0.208, 0.046, pd));
		}
		public static RegressionEquationFormula IVSs(PatientData pd) {
			return new RegressionEquationFormula(new Constants(-1.048, 1.751, -1.177, 0.318, 0.034, pd));
		}
		public static RegressionEquationFormula LVIDd(PatientData pd) {
			return new RegressionEquationFormula(new Constants(0.105, 2.859, -2.119, 0.552, 0.01, pd));
		}
		public static RegressionEquationFormula LVIDs(PatientData pd) {
			return new RegressionEquationFormula(new Constants(-0.371, 2.833, -2.081, 0.538, 0.016, pd));
		}
		public static RegressionEquationFormula LVPWd(PatientData pd) {
			return new RegressionEquationFormula(new Constants(-1.586, 1.849, -1.188, 0.313, 0.037, pd));
		}
		public static RegressionEquationFormula LVPWs(PatientData pd) {
			return new RegressionEquationFormula(new Constants(-0.947, 1.907, -1.259, 0.33, 0.023, pd));
		}
		public static RegressionEquationFormula AorticValveAnnulus(PatientData pd) {
			return new RegressionEquationFormula(new Constants(-0.874, 2.708, -1.841, 0.452, 0.01, pd));
		}
		public static RegressionEquationFormula SinusesOfValsalva(PatientData pd) {
			return new RegressionEquationFormula(new Constants(-0.5, 2.537, -1.707, 0.42, 0.012, pd));
		}
		public static RegressionEquationFormula SinotubularJunction(PatientData pd) {
			return new RegressionEquationFormula(new Constants(-0.759, 2.643, -1.797, 0.442, 0.018, pd));
		}
		public static RegressionEquationFormula TransverseAorticArch(PatientData pd) {
			return new RegressionEquationFormula(new Constants(-0.79, 3.02, -2.484, 0.712, 0.023, pd));
		}
		public static RegressionEquationFormula AorticIsthmus(PatientData pd) {
			return new RegressionEquationFormula(new Constants(-1.072, 2.539, -1.627, 0.368, 0.027, pd));
		}
		public static RegressionEquationFormula DistalAorticArch(PatientData pd) {
			return new RegressionEquationFormula(new Constants(-0.976, 2.469, -1.746, 0.445, 0.026, pd));
		}
		public static RegressionEquationFormula PulmonaryValveAnnulus(PatientData pd) {
			return new RegressionEquationFormula(new Constants(-0.761, 2.774, -1.808, 0.436, 0.023, pd));
		}
		public static RegressionEquationFormula MainPulmonaryArtery(PatientData pd) {
			return new RegressionEquationFormula(new Constants(-0.707, 2.746, -1.807, 0.424, 0.024, pd));
		}
		public static RegressionEquationFormula RightPulmonaryArtery(PatientData pd) {
			return new RegressionEquationFormula(new Constants(-1.36, 3.394, -2.508, 0.66, 0.027, pd));
		}
		public static RegressionEquationFormula LeftPulmonaryArtery(PatientData pd) {
			return new RegressionEquationFormula(new Constants(-1.348, 2.884, -1.954, 0.466, 0.028, pd));
		}
		public static RegressionEquationFormula MitralValveAnnulus(PatientData pd) {
			return new RegressionEquationFormula(new Constants(-0.271, 2.446, -1.7, 0.425, 0.022, pd));
		}
		public static RegressionEquationFormula TricuspidValveAnnulus(PatientData pd) {
			return new RegressionEquationFormula(new Constants(-0.164, 2.341, -1.596, 0.387, 0.036, pd));
		}
		private struct Constants {
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
			public Constants(double b0, double b1, double b2, double b3, double mse, PatientData pd) {
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
