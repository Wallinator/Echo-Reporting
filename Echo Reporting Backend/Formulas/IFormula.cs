using System;
using System.Collections.Generic;
using System.Text;

namespace DICOMReporting.Formulas {
	public interface IFormula {
		double GetZScore(double measurement);
		string ReportAnomaly(double ZScore);
		bool ZScoreable();
	}
}
