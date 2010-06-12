

using System;

using Tamir.SharpSsh;

namespace MCloud {

	public class ScriptDeployment : PutFileDeployment {

		public ScriptDeployment (string local) : base (local)
		{
		}

		public ScriptDeployment (string local, string remote_dir) : base (local, remote_dir)
		{
		}

		protected override void RunImpl (Node node, NodeAuth auth)
		{			
			string host = node.PublicIPs [0].ToString ();

			string remote = String.Concat (RemoteDirectory, FileName);
			PutFile (host, auth, FileName, remote);

			RunCommand (remote, host, auth);
		}
	}
}

