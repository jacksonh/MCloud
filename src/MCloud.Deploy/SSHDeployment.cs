
using System;

using Tamir.SharpSsh;

namespace MCloud.Deploy {

	public class SSHDeployment : Deployment {

		private static readonly int DefaultMaxConnectionAttempts = 3;

		public SSHDeployment (string cmd)
		{
			Command = cmd;
			MaxConnectionAttempts = DefaultMaxConnectionAttempts;
		}

		protected SSHDeployment ()
		{
		}

		public string Command {
			get;
			private set;
		}

		public int MaxConnectionAttempts {
			get;
			set;
		}

		protected override void RunImpl (Node node, NodeAuth auth)
		{
			if (node.PublicIPs.Count < 1)
				throw new ArgumentException ("node", "No public IPs available on node.");

			string host = node.PublicIPs [0].ToString ();

			RunCommand (Command, host, auth);
		}

		protected void RunCommand (string command, string host, NodeAuth auth)
		{
			SshExec exec = new SshExec (host, auth.UserName);

			SetupSSH (exec, auth);
			exec.Close ();
		}

		protected void SetupSSH (SshBase ssh, NodeAuth auth)
		{
			if (auth.Type == NodeAuthType.Password)
				ssh.Password = auth.Secret;
			if (auth.Type == NodeAuthType.SSHKey)
				ssh.AddIdentityFile (auth.Secret);

			Exception error = null;
			for (int i = 0; i < MaxConnectionAttempts; i++) {

				try{
					ssh.Connect ();
					return;
				} catch (Exception e) {
					Console.WriteLine ("Connection error: {0}", e);
					error = e;
				}
			}

			if (error != null)
				throw error;

		}
	}
}

