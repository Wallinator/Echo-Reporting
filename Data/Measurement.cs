using DICOMReporting.Data.Units;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DICOMReporting.Data {
	public class Measurement {
		public IMeasurementHeader Header;
		public Dictionary<string, string> Properties = new Dictionary<string, string>();

		public Measurement() {
		}

		public Measurement(IMeasurementHeader header) {
			Header = header;
		}

		public void AddProperty(string name, string value) {
			Properties[name] = value;
		}

		public void PrintDebug() {
			Debug.WriteLine("\t" + Header.ToString());
			foreach (var name in Properties.Keys) {
				Debug.WriteLine("\t" + "\t" + name + ": " + Properties[name]);
			}
		}

		public bool CompareProperties(Dictionary<string, string> properties) {
			return Properties.Count == properties.Count && !Properties.Except(properties).Any();
		}
	}
}
