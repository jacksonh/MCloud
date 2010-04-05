

using System;
using System.Collections.Generic;

namespace MCloud {

	public class MultiStepDeployment : Deployment {

		public MultiStepDeployment ()
		{
			Steps = new List<Deployment> ();
		}

		public List<Deployment> Steps {
			get;
			private set;
		}

		public override void Run (Node node, NodeAuth auth)
		{
			foreach (Deployment d in Steps) {
				d.Run (node, auth);
			}
		}
	}
}

