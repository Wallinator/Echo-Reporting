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
		public string OptionName {
			get;
			set;
		}
		public bool Value {
			get;
			set;
		}
		public BoolResult(string name, string optionName = "", bool value = false) {
			Name = name;
			OptionName = optionName;
			Value = value;
		}
	}
}
