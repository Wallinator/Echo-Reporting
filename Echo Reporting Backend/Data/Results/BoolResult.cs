using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DICOMReporting.Data.Results {
	public class BoolResult {
		public string Name {
			get;
			set;
		}
		public string TrueText {
			get;
			set;
		}
		public string FalseText {
			get;
			set;
		}
		public string OptionName {
			get;
			set;
		}
		public bool Value {
			get;
			set;
		}
		public BoolResult(string name, string optionName = "", bool value = false, string falseText = "", string trueText = "") {
			Name = name;
			OptionName = optionName;
			Value = value;
			FalseText = falseText;
			if (trueText.Length == 0) {
				TrueText = name;
			}
			else {
				TrueText = trueText;
			}
		}
		public string AsString() {
			return Name;
		}
	}
}
