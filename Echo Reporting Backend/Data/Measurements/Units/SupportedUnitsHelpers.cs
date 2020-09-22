using System;
using System.Collections.Generic;
using System.Text;
using UnitsNet;
using UnitsNet.Units;

namespace DICOMReporting.Data.Measurements.Units {
	public static class SupportedUnitsHelpers {
		public static List<Type> SupportedUnits = new List<Type> { typeof(Length), typeof(Speed), typeof(Mass), typeof(Duration) };
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
		public static void Convert(IMeasurementHeader measurement, Enum unit) {
			IQuantity quant = MeasurementToQuantity(measurement);
			UnitHeaderAdapter header = measurement as UnitHeaderAdapter;
			if (quant.Type == QuantityType.Length && unit is LengthUnit) {
				header.Quantity = ((Length) quant).ToUnit((LengthUnit) unit);
				header.UnitShorthand = GetUnitShortHand(header.Quantity);
			}
			else if (quant.Type == QuantityType.Speed && unit is SpeedUnit) {
				header.Quantity = ((Speed) quant).ToUnit((SpeedUnit) unit);
				header.UnitShorthand = GetUnitShortHand(header.Quantity);
			}
			else if (quant.Type == QuantityType.Mass && unit is MassUnit) {
				header.Quantity = ((Mass) quant).ToUnit((MassUnit) unit);
				header.UnitShorthand = GetUnitShortHand(header.Quantity);
			}
			else if (quant.Type == QuantityType.Duration && unit is DurationUnit) {
				header.Quantity = ((Duration) quant).ToUnit((DurationUnit) unit);
				header.UnitShorthand = GetUnitShortHand(header.Quantity);
			}
		}
		private static IQuantity MeasurementToQuantity(IMeasurementHeader measurement) {
			return ((UnitHeaderAdapter) measurement).Quantity;
		}
	}
}
