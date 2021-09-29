using ASBinCode;
using ASBinCode.rtData;
using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs
{
	class RegExp_constructor : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public RegExp_constructor() : base(2)
		{
			_paras = new List<RunTimeDataType>();
			_paras.Add(RunTimeDataType.rt_string);
			_paras.Add(RunTimeDataType.rt_string);
		}

		public override bool isMethod
		{
			get
			{
				return true;
			}
		}

		public override string name
		{
			get
			{
				return "_regexp_constructor";
			}
		}

		public override List<RunTimeDataType> parameters
		{
			get
			{
				return _paras;
			}
		}

		public override RunTimeDataType returnType
		{
			get
			{
				return RunTimeDataType.rt_void;
			}
		}

		

		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
//
			//((rtObject)thisObj).value.memberData[0].directSet(newvalue);

			string pattern = TypeConverter.ConvertToString( argements[0],stackframe,token);
			string options = TypeConverter.ConvertToString( argements[1], stackframe, token);

			string pv = "/";

			if (pattern != null )
			{
				pv = pv + pattern;
			}

			pv = pv + "/";


			
			


			var Options = System.Text.RegularExpressions.RegexOptions.None;

			if (options != null)
			{
				pv = pv + options;

				if (options.IndexOf('s') >= 0)
				{
					((rtObjectBase)thisObj).value.memberData[1].directSet(rtBoolean.True);
					Options |= System.Text.RegularExpressions.RegexOptions.Singleline;
				}

				if (options.IndexOf('x') >= 0)
				{
					((rtObjectBase)thisObj).value.memberData[2].directSet(rtBoolean.True);
					Options |= System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace;
				}

				if (options.IndexOf('g') >= 0)
				{
					((rtObjectBase)thisObj).value.memberData[3].directSet(rtBoolean.True);
				}

				if (options.IndexOf('i') >= 0)
				{
					((rtObjectBase)thisObj).value.memberData[4].directSet(rtBoolean.True);
					Options |= System.Text.RegularExpressions.RegexOptions.IgnoreCase;
				}

				if (options.IndexOf('m') >= 0)
				{
					((rtObjectBase)thisObj).value.memberData[5].directSet(rtBoolean.True);
					Options |= System.Text.RegularExpressions.RegexOptions.Multiline;
				}
			}

			((rtObjectBase)thisObj).value.memberData[0].directSet( new rtString( pv));

			if (pattern != null)
			{
				((rtObjectBase)thisObj).value.memberData[6].directSet(new rtString(pattern));
			}
			else
			{
				((rtObjectBase)thisObj).value.memberData[6].directSet(rtNull.nullptr);
			}

			((rtObjectBase)thisObj).value.memberData[7].directSet(new rtNumber(0));

			try
			{
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern, Options);
				HostedObject hobj = new HostedObject(stackframe.player.swc.ObjectClass);
				hobj.hosted_object = regex;
				rtObject rtobj = new rtObject(hobj, null);

				((rtObjectBase)thisObj).value.memberData[8].directSet(rtobj);

				
				success = true;
				returnSlot.directSet(rtUndefined.undefined);

			}
			catch (ArgumentException ex)
			{
				success = false;
				stackframe.throwArgementException(token, ex.Message);
			}
			
			//((HostedObject)((rtObject)thisObj).value).hosted_object = regex; 

			
		}
	}

	class RegExp_test : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public RegExp_test() : base(1)
		{
			_paras = new List<RunTimeDataType>();
			_paras.Add(RunTimeDataType.rt_string);
		}

		public override bool isMethod
		{
			get
			{
				return true;
			}
		}

		public override string name
		{
			get
			{
				return "_regexp_test";
			}
		}

		public override List<RunTimeDataType> parameters
		{
			get
			{
				return _paras;
			}
		}

		public override RunTimeDataType returnType
		{
			get
			{
				return RunTimeDataType.rt_boolean;
			}
		}



		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			//
			//((rtObject)thisObj).value.memberData[0].directSet(newvalue);

			string str = TypeConverter.ConvertToString(argements[0], stackframe, token);

			
			bool global = TypeConverter.ConvertToBoolean(((rtObjectBase)thisObj).value.memberData[3].getValue(), stackframe, token).value;
			string source = TypeConverter.ConvertToString(((rtObjectBase)thisObj).value.memberData[6].getValue(), stackframe, token);
			double lastIndex = TypeConverter.ConvertToNumber(((rtObjectBase)thisObj).value.memberData[7].getValue());


			if (source != null)
			{
				System.Text.RegularExpressions.Regex regex = (System.Text.RegularExpressions.Regex)((HostedObject)((rtObjectBase)((rtObjectBase)thisObj).value.memberData[8].getValue()).value).hosted_object;

				bool issuccess;
				var match = regex.Match(str, global ? (int)lastIndex : 0);
				if (match.Success)
				{
					issuccess = true;

					if (global)
					{
						((rtNumber)((rtObjectBase)thisObj).value.memberData[7].getValue()).value = match.Index+match.Length;
					}
				}
				else
				{
					issuccess = false;
				}



				success = true;
				returnSlot.directSet(issuccess ? rtBoolean.True : rtBoolean.False);
			}
			else
			{
				success = true;
				returnSlot.directSet(rtBoolean.False);
			}
		}
	}


	class RegExp_exec : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public RegExp_exec() : base(3)
		{
			_paras = new List<RunTimeDataType>();
			_paras.Add(RunTimeDataType.rt_string);
			_paras.Add(RunTimeDataType._OBJECT);
			_paras.Add(RunTimeDataType.rt_boolean);
		}

		public override bool isMethod
		{
			get
			{
				return true;
			}
		}

		public override string name
		{
			get
			{
				return "_regexp_exec";
			}
		}

		public override List<RunTimeDataType> parameters
		{
			get
			{
				return _paras;
			}
		}

		public override RunTimeDataType returnType
		{
			get
			{
				return RunTimeDataType._OBJECT;
			}
		}



		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			string str = TypeConverter.ConvertToString(argements[0], stackframe, token);

			DynamicObject result = (DynamicObject)((rtObjectBase)argements[1]).value;

			bool noglobal = TypeConverter.ConvertToBoolean(argements[2], stackframe, token).value;

			bool global = TypeConverter.ConvertToBoolean(((rtObjectBase)thisObj).value.memberData[3].getValue(), stackframe, token).value;
			string source = TypeConverter.ConvertToString(((rtObjectBase)thisObj).value.memberData[6].getValue(), stackframe, token);
			double lastIndex = TypeConverter.ConvertToNumber(((rtObjectBase)thisObj).value.memberData[7].getValue());

			if (noglobal)
				global = false;


			if (source != null)
			{
				System.Text.RegularExpressions.Regex regex = (System.Text.RegularExpressions.Regex)((HostedObject)((rtObjectBase)((rtObjectBase)thisObj).value.memberData[8].getValue()).value).hosted_object;

				
				var match = regex.Match(str, global ? (int)lastIndex : 0);

				if (match.Success)
				{

					var index = new DynamicPropertySlot((rtObjectBase)argements[1], true, stackframe.player.swc.FunctionClass.getRtType());
					index.directSet(new rtInt( match.Index));
					result.createproperty("index", index);


					rtArray array = (rtArray)result.memberData[0].getValue();

					for (int i = 0; i < match.Groups.Count; i++)
					{
						array.innerArray.Add(new rtString(match.Groups[i].Value));
					}

					if (global)
					{
						((rtNumber)((rtObjectBase)thisObj).value.memberData[7].getValue()).value = match.Index + match.Length;
					}
					
					returnSlot.directSet(argements[1]);

				}
				else
				{
					returnSlot.directSet(rtNull.nullptr);
				}

				success = true;
				//returnSlot.directSet(issuccess ? rtBoolean.True : rtBoolean.False);
			}
			else
			{
				success = true;
				returnSlot.directSet(rtNull.nullptr);
			}
		}
	}


}
