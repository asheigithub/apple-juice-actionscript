using System;
using System.Collections.Generic;
using System.Text;

namespace AutoGenCodeLib
{
    public class Testobj
	{
		//public static Testobj Ins=new Testobj();

		//public static long longvalue;

		//public static Testobj KKK(Testobj testobj,double v2)
		//{
		//	 testobj.y += v2;
		//	return new Testobj() { x=testobj.x,y=testobj.y };
		//}

		//public static int operator +(Testobj t1,Testobj t2)
		//{
		//	return (int)t1.x + (int)t2.x;
		//}

		//public static int operator +(Testobj t1, string t2)
		//{
		//	return (int)t1.x + t2.Length;
		//}

		//public static Testobj operator ++(Testobj t1)
		//{
		//	 ++t1.x;
		//	return t1;
		//}
		//public static Testobj operator --(Testobj t1)
		//{
		//	--t1.x;
		//	return t1;
		//}

		//public static ulong operator ~(Testobj t1)
		//{
		//	return ~t1.x;
		//}

		//public static string operator ^(Testobj t1, Testobj t2)
		//{
		//	return t1.ToString() + " ^ " + t2.ToString();
		//}

		//public static string operator |(Testobj t1, Testobj t2)
		//{
		//	return t1.ToString() + " | " + t2.ToString();
		//}

		//public static string operator &(Testobj t1, Testobj t2)
		//{
		//	return t1.ToString() + " & " + t2.ToString();
		//}


		//public static bool operator +(Testobj t)
		//{
		//	return t.x < 10;
		//}

		//public static bool operator -(Testobj t)
		//{
		//	return t.x > 10;
		//}

		//public static string operator <<(Testobj t1,int t2)
		//{
		//	return t1.ToString() + "<<" + t2.ToString();
		//}
		//public static string operator >>(Testobj t1, int t2)
		//{
		//	return t1.ToString() + ">>" + t2.ToString();
		//}

		//public Testobj(uint x)
		//{
		//	this.x = x;
		//	this.y = 0;
		//}

		//public Testobj(double y)
		//{
		//	this.y = y;
		//	this.x = 0;
		//}

		//public  ulong x;

		//public  double y;

		//public List<string> listtest = new List<string>();

		//public List<string> geteList(List<int> iii)
		//{
		//	return listtest;
		//}

		
		//public override string ToString()
		//{
		//	return string.Format("x:{0} y:{1}", x, y);
		//}

		//public void TestType(Type type)
		//{
		//	Console.WriteLine(type.)
		//}

		public Type TestType(Type type)
		{
			Console.WriteLine("inputtype: " + type);
			
			return typeof(long);
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
}
