using ASBinCode;
using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;
using ASRuntime.flash.utils;


namespace ASRuntime.nativefuncs
{
	




	class ByteArray_constructor : NativeFunctionBase
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_constructor()
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_constructor_";
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

		public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		{
			errormessage = null;
			errorno = 0;


			var bytearrayclass=((StackFrame)stackframe).player.swc.getClassByRunTimeDataType(thisObj.rtType);

			var rtobj = new ASBinCode.rtti.HostedObject(bytearrayclass);
			rtobj.hosted_object = new ByteArray();

			var newvalue = new ASBinCode.rtData.rtObject(rtobj,null);
			
			((rtObjectBase)thisObj).value.memberData[0].directSet(newvalue);
			
			return ASBinCode.rtData.rtUndefined.undefined;
		}
	}

	class ByteArray_clear : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_clear():base(0)
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_clear_";
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
				return RunTimeDataType.fun_void;
			}
		}

		
		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
				(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			ms.clear();

			success = true;

			returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);

		}
	}

	class ByteArray_bytesSetEndian : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_bytesSetEndian() : base(1)
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
				return "_bytearray_bytesSetEndian_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{
			string newvalue = TypeConverter.ConvertToString(argements[0],stackframe,token);

			if (newvalue != "bigEndian" && newvalue != "littleEndian")
			{
				success = false;
				stackframe.throwArgementException(token, "Parameter type must be one of the accepted values.");
			}
			else
			{
				success = true;
				
				ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;
				ms.isbig = (newvalue == "bigEndian");
				((rtObjectBase)thisObj).value.memberData[1].directSet(ms.isbig ? Endian.bigEndian : Endian.littleEndian);

			}



			returnSlot.directSet(ASBinCode.rtData.rtUndefined.undefined);

		}
	}

	class ByteArray_bytesAvailable : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_bytesAvailable() : base(0)
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_bytesAvailable_";
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
				return RunTimeDataType.rt_uint;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			success = true;

			returnSlot.setValue(ms.bytesAvailable);

		}
	}

	class ByteArray_getlength : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_getlength() : base(0)
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_getlength_";
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
				return RunTimeDataType.rt_uint;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			success = true;

			returnSlot.setValue(ms.length);

		}
	}

	class ByteArray_setlength : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_setlength() : base(1)
		{
			_paras = new List<RunTimeDataType>();
			_paras.Add(RunTimeDataType.rt_uint);
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
				return "_bytearray_setlength_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			

			try
			{
				success = true;
				ms.length = TypeConverter.ConvertToUInt(argements[0],stackframe,token);

				returnSlot.setValue(rtUndefined.undefined);
			}
			catch (OutOfMemoryException)
			{
				success = false;
				stackframe.throwError(token, 1000, "The system is out of memory.");
			}
			catch (ArgumentOutOfRangeException)
			{
				success = false;
				stackframe.throwError(token, 1000, "The system is out of memory.");
			}

		}
	}

	class ByteArray_getposition : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_getposition() : base(0)
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_getposition_";
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
				return RunTimeDataType.rt_uint;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			success = true;

			returnSlot.setValue(ms.position);

		}
	}

	class ByteArray_setposition : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_setposition() : base(1)
		{
			_paras = new List<RunTimeDataType>();
			_paras.Add(RunTimeDataType.rt_uint);
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
				return "_bytearray_setposition_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;



			
			success = true;
			ms.position = TypeConverter.ConvertToUInt(argements[0], stackframe, token);
				
			returnSlot.setValue(rtUndefined.undefined);
			
		}
	}

	class ByteArray_compress : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_compress() : base(1)
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
				return "_bytearray_compress_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;




			success = true;
			ms.compress();

			returnSlot.setValue(rtUndefined.undefined);

		}
	}

	class ByteArray_uncompress : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_uncompress() : base(1)
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
				return "_bytearray_uncompress_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;




			success = true;
			ms.uncompress();

			returnSlot.setValue(rtUndefined.undefined);

		}
	}

	class ByteArray_deflate : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_deflate() : base(0)
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_deflate_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			success = true;
			ms.compress();

			returnSlot.setValue(rtUndefined.undefined);

		}
	}

	class ByteArray_inflate : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_inflate() : base(0)
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_inflate_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;




			success = true;
			ms.uncompress();

			returnSlot.setValue(rtUndefined.undefined);

		}
	}

	class ByteArray_readBoolean : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_readBoolean() : base(0)
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_readBoolean_";
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

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			try
			{
				success = true;
				bool b = ms.readBoolean();
				returnSlot.setValue(b ? rtBoolean.True : rtBoolean.False);
			}
			catch (EOFException)
			{
				success = false;
				stackframe.throwEOFException(token, "End of file was encountered");
			}

			

		}
	}

	class ByteArray_readByte : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_readByte() : base(0)
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_readByte_";
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
				return RunTimeDataType.rt_int;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			try
			{
				success = true;
				int b = ms.readByte();
				returnSlot.setValue(b);
			}
			catch (EOFException)
			{
				success = false;
				stackframe.throwEOFException(token, "End of file was encountered");
			}



		}
	}

	class ByteArray_readBytes : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_readBytes() : base(3)
		{
			_paras = new List<RunTimeDataType>();
			_paras.Add(RunTimeDataType.rt_void);
			_paras.Add(RunTimeDataType.rt_uint);
			_paras.Add(RunTimeDataType.rt_uint);
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
				return "_bytearray_readBytes_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			if (argements[0].rtType == RunTimeDataType.rt_null)
			{
				success = false;
				stackframe.throwArgementException(token, "Parameter bytes must be non-null.");
				return;
			}



			var target = (ByteArray)((HostedObject) (((rtObjectBase)(((rtObjectBase)argements[0]).value.memberData[0].getValue())).value)).hosted_object;

			uint offset = TypeConverter.ConvertToUInt(argements[1], stackframe, token);
			uint length = TypeConverter.ConvertToUInt(argements[2], stackframe, token);
			
			try
			{
				success = true;
				ms.readBytes(target, offset, length);
				returnSlot.setValue(rtUndefined.undefined);
			}
			catch (EOFException)
			{
				success = false;
				stackframe.throwEOFException(token, "End of file was encountered");
			}



		}
	}

	class ByteArray_readDouble : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_readDouble() : base(0)
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_readDouble_";
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
				return RunTimeDataType.rt_number;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			try
			{
				success = true;
				
				returnSlot.setValue(ms.readDouble());
			}
			catch (EOFException)
			{
				success = false;
				stackframe.throwEOFException(token, "End of file was encountered");
			}



		}
	}

	class ByteArray_readFloat : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_readFloat() : base(0)
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_readFloat_";
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
				return RunTimeDataType.rt_number;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			try
			{
				success = true;

				returnSlot.setValue(ms.readFloat());
			}
			catch (EOFException)
			{
				success = false;
				stackframe.throwEOFException(token, "End of file was encountered");
			}



		}
	}

	class ByteArray_readInt : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_readInt() : base(0)
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_readInt_";
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
				return RunTimeDataType.rt_int;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			try
			{
				success = true;

				returnSlot.setValue(ms.readInt());
			}
			catch (EOFException)
			{
				success = false;
				stackframe.throwEOFException(token, "End of file was encountered");
			}



		}
	}

	class ByteArray_readMultiByte : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_readMultiByte() : base(2)
		{
			_paras = new List<RunTimeDataType>();
			_paras.Add(RunTimeDataType.rt_uint);
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
				return "_bytearray_readMultiByte_";
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
				return RunTimeDataType.rt_string;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			uint length = TypeConverter.ConvertToUInt(argements[0], stackframe, token);
			string charset = TypeConverter.ConvertToString(argements[1], stackframe, token);

			if (charset == null)
			{
				success = false;
				stackframe.throwArgementException(token, "Parameter "+ functionDefine.signature.parameters[1].name + " must be non-null.");
			}
			else
			{

				try
				{
					success = true;

					returnSlot.setValue(ms.readMultiByte(length, charset));
				}
				catch (EOFException)
				{
					success = false;
					stackframe.throwEOFException(token, "End of file was encountered");
				}
			}


		}
	}

	class ByteArray_readShort : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_readShort() : base(0)
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_readShort_";
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
				return RunTimeDataType.rt_int;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			try
			{
				success = true;

				returnSlot.setValue(ms.readShort());
			}
			catch (EOFException)
			{
				success = false;
				stackframe.throwEOFException(token, "End of file was encountered");
			}



		}
	}


	class ByteArray_readUnsignedByte : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_readUnsignedByte() : base(0)
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_readUnsignedByte_";
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
				return RunTimeDataType.rt_uint;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			try
			{
				success = true;
				returnSlot.setValue(ms.readUnsignedByte());
			}
			catch (EOFException)
			{
				success = false;
				stackframe.throwEOFException(token, "End of file was encountered");
			}



		}
	}

	class ByteArray_readUnsignedInt : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_readUnsignedInt() : base(0)
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_readUnsignedInt_";
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
				return RunTimeDataType.rt_uint;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			try
			{
				success = true;
				returnSlot.setValue(ms.readUnsignedInt());
			}
			catch (EOFException)
			{
				success = false;
				stackframe.throwEOFException(token, "End of file was encountered");
			}



		}
	}

	class ByteArray_readUnsignedShort : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_readUnsignedShort() : base(0)
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_readUnsignedShort_";
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
				return RunTimeDataType.rt_uint;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			try
			{
				success = true;
				returnSlot.setValue(ms.readUnsignedShort());
			}
			catch (EOFException)
			{
				success = false;
				stackframe.throwEOFException(token, "End of file was encountered");
			}



		}
	}

	class ByteArray_readUTF : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_readUTF() : base(0)
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_readUTF_";
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
				return RunTimeDataType.rt_string;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			try
			{
				success = true;
				returnSlot.setValue(ms.readUTF());
			}
			catch (EOFException)
			{
				success = false;
				stackframe.throwEOFException(token, "End of file was encountered");
			}



		}
	}

	class ByteArray_readUTFBytes : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_readUTFBytes() : base(1)
		{
			_paras = new List<RunTimeDataType>();
			_paras.Add(RunTimeDataType.rt_uint);
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
				return "_bytearray_readUTFBytes_";
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
				return RunTimeDataType.rt_string;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			uint len = TypeConverter.ConvertToUInt(argements[0], stackframe, token);

			try
			{
				success = true;
				returnSlot.setValue(ms.readUTFBytes(len));
			}
			catch (EOFException)
			{
				success = false;
				stackframe.throwEOFException(token, "End of file was encountered");
			}



		}
	}

	class ByteArray_toString : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_toString() : base(0)
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_toString_";
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
				return RunTimeDataType.rt_string;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			try
			{
				success = true;
				returnSlot.setValue(ms.ToString());
			}
			catch (EOFException)
			{
				success = false;
				stackframe.throwEOFException(token, "End of file was encountered");
			}



		}
	}


	class ByteArray_writeBoolean : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_writeBoolean() : base(1)
		{
			_paras = new List<RunTimeDataType>();
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
				return "_bytearray_writeBoolean_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			bool v = TypeConverter.ConvertToBoolean(argements[0],stackframe,token).value;

			ms.writeBoolean(v);

			success = true;

			

			returnSlot.setValue(rtUndefined.undefined);
			



		}
	}

	class ByteArray_writeByte : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_writeByte() : base(1)
		{
			_paras = new List<RunTimeDataType>();
			_paras.Add(RunTimeDataType.rt_int);
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
				return "_bytearray_writeByte_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			int v = TypeConverter.ConvertToInt(argements[0]);

			ms.writeByte(v);

			success = true;



			returnSlot.setValue(rtUndefined.undefined);




		}
	}

	class ByteArray_writeBytes : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_writeBytes() : base(3)
		{
			_paras = new List<RunTimeDataType>();
			_paras.Add(RunTimeDataType.rt_void);
			_paras.Add(RunTimeDataType.rt_uint);
			_paras.Add(RunTimeDataType.rt_uint);
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
				return "_bytearray_writeBytes_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			if (argements[0].rtType == RunTimeDataType.rt_null)
			{
				success = false;
				stackframe.throwArgementException(token, "Parameter bytes must be non-null.");
				return;
			}

			var target = (ByteArray)((HostedObject)(((rtObjectBase)(((rtObjectBase)argements[0]).value.memberData[0].getValue())).value)).hosted_object;

			uint offset = TypeConverter.ConvertToUInt(argements[1], stackframe, token);
			uint length = TypeConverter.ConvertToUInt(argements[2], stackframe, token);

			try
			{
				ms.writeBytes(target, offset, length);
				success = true;

				returnSlot.setValue(rtUndefined.undefined);
			}
			catch (ArgumentOutOfRangeException)
			{
				success = false;

				stackframe.throwError(token, 2006, "The supplied index is out of bounds.");
			}

			




		}
	}

	class ByteArray_writeDouble : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_writeDouble() : base(1)
		{
			_paras = new List<RunTimeDataType>();
			_paras.Add(RunTimeDataType.rt_number);
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
				return "_bytearray_writeDouble_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			double v = TypeConverter.ConvertToNumber(argements[0]);

			ms.writeDouble(v);

			success = true;



			returnSlot.setValue(rtUndefined.undefined);




		}
	}

	class ByteArray_writeFloat : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_writeFloat() : base(1)
		{
			_paras = new List<RunTimeDataType>();
			_paras.Add(RunTimeDataType.rt_number);
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
				return "_bytearray_writeFloat_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			double v = TypeConverter.ConvertToNumber(argements[0]);

			ms.writeFloat(v);

			success = true;



			returnSlot.setValue(rtUndefined.undefined);




		}
	}

	class ByteArray_writeInt : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_writeInt() : base(1)
		{
			_paras = new List<RunTimeDataType>();
			_paras.Add(RunTimeDataType.rt_int);
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
				return "_bytearray_writeInt_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			int v = TypeConverter.ConvertToInt(argements[0]);

			ms.writeInt(v);

			success = true;



			returnSlot.setValue(rtUndefined.undefined);




		}
	}

	class ByteArray_writeMultiByte : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_writeMultiByte() : base(2)
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
				return "_bytearray_writeMultiByte_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			string value = TypeConverter.ConvertToString(argements[0], stackframe, token);
			string charset = TypeConverter.ConvertToString(argements[1], stackframe, token);

			if (charset == null)
			{
				success = false;
				stackframe.throwArgementException(token, "Parameter " + functionDefine.signature.parameters[1].name + " must be non-null.");
			}
			else
			{
				ms.writeMultiByte(value,charset);

				success = true;

				returnSlot.setValue(rtUndefined.undefined);
			}


		}
	}

	class ByteArray_writeShort : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_writeShort() : base(1)
		{
			_paras = new List<RunTimeDataType>();
			_paras.Add(RunTimeDataType.rt_int);
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
				return "_bytearray_writeShort_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			int v = TypeConverter.ConvertToInt(argements[0]);

			ms.writeShort(v);

			success = true;



			returnSlot.setValue(rtUndefined.undefined);




		}
	}

	class ByteArray_writeUnsignedInt : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_writeUnsignedInt() : base(1)
		{
			_paras = new List<RunTimeDataType>();
			_paras.Add(RunTimeDataType.rt_uint);
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
				return "_bytearray_writeUnsignedInt_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			uint v = TypeConverter.ConvertToUInt(argements[0], stackframe, token);

			ms.writeUnsignedInt(v);

			success = true;



			returnSlot.setValue(rtUndefined.undefined);




		}
	}


	class ByteArray_writeUTF : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_writeUTF() : base(1)
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
				return "_bytearray_writeUTF_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			string v = TypeConverter.ConvertToString(argements[0], stackframe, token);

			if (v == null)
			{
				success = false;

				stackframe.throwError(token, 2007, "Parameter value must be non-null.");

				returnSlot.setValue(rtUndefined.undefined);
			}
			else
			{

				ms.writeUTF(v);

				success = true;



				returnSlot.setValue(rtUndefined.undefined);

			}


		}
	}

	class ByteArray_writeUTFBytes : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_writeUTFBytes() : base(1)
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
				return "_bytearray_writeUTFBytes_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			string v = TypeConverter.ConvertToString(argements[0], stackframe, token);

			if (v == null)
			{
				success = false;

				stackframe.throwError(token, 2007, "Parameter value must be non-null.");

				returnSlot.setValue(rtUndefined.undefined);
			}
			else
			{
				ms.writeUTFBytes(v);

				success = true;



				returnSlot.setValue(rtUndefined.undefined);

			}


		}
	}


	class ByteArray_getThisItem : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_getThisItem() : base(1)
		{
			_paras = new List<RunTimeDataType>();
			_paras.Add(RunTimeDataType.rt_number);
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
				return "_flash_utils_bytearray_getThisItem_";
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
				return RunTimeDataType.rt_number;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			int index = TypeConverter.ConvertToInt(argements[0]);

			if (index<0)
			{
				success = false;

				double origin = TypeConverter.ConvertToNumber(argements[0]);
				if (origin < 0)
				{
					stackframe.throwError(token, 1069, "Property " + index + " not found on flash.utils.ByteArray and there is no default value.");
				}
				returnSlot.setValue(rtUndefined.undefined);
			}
			else
			{
				if (index >= ms.length)
				{
					success = true;
					returnSlot.setValue(rtUndefined.undefined);
				}
				else
				{
					success = true;

					uint pos = ms.position;
					ms.position = (uint)index;

					returnSlot.setValue((double)ms.readUnsignedByte());

					ms.position = pos;
				}
				

				

			}


		}
	}

	class ByteArray_setThisItem : NativeConstParameterFunction
	{
		private List<RunTimeDataType> _paras;

		public ByteArray_setThisItem() : base(2)
		{
			_paras = new List<RunTimeDataType>();
			_paras.Add(RunTimeDataType.rt_number);
			_paras.Add(RunTimeDataType.rt_number);
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
				return "_flash_utils_bytearray_setThisItem_";
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
				return RunTimeDataType.fun_void;
			}
		}


		public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
		{

			ByteArray ms =
					(ByteArray)((ASBinCode.rtti.HostedObject)((rtObjectBase)(((rtObjectBase)thisObj).value.memberData[0].getValue())).value).hosted_object;

			double value = TypeConverter.ConvertToNumber(argements[0]);
			int index = TypeConverter.ConvertToInt(argements[1]);

			if (index <0)
			{
				success = false;

				stackframe.throwError(token, 1056, " Error #1056: Cannot create property "+index+" on flash.utils.ByteArray.");

				returnSlot.setValue(rtUndefined.undefined);
			}
			else
			{
				uint oldpos = ms.position;
				if (oldpos < ms.length)
				{
					oldpos = ms.length;
				}

				while (ms.length < index)
				{
					ms.writeByte(0);
				}

				ms.position = (uint)index;
				ms.writeByte( (SByte)value );

				ms.position = oldpos;


				success = true;
				returnSlot.setValue(rtUndefined.undefined);

			}


		}
	}
}


