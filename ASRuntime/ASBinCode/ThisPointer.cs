using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASBinCode
{
	
	/// <summary>
	/// this指针
	/// </summary>
	public sealed class ThisPointer : RightValueBase
    {

        //RunTimeDataType _vt;

        //int refblockid;
        private SourceToken token;
        public ThisPointer(IScope scope,SourceToken token)
        {
            this.token = token;
            scopes.FunctionScope funcscope = scope as scopes.FunctionScope;

            if (funcscope != null)
            {
                if (!funcscope.function.IsAnonymous && funcscope.parentScope is scopes.ObjectInstanceScope)
                {
                    //refblockid = ((scopes.ObjectInstanceScope)funcscope.parentScope)._class.blockid;
                    valueType =
                        ((scopes.ObjectInstanceScope)scope.parentScope)._class.getRtType();
                }
                else if (!funcscope.function.IsAnonymous && funcscope.parentScope is scopes.FunctionScope)
                {
                    var tempscope = funcscope.parentScope;
                    while (tempscope is scopes.FunctionScope)
                    {
                        
                        if (((scopes.FunctionScope)tempscope).function.IsAnonymous)
                        {
                            break;
                        }
                        
                        tempscope = tempscope.parentScope;

                    }

                    if (tempscope is scopes.ObjectInstanceScope)
                    {
                        valueType =
                            ((scopes.ObjectInstanceScope)tempscope)._class.getRtType();
                    }
                    else
                    {
                        valueType = RunTimeDataType.rt_void;
                    }
                }
                else if (funcscope.parentScope is scopes.OutPackageMemberScope)
                {
                    //refblockid = ((scopes.OutPackageMemberScope)funcscope.parentScope).mainclass.outscopeblockid;
                    valueType = RunTimeDataType.rt_void;
                }
                else
                {
                    //refblockid = -1;//动态绑定
                    valueType = RunTimeDataType.rt_void;
                }
            }
            else
            {
                //refblockid = ((scopes.OutPackageMemberScope)scope).mainclass.outscopeblockid;
                valueType = RunTimeDataType.rt_void;
            }
            

        }

        //public sealed override  RunTimeDataType valueType
        //{
        //    get
        //    {
        //        return _vt;
        //    }
        //}

        public sealed override  RunTimeValueBase getValue(RunTimeScope scope, ASRuntime.StackSlot[] slots, int stoffset)
		{
            //对方法的包含对象的引用。执行脚本时，this 关键字引用包含该脚本的对象。
            //在方法体的内部，this 关键字引用包含调用方法的类实例。
            //if (scope == null)
            //{
            //    holder.throwError(token, 0, "不能使用this（可能是[link_system]对象或[hosted]）");
            //    return rtData.rtUndefined.undefined;
            //    //return holder.scope_thispointer;
            //}
            //else
            {
                return scope.this_pointer;
            }

            //var tempscope = scope;

            //while (refblockid != tempscope.blockId && refblockid>-1)
            //{
            //    tempscope = tempscope.parent;
            //}

            //return tempscope.this_pointer;
        }

        public override string ToString()
        {
            return "this";
        }




		public static ThisPointer LoadThisPointer(BinaryReader reader, CSWCSerizlizer serizlizer, IDictionary<int, object> serizlized, int key)
		{
			RunTimeDataType type = reader.ReadInt32();
			SourceToken token = serizlizer.DeserializeObject<SourceToken>(reader, SourceToken.LoadToken);

			ThisPointer pointer = new ThisPointer(null, token); serizlized.Add(key, pointer);
			pointer.valueType = type;

			return pointer;
		}




		public override void Serialize(BinaryWriter writer, CSWCSerizlizer serizlizer)
		{
			writer.Write(8);
			base.Serialize(writer, serizlizer);
			serizlizer.SerializeObject<SourceToken>(writer, token);
		}

	}
}
