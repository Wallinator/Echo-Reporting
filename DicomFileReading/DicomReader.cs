using Dicom;
using Dicom.StructuredReport;
using DICOMReporting.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DICOMReporting.DicomFileReading {
	public class DicomReader {
		public static void test() {
			string directory = "../../../test materials";


			var e = new DicomFileEnumerator(new DirectoryInfo(directory));
			if (e.MoveNext()) {
				GetStructuredReport(e.Current.Dataset);
			}
		}

		public static void GetStructuredReport(DicomDataset ds) {
			var sr = new DicomStructuredReport(ds);
			PatientData pd;
			var findings = new Dictionary<string, List<Measurement>>();
			foreach (var group in sr.Children()) {
				if (group.Type == DicomValueType.Container && group.Code.Value.Equals("121070")) {
					HandleFinding(findings, group);
				}
				else if (group.Type == DicomValueType.Container && group.Code.Value.Equals("121118")) {
					pd = new PatientData(group);
				}
			}
		}

		private static void HandleFinding(Dictionary<string, List<Measurement>> findings, DicomContentItem finding) {
			string sitename = "";
			foreach (var site in finding.Children()) {
				if (site.Type != DicomValueType.Container) {
					sitename = site.Dataset.GetCodeItem(DicomTag.ConceptCodeSequence).Meaning;
					Debug.WriteLine(sitename);
				}
				else {
					var measurements = new List<Measurement>();
					foreach (var rawmeasurement in site.Children()) {
						Measurement measurement = GetMeasurementFromRaw(rawmeasurement);
						measurements.Add(measurement);
						//measurement.PrintDebug();
					}
					if (findings.ContainsKey(sitename)) {
						findings[sitename].AddRange(measurements);
					}
					else {
						findings.Add(sitename, measurements);
					}
				}
			}
		}

		private static Measurement GetMeasurementFromRaw(DicomContentItem raw) {
			DicomMeasuredValue measurementsequence = raw.Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
			MeasurementHeader m = new MeasurementHeader(raw.Code.Meaning, measurementsequence.Value, measurementsequence.Code.Meaning, measurementsequence.Code.Value);

			Measurement measurement = new Measurement(m);

			foreach (var child in raw.Children()) {
				string name;
				string value;

				if (child.Relationship == DicomRelationship.HasConceptModifier) {
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
