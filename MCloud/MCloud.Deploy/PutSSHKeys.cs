

using System;

using Tamir.SharpSsh.jsch;

namespace MCloud.Deploy {

	/// <summary>
	/// Copy the current users ssh authorized keys file to the server and place them 
	/// in the supplied directory
	/// </summary>
	public class PutSSHKeys : PutFile {

		/// <summary>
		///  Create a new instance of the CopySSHKeys deployment
		/// </summary>
		/// <param name="user">
		/// A <see cref="System.String"/>
		/// The folder to place the authorized keys file in
		/// </param>
		public PutSSHKeys (string key_path, string user) : base (key_path)
		{
			RemoteDirectory = String.Concat ("/home/", user, "/.ssh/");
		}
		
		protected override void RunImpl (Node node, NodeAuth auth)
		{			
			string host = node.PublicIPs [0].ToString ();
			
			RunCommand ("mkdir " + RemoteDirectory, host, auth);
			PutFile (host, auth, FilePath, RemoteDirectory);
		}

	}
}

