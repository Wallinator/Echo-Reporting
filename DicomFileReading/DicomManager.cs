using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Dicom;

namespace DICOMReporting.DicomFileReading {
	public class DicomManager {
		public static void test() {
			string directory = "../../../test materials";


			var e = new DicomFileEnumerator(new DirectoryInfo(directory));
			while (e.MoveNext()) {

				//var walker = new DicomDatasetWalker(e.Current.Dataset);
				System.Diagnostics.Debug.WriteLine(e.Current.File.Name);
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
		
	}
}
