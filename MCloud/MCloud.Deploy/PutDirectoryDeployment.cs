
using System;
using System.IO;

using Tamir.SharpSsh;

namespace MCloud.Deploy {

	/// <summary>
	/// Copy a local directory to the server. By default the directory will be copied to
	/// the /root/ folder.
	/// </summary>
	public class PutDirectoryDeployment : PutFilesDeployment {

		/// <summary>
		/// Copy the specified directory to the /root/ directory on
		/// the remote server.
		/// </summary>
		public PutDirectoryDeployment (string local) : base ()
		{
			if (!Directory.Exists (local))
				throw new ArgumentException ("local", "Directory does not exist.");
			LocalDirectory = local;
		}

		/// <summary>
		/// Put the specified local directory in the specified remote directory.
		/// If the remote directory does not exist the operation will fail.
		/// </summary>
		public PutDirectoryDeployment (string local, string remote) : base (remote)
		{
			if (!Directory.Exists (local))
				throw new ArgumentException ("local", "Directory does not exist.");
			LocalDirectory = local;
			RemoteDirectory = remote;
		}

		/// <summary>
		/// The local directory to put on the server
		/// </summary>
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

