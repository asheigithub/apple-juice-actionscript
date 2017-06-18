package 
{
	import system.Byte;
	import system.Char;
	import system.DateTime;
	import system.DateTimeKind;
	import system.DayOfWeek;
	import system.Int64;
	import system.SByte;
	import system.TimeSpan;
	import system.UInt64;
	import system._Object_;
	import system._Array_;
	import system.collections.ArrayList;
	import system.collections.Hashtable;
	import system.collections.ICollection;
	import system.collections.IDictionary;
	import system.collections.IDictionaryEnumerator;
	import system.collections.IEnumerable;
	import system.collections.IList;
	import system.collections.Queue;
	import system.collections.Stack;
	import system.collections._IEnumerable_;
	import system.collections._IEnumerator_;
	[Doc]
	/**
	 * ...
	 * @author ...
	 */
	public final class FuncTest
	{
		
		var temp;
		
		public function FuncTest() 
		{
			//var k:IEnumerable;
			//var o:Object;
			//
			//o = k;
			//trace(o);
			
			var hashtable:Hashtable = new Hashtable();
			
			//trace(hasttable.isFixedSize, hasttable.isReadOnly);
			hashtable[3] = { a:"a", b:"b" };
			hashtable[DateTimeKind.Local] = undefined;
			//trace(hasttable[3]);
			
			//var keys:_IEnumerator_ = _IEnumerable_( hashtable.keys).getEnumerator();
			////trace(keys);
			//
			//keys.reset();
			//while (keys.moveNext()) 
			//{
				//trace("key:" , keys.current , "value:", hashtable[keys.current] );
			//}
			
			hashtable.add(DateTimeKind.Unspecified, TimeSpan.fromDays(44));
			hashtable.add(4, null);
			
			trace(hashtable[4]);
			
			trace("hashtable contains:",hashtable.contains(3));
			trace("hashtable contains:",hashtable.contains(DateTimeKind.Utc));
			
			//hashtable.add(DateTimeKind.Unspecified, TimeSpan.fromDays(44));
			//hashtable.remove(3);
			//
			//var values=(hashtable.values.getEnumerator());
			//values.reset();
			//while (values.moveNext()) 
			//{
				//trace("v:",  values.current);
			//}
			//trace(hashtable.count);
			
			trace(hashtable.keys.count);
			
			var he = IDictionary( hashtable).getEnumerator();
			//trace(he);
			he.reset();
			he.moveNext();
			
			//he.current.key = 6;
			var b = he.current;
			var c = he.current;
			
			b.key = 6; b.value = undefined;
			
			trace("bk:",b.key, c.key);
			trace("bv:", b.value, c.value);
			
			trace(he.current.value);
			trace( IDictionaryEnumerator(he).entry.value.b );
			
			
			var arr = _Array_.createInstance(Object,3);
			arr[0] = [7,6,5,4];
			arr[1] = TimeSpan.fromHours(6);
			arr[2] = undefined;
			
			
			
			var arr2 = _Array_.createInstance(Object,3);;
			
			trace( IList(arr).count );
			
			trace(arr2.isFixedSize,_Array_(arr2).isReadOnly);
			
			IList(arr).copyTo(arr2, 0);
			
			trace( "contains? ",IList(arr).contains(TimeSpan.fromHours(6)));
			trace( "indexOf? ",IList(arr).indexOf(TimeSpan.fromHours(6)));
			//IList(arr).removeAt(0);
			
			
			trace(  IList(arr2)[0]);
			trace(  arr[0]===arr2[0] );
			
			temp = arr;

			var enum:_IEnumerator_ = _Array_(arr).getEnumerator();
			
			enum.reset();
			while (enum.moveNext()) 
			{
				trace(enum.current);
			}
			
			
			
			var al:ArrayList = new ArrayList();
			
			
			
			al = ArrayList.createInstance(hashtable);
			
			al.addRange(arr);
			
			trace(al.count);
			
			//al.copyTo_(arr2);
			
			
			var mySourceList:ArrayList = new ArrayList();
			  mySourceList.add( "three" );
			  mySourceList.add( "napping" );
			  mySourceList.add( "cats" );
			  mySourceList.add( "in" );
			  mySourceList.add( "the" );
			  mySourceList.add( "barn" );
			  
			  
			var myTargetArray:_Array_ = _Array_.createInstance(String, 15);  //new String[15];
			  myTargetArray[0] = "The";
			  myTargetArray[1] = "quick";
			  myTargetArray[2] = "brown";
			  myTargetArray[3] = "fox";
			  myTargetArray[4] = "jumped";
			  myTargetArray[5] = "over";
			  myTargetArray[6] = "the";
			  myTargetArray[7] = "lazy";
			  myTargetArray[8] = "dog";

			mySourceList.copyTo_( myTargetArray );

			
			mySourceList.sort();
			
			
			var stack:Stack = new Stack();
			
			stack = Stack.createInstance(myTargetArray);
			
			trace(stack.contains("the"));
			
			trace(stack.peek());
			
			stack.push("push into stack");
			
			while (stack.count>0) 
			{
				trace(stack.pop());
			}
			
			
			trace(stack.toArray().rank);

			Queue.createInstance_(3);
			
			var q:Queue = Queue.createInstance(myTargetArray);
			
			q.enqueue("queue enqueue");
			
			var qe = q.getEnumerator();
			qe.reset();
			while (qe.moveNext()) 
			{
				trace("qe:", qe.current);
				
			}
			trace(q.peek());
			trace(q.dequeue());
			
			trace(q.toArray());
			trace(q.count);
			q.trimToSize();
			trace(q.count);
			q.clear();
			trace(q.count);
			
		}
		
		public static function PrintValues(  myArr:IList, mySeparator:String ):void
		{
			for (var i:int = 0; i < myArr.count	; i++) 
			{
				trace(mySeparator, myArr[i]);
			}
			
		  //for ( int i = 0; i < myArr.Length; i++ )
			 //Console.Write( "{0}{1}", mySeparator, myArr[i] );
		  //Console.WriteLine();
	   }

		
		public function makfunc()
		{
			var temp = this.temp;
			function ff(f)
			{
				
				temp[1] = f;
				trace(temp[0], temp[1],temp[2] );
			}
			
			return ff;
		}
	}

}
