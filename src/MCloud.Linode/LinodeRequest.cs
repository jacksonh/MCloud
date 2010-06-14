
using System;
using System.Web;
using System.Text;
using System.Collections.Generic;

namespace MCloud.Linode {

	internal class LinodeRequest {

		public LinodeRequest (string action) : this (action, new Dictionary<string,object> ())
		{
		}

		public LinodeRequest (string action, Dictionary<string,object> parameters)
		{
			Action = action;
			Parameters = parameters;
		}

		public string Action {
			get;
			private set;
		}

		public Dictionary<string,object> Parameters {
			get;
			private set;
		}

		public string UrlParams ()
		{
			StringBuilder builder = new StringBuilder ();

			builder.Append ("&api_action=");
			builder.Append (Action);

			if (Parameters != null) {
				foreach (string key in Parameters.Keys) {
					string value = Parameters [key].ToString ();
					builder.Append ("&");
					builder.Append (HttpUtility.UrlEncode (key));
					builder.Append ("=");
					builder.Append (HttpUtility.UrlEncode (value));
				}
			}

			return builder.ToString ();
		}
	}
}


