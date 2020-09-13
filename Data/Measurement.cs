using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DICOMReporting.Data {
	public class Measurement {
		public MeasurementHeader Header;
		public Dictionary<string, string> Properties = new Dictionary<string, string>();

		public Measurement() {
		}

		public Measurement(MeasurementHeader measurement) {
			Header = measurement;
		}

		public void AddProperty(string name, string value) {
			Properties[name] = value;
		}

		public void PrintDebug() {
			Debug.WriteLine("\t" + Header);
			foreach (var name in Properties.Keys) {
				Debug.WriteLine("\t" + "\t" + name + ": " + Properties[name]);
			}
		}

		public bool CompareProperties(Dictionary<string, string> properties) {
			return Properties.Count == properties.Count && !Properties.Except(properties).Any();
		}
	}
}
