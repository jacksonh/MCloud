

using System;


namespace MCloud {

	/// <summary>
	/// An authorization mechanism for connecting to a node.  These can be userid/password pairs,
	/// or userid and keyfile. The default user name is "root".
	/// </summary>
	public class NodeAuth {

		public static readonly string DefaultUserName = "root";

		/// <summary>
		/// Create a new NodeAuth using the default username "root".
		/// </summary>
		public NodeAuth (NodeAuthType type, string secret) : this (type, DefaultUserName, secret)
		{
		}

		/// <summary>
		/// Create a new NodeAuth
		/// </summary>
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

		/// <summary>
		/// Either the path to a key file or the password.
		/// </summary>
		public string Secret {
			get;
			private set;
		}
	}

}

