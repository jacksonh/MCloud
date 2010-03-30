

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

		List<Node> nodes = driver.ListNodes ();

		foreach (Node node in nodes) {
			Console.WriteLine ("node: {0}", node);
			node.Reboot ();
		}
	}
}

