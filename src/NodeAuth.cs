

using System;


namespace MCloud {

	public class NodeAuth {

		public static readonly string DefaultUserName = "root";

		public NodeAuth (NodeAuthType type, string secret) : this (type, DefaultUserName, secret)
		{
		}

		public NodeAuth (NodeAuthType type, string username, string secret)
		{
			Type = type;
			Secret = secret;
			UserName = username;
		}

		public NodeAuthType Type {
			get;
			private set;
		}

		public string UserName {
			get;
			private set;
		}

		public string Secret {
			get;
			private set;
		}
	}

}

