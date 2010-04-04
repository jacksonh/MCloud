

using System;
using System.Reflection;

namespace MCloud {

	public abstract class NodeOptions {


		public PropertyInfo [] GetOptions ()
		{
			PropertyInfo [] props = GetType ().GetProperties ();

			return props;
		}
	}
}

