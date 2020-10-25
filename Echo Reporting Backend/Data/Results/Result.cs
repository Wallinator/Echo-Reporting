using DICOMReporting.Data.Measurements;
using DICOMReporting.Data.Measurements.Units;
using DICOMReporting.Formulas;
using System;
using System.Collections.Generic;
using System.Text;

namespace DICOMReporting.Data.Results {
	public class Result {
		public string Name;
		public string UnitShorthand;
		public double Value;
		public IFormula Formula;
		public bool ZScoreable;
		public bool HasComment;
		public bool Empty;

		public Result(string name, string unitShorthand, IFormula formula = null, bool empty = true, double value = 0) {
			Name = name;
			UnitShorthand = unitShorthand;
			Value = value;
			Formula = formula;
			ZScoreable = formula != null && formula.ZScoreable();
			HasComment = formula != null;
			Empty = empty;
		}
		public Result(IMeasurementHeader header, bool empty = false) {
			Name = header.Name;
			UnitShorthand = header.UnitShorthand;
			Value = header.Value;
			Formula = null;
			ZScoreable = false;
			Empty = empty;
		}

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
		public string ReportString() {
			string name;
			string Zscorestring = "";
			if (HasComment) {
				if (AnomalyText.Equals("")) {
					return "";
				}
				name = AnomalyText;
			}
			else {
				name = Name;
			}
			if (ZScoreable) {
				Zscorestring = ", Z-score=" + ZScore.ToString("N2");
			}
			return name + " (" + Value + " " + UnitShorthand + Zscorestring + ")";
		}
	}
}
