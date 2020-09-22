using UnitsNet;

namespace DICOMReporting.Data.Measurements.Units {
	public static class HeaderFactory {
		public static IMeasurementHeader Parse(string name, double value, string unitname, string unitshorthand) {
			IQuantity quantity;
			string parsestring = string.Format("{0} {1}", value, unitshorthand);

			foreach (var t in SupportedUnitsHelpers.SupportedUnits) {
				if (Quantity.TryParse(t, parsestring, out quantity)) {
					return new UnitHeaderAdapter(name, quantity);
				} 
			}
			return new MeasurementHeader(name, value, unitname, unitshorthand);

		}
	}
}
