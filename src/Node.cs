
using System;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace MCloud {

	public class Node : Entity {

		public Node (string id, string name, NodeState state, List<IPAddress> public_ips,
				List<IPAddress> private_ips, NodeDriver driver) : base (id, name, driver)
		{
			State = state;
			PublicIPs = public_ips;
			PrivateIPs = private_ips;
		}

		public NodeState State {
			get;
			internal set;
		}

		public List<IPAddress> PublicIPs {
			get;
			internal set;
		}

		public List<IPAddress> PrivateIPs {
			get;
			internal set;
		}

		public string UUID {
			get {
				return String.Format ("{0}:{1}", Id, Driver.Provider);
			}
		}

		public void Update ()
		{
			Driver.UpdateNode (this);
		}

		public bool Reboot ()
		{
			return Driver.RebootNode (this);
		}

		public bool Destroy ()
		{
			return Driver.DestroyNode (this);
		}

		public void Deploy (Deployment d, NodeAuth auth)
		{
			d.Run (this, auth);
		}

		public override string ToString ()
		{
			return String.Format ("Node uuid=\"{0}\" name=\"{1}\" state=\"{2}\" " +
					"public_ips=\"{3}\" private_ips=\"{4}\" driver=\"{5}\"",
					UUID, Name, State, IPsToString (PublicIPs), IPsToString (PrivateIPs), Driver.Provider);
		}

		private static string IPsToString (List<IPAddress> list)
		{
			StringBuilder builder = new StringBuilder ();

			foreach (IPAddress adr in list) {
				builder.Append (adr.ToString ());
				builder.Append (", ");
			}

			return builder.ToString ();
		}
	}
}

