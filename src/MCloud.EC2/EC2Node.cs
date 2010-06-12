

using System;
using System.Net;
using System.Collections.Generic;

using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;


namespace MCloud.EC2 {

	public class EC2Node : Node {

		public EC2Node (string id, string name, NodeState state, List<IPAddress> public_ips,
				List<IPAddress> private_ips, NodeDriver driver) : base (id, name, state, public_ips, private_ips, driver)
		{
		}

		internal static EC2Node FromRunningInstance (RunningInstance r, EC2Driver driver)
		{
			List<IPAddress> public_ips = new List<IPAddress> ();
			List<IPAddress> private_ips = new List<IPAddress> ();

			IPAddress ip;
			if (IPAddress.TryParse (r.IpAddress, out ip))
				public_ips.Add (ip);

			if (IPAddress.TryParse (r.PrivateIpAddress, out ip))
				private_ips.Add (ip);

			EC2Node node = new EC2Node (r.InstanceId, r.InstanceId, ToNodeState (r.InstanceState), public_ips, private_ips, driver);
			return node;
		}

		private static NodeState ToNodeState (InstanceState state)
		{
			switch ((int) state.Code) {
			case 0:
				return NodeState.Pending;
			case 16:
				return NodeState.Running;
			case 32:
			case 48:
			case 64:
			case 80:
				return NodeState.Terminated;
			default:
				return NodeState.Unknown;
			}
		}
	}
}

