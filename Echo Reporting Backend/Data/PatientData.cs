using Dicom;
using Dicom.StructuredReport;
using DICOMReporting.Data.Measurements;
using DICOMReporting.Data.Measurements.Units;
using DICOMReporting.Data.Results;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using UnitsNet;
using UnitsNet.Units;

namespace DICOMReporting.Data {
	public class PatientData {
		//public string StudyID;
		//public string StudyDate;
		public StringResult PatientID;
		public StringResult PatientName;
		public StringResult PatientSex;
		//public DateTime PatientDOB;
		public Result PatientAge;
		public Result PatientWeight;
		public Result PatientSize;
		public Result SystolicBloodPressure;
		public Result DiastolicBloodPressure;

		public double BSA => 
			//height in cm
			//weight in kg
			0.024265 * Math.Pow(PatientSize.Value, 0.3964) * Math.Pow(PatientWeight.Value, 0.5378);

		public PatientData(DicomContentItem patientcontainer) : this() {
			foreach (var child in patientcontainer.Children()) {

				DicomMeasuredValue measurementsequence;
				IMeasurementHeader temp;
				//Debug.WriteLine(child.Code + ": " + child.Get<string>());
				switch (child.Code.Value) {
					case "121033":
						measurementsequence = child.Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
						try {
							temp = HeaderFactory.Parse(child.Code.Meaning, (double) measurementsequence.Value, measurementsequence.Code.Meaning, measurementsequence.Code.Value);
							SupportedUnitsHelpers.Convert(temp, DurationUnit.Year365);
						}
						catch (Exception) {
							temp = new UnitHeaderAdapter(child.Code.Meaning, new Duration((double) measurementsequence.Value, DurationUnit.Year365));
						}
						Debug.WriteLine(temp);
						PatientAge = new Result(temp);
						break;
					case "121032":
						PatientSex = new StringResult("Patient Sex", child.Dataset.GetCodeItem(DicomTag.ConceptCodeSequence).Meaning);
						Debug.WriteLine(PatientSex);
						break;
					case "8302-2":
						measurementsequence = child.Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
						temp = HeaderFactory.Parse(child.Code.Meaning, (double) measurementsequence.Value, measurementsequence.Code.Meaning, measurementsequence.Code.Value);
						SupportedUnitsHelpers.Convert(temp, LengthUnit.Centimeter);
						Debug.WriteLine(temp);
						PatientSize = new Result(temp);
						break;
					case "29463-7":
						measurementsequence = child.Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
						temp = HeaderFactory.Parse(child.Code.Meaning, (double) measurementsequence.Value, measurementsequence.Code.Meaning, measurementsequence.Code.Value);
						SupportedUnitsHelpers.Convert(temp, MassUnit.Kilogram);
						Debug.WriteLine(temp);
						PatientWeight = new Result(temp);
						break;
					case "F-008EC":
						measurementsequence = child.Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
						temp = HeaderFactory.Parse(child.Code.Meaning, (double) measurementsequence.Value, measurementsequence.Code.Meaning, measurementsequence.Code.Value);
						Debug.WriteLine(temp);
						SystolicBloodPressure = new Result(temp);
						break;
					case "F-008ED":
						measurementsequence = child.Dataset.GetMeasuredValue(DicomTag.MeasuredValueSequence);
						temp = HeaderFactory.Parse(child.Code.Meaning, (double) measurementsequence.Value, measurementsequence.Code.Meaning, measurementsequence.Code.Value);
						Debug.WriteLine(temp);
						DiastolicBloodPressure = new Result(temp);
						break;
					case "121029":
						PatientName = new StringResult("Patient Name", child.Get<string>());
						Debug.WriteLine(PatientName);
						break;
					case "121030":
						PatientID = new StringResult("Patient ID", child.Get<string>());
						Debug.WriteLine(PatientID);
						break;
					case "121031":
						string input = child.Get<string>();

						var numbers = Regex.Split(input, @"\D+").ToList().GetRange(1, 3).Select(x => int.Parse(x)).ToArray();

						//PatientDOB = new DateTime(numbers[0], numbers[1], numbers[2]);
						//Debug.WriteLine(PatientDOB);
						break;
					default:
						break;
				}
			}
		}
		public PatientData() {

			IMeasurementHeader temp;

			temp = new UnitHeaderAdapter("Patient Age", new Duration(0, DurationUnit.Year365));
			PatientAge = new Result(temp, true);

			PatientSex = new StringResult("Patient Sex");

			temp = new UnitHeaderAdapter("Patient Size", new Length(0, LengthUnit.Centimeter));
			PatientSize = new Result(temp, true);

			temp = new UnitHeaderAdapter("Patient Size", new Mass(0, MassUnit.Kilogram));
			PatientWeight = new Result(temp, true);

			temp = new MeasurementHeader("Systolic Blood Pressure", 0, "", "mm[Hg]");
			SystolicBloodPressure = new Result(temp, true);

			temp = new MeasurementHeader("Diastolic Blood Pressure", 0, "", "mm[Hg]");
			DiastolicBloodPressure = new Result(temp, true);


			PatientName = new StringResult("Patient Name");

			PatientID = new StringResult("Patient ID");


		}
	}
}
