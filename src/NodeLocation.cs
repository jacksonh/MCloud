
using System;

namespace MCloud {

	/// <summary>
	/// The physical location of the cloud datacenter. You would use to choose the country where a node is located.
	/// </summary>
	public class NodeLocation : Entity {

		public NodeLocation (string id, string name, string country, NodeDriver driver) : base (id, name, driver)
		{
			Country = country;
		}

		public string Country {
			get;
			private set;
		}

		public override string ToString ()
		{
			return String.Format ("NodeLocation {0} Country=\"{1}\"", base.ToString (), Country);
		}
	}
}


