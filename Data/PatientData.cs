using Dicom;
using Dicom.StructuredReport;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace DICOMReporting.Data {
	public class PatientData {
		public string StudyID;
		public string StudyDate;
		public string PatientID;
		public string PatientName;
		public string PatientSex;
		public DateTime PatientDOB;
		public MeasurementHeader PatientAge;
		public MeasurementHeader PatientWeight;
		public MeasurementHeader PatientSize;
		public MeasurementHeader SystolicBloodPressure;
		public MeasurementHeader DiastolicBloodPressure;

		public PatientData(DicomContentItem patientcontainer) {
			foreach (var child in patientcontainer.Children()) {

				DicomMeasuredValue measurementsequence;
				//Debug.WriteLine(child.Code + ": " + child.Get<string>());
				switch (child.Code.Value) {
					case "121033":
						measurementsequence = child.Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
						PatientAge = new MeasurementHeader(child.Code.Meaning, (double) measurementsequence.Value, measurementsequence.Code.Meaning, measurementsequence.Code.Value);
						Debug.WriteLine(PatientAge);
						break;
					case "121032":
						PatientSex = child.Dataset.GetCodeItem(DicomTag.ConceptCodeSequence).Meaning;
						Debug.WriteLine(PatientSex);
						break;
					case "8302-2":
						measurementsequence = child.Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
						PatientSize = new MeasurementHeader(child.Code.Meaning, (double) measurementsequence.Value, measurementsequence.Code.Meaning, measurementsequence.Code.Value);
						Debug.WriteLine(PatientSize);
						break;
					case "29463-7":
						measurementsequence = child.Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
						PatientWeight = new MeasurementHeader(child.Code.Meaning, (double) measurementsequence.Value, measurementsequence.Code.Meaning, measurementsequence.Code.Value);
						Debug.WriteLine(PatientWeight);
						break;
					case "F-008EC":
						measurementsequence = child.Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
						SystolicBloodPressure = new MeasurementHeader(child.Code.Meaning, (double) measurementsequence.Value, measurementsequence.Code.Meaning, measurementsequence.Code.Value);
						Debug.WriteLine(SystolicBloodPressure);
						break;
					case "F-008ED":
						measurementsequence = child.Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
						DiastolicBloodPressure = new MeasurementHeader(child.Code.Meaning, (double) measurementsequence.Value, measurementsequence.Code.Meaning, measurementsequence.Code.Value);
						Debug.WriteLine(DiastolicBloodPressure);
						break;
					case "121029":
						PatientName = child.Get<string>();
						Debug.WriteLine(PatientName);
						break;
					case "121030":
						PatientID = child.Get<string>();
						Debug.WriteLine(PatientID);
						break;
					case "121031":
						string input = child.Get<string>();
						// Split on one or more non-digit characters.
						var numbers = Regex.Split(input, @"\D+").ToList().GetRange(1, 3).Select(x => int.Parse(x)).ToArray();
						//Debug.WriteLine(numbers[0] + " " + numbers[1] + " " + numbers[2]);
						PatientDOB = new DateTime(numbers[0], numbers[1], numbers[2]);
						Debug.WriteLine(PatientDOB);
						break;
					default:
						break;
				}
			}
		}

		public PatientData(string studyID, string studyDate, string patientID, string patientName, string patientSex, DateTime patientDOB, MeasurementHeader patientAge, MeasurementHeader patientWeight, MeasurementHeader patientSize, MeasurementHeader systolicBloodPressure, MeasurementHeader diastolicBloodPressure) {
			StudyID = studyID;
			StudyDate = studyDate;
			PatientID = patientID;
			PatientName = patientName;
			PatientSex = patientSex;
			PatientDOB = patientDOB;
			PatientAge = patientAge;
			PatientWeight = patientWeight;
			PatientSize = patientSize;
			SystolicBloodPressure = systolicBloodPressure;
			DiastolicBloodPressure = diastolicBloodPressure;
		}
	}
}
