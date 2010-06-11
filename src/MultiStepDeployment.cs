

using System;
using System.Collections;
using System.Collections.Generic;

namespace MCloud {

	public class MultiStepDeployment : Deployment, IEnumerable {

		public MultiStepDeployment ()
		{
			Steps = new List<Deployment> ();
		}

		public List<Deployment> Steps {
			get;
			private set;
		}

		public void Add (Deployment step)
		{
			Steps.Add (step);
		}

		protected override void RunImpl (Node node, NodeAuth auth)
		{
			foreach (Deployment d in Steps) {
				d.Run (node, auth);
			}
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return Steps.GetEnumerator ();
		}
	}
}

