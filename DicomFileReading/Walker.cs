using CharLS;
using Dicom;
using Dicom.IO.Buffer;
using Dicom.StructuredReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DICOMReporting.DicomFileReading {
	public class Walker : IDicomDatasetWalker {
		//public string level = "";
		public bool OnBeginFragment(DicomFragmentSequence fragment) {
			//System.Diagnostics.Debug.WriteLine(fragment.Tag.DictionaryEntry.Name);
			return true;		
		}

		public bool OnBeginSequence(DicomSequence sequence) {

			DicomDataset measuredvaluesequence;
			if (sequence.Tag.Equals(DicomTag.ContentSequence)) {
				DicomCodeItem codeitem;
				try {
					codeitem = sequence.Items.First().GetCodeItem(DicomTag.ConceptNameCodeSequence);
				}
				catch (Exception) {
					return true;
				}
				if (codeitem.Value == "121033") {
					measuredvaluesequence = sequence.Items.First().GetMeasuredValue(DicomTag.MeasuredValueSequence);
					System.Diagnostics.Debug.WriteLine(codeitem.Value + "  " + codeitem.Meaning + "  " + measuredvaluesequence.GetString(DicomTag.NumericValue));
				}
			}
			return true;
		}

		public bool OnBeginSequenceItem(DicomDataset dataset) {
			//level += " ";
			return true;
		}

		public void OnBeginWalk() {

		}

		public bool OnElement(DicomElement element) {
			/*
						if (element.ValueRepresentation.IsString) {
							System.Diagnostics.Debug.WriteLine(element.Get<string>());
						}*/
			return true;
		}

		public Task<bool> OnElementAsync(DicomElement element) {
			return Task.Run(() => { return true; });
		}

		public bool OnEndFragment() {
			return true;
		}

		public bool OnEndSequence() {
			//level.Remove(level.Length - 1, 1);
			return true;
		}

		public bool OnEndSequenceItem() {
			//level.Remove(level.Length - 1, 1);
			return true;
		}

		public void OnEndWalk() {
		}

		public bool OnFragmentItem(IByteBuffer item) {
			return true;
		}

		public Task<bool> OnFragmentItemAsync(IByteBuffer item) {
			return Task.Run(() => { return true; });
		}
	}
}
