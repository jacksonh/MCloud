

namespace MCloud {

	public abstract class Entity {

		public Entity (string id, string name, NodeDriver driver)
		{
			Id = id;
			Name = name;
			Driver = driver;
		}

		public string Id {
			get;
			private set;
		}

		public string Name {
			get;
			private set;
		}

		public NodeDriver Driver {
			get;
			private set;
		}
	}

}

