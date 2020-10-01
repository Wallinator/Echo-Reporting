using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DICOMReporting.Data.Results {
	public class StringResult {
		public string Name {
			get;
			set;
		}
		public string Value {
			get;
			set;
		}
		public StringResult(string name, string value = "") {
			Name = name;
			Value = value;
		}
	}
}
