using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DICOMReporting.Data {
	public static class MeasurementHelpers {

		public static bool CompareProperties(Measurement measurement, MeasurementGroup group) {
			Dictionary<string, string> properties = group.Properties;
			Dictionary<string, string> temp_props = new Dictionary<string, string>(measurement.Properties);
			RemoveMean(temp_props);

			return CompareProperties(temp_props, properties);
		}
		public static bool CompareProperties(Dictionary<string, string> p1, Dictionary<string, string> p2) {
			return p1.Count == p2.Count && !p1.Except(p2).Any();
		}
		public static void RemoveMean(Dictionary<string, string> p) {
			if (p.TryGetValue("Derivation", out string val) && val.Equals("Mean")) {
				p.Remove("Derivation");
			}
		}
	}
}
