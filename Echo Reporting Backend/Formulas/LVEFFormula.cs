using DICOMReporting.Formulas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo_Reporting_Backend.Formulas {
	public class LVEFFormula : IFormula {
		private Constants constants;
		public string ReportAnomaly(double measurement) {
			int bracket;
			if (measurement < 0.5) {
				bracket = 0;
			}
			else if (measurement < 0.55) {
				bracket = 1;
			}
			else {
				bracket = 2;
			}

			if (constants.AnomalyPrefix && !constants.Anomalies[bracket].Equals("")) {
				return constants.Anomalies[bracket] + " " + constants.MeasurementName;
			}
			else {
				return constants.Anomalies[bracket];
			}
		}
		public double GetZScore(double observed_y) {
			throw new NotImplementedException();
		}
		public bool ZScoreable() => false;
		public static LVEFFormula LVBiplaneEF() {
			return new LVEFFormula(new Constants("left ventricular biplane EF", new[] { "Reduced", "Borderline", "Normal" }));
		}
		public static LVEFFormula LV4ChamberEF() {
			return new LVEFFormula(new Constants("left ventricular 4 chamber EF", new[] { "Reduced", "Borderline", "Normal" }));
		}
		private LVEFFormula(Constants constants) {
			this.constants = constants;
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
			public Constants(string name, string[] anomalies, bool prefix = true) {
				AnomalyPrefix = prefix;
				MeasurementName = name;
				Anomalies = anomalies;
			}
		}
	}
}
