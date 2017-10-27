using ASBinCode.rtData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASBinCode
{
	[Serializable]
	public sealed class ClassMethodGetter : MethodGetterBase
    {
        public ClassMethodGetter(string name, rtti.Class _class, int indexofMember
            , int refdefinedinblockid
            ):base(name,_class,indexofMember,refdefinedinblockid)
        {
            //cache = new WeakReference(null);

			_cachemethod = new rtFunction(-1, false);

        }
        public sealed override  int functionId
        {
            get { return functionid; }
        }

        public sealed  override  RunTimeValueBase getValue(RunTimeScope scope, RunTimeDataHolder holder)
        {
			return (RunTimeValueBase)getMethod(scope).Clone();
			//throw new NotImplementedException();
        }


		private rtFunction _cachemethod;



        /// <summary>
        /// 如果此方法是一个构造函数。。在InstanceCreator中调用
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public sealed override  RunTimeValueBase getConstructor(RunTimeScope scope)
        {
			//rtData.rtFunction method = new rtData.rtFunction(functionid, true);
			//method.bind(scope);
			//method.setThis(scope.this_pointer);
			//return method;

			_cachemethod.SetValue(functionid, true);
			_cachemethod.bind(scope);
			_cachemethod.setThis(scope.this_pointer);

			return _cachemethod;

		}

        /// <summary>
        /// 转换函数，无所谓this和scope,并且有可能在包外使用
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public rtFunction getImplicitConvertFunction(RunTimeScope scope)
        {
			//rtData.rtFunction method = new rtData.rtFunction(functionid, true);
			//method.bind(scope);
			//method.setThis(scope.this_pointer);
			//return method;

			_cachemethod.SetValue(functionid, true);
			_cachemethod.bind(scope);
			_cachemethod.setThis(scope.this_pointer);

			return _cachemethod;
        }

        public rtFunction getMethodForClearThis(RunTimeScope scope)
        {
			//clearthis 即获取这个函数之后将删除this指针，所以这里直接把函数找出来返回即可

			//rtData.rtFunction method = new rtData.rtFunction(functionid, true);
			//method.bind(scope);
			//method.setThis(scope.this_pointer);
			//return method;


			_cachemethod.SetValue(functionid, true);
			_cachemethod.bind(scope);
			_cachemethod.setThis(scope.this_pointer);

			return _cachemethod;
		}

        //WeakReference cache;

        public override RunTimeValueBase getMethod(rtObject rtObj)
        {
            //if (cache.IsAlive)
            //{
            //    if ( ReferenceEquals( ((rtFunction)cache.Target).this_pointer , rtObj))
            //    {
            //        return (rtFunction)cache.Target;
            //    }
            //}


            if (!isNotReadVirtual)
            {

                var vmember = (ClassMethodGetter)(rtObj).value._class.classMembers[indexofMember].bindField;

				//rtData.rtFunction method = new rtData.rtFunction(vmember.functionid, true);
				//method.bind(rtObj.objScope);
				//method.setThis(rtObj);

				//cache.Target = method;

				//return method;
				_cachemethod.SetValue(vmember.functionid, true);
				_cachemethod.bind(rtObj.objScope);
				_cachemethod.setThis(rtObj);

				return _cachemethod;
			}
            else
            {
				//rtData.rtFunction method = new rtData.rtFunction(functionid, true);
				//method.bind(rtObj.objScope);
				//method.setThis(rtObj);

				//cache.Target = method;

				//return method;

				_cachemethod.SetValue(functionid, true);
				_cachemethod.bind(rtObj.objScope);
				_cachemethod.setThis(rtObj);

				return _cachemethod;
			}
        }

        public sealed override  RunTimeValueBase getMethod(RunTimeScope scope)
        {
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

            if (!isNotReadVirtual)
            {

                var vmember = (ClassMethodGetter)((rtObject)scope.this_pointer).value._class.classMembers[indexofMember].bindField;

				//rtData.rtFunction method = new rtData.rtFunction(vmember.functionid, true);
				//method.bind(scope);
				//method.setThis(scope.this_pointer);

				//cache.Target = method;

				//return method;
				_cachemethod.SetValue(vmember.functionid, true);
				_cachemethod.bind(scope);
				_cachemethod.setThis(scope.this_pointer);

				return _cachemethod;
			}
            else
            {
				//rtData.rtFunction method = new rtData.rtFunction(functionid, true);
				//method.bind(scope);
				//method.setThis(scope.this_pointer);

				//cache.Target = method;

				//return method;

				_cachemethod.SetValue(functionid, true);
				_cachemethod.bind(scope);
				_cachemethod.setThis(scope.this_pointer);

				return _cachemethod;

			}

            
        }

        public sealed override RunTimeValueBase getSuperMethod(RunTimeScope scope, ASBinCode.rtti.Class superClass)
        {
            while (scope.scopeType != RunTimeScopeType.objectinstance)
            {
                scope = scope.parent;
            }


            var m = ((rtObject)scope.this_pointer).value._class.classMembers[indexofMember];
            while (!ReferenceEquals(m.virtualLinkFromClass, superClass))
            {
                m = m.virtualLink;
            }


			//rtData.rtFunction method = new rtData.rtFunction(((ClassMethodGetter)m.bindField).functionid, true);
			//method.bind(scope);
			//method.setThis(scope.this_pointer);
			//return method;

			_cachemethod.SetValue(((ClassMethodGetter)m.bindField).functionid, true);
			_cachemethod.bind(scope);
			_cachemethod.setThis(scope.this_pointer);

			return _cachemethod;

		}

		
    }
}
