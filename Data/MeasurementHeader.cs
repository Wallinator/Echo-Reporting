namespace DICOMReporting.Data {
	public struct MeasurementHeader {
		public string Name;
		public double Value;
		public MeasurementUnit Unit;

		public MeasurementHeader(string name, double value, MeasurementUnit unit = new MeasurementUnit()) {
			Name = name;
			Value = value;
			Unit = unit;
		}
		public MeasurementHeader(string name, double value, string unitName, string unitShorthand) : this(name, value, new MeasurementUnit(unitName, unitShorthand)) {
		}

		public override string ToString() {
			return Name + ": " + Value + " " + Unit.Shorthand;
		}
	}
}
