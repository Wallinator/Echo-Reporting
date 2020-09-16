using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Transactions;
using UnitsNet;
using UnitsNet.Units;

namespace DICOMReporting.Data.Units {
	public class UnitHeaderAdapter : IMeasurementHeader {
		private IQuantity _quantity;
		public UnitHeaderAdapter(string name, IQuantity quantity) {
			Name = name;
			_quantity = quantity;
			UnitShorthand = SupportedUnitsHelpers.GetUnitShortHand(quantity);
		}

		public string Name { get; set; }

		public double Value => _quantity.Value;

		public string UnitName => _quantity.Unit.ToString();

		public string UnitShorthand {
			get;
			set;
		}

		
		public override string ToString() {
			return ((IMeasurementHeader) this).AsString();
		}

	}
}
