using System;
using System.Collections.Generic;
using System.Text;
using UnitsNet;
using UnitsNet.Units;

namespace DICOMReporting.Data.Units {
	public static class HeaderFactory {
		public static IMeasurementHeader Parse(string name, double value, string unitname, string unitshorthand) {
			IQuantity quantity;
			string parsestring = string.Format("{0} {1}", value, unitshorthand);
			var e = SupportedUnitsHelpers.SupportedUnits.GetEnumerator();
			while (e.MoveNext()) {
				if (Quantity.TryParse(e.Current, parsestring, out quantity)) {
					return new UnitHeaderAdapter(name, quantity);
				} 
			}
			return new MeasurementHeader(name, value, unitname, unitshorthand);

		}
	}
}
