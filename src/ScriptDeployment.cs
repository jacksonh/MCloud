

using System;

using Tamir.SharpSsh;

namespace MCloud {

	public class ScriptDeployment : Deployment {

		public ScriptDeployment (string local_path) : this (local_path, String.Concat ("/root/", local_path))
		{
		}

		public ScriptDeployment (string local_path, string remote_path)
		{
			LocalScriptPath = local_path;
			RemoteScriptPath = remote_path;
		}

		public string LocalScriptPath {
			get;
			private set;
		}

		public string RemoteScriptPath {
			get;
			private set;
		}

		public override void Run (Node node, NodeAuth auth)
		{
			if (node == null)
				throw new ArgumentNullException ("node");
			if (auth == null)
				throw new ArgumentNullException ("auth");

			if (node.PublicIPs.Count < 1)
				throw new ArgumentException ("node", "No public IPs available on node.");

			string host = node.PublicIPs [0].ToString ();
			CopyScript (host, auth);
			
			SshExec exec = new SshExec (host, auth.UserName);
		}

		private void CopyScript (string host, NodeAuth auth)
		{
			Scp scp = new Scp (host, auth.UserName);

			SetupSSH (scp, auth);

			scp.Put (LocalScriptPath, RemoteScriptPath);
			scp.Close ();
		}

		private void RunScript (string host, NodeAuth auth)
		{
			SshExec exec = new SshExec (host, auth.UserName);

			SetupSSH (exec, auth);

			exec.RunCommand (RemoteScriptPath);
		}

		private void SetupSSH (SshBase ssh, NodeAuth auth)
		{
			if (auth.Type == NodeAuthType.Password)
				ssh.Password = auth.Secret;
		}
	}
}

