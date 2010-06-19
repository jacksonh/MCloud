
using System;
using System.Threading;

using Tamir.SharpSsh;

namespace MCloud.Deploy {

	/// <summary>
	/// Base class for deployment types that use SSH.
	/// </summary>
	public abstract class SSHDeployment : Deployment {

		private static readonly int DefaultMaxConnectionAttempts = 10;

		public SSHDeployment ()
		{
			MaxConnectionAttempts = DefaultMaxConnectionAttempts;
		}

		/// <summary>
		/// The maximum number of times to attempt to connect to the node before failing
		/// </summary>
		public int MaxConnectionAttempts {
			get;
			set;
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
		
				
		protected void RunCommand (string command, string host, NodeAuth auth)
		{
               SshExec exec = new SshExec (host, auth.UserName);

               SetupSSH (exec, auth);

               Console.WriteLine ("running command:  {0}   on host:  {1}", command, host);
               Console.WriteLine (exec.RunCommand (command));
               exec.Close ();
       	}
		
		
		protected void PutFile (string host, NodeAuth auth, string local, string remote)
		{
			Scp scp = new Scp (host, auth.UserName);

			SetupSSH (scp, auth);

			scp.Put (local, remote);
			scp.Close ();
		}

		protected void PutDirectory (string host, NodeAuth auth, string local, string remote)
		{
			Scp scp = new Scp (host, auth.UserName);

			SetupSSH (scp, auth);

			scp.To (local, remote, true);
			scp.Close ();
		}
	}
}

