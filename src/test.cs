

using System;
using System.Collections.Generic;


using MCloud;
using MCloud.Linode;


public class T {

	public static void Main (string [] args)
	{
		if (args.Length < 1) {
			Console.WriteLine ("You need to supply your Linode API key.");
			return;
		}
		
		LinodeDriver driver = new LinodeDriver (args [0], null);

		
		Console.WriteLine (" -- available locations -- ");
		List<NodeLocation> locations = driver.ListLocations ();
		foreach (NodeLocation location in locations) {
			Console.WriteLine (location);
		}

		Console.WriteLine (" -- available images -- ");
		List<NodeImage> images = driver.ListImages ();
		foreach (NodeImage image in images) {
			Console.WriteLine (image);
		}

		Console.WriteLine (" -- available sizes -- ");
		List<NodeSize> sizes = driver.ListSizes ();
		foreach (NodeSize size in sizes) {
			Console.WriteLine (size);
		}

		
		Console.WriteLine (" -- your nodes -- ");
		List<Node> nodes = driver.ListNodes ();
		foreach (Node node in nodes) {
			Console.WriteLine (node);
		}

		Node new_node = driver.CreateNode ("test node", sizes [0], images [0], locations [0]);

		Console.WriteLine ("just created the node:  {0}", new_node);

		new_node.Destroy ();
		Console.WriteLine ("destroyed the new node");
	}
}

