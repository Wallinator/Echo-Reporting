using System;
using System.Collections.Generic;
using System.Text;
using UnitsNet;
using UnitsNet.Units;

namespace DICOMReporting.Data.Units {
	public static class SupportedUnitsHelpers {
		public static IEnumerable<Type> SupportedUnits = new List<Type> { typeof(Length), typeof(Speed), typeof(Mass), typeof(Duration) };
		public static string GetUnitShortHand(IQuantity quant) {
			if (quant.Type == QuantityType.Length) {
				return UnitAbbreviationsCache.Default.GetDefaultAbbreviation((LengthUnit) quant.Unit);
			}
			else if (quant.Type == QuantityType.Speed) {
				return UnitAbbreviationsCache.Default.GetDefaultAbbreviation((SpeedUnit) quant.Unit);
			}
			else if (quant.Type == QuantityType.Mass) {
				return UnitAbbreviationsCache.Default.GetDefaultAbbreviation((MassUnit) quant.Unit);
			}
			else if (quant.Type == QuantityType.Duration) {
				return UnitAbbreviationsCache.Default.GetDefaultAbbreviation((DurationUnit) quant.Unit);
			}
			else {
				throw new UnitNotFoundException();
			}
		}
	}
}
