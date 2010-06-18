
using System;

namespace MCloud {


	/// <summary>
	/// A node size is a package offered by a cloud provider. For example "Linode 512, 512MB RAM, 16GB storage,
	/// 200GB transfer for $XX".  Prices are in cents per a month.
	/// </summary>
	public class NodeSize : Entity {

		public NodeSize (string id, string name, int ram,
				int disk, int bandwidth, int price, NodeDriver driver) : base (id, name, driver)
		{
			Ram = ram;
			Disk = disk;
			Bandwidth = bandwidth;
			Price = price;
		}

		/// <summary>
		/// Ram in MBs
		/// </summary>
		public int Ram {
			get;
			private set;
		}

		/// <summary>
		/// Disk size in GBs
		/// </summary>
		public int Disk {
			get;
			private set;
		}

		/// <summary>
		/// Bandwidth in GBs
		/// </summary>
		public int Bandwidth {
			get;
			private set;
		}

		/// <summary>
		/// Price in cents per a month
		/// </summary>
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

