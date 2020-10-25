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
		public string AsString() {
			string final = "";
			if (!Name.Equals("")) {
				final += Name;
			}
			if (!Value.Equals("")) {
				final += " " + Value;
			}
			if (!Postfix.Equals("")) {
				final += " " + Postfix;
			}
			return final;
		}
	}
}
