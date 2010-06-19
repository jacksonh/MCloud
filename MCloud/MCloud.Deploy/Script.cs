

using System;
using System.IO;

using Tamir.SharpSsh;

namespace MCloud.Deploy {

	public class Script : PutFile {

		public Script (string local) : base (local)
		{
		}

		public Script (string local, string remote_dir) : base (local, remote_dir)
		{
		}

		protected override void RunImpl (Node node, NodeAuth auth)
		{			
			string host = node.PublicIPs [0].ToString ();

			string remote = String.Concat (RemoteDirectory, FilePath);

			PutFile (host, auth, FilePath, remote);
			RunCommand ("chmod 775 " + remote, host, auth);
			RunCommand (remote, host, auth);
		}
	}
}

