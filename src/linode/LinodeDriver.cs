

using System;
using System.Collections.Generic;

namespace MCloud.Linode {

	public class LinodeDriver : NodeDriver {

		public static readonly PaymentTerm DefaultPaymentTerm = PaymentTerm.Monthly;
		public static readonly string Default64BitKernel = "Latest 2.6 Stable (2.6.18.8-linode22)";
		public static readonly string Default32BitKernel = "Latest 2.6 Stable (2.6.18.8-x86_64-linode10)";

		public LinodeDriver (string key, string secret) : base (key, secret)
		{
			API = new LinodeAPI (this);

			PaymentTerm = DefaultPaymentTerm;
			Kernel64Bit = Default64BitKernel;
			Kernel32Bit = Default32BitKernel;
		}

		public LinodeAPI API {
			get;
			private set;
		}

		public PaymentTerm PaymentTerm {
			get;
			private set;
		}

		public bool Prefer64Bit {
			get;
			private set;
		}

		public string Kernel64Bit {
			get;
			private set;
		}

		public string Kernel32Bit {
			get;
			private set;
		}

		public override NodeProvider Provider {
			get { return NodeProvider.Linode; }
		}

		public override Node CreateNode (string name, NodeSize size, NodeImage image, NodeLocation location, NodeAuth auth)
		{
			return API.CreateNode (name, size, image, location, auth);
		}

		public override bool DestroyNode (Node node)
		{
			return API.DestroyNode (node);
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

