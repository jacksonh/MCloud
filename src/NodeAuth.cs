

using System;


namespace MCloud {

	public class NodeAuth {

		public NodeAuth (NodeAuthType type, string secret)
		{
			Type = type;
			Secret = secret;
		}

		public NodeAuthType Type {
			get;
			private set;
		}
		public string Secret {
			get;
			private set;
		}
	}

}

