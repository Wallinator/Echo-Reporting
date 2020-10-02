using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DICOMReporting.Data.Results {
	public class MultipleChoiceResult : StringResult {
		public string Postfix;
		public bool MultiSelect;
		public List<string> Options;
		public MultipleChoiceResult(string name, List<string> options, string postfix = "", bool multiselect = false) : base(name, options[0]) {
			MultiSelect = multiselect;
			Postfix = postfix;
			Options = options;
		}
	}
}
