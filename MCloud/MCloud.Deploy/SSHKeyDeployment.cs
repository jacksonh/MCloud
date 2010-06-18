

using System;

using Tamir.SharpSsh.jsch;

namespace MCloud.Deploy {

	/// <summary>
	/// Copy the current users ssh authorized keys file to the server
	/// </summary>
	public class SSHKeyDeployment : PutFileDeployment {

		public SSHKeyDeployment (string key) : base ("~/.ssh/authorized_keys", key)
		{
		}

	}
}

