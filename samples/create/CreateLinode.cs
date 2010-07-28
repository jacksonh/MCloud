
using System;
using System.IO;
using System.Linq;

using MCloud;
using MCloud.Linode;
using MCloud.Deploy;


namespace samples
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			///
			/// To run this sample you must have a 
			/// LINODE-KEY.txt file with your Linode API
			/// key in it.  See the getting started guide for 
			/// info on how to get a key.
			/// 
			/// Note that this sample will create a new node
			/// on your account and you will be billed at least
			/// 1 day for that node.  You can manually destroy the
			/// node when you are done at linode.com or you can 
			/// make a call to node.Destroy.
			/// 
			
			if (!File.Exists ("LINODE-KEY.txt"))
				throw new Exception ("You must have a LINODE-KEY.txt file to run this sample.");
			
			var key = File.ReadAllText ("LINODE-KEY.txt").Trim ();
			if (key.Length < 40)
				throw new Exception ("Your LINODE-KEY.txt file must contain a Linode API key.");
			
			var driver = new LinodeDriver (key);
			
			// Find a location in the USA
			var location = driver.ListLocations ().FirstOrDefault (l => l.Country == "US");
			
			// Pick the cheapest plan possible
			var size = driver.ListSizes ().OrderBy (s => s.Price).FirstOrDefault ();
			
			// Grab any OpenSuse image.
			var image = driver.ListImages ().FirstOrDefault (i => i.Name.Contains ("OpenSUSE"));
			
			Console.WriteLine ("location: {0}  size:  {1}  image:  {2}", location, size, image);
		
			// Create the new with a random password
			string password = System.Web.Security.Membership.GeneratePassword (10, 3);
			NodeAuth auth = new NodeAuth (NodeAuthType.Password, password);
			Node n = driver.CreateNode ("my new node", size, image, location, auth);
			
			Console.WriteLine ("created new node located at {0} with password {1}", n.PublicIPs [0], password);


			var deployment = new MultiStepDeployment () {
				// Create a file on the node
				new RunCommand ("touch /root/test"),
				// Upload a file to the node
				new PutFile ("CreateLinode.exe"),
			};
			n.Deploy (deployment, auth);
			
			Console.WriteLine ("Your node has been deployed.");
		}
	}
}
