namespace DICOMReporting.Data.Measurements {
	public interface IMeasurementHeader {
	
		string Name {
			get;
		}
		double Value {
			get;
		}
		string UnitName {
			get;
		}
		string UnitShorthand {
			get;
		}
	}
}