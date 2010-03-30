
using System;
using System.Collections.Generic;

namespace MCloud {

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

		public bool DeployNode ()
		{
			return true;
		}

		public abstract NodeProvider Provider {
			get;
		}

		public abstract Node CreateNode (string name, NodeSize size, NodeImage image, NodeLocation location);

		public abstract bool DestroyNode (Node node);
		public abstract bool RebootNode (Node node);
		public abstract List<Node> ListNodes ();
		public abstract List<NodeImage> ListImages ();
		public abstract List<NodeImage> ListImages (NodeLocation location);
		public abstract List<NodeSize> ListSizes ();
		public abstract List<NodeSize> ListSizes (NodeLocation location);
		public abstract List<NodeLocation> ListLocations ();
		
	}
}

