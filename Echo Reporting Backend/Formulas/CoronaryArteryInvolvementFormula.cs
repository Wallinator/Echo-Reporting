using DICOMReporting.Data;
using System;

namespace DICOMReporting.Formulas {
	public class CoronaryArteryInvolvementFormula : IFormula {
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
			else {
				bracket = 2;
			}

			if (constants.AnomalyPrefix && constants.Anomalies[bracket].Length != 0) {
				return constants.Anomalies[bracket] + " " + constants.MeasurementName;
			}
			else {
				return constants.Anomalies[bracket];
			}
		}
		public double GetZScore(double observed_y) {
			double mean_y = (constants.Multiplier * Math.Pow(constants.BSA, constants.Power)) + constants.Intercept;
			return ((observed_y / 10) - mean_y) / (constants.SDIntercept + (constants.SDMultiplier * constants.BSA));
		}
		public bool ZScoreable() => true;
		private CoronaryArteryInvolvementFormula(Constants constants) {
			this.constants = constants;
		}

		public static CoronaryArteryInvolvementFormula LeftMainCoronary(PatientData pd, string name) {
			return new CoronaryArteryInvolvementFormula(new Constants(0.31747, 0.36008, -0.02887, 0.03040, 0.01514, pd, name, new[] { "Normal", "Normal", "Dilated" }));
		}
		public static CoronaryArteryInvolvementFormula LeftAnteriorDescending(PatientData pd, string name) {
			return new CoronaryArteryInvolvementFormula(new Constants(0.26108, 0.37893, -0.02852, 0.01465, 0.01996, pd, name, new[] { "Normal", "Normal", "Dilated" }));
		}
		public static CoronaryArteryInvolvementFormula RightCoronaryArtery(PatientData pd, string name) {
			return new CoronaryArteryInvolvementFormula(new Constants(0.26117, 0.3992,  -0.02756, 0.02407, 0.01597, pd, name, new[] { "Normal", "Normal", "Dilated" }));
		}


		private struct Constants {
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
			public double Power {
				get; private set;
			}
			public double Intercept {
				get; private set;
			}
			public double SDIntercept {
				get; private set;
			}
			public double SDMultiplier {
				get; private set;
			}
			public double BSA => Pd.BSA.Value;
			private PatientData Pd;
			public Constants(double multiplier, double power, double intercept, double sdintercept, double sdmultiplier, PatientData pd, string name, string[] anomalies, bool prefix = true) {
				AnomalyPrefix = prefix;
				MeasurementName = name;
				Anomalies = anomalies;
				Multiplier = multiplier;
				Power = power;
				Intercept = intercept;
				SDIntercept = sdintercept;
				SDMultiplier = sdmultiplier;
				Pd = pd;
			}
		}
	}
}
