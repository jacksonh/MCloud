
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using Tamir.SharpSsh;

namespace MCloud.Deploy {

	/// <summary>
	/// Transfer a collection of files to the server.  By default the files will
	/// be saved in the /root/ directory.
	/// </summary>
	public class PutFilesDeployment : SSHDeployment, IEnumerable {

		public static readonly string DefaultRemoteDirectory = "/root/";

		/// <summary>
		/// This constructor is mainly used with collection initializers
		/// like this: new PutFilesDeployment () { "/foo/bar", "/foo/baz" }
		/// </summary>
		public PutFilesDeployment () : this (DefaultRemoteDirectory)
		{
		}

		/// <summary>
		/// Put the specified file in the remote directory directory.
		/// </summary>
		public PutFilesDeployment (string remote)
		{
			if (remote == null)
				throw new ArgumentNullException ("remote");

			RemoteDirectory = remote;
			Files = new List<string> ();
		}

		/// <summary>
		/// Put the specified files in the remote directory
		/// </summary>
		public PutFilesDeployment (string [] files) : this ()
		{
			Files.AddRange (files);
		}

		/// <summary>
		/// The local files that will be placed on the remote node.
		/// </summary>
		public List<string> Files {
			get;
			private set;
		}

		/// <summary>
		/// The remote directory to put the files in
		/// </summary>
		public string RemoteDirectory {
			get;
			set;
		}

		/// <summary>
		/// Add a local file to the list of files to be put on the node.
		/// </summary>
		public void Add (string file)
		{
			Files.Add (file);
		}

		protected override void RunImpl (Node node, NodeAuth auth)
		{			
			string host = node.PublicIPs [0].ToString ();

			foreach (string file in Files) {
				string remote = Path.Combine (RemoteDirectory, Path.GetFileName (file));

				PutFile (host, auth, file, remote);
			}
		}

		protected void PutFile (string host, NodeAuth auth, string local, string remote)
		{
			Scp scp = new Scp (host, auth.UserName);

			SetupSSH (scp, auth);

			scp.Put (local, remote);
			scp.Close ();
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return Files.GetEnumerator ();
		}
	}
}

