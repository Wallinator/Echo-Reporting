using DICOMReporting.Data.Measurements;
using DICOMReporting.Data.Measurements.Units;
using DICOMReporting.Formulas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DICOMReporting.Data.Results {
	public class Result {
		public string Name;
		public string UnitShorthand;
		public double Value;
		public IFormula Formula;
		public bool HasComment;
		public bool Empty;
		public string AltName;

		public Result(string name, string unitShorthand, IFormula formula = null, bool empty = true, double value = 0, string altName = "") {
			Name = name;
			UnitShorthand = unitShorthand;
			Value = value;
			Formula = formula;
			HasComment = formula != null;
			Empty = empty;
			AltName = altName;
		}
		public Result(IMeasurementHeader header, bool empty = false) {
			Name = header.Name;
			UnitShorthand = header.UnitShorthand;
			Value = header.Value;
			Formula = null;
			Empty = empty;
			AltName = "";
		}

		public bool ZScoreable => Formula != null && Formula.ZScoreable();
		public double ZScore => Formula.GetZScore(Value);
		public string AnomalyText {
			get {
				if (Formula != null) {
					if (!Empty) {
						return Formula.ReportAnomaly(Value);
					}
				}
				return "";
			}
		}

		public string DebugString() {
			string emptyZscorestring = "";
			if (Empty) {
				emptyZscorestring = "\n\t!! MEASUREMENT NOT FOUND !!";
			}
			else {
				if (ZScoreable) {
					emptyZscorestring = "\n\tZ Score: " + ZScore;
				}
			}
			return Name + ": \n\t" + "Value: " + Value + " " + UnitShorthand + emptyZscorestring;
		}
		public string ReportString(bool includeZScore = true) {
			string name;
			string value;
			string Zscorestring = "";
			if (HasComment) {
				if (AnomalyText.Length == 0) {
					return "";
				}
				name = AnomalyText;
			}
			else {
				name = Name;
			}

			if (UnitShorthand.Equals("mmHg") ||
				UnitShorthand.Equals("cm/s") ||
				UnitShorthand.Equals("m/s")) {
				value = Math.Round(Value, 1).ToString();
			}
			else {
				value = Math.Round(Value, 2).ToString();
			}

			if (ZScoreable && includeZScore) {
				Zscorestring = ", Z-score=" + ZScore.ToString("N2");
			}
			return name + " (" + value + " " + UnitShorthand + Zscorestring + ")";
		}

		public List<string> TableString() {
			string value;
			if (UnitShorthand.Equals("m2")) {
				value = Math.Round(Value, 2).ToString();
			}
			else {
				value = Math.Round(Value, 1).ToString();
			}
			return new List<string>() { Name, value + " " + UnitShorthand };
		}

		public List<string> AsString() {
			string value;
			string zscore;
			int rounding;
			if (UnitShorthand.Equals("mmHg") ||
				UnitShorthand.Equals("cm/s") ||
				UnitShorthand.Equals("m/s")) {
				rounding = 1;
			}
			else {
				rounding = 2;
			}
			value = Math.Round(Value, rounding).ToString();

			if (ZScoreable) {
				zscore = ZScore.ToString("N2");
			}
			else {
				zscore = "";
			}

			return new List<string> {
				Name,
				value,
				UnitShorthand,
				zscore
			};
		}
	}
}
