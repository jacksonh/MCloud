
using System;

using Tamir.SharpSsh;

namespace MCloud {

	public class SSHDeployment : Deployment {

		public SSHDeployment (string cmd)
		{
			Command = cmd;
		}

		protected SSHDeployment ()
		{
		}

		public string Command {
			get;
			private set;
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

			Console.WriteLine ("Attempting to connect with to {2} {0}  and pass: {1}", auth.UserName, auth.Secret, host);
			SetupSSH (exec, auth);

			Console.WriteLine ("running command:  {0}   on host:  {1}", command, host);
			Console.WriteLine (exec.RunCommand (command));
			exec.Close ();
		}

		protected void SetupSSH (SshBase ssh, NodeAuth auth)
		{
			if (auth.Type == NodeAuthType.Password)
				ssh.Password = auth.Secret;
			if (auth.Type == NodeAuthType.SSHKey)
				ssh.AddIdentityFile (auth.Secret);

			Console.WriteLine ("Connecting...");
			ssh.Connect ();
			Console.WriteLine ("OK");
			
		}
	}
}

