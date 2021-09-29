package
{
	public dynamic class Array
	{
		
		public function Array (...rest){}

		public function some (callback:Function, thisObject:*=null) : Boolean
		{
			
			return false;
		}

		public function every (callback:Function, thisObject:*=null) : Boolean
		{
			return true;
		}



		public function forEach (callback:Function, thisObject:*=null) : void
		{
			
		}

		public function filter(callback:Function, thisObject:*=null) : Array
		{
			return null;
		}

		public function map(callback:Function, thisObject:*=null) : Array
		{			
			return null;
		}

		/**
		 * 指定数组中元素数量的非负整数。在向数组中添加新元素时，此属性会自动更新。当您给数组元素赋值（例如，my_array[index] = value）时，如果 index 是数字，而且 index+1 大于 length 属性，则 length 属性会更新为 index+1。
		 * 注意：如果您为 length 属性所赋的值小于现有长度，会将数组截断。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function get length () : uint{return 0; }

		
		public function set length (newLength:uint) : void{}


		public function indexOf (searchElement:*, fromIndex:uint=0) : int
		{
			
			return -1;
		}

		public function lastIndexOf(searchElement:*, fromIndex:int = 0x7fffffff):int
		{
			
			return -1;
		}

		/**
		 * 将参数中指定的元素与数组中的元素连接，并创建新的数组。如果这些参数指定了一个数组，将连接该数组中的元素。如果不传递任何参数，则新数组是原始数组的副本（浅表克隆）。
		 * @param	args	要连接到新数组中的任意数据类型的值（如数字、元素或字符串）。
		 * @return	一个数组，其中包含此数组中的元素，后跟参数中的元素。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function concat (...rest) : Array{ return null; }

		public function insertAt (index:int, element:*) : void{}

		public function join (sep:*= null) : String{ return null; }

		public function pop () : * {return 0; }

		/**
		 * 将一个或多个元素添加到数组的结尾，并返回该数组的新长度。
		 * @param	args	要追加到数组中的一个或多个值。
		 * @return	一个表示新数组长度的整数。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function push (...rest) : uint{return 0; }
		public function removeAt (index:int) : * {return undefined; }

		/**
		 * 在当前位置倒转数组。
		 * @return	新数组。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function reverse () : Array{return null; }

		/**
		 * 删除数组中第一个元素，并返回该元素。其余数组元素将从其原始位置 i 移至 i-1。
		 * @return	数组中的第一个元素（可以是任意数据类型）。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function shift () : * {return undefined; }


		/**
		 * 返回由原始数组中某一范围的元素构成的新数组，而不修改原始数组。返回的数组包括 startIndex 元素以及从其开始到 endIndex 元素（但不包括该元素）的所有元素。
		 * 
		 *   如果不传递任何参数，则新数组是原始数组的副本（浅表克隆）。
		 * @param	startIndex	一个数字，指定片段起始点的索引。如果 startIndex 是负数，则起始点从数组的结尾开始，其中 -1 指的是最后一个元素。
		 * @param	endIndex	一个数字，指定片段终点的索引。如果省略此参数，则片段包括数组中从开头到结尾的所有元素。如果 endIndex 是负数，则终点从数组的结尾指定，其中 -1 指的是最后一个元素。
		 * @return	一个数组，由原始数组中某一范围的元素组成。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function slice (A:int = 0, B:int = 16777215) : Array{return null; }


		/**
		 * 给数组添加元素以及从数组中删除元素。此方法会修改数组但不制作副本。
		 * @param	startIndex	一个整数，它指定数组中开始进行插入或删除的位置处的元素的索引。您可以用一个负整数来指定相对于数组结尾的位置（例如，-1 是数组的最后一个元素）。
		 * @param	deleteCount	一个整数，它指定要删除的元素数量。该数量包括 startIndex 参数中指定的元素。如果没有为 deleteCount 参数指定值，则该方法将删除从 startIndex 元素到数组中最后一个元素的所有值。如果该参数的值为 0，则不删除任何元素。
		 * @param	values	用逗号分隔的一个或多个值的可选列表，此可选列表将插入 startIndex 参数中的指定位置处的数组中。如果插入的值是数组类型，则保持此数组的原样并将其作为单个元素插入。例如，如果您将长度为 3 的现有数组与另一长度为 3 的数组结合，则生成的数组将只包含 4 个元素。但是，其中的一个元素将是长度为 3 的一个数组。
		 * @return	一个数组，包含从原始数组中删除的元素。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public final function splice (startIndex:int = 0, deleteCount:uint = 4294967295, ... values) : Array{return null; }


		/**
		 * 将一个或多个元素添加到数组的开头，并返回该数组的新长度。数组中的其他元素从其原始位置 i 移到 i+1。
		 * @param	args	一个或多个要插入到数组开头的数字、元素或变量。
		 * @return	一个整数，表示该数组的新长度。
		 * @langversion	3.0
		 * @playerversion	Flash 9
		 * @playerversion	Lite 4
		 */
		public function unshift (...rest) : uint{return 0; }

		public function toString():String{ return null; }


	}
}