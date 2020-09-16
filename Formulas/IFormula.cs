using System;
using System.Collections.Generic;
using System.Text;

namespace DICOMReporting.Formulas {
	public interface IFormula {
		public double GetZScore(double measurement);
	}
}
