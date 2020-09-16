using System;

namespace DICOMReporting.Data {
	public class StructuredReport {
		private PatientData patientData;

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
	}
}
