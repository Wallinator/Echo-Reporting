using DICOMReporting.Data.Measurements;
using DICOMReporting.Data.Measurements.Units;
using DICOMReporting.Formulas;
using System;
using System.Collections.Generic;
using System.Text;

namespace DICOMReporting.Data {
	public class Result {
		public string Name;
		public string UnitShorthand;
		public double Value;
		public IFormula Formula;
		public bool ZScoreable;
		public bool Empty;


		public Result(string name, string unitShorthand, IFormula formula = null, bool empty = true, double value = 0) {
			Name = name;
			UnitShorthand = unitShorthand;
			Value = value;
			Formula = formula;
			ZScoreable = formula != null;
			Empty = empty;
		}

		public double ZScore => Formula.GetZScore(Value);

		public override string ToString() {
			string ZSCORE = "";
			if (ZScoreable) {
				ZSCORE = " Z Score: " + ZScore;
			}
			return Name + ": " + Value + " " + UnitShorthand + ZSCORE;
		}
	}
}
