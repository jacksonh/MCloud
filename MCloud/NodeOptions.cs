

using System;
using System.Reflection;

namespace MCloud {

	/// <summary>
	///  Each node driver has a different option set for creating nodes. This is an abstract class
	///  that all of the NodeOption implentations inherit from, you should use the driver specific
	///  NodeOptions classes when you create a node such as MCloud.Linode.LinodeNodeDriver.
	/// </summary>
	public abstract class NodeOptions {


		public PropertyInfo [] GetOptions ()
		{
			PropertyInfo [] props = GetType ().GetProperties ();

			return props;
		}
	}
}

