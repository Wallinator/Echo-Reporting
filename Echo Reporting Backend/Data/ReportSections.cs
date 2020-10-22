using DICOMReporting.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo_Reporting_Backend.Data {
	public class ReportSections {
		public string Situs = "";
		public string SystemicVeins = "";
		public string Atria = "";
		public string AVValves = "";
		public string Ventricles = "";
		public string Outlets = "";
		public string GreatArteries = "";
		public string PulmonaryVeins = "";
		public string CoronaryArteries = "";
		public string Other = "";

		public ReportSections(StructuredReport sr) {
			Situs = sr.ReportingOptions.Situs.ToString();
			SystemicVeins = sr.ReportingOptions.SystemicVeins.ToString();
		}
	}
}
