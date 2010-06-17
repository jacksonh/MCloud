

using System;
using System.Threading;

using Tamir.SharpSsh.jsch;


namespace MCloud.Deploy {

	/// <summary>
	/// A deployment is an operation performed on a node, such as copying over a file,
	/// or running a script. All deployments inherit from this class.  
	/// </summary>
	public abstract class Deployment {

		public static readonly int DefaultEnsureRunningWaitTime = 10000;
		public static readonly int DefaultEnsureRunningAttempts = 20;

		public Deployment ()
		{
			EnsureRunningWaitTime = DefaultEnsureRunningWaitTime;
			EnsureRunningAttempts = DefaultEnsureRunningAttempts;
		}

		/// <summary>
		/// The amount of time to wait for a node to boot.  Typically you would create a new node
		/// and run a deployment on it.  In this scenario the deployment would need to wait for the
		/// node to start running.  You should not have to adjust this property unless you have
		/// a node that takes much longer to boot than a normal node.
		/// </summary>
		public int EnsureRunningWaitTime {
			get;
			private set;
		}

		/// <summary>
		/// The number of times to try check if the node is running.  You should not have to adjust this
		/// property unless your nodes are taking much longer to boot than normal.
		/// </summary>
		public int EnsureRunningAttempts {
			get;
			private set;
		}

		/// <summary>
		/// Execute the deployment using the supplied NodeAuth to log into the node.
		/// </summary>
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

