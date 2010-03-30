
using System;
using System.Net;
using System.Collections.Generic;

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
			foreach (var node in response.DATA) {
				nodes.Add (LinodeNode.FromData (node, driver));
			}

			return nodes;
		}

		public bool RebootNode (Node node)
		{
			LinodeRequest request = new LinodeRequest ("linode.boot", new Dictionary<string,object> {{"LINODEID", node.Id}});
			LinodeJobResponse response = ExecuteJob (request);

			return true;
		}

		public static NodeState StateFromStatus (string status)
		{
			switch (status) {
			case "-2": return NodeState.Unknown;
			case "-1": return NodeState.Pending;
			case "0": return NodeState.Pending;
			case "1": return NodeState.Running;
			case "2": return NodeState.Rebooting;
			case "3": return NodeState.Rebooting;
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

		internal LinodeJobResponse ExecuteJob (LinodeRequest request)
		{
			string url = String.Concat (base_url, request.UrlParams ());

			string data = webclient.DownloadString (url);
			return LinodeJobResponse.FromJson (data);
		}

		internal void IPsForNode (string id, List<IPAddress> public_ips, List<IPAddress> private_ips)
		{
			LinodeRequest request = new LinodeRequest ("linode.ip.list", new Dictionary<string,object> () {{"LINODEID", id}});
			LinodeResponse response = Execute (request);
			var ip_data = response.DATA;

			foreach (var ip in ip_data) {
				if (ip.ContainsKey ("ISPUBLIC"))
					public_ips.Add (IPAddress.Parse (ip ["IPADDRESS"]));
				else
					private_ips.Add (IPAddress.Parse (ip ["IPADDRESS"]));
			}
		}

		private string GenerateBaseURL ()
		{
			return String.Concat (ApiEndpoint, "?api_key=", driver.Key);
		}
	}
}

