

using System;
using System.Collections.Generic;

namespace MCloud.Linode {

	public class LinodeDriver : NodeDriver {

		public LinodeDriver (string key, string secret) : base (key, secret)
		{
			API = new LinodeAPI (this);
		}

		public LinodeAPI API {
			get;
			private set;
		}

		public override NodeProvider Provider {
			get { return NodeProvider.Linode; }
		}

		public override Node CreateNode (string name, NodeSize size, NodeImage image, NodeLocation location)
		{
			return null;
		}

		public override bool DestroyNode (Node node)
		{
			return true;
		}

		public override bool RebootNode (Node node)
		{
			return API.RebootNode (node);
		}

		public override List<Node> ListNodes ()
		{
			return API.ListNodes ();
		}

		public override List<NodeImage> ListImages ()
		{
			return API.ListImages (null);
		}

		public override List<NodeImage> ListImages (NodeLocation location)
		{
			return API.ListImages (location);
		}

		public override List<NodeSize> ListSizes ()
		{
			return API.ListSizes (null);
		}

		public override List<NodeSize> ListSizes (NodeLocation location)
		{
			return API.ListSizes (location);
		}

		public override List<NodeLocation> ListLocations ()
		{
			return API.ListLocations ();
		}
	}

}

