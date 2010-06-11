
using System;
using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MCloud.Linode {

	public class LinodeAPI {

		public static readonly string ApiEndpoint = "https://api.linode.com/";

		private LinodeDriver driver;

		private string base_url;
		private WebClient webclient;

		internal LinodeAPI (LinodeDriver driver)
		{
			this.driver = driver;
			this.base_url = GenerateBaseURL ();

			ServicePointManager.CertificatePolicy = new LinodeCertificatePolicy ();
			webclient = new WebClient ();
		}

		public void Echo ()
		{
			LinodeRequest request = new LinodeRequest ("test.echo");
			LinodeResponse response = Execute (request);
		}

		public List<Node> ListNodes ()
		{
			LinodeRequest request = new LinodeRequest ("linode.list");
			LinodeResponse response = Execute (request);
			
			List<Node> nodes = new List<Node> ();
			foreach (var node in response.Data) {
				nodes.Add (LinodeNode.FromData (node, driver));
			}

			return nodes;
		}

		public List<NodeImage> ListImages (NodeLocation location)
		{
			LinodeRequest request = new LinodeRequest ("avail.distributions");
			LinodeResponse response = Execute (request);

			List<NodeImage> images = new List<NodeImage> ();
			foreach (JObject obj in response.Data) {
				string id = obj ["DISTRIBUTIONID"].ToString ();
				string name = (string) obj ["LABEL"];
				
				NodeImage image = new NodeImage (id, name, driver);
				images.Add (image);
			}

			return images;
		}

		public List<NodeSize> ListSizes (NodeLocation location)
		{
			LinodeRequest request = new LinodeRequest ("avail.linodeplans");
			LinodeResponse response = Execute (request);

			List<NodeSize> sizes = new List<NodeSize> ();
			foreach (JObject obj in response.Data) {
				string id = obj ["PLANID"].ToString ();
				string name = (string) obj ["LABEL"];
				int ram = (int) obj ["RAM"];
				int disk = (int) obj ["DISK"] * 1024;
				int bandwidth = (int) obj ["XFER"];
				float price = (float) obj ["PRICE"]; // price in dollars, we use cents

				NodeSize size = new NodeSize (id, name, ram, disk, bandwidth, (int) (price * 100), driver);
				sizes.Add (size);
			}

			return sizes;
		}

		public List<NodeLocation> ListLocations ()
		{
			LinodeRequest request = new LinodeRequest ("avail.datacenters");
			LinodeResponse response = Execute (request);

			List<NodeLocation> locations = new List<NodeLocation> ();
			foreach (JObject obj in response.Data) {
				string id = obj ["DATACENTERID"].ToString ();
				string location = (string) obj ["LOCATION"];
				string country = null;

				if (location.Contains ("USA"))
					country = "US";
				else if (location.Contains ("UK"))
					country = "GB";
				else
					throw new Exception (String.Format ("Can not determine location country: {0}", location));

				NodeLocation loc= new NodeLocation (id, location, country, driver);
				locations.Add (loc);
			}

			return locations;
		}

		public Node CreateNode (string name, NodeSize size, NodeImage image, NodeLocation location, NodeAuth auth, LinodeNodeOptions options)
		{
			int rsize = size.Disk - options.SwapSize;

			string kernel = FindKernel (options);

			LinodeRequest request = new LinodeRequest ("linode.create", new Dictionary<string,object> {
				{"DatacenterID", location.Id}, {"PlanID", size.Id},
				{"PaymentTerm", (int) options.PaymentTerm}});
			LinodeResponse response = Execute (request);

			Console.WriteLine ("DATA:  {0}", response.Data);
			JObject node = response.Data [0];
			string id = node ["LinodeID"].ToString ();

			string root_pass;
			if (auth.Type == NodeAuthType.Password)
				root_pass = auth.Secret;
			else
				root_pass = GenerateRandomPassword ();

			request = new LinodeRequest ("linode.disk.createfromdistribution", new Dictionary<string,object> {
				{"LinodeID", id}, {"DistributionID", image.Id}, {"Label", name}, {"Size", rsize},
				{"rootPass", root_pass}});

			if (auth.Type == NodeAuthType.SSHKey)
				request.Parameters.Add ("rootSSHKey", auth.Secret);

			response = Execute (request);

			JObject distro = response.Data [0];
			Console.WriteLine ("DISTRO:  {0}", response.Data [0]);
			string root_disk = distro ["DiskID"].ToString ();

			request = new LinodeRequest ("linode.disk.create", new Dictionary<string,object> {
				{"LinodeID", id}, {"Label", "Swap"}, {"Type", "swap"}, {"Size", options.SwapSize}});
			response = Execute (request);

			string swap_disk = response.Data [0] ["DiskID"].ToString ();
			string disks = String.Format ("{0},{1},,,,,,,", root_disk, swap_disk);


			request = new LinodeRequest ("linode.config.create", new Dictionary<string,object> {
				{"LinodeID", id}, {"KernelID", kernel}, {"Label", "mcloud config"}, {"DiskList", disks}});
			response = Execute (request);

			string config = response.Data [0]["ConfigID"].ToString ();

			request = new LinodeRequest ("linode.boot", new Dictionary<string,object> {
				{"LinodeID", id}, {"ConfigID", config}});
			response = Execute (request);

			request = new LinodeRequest ("linode.list", new Dictionary<string,object> {{"LinodeID", id}});
			response = Execute (request);

			return LinodeNode.FromData (response.Data [0], driver);
		}

		public bool DestroyNode (Node node)
		{
			LinodeRequest request = new LinodeRequest ("linode.delete", new Dictionary<string,object> {
				{"LinodeID", node.Id}, {"skipChecks", true}});
			LinodeResponse response = Execute (request);

			return true;
		}

		public bool RebootNode (Node node)
		{
			LinodeRequest request = new LinodeRequest ("linode.reboot", new Dictionary<string,object> {{"LINODEID", node.Id}});
			LinodeResponse response = Execute (request);

			return true;
		}

		public static NodeState StateFromStatus (int status)
		{
			switch (status) {
			case -2: return NodeState.Unknown;
			case -1: return NodeState.Pending;
			case 0: return NodeState.Pending;
			case 1: return NodeState.Running;
			case 2: return NodeState.Rebooting;
			case 3: return NodeState.Rebooting;
			default:
				return NodeState.Unknown;
			}
		}

		internal LinodeResponse Execute (LinodeRequest request)
		{
			string url = String.Concat (base_url, request.UrlParams ());

			string data = webclient.DownloadString (url);
			return LinodeResponse.FromJson (data);
		}

		internal void IPsForNode (string id, List<IPAddress> public_ips, List<IPAddress> private_ips)
		{
			LinodeRequest request = new LinodeRequest ("linode.ip.list", new Dictionary<string,object> () {{"LINODEID", id}});
			LinodeResponse response = Execute (request);
			var ip_data = response.Data;

			foreach (var ip in ip_data) {
				if (ip ["ISPUBLIC"] != null)
					public_ips.Add (IPAddress.Parse ((string) ip ["IPADDRESS"]));
				else
					private_ips.Add (IPAddress.Parse ((string) ip ["IPADDRESS"]));
			}
		}

		private string FindKernel (LinodeNodeOptions options)
		{
			LinodeRequest request = new LinodeRequest ("avail.kernels");
			LinodeResponse response = Execute (request);

			foreach (JObject kernel in response.Data) {
				string label = (string) kernel ["LABEL"];
				if (options.Prefer64Bit && label == options.Kernel64Bit)
					return kernel ["KERNELID"].ToString ();
				else if (!options.Prefer64Bit && label == options.Kernel32Bit)
					return kernel ["KERNELID"].ToString ();
			}

			throw new Exception ("Unable to find a suitable Linode kernel");
		}

		private string GenerateBaseURL ()
		{
			return String.Concat (ApiEndpoint, "?api_key=", driver.Key);
		}

		private string GenerateRandomPassword ()
		{
			return System.Web.Security.Membership.GeneratePassword (12, 3);
		}
	}
}

