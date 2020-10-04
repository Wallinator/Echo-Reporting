using DICOMReporting.Data.Measurements;
using DICOMReporting.Data.Results;
using DICOMReporting.Formulas;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DICOMReporting.Data {
	public class StructuredReport {
		public Dictionary<string, Result> Results = new Dictionary<string, Result>();
		public PatientData PatientData {
			get;
			set;
		}
		public ReportingOptions ReportingOptions {
			get;
			set;
		}

		public StructuredReport(PatientData patientData, Dictionary<string, List<MeasurementGroup>> findings, ReportingOptions options) {
			ReportingOptions = options;
			PatientData = patientData;
			ResultsFromFindings(findings);
		}
		public StructuredReport() : this(new PatientData(), new Dictionary<string, List<MeasurementGroup>>(), new ReportingOptions()) {
		}
		public StructuredReport(PatientData patientData) : this(patientData, new Dictionary<string, List<MeasurementGroup>>(), new ReportingOptions()) {
		}
		public StructuredReport(PatientData patientData, Dictionary<string, List<MeasurementGroup>> findings) : this(patientData, findings, new ReportingOptions()) {
		}
		private void ResultsFromFindings(Dictionary<string, List<MeasurementGroup>> findings) {
			Dictionary<string, List<MeasurementSpecification>> specsbysite = SpecificationHelper.SpecsBySite(PatientData);
			foreach (var sitename in specsbysite.Keys) {
				if (findings.ContainsKey(sitename)) {
					ResultsFromGroups(findings[sitename], specsbysite[sitename]);
				}
				else {
					ResultsFromGroups(new List<MeasurementGroup>(), specsbysite[sitename]);
				}
			}
		}

		private void ResultsFromGroups(List<MeasurementGroup> groups, List<MeasurementSpecification> specs) {
			foreach (var spec in specs) {
				Result r = spec.FindAndRemoveFromGroups(groups);
				Results.Add(r.Name, r);
				Debug.WriteLine(r);
			}
		}
	}
}
