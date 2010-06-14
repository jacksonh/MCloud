

using System;

using Tamir.SharpSsh.jsch;

namespace MCloud.Deploy {

	public class SSHKeyDeployment : ScriptDeployment {

		public SSHKeyDeployment (string key) : base ("~/.ssh/authorized_keys", key)
		{
		}

	}
}

