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
		public string Postfix {
			get;
			set;
		}
		public StringResult(string name, string value = "", string postfix = "") {
			Name = name;
			Value = value;
			Postfix = postfix;
		}
		public override string ToString() {
			return Name + " " + Value + " " + Postfix;
		}
	}
}
