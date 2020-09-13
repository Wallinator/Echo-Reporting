namespace DICOMReporting.Data {
	public struct MeasurementUnit {
		public string Name;
		public string Shorthand;

		public MeasurementUnit(string name = "", string shorthand = "") {
			Name = name;
			Shorthand = shorthand;
		}
	}
}
