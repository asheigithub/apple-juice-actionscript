using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
    /// <summary>
    /// this指针
    /// </summary>
    public class ThisPointer : IRightValue
    {

        RunTimeDataType _vt;

        //int refblockid;

        public ThisPointer(IScope scope)
        {
            scopes.FunctionScope funcscope = scope as scopes.FunctionScope;

            if (funcscope != null)
            {
                if (!funcscope.function.IsAnonymous && funcscope.parentScope is scopes.ObjectInstanceScope)
                {
                    //refblockid = ((scopes.ObjectInstanceScope)funcscope.parentScope)._class.blockid;
                    _vt =
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
                        _vt =
                            ((scopes.ObjectInstanceScope)tempscope)._class.getRtType();
                    }
                    else
                    {
                        _vt = RunTimeDataType.rt_void;
                    }
                }
                else if (funcscope.parentScope is scopes.OutPackageMemberScope)
                {
                    //refblockid = ((scopes.OutPackageMemberScope)funcscope.parentScope).mainclass.outscopeblockid;
                    _vt = RunTimeDataType.rt_void;
                }
                else
                {
                    //refblockid = -1;//动态绑定
                    _vt = RunTimeDataType.rt_void;
                }
            }
            else
            {
                //refblockid = ((scopes.OutPackageMemberScope)scope).mainclass.outscopeblockid;
                _vt = RunTimeDataType.rt_void;
            }
            

        }

        public RunTimeDataType valueType
        {
            get
            {
                return _vt;
            }
        }

        public RunTimeValueBase getValue(RunTimeScope scope)
        {
            //对方法的包含对象的引用。执行脚本时，this 关键字引用包含该脚本的对象。
            //在方法体的内部，this 关键字引用包含调用方法的类实例。

            return scope.this_pointer;

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

    }
}
