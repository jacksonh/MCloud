
using System;

namespace MCloud {

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


