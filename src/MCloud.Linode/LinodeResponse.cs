
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MCloud.Linode {

	internal class LinodeResponse {

		
		public LinodeResponse ()
		{
		}

		public string Action {
			get;
			set;
		}

		public JObject [] Data {
			get;
			set;
		}

		public LinodeError [] Errors {
			get;
			set;
		}

		public static LinodeResponse FromJson (string json)
		{
			JObject obj = JObject.Parse (json);
			LinodeResponse response = new LinodeResponse ();

			response.Action = (string) obj ["ACTION"];

			List<LinodeError> errors = new List<LinodeError> ();
			foreach (JObject error in obj ["ERRORARRAY"]) {
				errors.Add (new LinodeError ((string) error ["ERRORMESSAGE"], (int) error ["ERRORCODE"]));
			}
			response.Errors = errors.ToArray ();

			List<JObject> datas = new List<JObject> ();
			JArray data = obj ["DATA"] as JArray;
			if (data != null) {
				foreach (JObject dobj in data) {
					datas.Add (dobj);
				}
			} else
				datas.Add ((JObject) obj ["DATA"]);
			response.Data = datas.ToArray ();

			return response;
		}
	}
}


