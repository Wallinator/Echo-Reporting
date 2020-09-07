using System.Collections.Generic;

namespace DICOMReporting.Data {
	public class MeasurementGroup {
		public List<Measurement> Measurements = new List<Measurement>();
		public Dictionary<string, string> Properties = new Dictionary<string, string>();

		public MeasurementGroup(Dictionary<string, string> properties) {
			Properties = properties;
		}

		public MeasurementGroup(Measurement measurement) {
			Properties = measurement.Properties;
			Measurements.Add(measurement);
		}

		public static List<MeasurementGroup> GroupMeasurements(List<Measurement> measurements) {
			List<MeasurementGroup> groups = new List<MeasurementGroup>();
			bool assigned;
			foreach (var measurement in measurements) {
				assigned = false;
				foreach (var group in groups) {
					if (measurement.CompareProperties(group.Properties)) {
						group.Measurements.Add(measurement);
						assigned = true;
						break;
					}
				}

				if (!assigned) {
					groups.Add(new MeasurementGroup(measurement));
				}
			}
			return groups;
		}
	}
}
