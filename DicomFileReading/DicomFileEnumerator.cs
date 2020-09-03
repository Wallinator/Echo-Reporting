using Dicom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DICOMReporting.DicomFileReading {
	public class DicomFileEnumerator : IEnumerator<DicomFile> {
		public DicomFile Current {
			get;
			private set;
		}

		object IEnumerator.Current => Current;

		private IEnumerator<FileInfo> fileEnumerator;

		private DirectoryInfo dirinfo;

		public DicomFileEnumerator(DirectoryInfo dirinfo) {
			this.dirinfo = dirinfo;
			init();
		}

		public void Dispose() {
			return;
		}

		public bool MoveNext() {
			while (fileEnumerator.MoveNext()) {
				DicomFile file;
				try {
					file = DicomFile.Open(fileEnumerator.Current.FullName);
				}
				catch (Exception) {
					continue;
				}
				if (IsValidDicom(file)) {
					Current = file;
					return true;
				}
			}
			return false;
		}

		public void Reset() {
			fileEnumerator.Reset();
			Current = null;
		}

		private bool IsValidDicom(DicomFile file) {
			return file.Dataset.InternalTransferSyntax.Equals(DicomTransferSyntax.ImplicitVRLittleEndian);
		}

		private void init() {
			fileEnumerator = dirinfo.EnumerateFiles("*", new EnumerationOptions() { RecurseSubdirectories = true, IgnoreInaccessible = true }).GetEnumerator();
		}
	}
}
