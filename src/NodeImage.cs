
using System;

namespace MCloud {

	/// <summary>
	/// Describes an available image on the cloud provider.  Node images will have descriptive names
	/// like "Open Suse 11.2".
	/// </summary>
	public class NodeImage : Entity {

		public NodeImage (string id, string name, NodeDriver driver) : base (id, name, driver)
		{
		}

		public override string ToString ()
		{
			return String.Concat ("NodeImage ", base.ToString ());
		}
	}
}


