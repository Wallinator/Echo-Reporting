using DICOMReporting.Data.Measurements;
using DICOMReporting.Formulas;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DICOMReporting.Data {
	public class StructuredReport {
		private PatientData patientData;
		public Dictionary<string, Result> Results = new Dictionary<string, Result>();

		public PatientData PatientData {
			get => patientData;

			set {
				patientData = value;
				BSA = CalculateBSA(value.PatientSize.Value, value.PatientWeight.Value);
			}
		}
		public double BSA {
			get; private set;
		}

		public StructuredReport(PatientData patientData) {
			PatientData = patientData;
		}
		private static double CalculateBSA(double height, double weight) {
			//height in cm
			//weight in kg
			return 0.024265 * Math.Pow(height, 0.3964) * Math.Pow(weight, 0.5378);
		}
		public void ResultsFromFindings(Dictionary<string, List<MeasurementGroup>> findings) {
			Dictionary<string, List<MeasurementSpecification>> specsbysite = MeasurementSpecification.SpecsBySite(BSA, patientData.PatientAge.Value);
			foreach (var sitename in specsbysite.Keys) {
				ResultsFromGroups(findings[sitename], specsbysite[sitename]);
			}
		}

		private void ResultsFromGroups(List<MeasurementGroup> groups, List<MeasurementSpecification> specs) {
			foreach (var spec in specs) {
				var r = spec.FindAndRemoveFromGroups(groups);
				Results.Add(r.Name, r);
				Debug.WriteLine(r);
			}
		}
	}
}
