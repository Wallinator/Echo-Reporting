using Dicom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Management;

namespace DICOMReporting.DicomFileReading {
	public class DicomFileEnumerator : IEnumerator<DicomFile> {
		public DicomFile Current {
			get;
			private set;
		}

		object IEnumerator.Current => Current;

		private IEnumerator<FileInfo> fileEnumerator;

		private readonly DirectoryInfo dirinfo;

		public DicomFileEnumerator(DirectoryInfo dirinfo) {
			this.dirinfo = dirinfo;
			Init();
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

		private void Init() {
			fileEnumerator = dirinfo.EnumerateFiles("*", SearchOption.AllDirectories).GetEnumerator();
		}
	}
}
