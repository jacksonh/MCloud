

using System;

using Tamir.SharpSsh;

namespace MCloud.Deploy {

	public class PutFileDeployment : PutFilesDeployment {

		public PutFileDeployment (string local) : base ()
		{
			Files.Add (local);
		}

		public PutFileDeployment (string local, string remote_dir) : base (remote_dir)
		{
			Files.Add (local);
			
		}

		public string FileName {
			get { return Files [0]; }
		}
	}
}

