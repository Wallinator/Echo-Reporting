﻿using DICOMReporting.Data.Measurements;
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

		public StructuredReport(PatientData patientData, Dictionary<string, List<MeasurementGroup>> findings) {
			PatientData = patientData;
			ResultsFromFindings(findings);
		}
		public StructuredReport() : this(new PatientData(), new Dictionary<string, List<MeasurementGroup>>()) { }
		private void ResultsFromFindings(Dictionary<string, List<MeasurementGroup>> findings) {
			Dictionary<string, List<MeasurementSpecification>> specsbysite = MeasurementSpecification.SpecsBySite(PatientData);
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
