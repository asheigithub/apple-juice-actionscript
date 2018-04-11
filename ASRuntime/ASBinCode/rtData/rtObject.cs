using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode.rtData
{
	
    public abstract class rtObjectBase :RunTimeValueBase
    {

        public RunTimeScope objScope;
        public rtti.Object value;

        public rtObjectBase(rtti.Object v, RunTimeScope scope):base(v._class.classid + RunTimeDataType._OBJECT)
        {
            value = v;
            objScope = scope;
            
        }

        public sealed override double toNumber()
        {
            return double.NaN;
        }

		//public virtual rtObject getSrcObject()
		//{
		//	return this;
		//}

        public sealed override bool Equals(object obj)
        {
            rtObjectBase o = obj as rtObjectBase;
            if (o == null)
            {
                return false;
            }

            return value.Equals(o.value);
        }


        public sealed override int GetHashCode()
        {
            return value.GetHashCode();
        }

        
        public sealed override string ToString()
        {
            if (value == null)
            {
                return string.Empty;
            }
            return value.ToString();
        }



		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			throw new NotImplementedException();
		}


	}

	public sealed class rtObject : rtObjectBase
	{
		public rtObject(rtti.Object v, RunTimeScope scope):base(v,scope)
        {
			
		}

		public sealed override object Clone()
		{
			if (value._class.isLink_System && value._class.isStruct )
			{
				rtti.LinkSystemObject lobj = (rtti.LinkSystemObject)value;

				rtObject clone = new rtObject((lobj).Clone(),
					null
					);

				RunTimeScope scope =
					new RunTimeScope(null, objScope.blockId, null,
					clone, RunTimeScopeType.objectinstance);
				clone.objScope = scope;

				return clone;

			}
			else
			{
				return this;
			}
			//var result= new rtObject(value,objScope);
			//return result;
		}

	}

}
