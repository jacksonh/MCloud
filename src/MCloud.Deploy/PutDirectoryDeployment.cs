
using System;
using System.IO;

using Tamir.SharpSsh;

namespace MCloud.Deploy {

	public class PutDirectoryDeployment : PutFilesDeployment {

		public PutDirectoryDeployment (string local) : base ()
		{
			LocalDirectory = local;
		}


		public PutDirectoryDeployment (string local, string remote) : base (remote)
		{
			LocalDirectory = local;
			RemoteDirectory = remote;
		}

		public string LocalDirectory {
			get;
			set;
		}

		protected override void RunImpl (Node node, NodeAuth auth)
		{			
			string host = node.PublicIPs [0].ToString ();

			PutDirectory (host, auth, LocalDirectory, RemoteDirectory);
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

