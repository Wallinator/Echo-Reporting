using DICOMReporting.Data;
using System;

namespace DICOMReporting.Formulas {
	public class AgeBasedLimitFormula : IFormula {
		private Constants constants;
		public string ReportAnomaly(double measurement) {
			int ageBracket = GetAgeBracket();
			int bracket = measurement < constants.Limits[ageBracket] ? 0 : 1;

			if (constants.AnomalyPrefix && constants.Anomalies[bracket].Length != 0) {
				return constants.Anomalies[bracket] + " " + constants.MeasurementName + string.Format(constants.AnomalyTemplate, Math.Round(measurement, constants.Rounding), constants.Limits[ageBracket]);
			}
			else {
				return constants.Anomalies[bracket];
			}
		}

		public int GetAgeBracket() {
			int bracket = -1;
			foreach(var age in constants.Ages) {
				if(constants.PD.PatientAge.Value >= age) {
					++bracket;
				}
				else {
					return bracket;
				}
			}
			return bracket;
		}

		private AgeBasedLimitFormula(Constants constants) {
			this.constants = constants;
		}
		public double GetZScore(double measurement) {
			throw new NotImplementedException();
		}
		public bool ZScoreable() => false;
		public static AgeBasedLimitFormula TAPSE(PatientData pd, string name) {
			double[] limits = { 0.68, 0.85, 1.01, 1.13, 1.25, 1.36, 1.48, 1.56, 1.6, 1.62, 1.64, 1.67, 1.73, 1.79, 1.83, 1.84, 1.85, 1.87, 1.93, 1.98, 2.04, 2.05 };
			double[] ages = { 0, 0.0833, 0.3333, 0.5833, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
			return new AgeBasedLimitFormula(new Constants(limits, ages, pd, name, new[] { "Reduced", "Normal" }, " ({0} cm), lower limit of normal range {1} cm. ", 2));
		}
		public static AgeBasedLimitFormula PulmArtAccelTime(PatientData pd, string name) {
			double[] flimits = { 54.5, 56.6, 58.1, 60.3, 64, 69.2, 74.5, 78.9, 82.8, 86.3, 89.5, 92.5, 95.3, 98, 100.6, 103.1, 105.4, 107.8, 110, 112.2, 114.3, 116.3 };
			double[] mlimits = { 52.5, 54.6, 56, 58.3, 61.9, 67.2, 72.5, 76.9, 80.8, 84.2, 87.5, 90.5, 93.3, 96, 98.6, 101, 103.4, 105.7, 108, 110.1, 112.2, 114.3 };

			double[] ages = { 0, 0.0833, 0.1666, 0.3333, 0.5833, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };

			double[] limits = pd.PatientSex.Value.ToLower().Contains("f") ? flimits : mlimits;

			return new AgeBasedLimitFormula(new Constants(limits, ages, pd, name, new[] { "Reduced", "Normal" }, "({0} ms), lower limit of normal range {1} ms. ", 0));
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
			public int Rounding {
				get; set;
			}
			public string AnomalyTemplate {
				get; set;
			}
			public double[] Limits {
				get; private set;
			}
			public double[] Ages {
				get; private set;
			}
			public PatientData PD {
				get; private set;
			}
			public Constants(double[] limits, double[] ages, PatientData pd, string name, string[] anomalies, string template, int rounding, bool prefix = true) {
				AnomalyPrefix = prefix;
				AnomalyTemplate = template;
				MeasurementName = name;
				Anomalies = anomalies;
				PD = pd;
				Limits = limits;
				Ages = ages;
				Rounding = rounding;
			}
		}
	}
}
