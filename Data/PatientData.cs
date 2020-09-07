using Dicom;

namespace DICOMReporting.Data {
	public struct PatientData {
		public string StudyID;
		public string StudyDate;
		public string PatientID;
		public string PatientName;
		public string PatientSex;
		public string PatientAge;
		public string PatientWeight;
		public string PatientSize;

		public PatientData(DicomDataset ds) {
			StudyID = ds.GetSingleValue<string>(DicomTag.StudyInstanceUID);

			StudyDate = ds.GetSingleValue<string>(DicomTag.StudyDate);

			PatientID = ds.GetSingleValue<string>(DicomTag.PatientID);

			PatientName = ds.GetSingleValue<string>(DicomTag.PatientName);

			PatientSex = ds.GetSingleValue<string>(DicomTag.PatientSex);

			PatientAge = ds.GetSingleValue<string>(DicomTag.PatientAge) + "ays";

			PatientWeight = ds.GetSingleValue<string>(DicomTag.PatientWeight) + " kg";

			PatientSize = ds.GetSingleValue<string>(DicomTag.PatientSize) + " cm";
		}

		public PatientData(string studyID, string studyDate, string patientID, string patientName, string patientSex, string patientAge, string patientWeight, string patientSize) {
			StudyID = studyID;
			StudyDate = studyDate;
			PatientID = patientID;
			PatientName = patientName;
			PatientSex = patientSex;
			PatientAge = patientAge;
			PatientWeight = patientWeight;
			PatientSize = patientSize;
		}
	}
}
