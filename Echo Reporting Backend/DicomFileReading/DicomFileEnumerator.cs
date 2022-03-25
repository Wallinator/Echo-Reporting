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
				DicomFile file = TryOpen(fileEnumerator.Current.FullName);
				if (file != null) {
					Current = file;
					return true;
				}
				else {
					continue;
				}
			}
			return false;
		}

		public void Reset() {
			fileEnumerator.Reset();
			Current = null;
		}
		public static DicomFile TryOpen(string filename) {
			DicomFile file;
			try {
				file = DicomFile.Open(filename);
			}
			catch (Exception) {
				return null;
			}
			if (IsValidDicom(file)) {
				return file;
			}
			else {
				return null;
			}
		}
		private static bool IsValidDicom(DicomFile file) {
			return file.Dataset.InternalTransferSyntax.Equals(DicomTransferSyntax.ExplicitVRLittleEndian);
		}

		private void Init() {
			fileEnumerator = dirinfo.EnumerateFiles("*", SearchOption.AllDirectories).GetEnumerator();
		}
	}
}
