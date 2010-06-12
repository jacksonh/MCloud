
using System;
using System.IO;

using Tamir.SharpSsh;

namespace MCloud {

	public class PutDirectoryDeployment : PutFilesDeployment {

		public static readonly string DefaultPattern = "*";

		public PutDirectoryDeployment (string local) : this (local, DefaultPattern)
		{
		}

		public PutDirectoryDeployment (string local, string pattern)
		{
			Files.AddRange (Directory.GetFiles (local, pattern));
		}
	}
}

