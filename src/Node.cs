
using System;
using System.Net;
using System.Text;
using System.Collections.Generic;

using MCloud.Deploy;

namespace MCloud {

	/// <summary>
	///  An abstract representation of a node in the cloud. Typically you would obtain an instance
	///  of a Node by calling driver.ListNodes () or by using driver.CreateNode ().
	/// </summary>
	public class Node : Entity {

		internal Node (string id, string name, NodeState state,	List<IPAddress> public_ips,
				List<IPAddress> private_ips, NodeDriver driver) : base (id, name, driver)
		{
			State = state;
			PublicIPs = public_ips;
			PrivateIPs = private_ips;
		}

		/// <summary>
		/// The state of the node. NOTE: This property is set when the node instance is first obtained, either by
		/// creating the node or calling ListNodes. You can call the Update method to update this value.
		/// </summary>
		public NodeState State {
			get;
			internal set;
		}

		/// <summary>
		/// A list of the Node's public IP addresses. All nodes are guaranteed to have at list one public address.
		/// NOTE: This property is set when the node instance is first obtained, either by
		/// creating the node or calling ListNodes. You can call the Update method to update this value.
		/// </summary>
		public List<IPAddress> PublicIPs {
			get;
			internal set;
		}

		/// <summary>
		/// A list of the Node's private IP addresses.  A node is not guaranteed to have any private addresses.
		/// NOTE: This property is set when the node instance is first obtained, either by
		/// creating the node or calling ListNodes. You can call the Update method to update this value.
		/// </summary>
		public List<IPAddress> PrivateIPs {
			get;
			internal set;
		}

		/// <summary>
		/// A globally unique ID.  This ID is unique across all your nodes, regardless of the driver.
		/// </summary>
		public string UUID {
			get {
				return String.Format ("{0}:{1}", Id, Driver.Provider);
			}
		}

		/// <summary>
		/// Gets updated information for the node. This function will update the node's
		/// State, Public and Private IP addresses and the node's name.
		/// </summary>
		public void Update ()
		{
			Driver.UpdateNode (this);
		}

		/// <summary>
		/// Issue a reboot command.
		/// </summary>
		public void Reboot ()
		{
			Driver.RebootNode (this);
		}

		/// <summary>
		/// Issue a destroy node command.  This will request that the cloud provider
		/// terminates and destroys the node.
		/// </summary>
		public void Destroy ()
		{
			Driver.DestroyNode (this);
		}

		/// <summary>
		/// Run the supplied deployment on the node.  This is a syncrhonous operation and will block until
		/// the node deployment is completed.
		/// </summary>
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

