using System;
using System.Collections.Generic;
using System.Text;

namespace AutoGenCodeLib
{
	//public static class TestObjExtends
	//{
	//	public static string abc(this Testobj testobj,string abc)
	//	{
	//		return "extabc";
	//	}
	//}
	public static class SD
	{
		public static int DOTest(this Testobj obj, int a, int b)
		{
			return a + b;
		}
	}

	/// <summary>
	/// Testobj
	/// </summary>
    public class Testobj
	{
		

		public static Testobj Ins = new Testobj();

		public static long longvalue;

		public static Testobj KKK(Testobj testobj, double v2)
		{
			testobj.y += v2;
			return new Testobj() { x = testobj.x, y = testobj.y };
		}

		/// <summary>
		/// 构造Testobj实例
		/// </summary>
		/// <param name="x">x的初始值</param>
		public Testobj(uint x)
		{
			this.x = x;
			this.y = 0;
			nc = new innerClass(this);
		}

		public Testobj(double y)
		{
			this.y = y;
			this.x = 0;
			nc = new innerClass(this);
		}

		/// <summary>
		/// 字段测试 x
		/// </summary>
		public ulong x;

		public double y;

		public List<string> listtest = new List<string>();

		public List<string> geteList(List<int> iii)
		{
			return listtest;
		}

		public float a;
		public float b;
		public float c;

		public static void stest()
		{

		}

		public object LoadObject()
		{
			return DateTime.Now;
		}


		//public override string ToString()
		//{
		//	return string.Format("x:{0} y:{1}", x, y);
		//}

		//public void TestType(Type type)
		//{
		//	Console.WriteLine(type.)
		//}
		/// <summary>
		/// inner
		/// </summary>
		public class innerClass
		{
			private Testobj testobj;

			public innerClass(Testobj testobj)
			{
				this.testobj = testobj;
			}

			public innerClass inner
			{
				get
				{
					return testobj.nc;
				}
			}

			public string name { get; set; }

		}


		public string this[int a]
		{
			get { return "idx:" + a + " hashcode:"+a.GetHashCode(); }

			set
			{
				Console.WriteLine("set idx " + a + " to value:" + value);	
			}
		}


		private innerClass nc;

		public innerClass inner
		{
			get { return nc; }
		}

		/// <summary>
		/// 创建Testobj数组
		/// </summary>
		/// <param name="count">要创建的个数</param>
		/// <returns>Testobj[]数组</returns>
		public static Testobj[] make(int count)
		{
			Testobj[] r = new Testobj[count];
			for (int i = 0; i < count; i++)
			{
				r[i] = new Testobj((uint)i);
			}
			return r;
		}

		public static Testobj CreateTestObj(Type type)
		{

			return (Testobj)System.Activator.CreateInstance(type);

		}

		public void ParaTest(int a,params string[] arr)
		{
			Console.WriteLine("arr len:" + arr.Length);
		}

		public void ATTT( EnvironmentVariableTarget environment )
		{
			Console.WriteLine(environment.ToString());
		}


		public delegate int TESTHandler<T>(string v1,int v2,T v3);

		public TESTHandler<float> handler;// = (v1,v2,v3)=> { return 98765; };


		public void read(ref byte[] buffer)
		{

		}

		public Testobj()
		{
			
		}

		public int DoHandler(string v1,int v2,float v3)
		{
			//TestType(typeof(System.Int64));
			return handler(v1, v2, v3);
			//return int.Parse(v1) + v2 + (int)v3;
		}

		public static void test(out int t2)
		{
			t2 = 100;
		}

		public virtual string ArgsTest(int a1,int a2,int a3,int a4,int a5,int a6)
		{
			return "a6:" + a6;
		}

		public virtual Type TestType(Type type)
		{
			Console.WriteLine("inputtype: " + type);

			return typeof(long);
		}

		public virtual void Dialog()
		{
			Console.WriteLine("base dialog ");
		}

		public string DoArgsTest(int a6)
		{
			return ArgsTest(6, 7, 8, 9, 0, a6);
		}

		/// <summary>
		/// 事件测试
		/// </summary>
		public event EventHandler EventTest;

		public void OnEvent()
		{
			if (EventTest != null)
			{
				EventTest(this, EventArgs.Empty);
			}
		}

		public TimeSpan GetTS(Decimal ms)
		{
			return TimeSpan.FromMilliseconds((double)ms);
		}

		public TimeSpan GetTS<T>(Decimal ms)
		{
			T a;
			return TimeSpan.MinValue;
		}


		public void Roation(float x, float y, float z)
		{

		}



		//public void TestArray(int[] arr)
		//{
		//	Console.WriteLine(arr[0]);
		//}


		new public string ToString()
		{
			return "new tostring";
		}
		
	}

	public  class pttest
	{
		
		//public abstract void abc();

		//public abstract int abc2();

		protected virtual int b { get; }

		protected virtual string testabsstring(int a, long b) { return null; }

		protected int ptint()
		{
			return 5;
		}




		public void Test()
		{
			var s = testabsstring(6, 7);
		}

		public virtual int getvvv()
		{
			return 123;
		}

	}

	public class ptt2 :pttest
	{
		public virtual void kkk()
		{
			
		}
	}



}

