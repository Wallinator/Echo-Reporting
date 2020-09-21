﻿using System.Collections.Generic;
using System.Linq;

namespace DICOMReporting.Data.Measurements {
	public class MeasurementGroup {
		public string Name;
		public Dictionary<string, string> Properties;
		public List<Measurement> Measurements = new List<Measurement>();

		public MeasurementGroup(Measurement measurement) {
			Name = measurement.Header.Name;
			Properties = new Dictionary<string, string>(measurement.Properties);
			Measurements.Add(measurement);
			MeasurementHelpers.RemoveMean(Properties);
		}

		public static List<MeasurementGroup> GroupMeasurements(List<Measurement> measurements) {
			List<MeasurementGroup> groups = new List<MeasurementGroup>();
			bool assigned;
			foreach (var measurement in measurements) {
				assigned = false;
				foreach (var group in groups) {
					if (measurement.Header.Name.Equals(group.Name) && MeasurementHelpers.CompareProperties(measurement.Properties, group)) {
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
		public Measurement SelectMean() {
			if (Measurements.Count == 1) {
				return Measurements.First();
			}
			else {
				return Measurements.Find(m => m.Properties.TryGetValue("Derivation", out string val) && val.Equals("Mean"));
			}
		}
		public Measurement SelectNonMean() {
			if (Measurements.Count == 1) {
				return Measurements.First();
			}
			else {
				return Measurements.Find(m => !(m.Properties.TryGetValue("Derivation", out string val) && val.Equals("Mean")));
			}
		}
	}
}