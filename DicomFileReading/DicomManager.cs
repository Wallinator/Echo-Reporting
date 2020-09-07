using Dicom;
using DICOMReporting.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DICOMReporting.DicomFileReading {
	public class DicomManager {
		public static void test() {
			string directory = "../../../test materials";


			var e = new DicomFileEnumerator(new DirectoryInfo(directory));
			if (e.MoveNext()) {
				new StructuredReport(e.Current.Dataset);
				var list = GetMeasurements(e.Current.Dataset);
				foreach (MeasurementHeader x in list) {
					Debug.WriteLine(x);
				}
			}
			//walker.Walk(new Walker());

			/*var en = file.Dataset.GetEnumerator();
			while (en.MoveNext()) {
				if (!en.Current.ValueRepresentation.IsMultiValue) {
					System.Diagnostics.Debug.WriteLine(en.Current.Tag.DictionaryEntry.Name);
				}
				//System.Diagnostics.Debug.WriteLine(en.Current.ValueRepresentation.IsMultiValue);
			}*/
		}
		public static List<MeasurementHeader> GetMeasurements(DicomDataset dataset) {
			List<MeasurementHeader> list = new List<MeasurementHeader>();
			;
			DicomDatasetWalker walker = new DicomDatasetWalker(dataset);
			//walker.Walk(new SRWalker());
			return list;
		}
	}
}
