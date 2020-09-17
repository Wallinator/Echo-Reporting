using DICOMReporting.Data.Measurements;
using DICOMReporting.Data.Measurements.Units;
using DICOMReporting.Formulas;
using System;
using System.Collections.Generic;
using System.Text;
using UnitsNet.Units;

namespace DICOMReporting.Data.Measurements {
	public class MeasurementSpecification {
		public string Name;
		public string RawMeasurementName;
		public Dictionary<string, string> Properties;
		public string DefaultUnitShorthand;
		public bool IncludeImageMode;
		public Enum UnitEnum;
		public IFormula Formula;

		public MeasurementSpecification(string name, string rawMeasurementName, Dictionary<string, string> properties, string defaultUnitShorthand, IFormula formula = null, bool includeImageMode = false, Enum unitEnum = null) {
			Name = name;
			RawMeasurementName = rawMeasurementName;
			Properties = properties;
			DefaultUnitShorthand = defaultUnitShorthand;
			Formula = formula;
			IncludeImageMode = includeImageMode;
			UnitEnum = unitEnum;
		}

		public Result FindAndRemoveFromGroups(List<MeasurementGroup> groups) {
			if (IncludeImageMode) {
				Properties.Add("Image Mode", "M Mode");
			}

			var groupIndex = groups.FindIndex(g => g.Name.Equals(RawMeasurementName) && MeasurementHelpers.CompareProperties(Properties, g));
			if (groupIndex > -1) {
				var group = groups[groupIndex];
				groups.RemoveAt(groupIndex);
				return MeasurementToResult(group.SelectMean());
			}

			else {
				if (IncludeImageMode) {
					Properties["Image Mode"] = "2D Mode";
					IncludeImageMode = false;
					return FindAndRemoveFromGroups(groups);
				}
				return MeasurementToResult(null);
			}
		}
		private Result MeasurementToResult(Measurement measurement) {
			if (measurement == null) {
				return new Result(Name, DefaultUnitShorthand, Formula);
			}
			if (UnitEnum != null) {
				SupportedUnitsHelpers.Convert(measurement.Header, UnitEnum);
			}
			return new Result(Name, measurement.Header.UnitShorthand, Formula, false, measurement.Header.Value);

		}


		public static Dictionary<string, List<MeasurementSpecification>> SpecsBySite(double BSA, double AgeInYears) {
			var x = new MeasurementSpecification("", "", null, unitEnum: null);
			return null;
		}
	}

}
