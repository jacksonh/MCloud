

using System;
using System.Collections.Generic;

using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;


namespace MCloud.EC2 {

	public class EC2Driver : NodeDriver {

		public EC2Driver (string key, string secret) : base (key, secret)
		{
			Client = AWSClientFactory.CreateAmazonEC2Client (Key, Secret);
		}

		public AmazonEC2 Client {
			get;
			private set;
		}

		public override NodeProvider Provider {
			get { return NodeProvider.EC2; }
		}

		public override NodeOptions DefaultOptions {
			get { return new EC2NodeOptions (); }
		}

		public override Node CreateNode (string name, NodeSize size, NodeImage image, NodeLocation location, NodeAuth auth, NodeOptions options)
		{
			EC2NodeOptions ops = options as EC2NodeOptions;
			if (ops == null && options != null)
				throw new Exception ("Only EC2NodeOptions can be used as NodeOptions for creating EC2 Nodes.");
			else if (ops == null)
				ops = new EC2NodeOptions ();

			RunInstancesRequest request = new RunInstancesRequest () {
				InstanceType = size.Id,
				ImageId = image.Id,
				MinCount = 1,
				MaxCount = 1,
				KeyName = auth.UserName,
			};
			RunInstancesResponse response = Client.RunInstances (request);

			foreach (var i in response.RunInstancesResult.Reservation.RunningInstance) {
				return EC2Node.FromRunningInstance (i, this);
			}

			return null;
		}

		
		public override void UpdateNode (Node node)
		{
			List<Node> nodes = ListNodes (node.Id);

			if (nodes.Count < 1)
				throw new Exception ("Unable to update node. The node no longer exists.");

			Node n = nodes [0];

			node.Name = n.Name;
			node.State = n.State;
			node.PublicIPs = n.PublicIPs;
			node.PrivateIPs = n.PrivateIPs;
		}

		public override void DestroyNode (Node node)
		{
			TerminateInstancesRequest request = new TerminateInstancesRequest () { InstanceId = new List<string> () { node.Id }};
			TerminateInstancesResponse response = Client.TerminateInstances (request);
		}

		public override void RebootNode (Node node)
		{
			RebootInstancesRequest request = new RebootInstancesRequest () { InstanceId = new List<string> () { node.Id }};
			RebootInstancesResponse response = Client.RebootInstances (request);
		}

		public override List<Node> ListNodes ()
		{
			return ListNodes (null);
		}

		private List<Node> ListNodes (string id)
		{
			DescribeInstancesRequest request = new DescribeInstancesRequest ();

			if (id != null)
				request.InstanceId.Add (id);

			DescribeInstancesResponse response = Client.DescribeInstances (request);

			List<Node> res = new List<Node> ();
			foreach (var r in response.DescribeInstancesResult.Reservation) {
				foreach (var i in r.RunningInstance) {
					res.Add (EC2Node.FromRunningInstance (i, this));
				}
			}
			
			return res;
		}

		public override List<NodeImage> ListImages ()
		{
			DescribeImagesRequest request = new DescribeImagesRequest ();
			DescribeImagesResponse response = Client.DescribeImages (request);

			List<NodeImage> res = new List<NodeImage> ();
			foreach (var i in response.DescribeImagesResult.Image) {
				Console.WriteLine ("location:   {0}", i.ImageLocation);
				res.Add (new NodeImage (i.ImageId, i.ImageLocation, this));
			}

			return res;
		}

		public override List<NodeImage> ListImages (NodeLocation location)
		{
			return ListImages ();
		}

		public override List<NodeSize> ListSizes ()
		{
			return EC2NodeSize.List (this, "us-east-1");
		}

		public override List<NodeSize> ListSizes (NodeLocation location)
		{
			return EC2NodeSize.List (this, location.Name);
		}

		public override List<NodeLocation> ListLocations ()
		{
			DescribeRegionsRequest request = new DescribeRegionsRequest ();
			DescribeRegionsResponse response = Client.DescribeRegions (request);

			List<NodeLocation> res = new List<NodeLocation> ();
			foreach (var r in response.DescribeRegionsResult.Region) {
				res.Add (new NodeLocation (r.Endpoint, r.RegionName, String.Empty, this));
			}

			return res;
		}
	}

}

