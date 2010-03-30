

using System;
using System.Collections.Generic;


using MCloud;
using MCloud.Linode;


public class T {

	public static void Main ()
	{
		LinodeDriver driver = new LinodeDriver ("iulALXI8yPVLudSIWTKvRfdEqJZmtkKYayMucS6xr5lWugGofTS5QGpiwTBI8opO", null);

		List<Node> nodes = driver.ListNodes ();

		Console.WriteLine ("got nodes: {0}", nodes);
		foreach (Node node in nodes) {
			Console.WriteLine ("node: {0}", node);
			node.Reboot ();
		}
	}
}

