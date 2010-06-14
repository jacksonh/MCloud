
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using Tamir.SharpSsh;

namespace MCloud.Deploy {

	public class PutFilesDeployment : SSHDeployment, IEnumerable {

		public static readonly string DefaultRemoteDirectory = "/root/";

		public PutFilesDeployment () : this (DefaultRemoteDirectory)
		{
		}

		public PutFilesDeployment (string remote) : base (remote)
		{
			RemoteDirectory = remote;
			Files = new List<string> ();
		}

		public PutFilesDeployment (string [] files) : this ()
		{
			Files.AddRange (files);
		}

		public List<string> Files {
			get;
			private set;
		}

		public string RemoteDirectory {
			get;
			set;
		}

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

