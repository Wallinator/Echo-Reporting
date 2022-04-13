using Dicom;
using Dicom.StructuredReport;
using DICOMReporting.Data;
using DICOMReporting.Data.Measurements;
using DICOMReporting.Data.Measurements.Units;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DICOMReporting.DicomFileReading {
	public class DicomReader {
		public static StructuredReport GetStructuredReport(DicomDataset ds) {
			var sr = new DicomStructuredReport(ds);
			PatientData pd = new PatientData();
			var findings = new Dictionary<string, List<MeasurementGroup>>();
			foreach (var group in sr.Children()) {
				if (group.Type == DicomValueType.Container && group.Code.Value.Equals("121070")) {
					HandleFinding(findings, group);
				}
				else if (group.Type == DicomValueType.Container && group.Code.Value.Equals("121118")) {
					pd = new PatientData(group);
				}
			}
			var report = new StructuredReport(pd, findings);
			pd.GetPatientDataFromDataset(ds);
			return report;
		}

		private static void HandleFinding(Dictionary<string, List<MeasurementGroup>> findings, DicomContentItem finding) {
			string sitename = "";
			foreach (var site in finding.Children()) {
				if (site.Type != DicomValueType.Container) {
					sitename = site.Dataset.GetCodeItem(DicomTag.ConceptCodeSequence).Meaning;
					//Debug.WriteLine(sitename);
				}
				else {
					var measurements = new List<Measurement>();
					foreach (var rawmeasurement in site.Children()) {
						try {
							Measurement measurement = GetMeasurementFromRaw(rawmeasurement);
							measurements.Add(measurement);
						}
						catch(Exception) {
							Console.WriteLine(rawmeasurement.Code);
						}
						//measurement.PrintDebug();
					}
					if (findings.ContainsKey(sitename)) {
						findings[sitename].AddRange(MeasurementGroup.GroupMeasurements(measurements));
					}
					else {
						findings.Add(sitename, MeasurementGroup.GroupMeasurements(measurements));
					}
				}
			}
		}

		private static Measurement GetMeasurementFromRaw(DicomContentItem raw) {
			DicomMeasuredValue measurementsequence = raw.Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
			IMeasurementHeader m = HeaderFactory.Parse(raw.Code.Meaning, (double) measurementsequence.Value, measurementsequence.Code.Meaning, measurementsequence.Code.Value);

			Measurement measurement = new Measurement(m);

			foreach (var child in raw.Children()) {
				string name;
				string value;

				if (child.Relationship == DicomRelationship.HasConceptModifier || child.Relationship == DicomRelationship.HasProperties) {
					if (!child.Code.Value.Equals("T5203-01")) {
						name = child.Code.Meaning;
						value = child.Dataset.GetDicomItem<DicomSequence>(DicomTag.ConceptCodeSequence).Items[0].GetString(DicomTag.CodeMeaning);
						measurement.AddProperty(name, value);

					}
				}
			}
			return measurement;
		}
	}
}
