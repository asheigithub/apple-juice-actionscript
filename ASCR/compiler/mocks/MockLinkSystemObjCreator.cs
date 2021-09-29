using ASBinCode.rtti;
using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode;

namespace ASCompiler.compiler.mocks
{
	class MockNativeFunction : NativeFunctionBase, ILinkSystemObjCreator
	{
		private string _name;

		public MockNativeFunction(string _name)
		{
			this._name = _name;
		}


		public override string name { get  { return _name; } }

		public override List<RunTimeDataType> parameters { get { throw new NotImplementedException(); } }

		public override RunTimeDataType returnType { get { throw new NotImplementedException(); } }

		public override bool isMethod { get { throw new NotImplementedException(); } }

		public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
		{
			throw new NotImplementedException();
		}

		public Type getLinkSystemObjType()
		{
			throw new NotImplementedException();
		}

		public RunTimeValueBase makeObject(Class cls)
		{
			throw new NotImplementedException();
		}

		public void setLinkObjectValueToSlot(SLOT slot, object player, object value, Class clsType)
		{
			throw new NotImplementedException();
		}
	}
}
