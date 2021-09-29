using System;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime.nativefuncs.linksystem
{
	internal sealed class Iterator : System.Collections.IEnumerator
	{
		private ASBinCode.rtData.rtObjectBase as3ienumerator;
		private ASBinCode.rtData.rtFunction reset;
		private ASBinCode.rtData.rtFunction movenext;
		private ASBinCode.rtData.rtFunction get_current;




		private Player player;

		private System.Collections.IEnumerator innerenumerator;


		internal Iterator(ASBinCode.RunTimeValueBase v, Player player)
		{
			this.player = player;
			
			if (v.rtType > ASBinCode.RunTimeDataType.unknown)
			{
				var cls = player.swc.getClassByRunTimeDataType(v.rtType);
				if (cls.isLink_System)
				{
					var lobj = ((ASBinCode.rtti.LinkSystemObject)((ASBinCode.rtData.rtObjectBase)v).value).GetLinkData();

					if (lobj is System.Collections.IEnumerator)
					{
						innerenumerator = (System.Collections.IEnumerator)lobj;
					}
					else if (lobj is System.Collections.IEnumerable)
					{
						innerenumerator = ((System.Collections.IEnumerable)lobj).GetEnumerator();
					}
					else
					{
						throw new InvalidCastException("不能将" + v.ToString() + "包装为Iterator");
					}
				}
				else if (ASRuntime.TypeConverter.testImplicitConvert(v.rtType, player.swc.IEnumeratorInterface.getRtType(), player.swc))
				{
					as3ienumerator = (ASBinCode.rtData.rtObjectBase)v;
					get_current  = player.getMethod(as3ienumerator, "@current_get");
					movenext = player.getMethod(as3ienumerator, "moveNext");
					reset = player.getMethod(as3ienumerator, "reset");
				}
				else if (ASRuntime.TypeConverter.testImplicitConvert(v.rtType, player.swc.IEnumerableInterface.getRtType(), player.swc))
				{
					var getenumerator= player.getMethod((ASBinCode.rtData.rtObjectBase)v, "getEnumerator");
					as3ienumerator = (ASBinCode.rtData.rtObjectBase)player.InvokeFunction(getenumerator, 0, null, null, null, null, null, null);
					get_current = player.getMethod(as3ienumerator, "@current_get");
					movenext = player.getMethod(as3ienumerator, "moveNext");
					reset = player.getMethod(as3ienumerator, "reset");
				}
				else
				{
					throw new InvalidCastException("不能将" + v.ToString() + "包装为Iterator");
				}
			}
			else
			{
				throw new InvalidCastException("不能将"+v.ToString() + "包装为Iterator");
			}

		}

		public override string ToString()
		{
			if (as3ienumerator != null)
			{
				return "Iterator(" + as3ienumerator.ToString() + ")";
			}
			else
			{
				return "Iterator(" + innerenumerator.ToString() + ")";
			}
			
		}

		public object Current {
			get
			{
				if (innerenumerator != null)
				{
					return innerenumerator.Current;
				}
				else
				{
					return player.InvokeFunction(get_current, 0, null, null, null, null, null, null);
				}
			}
		}

		public bool MoveNext()
		{
			if (innerenumerator != null)
			{
				return innerenumerator.MoveNext();
			}
			else
			{
				return (bool)player.InvokeFunction(movenext, 0, null, null, null, null, null, null);
			}
		}

		public void Reset()
		{
			if (innerenumerator != null)
			{
				innerenumerator.Reset();
			}
			else
			{
				player.InvokeFunction(reset, 0, null, null, null, null, null, null);
			}
		}
	}
}
