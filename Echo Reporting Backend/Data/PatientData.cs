﻿using Dicom;
using Dicom.StructuredReport;
using DICOMReporting.Data.Measurements;
using DICOMReporting.Data.Measurements.Units;
using DICOMReporting.Data.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using UnitsNet;
using UnitsNet.Units;

namespace DICOMReporting.Data {
	public class PatientData {
		//public string StudyDate;
		//public StringResult StudyID;
		public StringResult PatientID;
		public StringResult PatientName;
		public StringResult PatientDOB;
		public StringResult StudyDate;
		public StringResult PatientSex;
		public StringResult ReasonForStudy;
		public StringResult ReferringPhysician;
		public MultipleChoiceResult EchoType;
		public MultipleChoiceResult ReportingDoctor;
		public Result PatientAge;
		public Result PatientWeight;
		public Result PatientHeight;
		public Result SystolicBloodPressure;
		public Result DiastolicBloodPressure;
		public Result BSA;


		public PatientData(DicomContentItem patientcontainer) : this() {
			foreach (var child in patientcontainer.Children()) {

				DicomMeasuredValue measurementsequence;
				IMeasurementHeader temp;
				//Debug.WriteLine(child.Code + ": " + child.Get<string>());
				switch (child.Code.Value) {
					case "121033":
						measurementsequence = child.Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
						try {
							temp = HeaderFactory.Parse("Age", (double) measurementsequence.Value, measurementsequence.Code.Meaning, measurementsequence.Code.Value);
							SupportedUnitsHelpers.Convert(temp, DurationUnit.Year365);
						}
						catch (Exception) {
							temp = new UnitHeaderAdapter("Age", new Duration((double) measurementsequence.Value, DurationUnit.Year365));
						}
						Debug.WriteLine(temp);
						PatientAge = new Result(temp);
						break;
					case "121032":
						PatientSex = new StringResult("Sex", child.Dataset.GetCodeItem(DicomTag.ConceptCodeSequence).Meaning);
						Debug.WriteLine(PatientSex);
						break;
					case "8302-2":
						measurementsequence = child.Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
						temp = HeaderFactory.Parse("Height", (double) measurementsequence.Value, measurementsequence.Code.Meaning, measurementsequence.Code.Value);
						SupportedUnitsHelpers.Convert(temp, LengthUnit.Centimeter);
						Debug.WriteLine(temp);
						PatientHeight = new Result(temp);
						break;
					case "29463-7":
						measurementsequence = child.Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
						temp = HeaderFactory.Parse("Weight", (double) measurementsequence.Value, measurementsequence.Code.Meaning, measurementsequence.Code.Value);
						SupportedUnitsHelpers.Convert(temp, MassUnit.Kilogram);
						Debug.WriteLine(temp);
						PatientWeight = new Result(temp);
						break;
					case "F-008EC":
						measurementsequence = child.Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
						temp = HeaderFactory.Parse(child.Code.Meaning, (double) measurementsequence.Value, measurementsequence.Code.Meaning, "mmHg");
						Debug.WriteLine(temp);
						SystolicBloodPressure = new Result(temp);
						break;
					case "F-008ED":
						measurementsequence = child.Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
						temp = HeaderFactory.Parse(child.Code.Meaning, (double) measurementsequence.Value, measurementsequence.Code.Meaning, "mmHg");
						Debug.WriteLine(temp);
						DiastolicBloodPressure = new Result(temp);
						break;
					case "121029":
						PatientName = new StringResult("Patient Name", FormatName(child.Get<string>()));
						PatientName.Value = PatientName.Value.Split(' ').Reverse().Aggregate((x, y) => { return (x + " " + y).Trim(); });
						Debug.WriteLine(PatientName);
						break;
					case "121030":
						PatientID = new StringResult("Patient ID", child.Get<string>());
						Debug.WriteLine(PatientID);
						break;
					case "T9910-08":
						ReferringPhysician = new StringResult("Referring Physician", child.Get<string>());
						Debug.WriteLine(ReferringPhysician);
						break;
					case "T9910-04":
						ReasonForStudy = new StringResult("Reason For Study", child.Get<string>());
						Debug.WriteLine(ReasonForStudy);
						break;
					case "121031":
						string input = child.Get<string>();
						Debug.WriteLine("dob input: " + input);

						PatientDOB = new StringResult("DOB", FormatDate(input));

						Debug.WriteLine(PatientDOB.Value);
						break;
					case "T9910-09":
						string dateinput = child.Get<string>();
						Debug.WriteLine("exam date input: " + dateinput);

						StudyDate = new StringResult("Study Date", FormatDate(dateinput));

						Debug.WriteLine(StudyDate.Value);
						break;
					default:
						break;
				}
			}
			UpdateBSAResult();
			UpdateAgeFromDOB();
		}
		public void UpdateBSAResult() {
			BSA = new Result("Body Surface Area", "m2", value: 0.024265 * Math.Pow(PatientHeight.Value, 0.3964) * Math.Pow(PatientWeight.Value, 0.5378), empty: false);
			if(PatientHeight.Empty || PatientWeight.Empty) {
				BSA.Empty = true;
			}
		}
		public void UpdateAgeFromDOB() {
			if(!string.IsNullOrEmpty(PatientDOB.Value) && !string.IsNullOrEmpty(StudyDate.Value) && (PatientAge.Empty || PatientAge.Value == 0)) {
				PatientAge.Value = (DateTime.Parse(StudyDate.Value) - DateTime.Parse(PatientDOB.Value)).Duration().TotalDays / 365;
				PatientAge.Empty = false;
			}
		}
		public PatientData() {

			IMeasurementHeader temp;

			temp = new UnitHeaderAdapter("Age", new Duration(0, DurationUnit.Year365));
			PatientAge = new Result(temp, true);

			PatientSex = new StringResult("Sex");

			temp = new UnitHeaderAdapter("Height", new Length(0, LengthUnit.Centimeter));
			PatientHeight = new Result(temp, true);

			temp = new UnitHeaderAdapter("Weight", new Mass(0, MassUnit.Kilogram));
			PatientWeight = new Result(temp, true);

			temp = new MeasurementHeader("Systolic Blood Pressure", 0, "", "mmHg");
			SystolicBloodPressure = new Result(temp, true);

			temp = new MeasurementHeader("Diastolic Blood Pressure", 0, "", "mmHg");
			DiastolicBloodPressure = new Result(temp, true);

			PatientName = new StringResult("Patient Name");
			
			PatientID = new StringResult("Patient ID");

			PatientDOB = new StringResult("DOB");

			StudyDate = new StringResult("Study Date");

			ReasonForStudy = new StringResult("Reason For Study");

			//ReferringPhysician = new StringResult("Referring Physician");

			EchoType = new MultipleChoiceResult("Echo Type", new List<string>() { "Transthoracic echo" });

			ReportingDoctor = new MultipleChoiceResult("Reporting Doctor", new List<string>() { "Dr Paul Brooks", "Dr Hannah Bourne" });

			ReferringPhysician = new StringResult("Referring Physician");

			UpdateBSAResult();
		}
		private string FormatName(string name) {
			//return name.Replace('^', ' ').Trim();
			var list = name.Split('^');
			return string.Format("{0} {1}", list[1], list[0]);
		}
		private string FormatDate(string dateinput) {
			//var numbers2 = Regex.Split(dateinput, @"\D+").ToList().GetRange(1, 3).Select(x => int.Parse(x)).ToArray();
			var year = int.Parse(dateinput.Substring(0, 4));
			var month = int.Parse(dateinput.Substring(4, 2));
			var day = int.Parse(dateinput.Substring(6, 2));

			return new DateTime(year, month, day).Date.ToShortDateString();
		}
		public void GetPatientDataFromDataset(DicomDataset ds) {
			string value;
			if(ds.TryGetSingleValue<string>(DicomTag.PatientName, out value)) {
				PatientName = new StringResult("Patient Name", FormatName(value));
			}

			if(ds.TryGetSingleValue<string>(DicomTag.PatientID, out value)) {
				PatientID = new StringResult("Patient ID", value);
			}

			if(ds.TryGetSingleValue<string>(DicomTag.StudyDate, out value)) {
				StudyDate = new StringResult("Study Date", FormatDate(value));
			}

			if(ds.TryGetSingleValue<string>(DicomTag.PatientBirthDate, out value)) {
				PatientDOB = new StringResult("DOB", FormatDate(value));
			}

			if(ds.TryGetSingleValue<string>(DicomTag.ReferringPhysicianName, out value)) {
				ReferringPhysician = new StringResult("Referring Physician", value);
			}

			UpdateAgeFromDOB();
		}
		
	}
}
