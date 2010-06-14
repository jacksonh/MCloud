

using System;
using System.Threading;

using Tamir.SharpSsh.jsch;


namespace MCloud.Deploy {

	public abstract class Deployment {

		public static readonly int DefaultEnsureRunningWaitTime = 10000;
		public static readonly int DefaultEnsureRunningAttempts = 20;

		public Deployment ()
		{
			EnsureRunningWaitTime = DefaultEnsureRunningWaitTime;
			EnsureRunningAttempts = DefaultEnsureRunningAttempts;
		}

		public int EnsureRunningWaitTime {
			get;
			private set;
		}

		public int EnsureRunningAttempts {
			get;
			private set;
		}

		public void Run (Node node, NodeAuth auth)
		{
			if (node == null)
				throw new ArgumentNullException ("node");
			if (auth == null)
				throw new ArgumentNullException ("auth");

			EnsureNodeRunning (node);
			RunImpl (node, auth);
		}

		private void EnsureNodeRunning (Node node)
		{
			int tries = 0;

			while (node.State != NodeState.Running && tries < EnsureRunningAttempts) {

				node.Update ();

				tries++;
				Thread.Sleep (EnsureRunningWaitTime);
			}

			if (tries == EnsureRunningAttempts)
				throw new Exception ("Node is not running.");
		}

		protected abstract void RunImpl (Node node, NodeAuth auth);
	}
}

