namespace DICOMReporting.Data {
	public struct MeasurementHeader {
		public string Name;
		public decimal Value;
		public MeasurementUnit Unit;

		public MeasurementHeader(string name, decimal value, MeasurementUnit unit) {
			Name = name;
			Value = value;
			Unit = unit;
		}
		public MeasurementHeader(string name, decimal value, string unitName, string unitShorthand) : this(name, value, new MeasurementUnit(unitName, unitShorthand)) {
		}

		public override string ToString() {
			return Name + ": " + Value + " " + Unit.Shorthand;
		}
	}
}
