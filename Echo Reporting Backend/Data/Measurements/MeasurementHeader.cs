
namespace DICOMReporting.Data.Measurements {
	public class MeasurementHeader : IMeasurementHeader {
		public MeasurementHeader()  {
		}

		public MeasurementHeader(string name, double value, string unitName, string unitShorthand) {
			Name = name;
			Value = value;
			UnitName = unitName;
			UnitShorthand = unitShorthand;
		}
		public MeasurementHeader(IMeasurementHeader header) {
			Name = header.Name;
			Value = header.Value;
			UnitName = header.UnitName;
			UnitShorthand = header.UnitShorthand;
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
			return Name + ": " + Value + " " + UnitShorthand;
		}
	}
}
