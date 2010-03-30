
using System;

namespace MCloud {

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


