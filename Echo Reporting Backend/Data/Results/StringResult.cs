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

		public List<string> TableString() {
			return new List<string>() { Name, Value + " " + Postfix };
		}

		public string AsString() {
			string final = "";
			if (Name.Length != 0) {
				final += Name;
			}
			if (Value.Length != 0) {
				final += " " + Value;
			}
			if (Postfix.Length != 0) {
				final += " " + Postfix;
			}
			return final;
		}
	}
}
