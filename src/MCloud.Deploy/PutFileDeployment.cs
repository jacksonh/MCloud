

using System;
using System.IO;

using Tamir.SharpSsh;

namespace MCloud.Deploy {

	/// <summary>
	/// Put a single file from the local machine onto the remote machine.
	/// If no remote directory is supplied the file will be put in the /root/
	/// directory.
	/// </summary>
	public class PutFileDeployment : PutFilesDeployment {

		/// <summary>
		/// put the specified file on the server in the /root/ directory
		/// </summary>
		public PutFileDeployment (string local) : this (local, DefaultRemoteDirectory)
		{
		}

		/// <summary>
		/// Put the specified file in the specified directory.
		/// </summary>
		public PutFileDeployment (string local, string remote_dir) : base (remote_dir)
		{
			if (local == null)
				throw new ArgumentNullException ("local");
			if (!File.Exists (local))
				throw new ArgumentException ("local", "The file does not exist.");
			Files.Add (local);			
		}

		/// <summary>
		/// The path of the file to put on the server.
		/// </summary>
		public string FilePath {
			get { return Files [0]; }
		}
	}
}

