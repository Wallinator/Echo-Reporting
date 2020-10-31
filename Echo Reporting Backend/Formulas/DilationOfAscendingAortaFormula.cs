using DICOMReporting.Data;
using System;

namespace DICOMReporting.Formulas {
	public class DilationOfAscendingAortaFormula : IFormula {
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

			if (constants.AnomalyPrefix && constants.Anomalies[bracket].Length != 0) {
				return constants.Anomalies[bracket] + " " + constants.MeasurementName;
			}
			else {
				return constants.Anomalies[bracket];
			}
		}
		public double GetZScore(double observed_y) {
			double mean_y = (constants.Multiplier * Math.Log(constants.BSA)) + constants.Intercept;
			return (Math.Log(observed_y) - mean_y) / constants.MSE;
		}
		public bool ZScoreable() => true;
		public static DilationOfAscendingAortaFormula AscendingAorta(PatientData pd, string name) {
			return new DilationOfAscendingAortaFormula(new Constants(0.421, 2.898, 0.09111, pd, name, new[] { "Hypoplastic", "Normal", "Mildly dilated", "Moderately dilated", "Severely dilated" }));
		}/*
			public static Constants AorticAnnulus(double BSA) {
				return new Constants(0.426, 2.732, 0.10392, BSA);
			}
			public static Constants AorticSinuses(double BSA) {
				return new Constants(0.443, 3.021, 0.10173, BSA);
			}
			public static Constants STJunction(double BSA) {
				return new Constants(0.434, 2.819, 0.10961, BSA);
			}*/
		private DilationOfAscendingAortaFormula(Constants constants) {
			this.constants = constants;
		}
		private struct Constants {
			private PatientData Pd;
			public string MeasurementName {
				get; set;
			}
			public string[] Anomalies {
				get; private set;
			}
			public bool AnomalyPrefix {
				get; private set;
			}
			public double Multiplier {
				get; private set;
			}
			public double Intercept {
				get; private set;
			}
			public double MSE {
				get; private set;
			}
			public double BSA => Pd.BSA.Value;
			public Constants(double multiplier, double intercept, double mse, PatientData pd, string name, string[] anomalies, bool prefix = true) {
				AnomalyPrefix = prefix;
				MeasurementName = name;
				Anomalies = anomalies;
				Multiplier = multiplier;
				Intercept = intercept;
				MSE = mse;
				Pd = pd;
			}
		}
	}
}
