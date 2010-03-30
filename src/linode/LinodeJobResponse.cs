

using System;
using System.Collections.Generic;


namespace MCloud.Linode {

	internal class LinodeJobResponse {

		public LinodeJobResponse ()
		{
		}

		public string ACTION {
			get;
			set;
		}

		public Dictionary<string,string> DATA {
			get;
			set;
		}

		public LinodeError [] ERRORARRAY {
			get;
			set;
		}

		public static LinodeJobResponse FromJson (string js)
		{
			/*
			Console.WriteLine ("RESPONSE");
			Console.WriteLine (js);
			Console.WriteLine ();

			JavaScriptSerializer jss = new JavaScriptSerializer ();
			return jss.Deserialize<LinodeJobResponse> (js);
			*/

			return null;
		}
	}
}


