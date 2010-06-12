

using System;
using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MCloud.Linode {

	public class LinodeNode : Node {

		public LinodeNode (string id, string name, NodeState state, List<IPAddress> public_ips,
				List<IPAddress> private_ips, NodeDriver driver) : base (id, name, state, public_ips, private_ips, driver)
		{
		}

		internal static LinodeNode FromData (JObject node_data, LinodeDriver driver)
		{
			List<IPAddress> public_ips = new List<IPAddress> ();
			List<IPAddress> private_ips = new List<IPAddress> ();

			string id = node_data ["LINODEID"].ToString ();

			driver.API.IPsForNode (id, public_ips, private_ips);

			return new LinodeNode (id,
					(string) node_data ["LABEL"],
					LinodeAPI.StateFromStatus ((int) node_data ["STATUS"]),
					public_ips, private_ips, driver);
		}
	}
}


