
using System;

namespace MCloud {

	public class NodeLocation : Entity {

		public NodeLocation (string id, string name, NodeDriver driver) : base (id, name, driver)
		{
		}

		public override string ToString ()
		{
			return String.Concat ("NodeLocation ", base.ToString ());
		}
	}
}


