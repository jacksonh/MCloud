

using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace MCloud.Linode {

	internal class LinodeResponse {

		public LinodeResponse ()
		{
		}

		public string ACTION {
			get;
			set;
		}

		public Dictionary<string,string> [] DATA {
			get;
			set;
		}

		public LinodeError [] ERRORARRAY {
			get;
			set;
		}

		public static LinodeResponse FromJson (string js)
		{
			Console.WriteLine ("RESPONSE");
			Console.WriteLine (js);
			Console.WriteLine ();

			JavaScriptSerializer jss = new JavaScriptSerializer ();
			return jss.Deserialize<LinodeResponse> (js);
		}
	}
}


