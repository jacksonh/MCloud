

using System;

namespace MCloud.Linode {

	public class LinodeNodeOptions : NodeOptions {

		public static readonly int DefaultSwapSize = 128;
		public static readonly PaymentTerm DefaultPaymentTerm = PaymentTerm.Monthly;
		public static readonly string Default64BitKernel = "Latest 2.6 Stable (2.6.18.8-linode22)";
		public static readonly string Default32BitKernel = "Latest 2.6 Stable (2.6.18.8-x86_64-linode10)";


		public LinodeNodeOptions ()
		{
			SwapSize = DefaultSwapSize;
			PaymentTerm = DefaultPaymentTerm;
			Kernel64Bit = Default64BitKernel;
			Kernel32Bit = Default32BitKernel;
		}

		public int SwapSize {
			get;
			set;
		}

		public PaymentTerm PaymentTerm {
			get;
			private set;
		}

		public bool Prefer64Bit {
			get;
			private set;
		}

		public string Kernel64Bit {
			get;
			private set;
		}

		public string Kernel32Bit {
			get;
			private set;
		}

	}
}

