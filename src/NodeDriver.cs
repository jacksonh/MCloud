
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

		public abstract NodeProvider Provider {
			get;
		}

		public abstract NodeOptions DefaultOptions {
			get;
		}

		public abstract Node CreateNode (string name, NodeSize size, NodeImage image, NodeLocation location, NodeAuth auth, NodeOptions options);

		public abstract void UpdateNode (Node node);
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

