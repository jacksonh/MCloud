
using System;

namespace MCloud {

	public class NodeSize : Entity {

		public NodeSize (string id, string name, int ram, int disk, int bandwidth, int price, NodeDriver driver) : base (id, name, driver)
		{
			Ram = ram;
			Disk = disk;
			Bandwidth = bandwidth;
			Price = price;
		}

		public int Ram {
			get;
			private set;
		}

		public int Disk {
			get;
			private set;
		}

		public int Bandwidth {
			get;
			private set;
		}

		public int Price {
			get;
			private set;
		}

		public override string ToString ()
		{
			return String.Format ("NodeSize {0} ram=\"{1}\" disk=\"{2}\" bandwidth=\"{3}\" price=\"{4}\"",
					base.ToString (), Ram, Disk, Bandwidth, Price);
		}
	}
}

