
using System;

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
			internal set;
		}

		public override string ToString ()
		{
			return String.Format ("id=\"{0}\" name=\"{1}\" driver=\"{2}\"", Id, Name, Driver.Provider);
		}
	}

}

