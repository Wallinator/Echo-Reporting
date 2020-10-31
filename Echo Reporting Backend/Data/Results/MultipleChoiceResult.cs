using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DICOMReporting.Data.Results {
	public class MultipleChoiceResult : StringResult {
		public bool MultiSelect;
		public string DisplayName;
		public List<string> Options;
		public MultipleChoiceResult(string name, List<string> options, string postfix = "", bool multiselect = false, string displayname = "") : base(name, options[0], postfix) {
			DisplayName = displayname;
			MultiSelect = multiselect;
			Options = options;
		}
	}
}
