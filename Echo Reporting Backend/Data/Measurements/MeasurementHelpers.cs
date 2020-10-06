using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DICOMReporting.Data.Measurements {
	public static class MeasurementHelpers {

		public static bool CompareProperties(Dictionary<string, string> properties, MeasurementGroup group) {
			Dictionary<string, string> groupproperties = group.Properties;
			Dictionary<string, string> temp_props = new Dictionary<string, string>(properties);
			RemoveMean(temp_props);

			return CompareProperties(temp_props, groupproperties);
		}
		public static bool CompareProperties(Dictionary<string, string> p1, Dictionary<string, string> p2) {
			return p1.Count == p2.Count && !p1.Except(p2).Any();
		}
		public static void RemoveMean(Dictionary<string, string> p) {
			if (p.TryGetValue("Derivation", out string val) && val.Equals("Mean")) {
				p.Remove("Derivation");
			}
			if (p.TryGetValue("Selection Status", out val) && val.Equals("Mean value chosen")) {
				p.Remove("Selection Status");
			}
		}
	}
}
