
using System;
using System.Threading;

using Tamir.SharpSsh;

namespace MCloud.Deploy {

	/// <summary>
	/// Run an SSH command on the remote server
	/// </summary>
	public class SSHDeployment : Deployment {

		private static readonly int DefaultMaxConnectionAttempts = 10;

		/// <summary>
		/// Run the specified command on the server
		/// </summary>
		public SSHDeployment (string cmd)
		{
			Command = cmd;
			MaxConnectionAttempts = DefaultMaxConnectionAttempts;
		}

		protected SSHDeployment ()
		{
		}

		/// <summary>
		/// The command to run on the server
		/// </summary>
		public string Command {
			get;
			private set;
		}

		/// <summary>
		/// The maximum number of times to attempt to connect to the node before failing
		/// </summary>
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

			Exception error = null;
			for (int i = 0; i < MaxConnectionAttempts; i++) {

				try{
					ssh.Connect ();
					return;
				} catch (Exception e) {
					Console.WriteLine ("Connection error: {0}", e);
					error = e;
				}

				Thread.Sleep (100);
			}

			if (error != null)
				throw error;

		}
	}
}

