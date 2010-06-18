

using System;
using System.Collections.Generic;

namespace MCloud {

	/// <summary>
	/// A node driver is used to interact with a cloud provider. Each cloud provider
	/// will have its own node driver such as the NC2NodeDriver.
	/// </summary>
	public abstract class NodeDriver {

		public NodeDriver (string key, string secret)
		{
			Key = key;
			Secret = secret;
		}

		public string Key {
			get;
			private set;
		}

		public string Secret {
			get;
			private set;
		}

		/// <summary>
		/// The type of node this driver interacts with, such as EC2 or Linode
		/// </summary>
		public abstract NodeProvider Provider {
			get;
		}

		public abstract NodeOptions DefaultOptions {
			get;
		}

		/// <summary>
		/// Create a new node using the default options for this node driver type.
		/// </summary>
		public Node CreateNode (string name, NodeSize size, NodeImage image, NodeLocation location, NodeAuth auth)
		{
			return CreateNode (name, size, image, location, auth, DefaultOptions);
		}

		public abstract Node CreateNode (string name, NodeSize size, NodeImage image, NodeLocation location, NodeAuth auth, NodeOptions options);

		/// <summary>
		/// Get updated information on the specified node from the cloud provider.
		/// You would use this method to check to see if a node has changed its state.
		/// </summary>
		public abstract void UpdateNode (Node node);

		/// <summary>
		/// Destroy the node, once a node is destroyed it is no longer usable and can not be recreated.
		/// </summary>
		public abstract void DestroyNode (Node node);

		/// <summary>
		/// Reboot the node.
		/// </summary>
		public abstract void RebootNode (Node node);

		/// <summary>
		/// Get a list of nodes that exist on the cloud provider.
		/// </summary>
		public abstract List<Node> ListNodes ();

		/// <summary>
		/// A list of images available from the cloud provider
		/// </summary>
		public abstract List<NodeImage> ListImages ();

		/// <summary>
		/// A list of images available from the cloud provider at a specific location.
		/// </summary>
		public abstract List<NodeImage> ListImages (NodeLocation location);

		/// <summary>
		/// A list of sizes available from the cloud provider.
		/// </summary>
		public abstract List<NodeSize> ListSizes ();

		/// <summary>
		/// A list of sizes available from the cloud provider, at a specific location.
		/// </summary>
		public abstract List<NodeSize> ListSizes (NodeLocation location);

		/// <summary>
		/// A list of all the data center locations from this cloud provider
		/// </summary>
		public abstract List<NodeLocation> ListLocations ();
		
	}
}

