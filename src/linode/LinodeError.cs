

using System;

namespace MCloud.Linode {

	public class LinodeError {

		public LinodeError (string message, int code)
		{
			Message = message;
			Code = code;
		}

		public string Message {
			get;
			set;
		}

		public int Code {
			get;
			set;
		}
	}
}

