using DICOMReporting.Data.Measurements;
using DICOMReporting.Formulas;
using System;
using System.Collections.Generic;

namespace DICOMReporting.Data {
	public class StructuredReport {
		private PatientData patientData;
		public Dictionary<string, Result> Results;

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
		public void something(Dictionary<string, List<MeasurementGroup>> findings, Dictionary<string, List<MeasurementSpecification>> specsbysite) {
			foreach (var sitename in specsbysite.Keys) {
				dosomething(findings[sitename], specsbysite[sitename]);
			}
		}

		private void dosomething(List<MeasurementGroup> groups, List<MeasurementSpecification> specs) {
			foreach (var spec in specs) {
				spec.FindAndRemoveFromGroups(groups);
			}
		}
	}
}
