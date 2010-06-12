

using System;

using Tamir.SharpSsh;

namespace MCloud {

	public class ScriptDeployment : SSHDeployment {

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

		protected override void RunImpl (Node node, NodeAuth auth)
		{
			
			if (node == null)
				throw new ArgumentNullException ("node");
			if (auth == null)
				throw new ArgumentNullException ("auth");

			if (node.PublicIPs.Count < 1)
				throw new ArgumentException ("node", "No public IPs available on node.");

			string host = node.PublicIPs [0].ToString ();
			CopyScript (host, auth);
			RunCommand (RemoteScriptPath, host, auth);
		}

		private void CopyScript (string host, NodeAuth auth)
		{
			Scp scp = new Scp (host, auth.UserName);

			SetupSSH (scp, auth);

			scp.Put (LocalScriptPath, RemoteScriptPath);
			scp.Close ();
		}
	}
}

