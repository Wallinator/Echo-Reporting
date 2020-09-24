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
			ZScoreable = formula != null && formula.ZScoreable();
			Empty = empty;
		}
		public Result(IMeasurementHeader header) {
			Name = header.Name;
			UnitShorthand = header.UnitShorthand;
			Value = header.Value;
			Formula = null;
			ZScoreable = false;
			Empty = false;
		}

		public double ZScore => Formula.GetZScore(Value);

		public override string ToString() {
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
	}
}
