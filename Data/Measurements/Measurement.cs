using DICOMReporting.Data.Measurements.Units;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DICOMReporting.Data.Measurements {
	public class Measurement {
		public IMeasurementHeader Header;
		public Dictionary<string, string> Properties = new Dictionary<string, string>();

		public Measurement(IMeasurementHeader header) {
			Header = header;
		}

		public void AddProperty(string name, string value) {
			Properties.Add(name, value);
		}

		public void PrintDebug() {
			Debug.WriteLine("\t" + Header.ToString());
			foreach (var name in Properties.Keys) {
				Debug.WriteLine("\t" + "\t" + name + ": " + Properties[name]);
			}
		}

	}
}
