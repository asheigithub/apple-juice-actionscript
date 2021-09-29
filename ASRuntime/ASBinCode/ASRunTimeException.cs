using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    /// <summary>
    /// 运行时异常基类
    /// </summary>
    public class ASRunTimeException :Exception
    {
		private string as3stacktrace;

        public ASRunTimeException() { }
        public ASRunTimeException(string msg,string as3stacktrace) : base(msg) { this.as3stacktrace = as3stacktrace; }

        public ASRunTimeException(string msg, Exception innerException) : base(msg, innerException) { }

		public override string Message
		{
			get
			{
				if (InnerException == null)
					return base.Message;
				else
				{
					return base.Message + "\n\t" + InnerException.Message;

				}
			}
		}

		public string AS3StackTrace
		{
			get
			{
				return as3stacktrace==null?string.Empty:as3stacktrace;
			}
		}


		public override string StackTrace
		{
			get
			{
				if (as3stacktrace == null)
				{
					return base.StackTrace;
				}
				else
				{
					return as3stacktrace + "\n" + base.StackTrace;
				}
			}
		}
	}
}
