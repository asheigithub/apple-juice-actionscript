using System;
using System.Collections.Generic;
using System.Text;
using ASBinCode.rtti;
using ASBinCode.rtData;
using System.IO;

namespace ASBinCode
{
	[Serializable]
    public sealed class InterfaceMethodGetter : MethodGetterBase
    {
        public InterfaceMethodGetter(string name, rtti.Class _class
            , int refdefinedinblockid
            ):base(name,_class,0,refdefinedinblockid)
        {
			//cache = new WeakReference(null);
			_cachemethod = new rtFunction(-1, false);
		}

        public void setIndexMember(int value,Class setter)
        {
            if (!ReferenceEquals(setter, _class))
            {
                throw new InvalidOperationException();
            }

            indexofMember = value;
        }

        private InterfaceMethodGetter link;
        public void setLinkMethod(InterfaceMethodGetter link)
        {
            this.link = link;
        }

        public sealed override int functionId
        {
            get
            {
                if (link == null)
                {
                    return functionid;
                }
                else
                {
                    return link.functionId;
                }
            }
        }

		private rtFunction _cachemethod;

		public sealed override RunTimeValueBase getConstructor(RunTimeScope scope)
        {
            throw new NotImplementedException();
        }

        public override RunTimeValueBase getMethod(rtObject rtObj)
        {
            //if (cache.IsAlive)
            //{
            //    if (ReferenceEquals(((rtFunction)cache.Target).this_pointer, rtObj))
            //    {
            //        if (((rtFunction)cache.Target).this_pointer == rtObj)
            //        {
            //            return (rtFunction)cache.Target;
            //        }
            //    }
            //}
            var instance_class = rtObj.value._class;

            if (instance_class.isInterface && instance_class.isLink_System)
            {//***可能是链接到系统的接口****
#if DEBUG
                if (instance_class != _class)
                {
                    if (!instance_class.implements.ContainsKey(_class))
                    {
                        throw new ASBinCode.ASRunTimeException();
                    }
                }
#endif


                var vmember = (InterfaceMethodGetter)_class.classMembers[indexofMember].bindField;

				//rtData.rtFunction method = new rtData.rtFunction(vmember.functionId, true);
				//method.bind(rtObj.objScope);
				//method.setThis(rtObj);

				//cache.Target = method;

				//return method;
				_cachemethod.SetValue(vmember.functionId, true);
				_cachemethod.bind(rtObj.objScope);
				_cachemethod.setThis(rtObj);

				return _cachemethod;
			}
            else
            {
                var vmember = (ClassMethodGetter)instance_class.classMembers[instance_class.implements[_class][indexofMember]].bindField;

				//rtData.rtFunction method = new rtData.rtFunction(vmember.functionId, true);
				//method.bind(rtObj.objScope);
				//method.setThis(rtObj);

				//cache.Target = method;

				//return method;
				_cachemethod.SetValue(vmember.functionId, true);
				_cachemethod.bind(rtObj.objScope);
				_cachemethod.setThis(rtObj);

				return _cachemethod;
			}
        }


        
        public sealed override RunTimeValueBase getMethod(RunTimeScope scope)
        {
            //throw new NotImplementedException();

            //if (cache.IsAlive)
            //{
            //    if (((rtFunction)cache.Target).bindScope == scope)
            //    {
            //        return (rtFunction)cache.Target;
            //    }
            //}

            while (scope.scopeType != RunTimeScopeType.objectinstance)
            {
                scope = scope.parent;
            }


            var instance_class = ((rtObject)scope.this_pointer).value._class;
            var vmember = (ClassMethodGetter)instance_class.classMembers[instance_class.implements[_class][indexofMember]].bindField;

			//rtData.rtFunction method = new rtData.rtFunction(vmember.functionId, true);
			//method.bind(scope);
			//method.setThis(scope.this_pointer);

			//cache.Target = method;

			//return method;

			_cachemethod.SetValue(vmember.functionId, true);
			_cachemethod.bind(scope);
			_cachemethod.setThis(scope.this_pointer);

			return _cachemethod;
        }

        

        public sealed override RunTimeValueBase getSuperMethod(RunTimeScope scope, Class superClass)
        {
            throw new NotImplementedException();
        }

        

        public sealed override RunTimeValueBase getValue(RunTimeScope scope, RunTimeDataHolder holder)
        {
            throw new NotImplementedException();
        }




		public static InterfaceMethodGetter LoadInterfaceMethodGetter(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int, object> serizlized, int key)
		{
			
			//protected int indexofMember;
			int indexofMember = reader.ReadInt32();
			//private readonly string _name;
			string _name = reader.ReadString();
			//public readonly int refdefinedinblockid;
			int refdefinedinblockid = reader.ReadInt32();
			//protected int functionid;
			int functionid = reader.ReadInt32();

			///// <summary>
			///// 比如说，私有方法就肯定不是虚方法
			///// </summary>
			//protected bool isNotReadVirtual = false;
			bool isNotReadVirtual = reader.ReadBoolean();

			RunTimeDataType datetype = reader.ReadInt32();

			InterfaceMethodGetter ig = new InterfaceMethodGetter(_name, null, refdefinedinblockid);
			serizlized.Add(key, ig);
			//protected readonly ASBinCode.rtti.Class _class;
			ASBinCode.rtti.Class _class = serizlizer.DeserializeObject<rtti.Class>(reader, rtti.Class.LoadClass);
			ig._class = _class;
			ig.functionid = functionid;
			ig.indexofMember = indexofMember;
			ig.isNotReadVirtual = isNotReadVirtual;

			ig.valueType = datetype;
			ig.link = (InterfaceMethodGetter)serizlizer.DeserializeObject<ISWCSerializable>(reader,ISWCSerializableLoader.LoadIMember);

			return ig;
		}



		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(1);
			base.Serialize(writer, serizlizer);
			serizlizer.SerializeObject(writer, link);
		}

	}
}
