using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Dicom;

namespace DICOMReporting.DicomFileReading {
	public class DicomManager {
		public static void test() {
			DicomFile file = DicomFile.Open("../../../test materials/STUDYX0W");

			var walker = new DicomDatasetWalker(file.Dataset);

			//GetFiles();
			walker.Walk(new Walker());

			/*var en = file.Dataset.GetEnumerator();
			while (en.MoveNext()) {
				if (!en.Current.ValueRepresentation.IsMultiValue) {
					System.Diagnostics.Debug.WriteLine(en.Current.Tag.DictionaryEntry.Name);
				}
				//System.Diagnostics.Debug.WriteLine(en.Current.ValueRepresentation.IsMultiValue);
			}*/
		}
		public static void GetFiles() {
			string directory = "../../../test materials";
			var dirinfo = new DirectoryInfo(directory);
			List<DicomFile> filenames = new List<DicomFile>();
			var fileenum = dirinfo.EnumerateFiles("*", new EnumerationOptions() { RecurseSubdirectories = true, IgnoreInaccessible = true }).GetEnumerator();
			while (fileenum.MoveNext()) {
				DicomFile file;
				try {
					file = DicomFile.Open(fileenum.Current.FullName);
				}
				catch (Exception) {
					continue;
				}
				if (file.Dataset.InternalTransferSyntax.Equals(DicomTransferSyntax.ImplicitVRLittleEndian)) {
					filenames.Add(file);
					System.Diagnostics.Debug.WriteLine(fileenum.Current.Name);
				}
			}
		}
	}
}
