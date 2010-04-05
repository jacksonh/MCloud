

using System;

using Tamir.SharpSsh.jsch;


namespace MCloud {

	public abstract class Deployment {

		public Deployment ()
		{
		}

		public abstract void Run (Node node, NodeAuth auth);
	}
}

