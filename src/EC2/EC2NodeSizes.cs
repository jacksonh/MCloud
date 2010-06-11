

using System;
using System.Collections.Generic;

using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;

namespace MCloud.EC2 {

	internal static class EC2NodeSize {

	       
		static EC2NodeSize () {
			Sizes = new Dictionary<string,List<NodeSize>> ();

			// TODO: hardcode the rest of the prices
			
			Sizes ["us-east-1"] = new List<NodeSize> () {
				new NodeSize ("m1.small", "Small Instance", 1740, 160, 0, 0 /*0.085*/, null),
				new NodeSize ("m1.large", "Large Instance", 7680, 1690, 0, 0 /*0.34*/, null),
				new NodeSize ("m1.xlarge", "Extra Large Instance", 15360, 1690, 0, 0 /*0.68*/, null),
				new NodeSize ("c1.medium", "High-CPU Medium Instance", 1740, 350, 0, 0 /*0.17*/, null),
				new NodeSize ("c1.xlarge", "High-CPU Extra Large Instance", 7680, 1690, 0, 0 /*0.68*/, null),
				new NodeSize ("m2.xlarge", "High-Memory Extra Large Instance", 17510, 420, 0, 0 /*0.50*/, null),
				new NodeSize ("m2.4xlarge", "High-Memory Quadruple Extra Large Instance", 70042, 1690, 0, 0 /*2.4*/, null),
			};

		}

		private static Dictionary<string,List<NodeSize>> Sizes {
			get;
			set;
		}

		public static List<NodeSize> List (EC2Driver driver, string region)
		{
			List<NodeSize> res = Sizes [region];

			res.ForEach ((n) => n.Driver = driver);

			return res;
		}
	}
}

