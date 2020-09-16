using DICOMReporting.Data.Units;

namespace DICOMReporting.Data {
	public class MeasurementHeader : IMeasurementHeader {
		public MeasurementHeader()  {
		}

		public MeasurementHeader(string name, double value, string unitName, string unitShorthand) {
			Name = name;
			Value = value;
			UnitName = unitName;
			UnitShorthand = unitShorthand;
		}

		public double Value {
			get;
			set;
		}
		public string Name {
			get;
			set;
		}

		public string UnitName {
			get;
			set;
		}

		public string UnitShorthand {
			get;
			set;
		}
		public override string ToString() {
			return ((IMeasurementHeader) this).AsString();
		}


	}
}
