namespace DICOMReporting.Data.Units {
	public interface IMeasurementHeader {
	
		public string Name {
			get;
		}
		public double Value {
			get;
		}
		public string UnitName {
			get;
		}
		public string UnitShorthand {
			get;
		}
		public string AsString() {
			return Name + ": " + Value + " " + UnitShorthand;
		}
	}
}