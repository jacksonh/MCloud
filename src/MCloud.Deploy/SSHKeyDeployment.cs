

using System;

using Tamir.SharpSsh.jsch;

namespace MCloud {

	public class SSHKeyDeployment : ScriptDeployment {

		public SSHKeyDeployment (string key) : base ("~/.ssh/authorized_keys", key)
		{
		}

	}
}

