
package {
	[Doc]
	public class FuncTest{};
}
//下面的示例使用 for 在数组中添加元素： 
var my_array:Array = new Array(); 
for (var i:Number = 0; i < 10; i++) { 
	my_array[i] = (i + 5) * 10;  
} 
trace(my_array); // 50,60,70,80,90,100,110,120,130,140 



//下面的示例使用 for 重复执行相同的动作。在代码中，for 循环将从 1 到 100 的数字相加。 
var sum:Number = 0; 
for (var i:Number = 1; i <= 100; i++) { 
	sum += i; 
} 
trace(sum); // 5050

//以下示例说明，如果仅执行一条语句，则不必用大括号 ({})： 
var sum:Number = 0; 
for (var i:Number = 1; i <= 100; i++) 
	sum += i; 
trace(sum); // 5050

